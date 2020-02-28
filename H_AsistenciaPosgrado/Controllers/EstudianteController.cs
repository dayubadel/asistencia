using H_AsistenciaPosgrado.Models.Catalogos;
using H_AsistenciaPosgrado.Models.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class EstudianteController : Controller
    {
        CatalogoMaestria _objCatalogoMaestria = new CatalogoMaestria();
        CatalogoCohorte _objCatalogoCohorte = new CatalogoCohorte();
        CatalogoConfigurarCohorte _objCatalogoConfigurarCohorte = new CatalogoConfigurarCohorte();
        CatalogoSemestre _objCatalogoSemestre = new CatalogoSemestre();
        CatalogoModulo _objCatalogoModulo = new CatalogoModulo();
        CatalogoDocente _objCatalogoDocente = new CatalogoDocente();
        CatalogoMatricula _objCatalogoMatricula = new CatalogoMatricula();
        CatalogoConfigurarModuloDocente _objCatalogoConfigurarModuloDocente = new CatalogoConfigurarModuloDocente();
        CatalogoConfigurarSemestre _objCatalogoConfigurarSemestre = new CatalogoConfigurarSemestre();
        Seguridad _objSeguridad = new Seguridad();


        [HttpPost]
        public ActionResult Consultarestudiantespormodulos(string _idMaestriaEncriptado, string _idCohorteEncriptado, string _idSemestreEncriptado, string _idModuloEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "40")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else if (_idMaestriaEncriptado == "0" || string.IsNullOrEmpty(_idMaestriaEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA MAESTRÍA</div>";
                }
                else if (_idCohorteEncriptado == "0" || string.IsNullOrEmpty(_idCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA COHORTE</div>";
                }
                else if (_idSemestreEncriptado == "0" || string.IsNullOrEmpty(_idSemestreEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN SEMESTRE</div>";
                }
                else if (_idModuloEncriptado == "0" || string.IsNullOrEmpty(_idModuloEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN MÓDULO</div>";
                }
                else
                {
                    int _idMaestria = Convert.ToInt32(_objSeguridad.DesEncriptar(_idMaestriaEncriptado));
                    int _idCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idCohorteEncriptado));
                    int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                    int _idModulo = Convert.ToInt32(_objSeguridad.DesEncriptar(_idModuloEncriptado));

                    var _objConfigurarCohorte = _objCatalogoConfigurarCohorte.ConsultarConfigurarCohorte().Where(c => c.Cohorte.IdCohorte == _idCohorte && c.Cohorte.Maestria.IdMestria == _idMaestria && c.Eliminado == false).FirstOrDefault();
                    if (_objConfigurarCohorte != null)
                    {
                        var _listaMatriculados = _objCatalogoMatricula.ConsultarMatricula().Where(c => c.MatriculaVigente == true && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte).ToList();
                        if (_listaMatriculados.Count == 0)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO HAY ESTUDIANTES MATRICULADOS EN LA MAESTRÍA Y COHORTE SELECCIONADOS</div>";
                        }
                        else
                        {
                            var _objSemestre = _objCatalogoSemestre.ConsultarSemestre().Where(c => c.IdSemestre == _idSemestre && c.Eliminado == false).FirstOrDefault();
                            var _objModulo = _objCatalogoModulo.ConsultarModulos().Where(c => c.IdModulo == _idModulo && c.Eliminado == false).FirstOrDefault();
                            int _idPersona = Convert.ToInt32(Session["idPersona"]);
                            var _objDocente = _objCatalogoDocente.ConsultarDocente().Where(c => c.Persona.IdPersona == _idPersona && c.Eliminado == false).FirstOrDefault();

                            if (_objSemestre != null && _objModulo != null && _objDocente != null)
                            {
                                string _cabecera = "<thead>" +
                                          "<tr>" +
                                            "<th style='width: 10px' >#</th>" +
                                            "<th>Nombres</th>" +
                                            "<th>Cédula</th>" +
                                          "</tr>" +
                                        "</thead>";

                                string _filasCuerpo = "";
                                int _contador = 1;
                                foreach (var item in _listaMatriculados)
                                {
                                    _filasCuerpo = _filasCuerpo +
                                        "<tr>" +
                                              "<td>" + _contador + "</td>" +
                                              "<td>" + item.Preseleccionado.Inscripcion.Persona.Nombres.ToUpper() + "</td>" +
                                              "<td>" + item.Preseleccionado.Inscripcion.Persona.NumeroIdentificacion.ToUpper() + "</td>" +
                                        "</tr>";
                                    _contador++;
                                }
                                var _listaConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte && c.ConfigurarModuloDocente.Docente.IdDocente == _objDocente.IdDocente && c.Semestre.IdSemestre == _idSemestre && c.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false).ToList();

                                DateTime _fechaInicioSemestre = _listaConfigurarSemestre.OrderBy(c => c.ConfigurarModuloDocente.FechaInicio).FirstOrDefault().ConfigurarModuloDocente.FechaInicio;
                                DateTime _fechaFinSemestre = _listaConfigurarSemestre.OrderByDescending(c => c.ConfigurarModuloDocente.FechaFin).FirstOrDefault().ConfigurarModuloDocente.FechaFin;

                                var _fechasModulo = _listaConfigurarSemestre.Where(c => c.ConfigurarModuloDocente.Modulo.IdModulo == _idModulo).FirstOrDefault();

                                string _tablaFinal = "<br>" +

                                               "<div class='col-lg-12 row'>" +
                                                    "<div class='col-lg-12 text-center'><h4>MAESTRÍA DE " + _objConfigurarCohorte.Cohorte.Maestria.Nombre.ToUpper() + "</h4></div>" +
                                                 "</div>" +
                                                 "<div class='col-lg-12 row'>" +
                                                    "<div class='col-lg-12 text-center'><h5>" + _objConfigurarCohorte.Cohorte.Detalle.ToUpper() + " (" + _objConfigurarCohorte.FechaInicio.ToShortDateString() + " - " + _objConfigurarCohorte.FechaFin.ToShortDateString() + " ) " + "</h5></div>" +
                                                 "</div>" +
                                                "<div class='card'>" +
                                                  "<div class='card-header'>" +
                                                   "<table class='table'>" +
                                                        "<tr>" +
                                                            "<td colspan='2'><b>SEMESTRE: </b>" + _listaConfigurarSemestre.FirstOrDefault().Semestre.Descripcion.ToUpper() + " ( " + _fechaInicioSemestre.ToShortDateString() + " - " + _fechaFinSemestre.ToShortDateString() + " ) " + "</td>" +
                                                            "<td colspan='2'><b>MÓDULO: </b>" + _listaConfigurarSemestre.FirstOrDefault().Semestre.Descripcion.ToUpper() + " ( " + _fechasModulo.ConfigurarModuloDocente.FechaInicio.ToShortDateString() + " - " + _fechasModulo.ConfigurarModuloDocente.FechaFin.ToShortDateString() + " ) " + "</td>" +
                                                            "<td colspan='2'><b>DOCENTE:</b> " + _objDocente.Persona.Nombres.ToUpper() + "</td>" +
                                                        "</tr>" +
                                                   "</table>" +
                                                "</div>" +
                                                  "<div class='card-body p-0'>" +
                                                  
                                                   "<table class='table table-striped'>" +
                                                    _cabecera +
                                                      "<tbody>" +
                                                        _filasCuerpo +
                                                      "</tbody> " +
                                                    "</table> " +
                                                  "</div> " +
                                                "</div>";

                                _mensaje = "";
                                _validar = true;
                                return Json(new { mensaje = _mensaje, validar = _validar, tabla = _tablaFinal }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Cambiarmodulosporsemestre(string _idMaestriaEncriptado, string _idCohorteEncriptado, string _idSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "40")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else if (string.IsNullOrEmpty(_idMaestriaEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA MAESTRÍA</div>";
                }
                else if (string.IsNullOrEmpty(_idCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA COHORTE</div>";
                }
                else if (string.IsNullOrEmpty(_idSemestreEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN SEMESTRE</div>";
                }
                else
                {
                    int _idCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idCohorteEncriptado));
                    var _objConfigurarCohorte = _objCatalogoConfigurarCohorte.ConsultarConfigurarCohorte().Where(c => c.Eliminado == false && c.Cohorte.IdCohorte == _idCohorte).FirstOrDefault();
                    if (_objConfigurarCohorte == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>ES NECESARIO QUE REALICE PREVIAMENTE LA CONFIGURACIÓN PARA ESTA MAESTRÍA Y COHORTE</div>";
                    }
                    else
                    {
                        int _idPersona = Convert.ToInt32(Session["idPersona"]);
                        int _idDocente = _objCatalogoDocente.ConsultarDocente().Where(c => c.Persona.IdPersona == _idPersona && c.Eliminado == false).FirstOrDefault().IdDocente;
                        int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                        var _listaModulos = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.Eliminado == false && c.Semestre.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte && c.Semestre.IdSemestre == _idSemestre && c.ConfigurarModuloDocente.Docente.Persona.IdPersona==_idPersona).ToList();
                        string _optionModulo = "<option value='0'>SELECCIONE UN MÓDULO</option>";
                        foreach (var item in _listaModulos.OrderBy(c => c.ConfigurarModuloDocente.Modulo.Descripcion))
                        {
                            _optionModulo = _optionModulo + "<option value='" + _objSeguridad.Encriptar(item.IdConfigurarSemestre.ToString()) + "'>" + item.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper() + " ( " + item.ConfigurarModuloDocente.Docente.Persona.Nombres.ToUpper() + " ) " + "</option>";
                        }
                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, tabla = _optionModulo }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cargarformulario()
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "40")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else
                {
                    var _listaMaestria = _objCatalogoMaestria.ConsultarMaestria().Where(c => c.Eliminado == false && c.Estado == "ACTIVA").ToList();
                    string _optionMaestria = "<option value='0'>SELECCIONE UNA MAESTRÍA</option>";
                    foreach (var item in _listaMaestria)
                    {
                        _optionMaestria = _optionMaestria + "<option value='" + _objSeguridad.Encriptar(item.IdMestria.ToString()) + "'>" + item.Nombre.ToUpper() + "</option>";
                    }
                    string _selectMaestria = "<select id='selectMaestria' onchange='cambiarCohortePorMaestria();' class='form-control'>" + _optionMaestria + "</select>";
                    string _formSelectMaestria = "<div class='form-group'>" +
                                                      "<label>Maestría:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectMaestria +
                                                      "</div>" +
                                                    "</div>";
                    string _optionCohorte = "<option value='0'>SELECCIONE UNA COHORTE</option>";
                    string _selectCohorte = "<select onchange='cambiarSemestrePorCohorte();' id='selectCohorte' class='form-control'>" + _optionCohorte + "</select>";
                    string _formSelectCohorte = "<div class='form-group'>" +
                                                      "<label>Cohorte:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectCohorte +
                                                      "</div>" +
                                                    "</div>";
                    string _optionSemestre = "<option value='0'>SELECCIONE UN SEMESTRE</option>";
                    string _selectSemestre = "<select onchange='cambiarModulosPorSemestre();' id='selectSemestre' class='form-control'>" + _optionSemestre + "</select>";
                    string _formSelectSemestre = "<div class='form-group'>" +
                                                      "<label>Semestre:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectSemestre +
                                                      "</div>" +
                                                    "</div>";
                    string _optionModulo = "<option value='0'>SELECCIONE UN MÓDULO</option>";
                    string _selectModulo = "<select  id='selectModulo' class='form-control'>" + _optionModulo + "</select>";
                    string _formSelectModulo = "<div class='form-group'>" +
                                                      "<label>Módulo:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectModulo +
                                                      "</div>" +
                                                    "</div>";
                    string _buttonConsultar = "<button onclick='consultarEstudiantesPorModulos();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";


                    string _tabla = "<div class='row'>" +
                                    "<div class='col-md-6'>" + _formSelectMaestria + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectCohorte + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectSemestre + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectModulo + "</div>" +
                                    "<div class='col-md-12'>" + _buttonConsultar + "</div>" +
                                    "</div>";
                    _mensaje = "";
                    _validar = true;
                    return Json(new { mensaje = _mensaje, validar = _validar, tabla = _tabla }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }
    }
}