using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H_AsistenciaPosgrado.Models.Catalogos;
using H_AsistenciaPosgrado.Models.Entidades;
using H_AsistenciaPosgrado.Models.Metodos;

namespace H_AsistenciaPosgrado.Controllers
{
    public class TomarAsistenciaController : Controller
    {
        CatalogoDia _objCatalogoDia = new CatalogoDia();
        CatalogoMatricula _objCatalogoMatricula = new CatalogoMatricula();
        CatalogoAsistencia _objCatalogoAsistencia = new CatalogoAsistencia();
        CatalogoAsistenciaTipo _objCatalogoAsistenciaTipo = new CatalogoAsistenciaTipo();
        CatalogoHorario _objCatalogoHorario = new CatalogoHorario();
        CatalogoMaestria _objCatalogoMaestria = new CatalogoMaestria();
        CatalogoCohorte _objCatalogoCohorte = new CatalogoCohorte();
        CatalogoConfigurarCohorte _objCatalogoConfigurarCohorte = new CatalogoConfigurarCohorte();
        CatalogoSemestre _objCatalogoSemestre = new CatalogoSemestre();
        CatalogoModulo _objCatalogoModulo = new CatalogoModulo();
        CatalogoDocente _objCatalogoDocente = new CatalogoDocente();
        CatalogoConfigurarModuloDocente _objCatalogoConfigurarModuloDocente = new CatalogoConfigurarModuloDocente();
        CatalogoConfigurarSemestre _objCatalogoConfigurarSemestre = new CatalogoConfigurarSemestre();
        CatalogoFechaAsistencia _objCatalogoFechaAsistencia = new CatalogoFechaAsistencia();
        Seguridad _objSeguridad = new Seguridad();

        [HttpPost]
        public ActionResult Tomarasistencia(string _idNuevoTipoAsistenciaPorEstudianteEncriptado, string _idAsistenciaEncriptado)
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
                else if (string.IsNullOrEmpty(_idNuevoTipoAsistenciaPorEstudianteEncriptado) || _idNuevoTipoAsistenciaPorEstudianteEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN NUEVO TIPO DE ASISTENCIA</div>";
                }
                else if (string.IsNullOrEmpty(_idAsistenciaEncriptado) || _idAsistenciaEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE LA ASISTENCIA ACTUAL</div>";
                }
                else
                {
                    int _idTipoAsistencia = Convert.ToInt32(_objSeguridad.DesEncriptar(_idNuevoTipoAsistenciaPorEstudianteEncriptado));
                    int _idAsistencia = Convert.ToInt32(_objSeguridad.DesEncriptar(_idAsistenciaEncriptado));
                    int _idAsistenciaModificado = _objCatalogoAsistencia.ModificarAsistencia(_idAsistencia, _idTipoAsistencia);
                    if (_idAsistenciaModificado == 0)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR AL TRATAR DE CAMBIAR LA ASISTENCIA</div>";
                    }
                    else
                    {
                        var _listaTipoAsistencia = _objCatalogoAsistenciaTipo.ConsultarAsistenciaTipo().Where(c => c.Eliminado == false).ToList();
                        var _objTipoSeleccionado = _listaTipoAsistencia.Where(c => c.IdAsistenciaTipo == _idTipoAsistencia).FirstOrDefault();
                        string _optionTipo = "<option value='" + _objSeguridad.Encriptar(_objTipoSeleccionado.IdAsistenciaTipo.ToString()) + "'>" + _objTipoSeleccionado.Descripcion.ToUpper() + "</option>";
                        foreach (var itemTipo in _listaTipoAsistencia.Where(c => c.IdAsistenciaTipo != _objTipoSeleccionado.IdAsistenciaTipo))
                        {
                            _optionTipo = _optionTipo + "<option value='" + _objSeguridad.Encriptar(itemTipo.IdAsistenciaTipo.ToString()) + "'>" + itemTipo.Descripcion.ToUpper() + "</option>";
                        }

                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, tabla = _optionTipo }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Consultarasistenciaestudiantespormodulo(string _idAsistenciaFechaEncriptado)
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
                else if (string.IsNullOrEmpty(_idAsistenciaFechaEncriptado) || _idAsistenciaFechaEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA FECHA ASISTENCIA</div>";
                }
                else
                {
                    int _idFechaAsistencia = Convert.ToInt32(_objSeguridad.DesEncriptar(_idAsistenciaFechaEncriptado));
                    var _objFechaAsistencia = _objCatalogoFechaAsistencia.ConsultarFechaAsistenciaPorId(_idFechaAsistencia).Where(c => c.Eliminado == false).FirstOrDefault();
                    if (_objFechaAsistencia == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL OBJETO  FECHA ASISTENCIA</div>";
                    }
                    else
                    {
                        int _idPersona = Convert.ToInt32(Session["idPersona"]);
                        var _objDocente = _objCatalogoDocente.ConsultarDocente().Where(c => c.Persona.IdPersona == _idPersona && c.Eliminado == false).FirstOrDefault();
                        if (_objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarModuloDocente.Docente.IdDocente != _objDocente.IdDocente)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>UD. NO HA SIDO ASIGNADO COMO DOCENTE DE ESTE MÓDULO</div>";
                        }
                        else
                        {
                            var _listaAsistencia = _objCatalogoAsistencia.ConsultarAsistenciaPorIdFechaAsistencia(_idFechaAsistencia).Where(c => c.Eliminado == false).OrderBy(c => c.Matricula.Preseleccionado.Inscripcion.Persona.Nombres).ToList();
                            string _cabecera = "<thead>" +
                                                     "<tr>" +
                                                       "<th>#</th>" +
                                                       "<th>NOMBRES Y APELLIDOS</th>" +
                                                       "<th>CÉDULA</th>" +
                                                       "<th>TIPO DE ASISTENCIA</th>" +
                                                     "</tr>" +
                                                   "</thead>";

                            string _filasCuerpo = "";
                            int _contador = 1;

                            var _listaTipoAsistencia = _objCatalogoAsistenciaTipo.ConsultarAsistenciaTipo().Where(c => c.Eliminado == false).ToList();
                            foreach (var item in _listaAsistencia)
                            {
                                string _idAsistenciaEncriptado = _objSeguridad.Encriptar(item.IdAsistencia.ToString());
                                var _objTipoSeleccionado = _listaTipoAsistencia.Where(c => c.IdAsistenciaTipo == item.AsistenciaTipo.IdAsistenciaTipo).FirstOrDefault();
                                string _optionTipo = "<option value='" + _objSeguridad.Encriptar(_objTipoSeleccionado.IdAsistenciaTipo.ToString()) + "'>" + _objTipoSeleccionado.Descripcion.ToUpper() + "</option>";
                                foreach (var itemTipo in _listaTipoAsistencia.Where(c => c.IdAsistenciaTipo != _objTipoSeleccionado.IdAsistenciaTipo))
                                {
                                    _optionTipo = _optionTipo + "<option value='" + _objSeguridad.Encriptar(itemTipo.IdAsistenciaTipo.ToString()) + "'>" + itemTipo.Descripcion.ToUpper() + "</option>";
                                }
                                string _selectTipoAsistencia = "<select class='form-control' id='selectNuevoTipoAsistenciaPorEstudiante" + _contador + "' onchange='TomarAsistencia(\"" + _idAsistenciaEncriptado + "\", " + _contador + ")'>" +
                                                                    _optionTipo +
                                                                "</select>";

                                _filasCuerpo = _filasCuerpo +
                                    "<tr id='" + _contador + "'>" +
                                          "<td>" + _contador + "</td>" +
                                          "<td>" + item.Matricula.Preseleccionado.Inscripcion.Persona.Nombres.ToUpper() + "</td>" +
                                          "<td>" + item.Matricula.Preseleccionado.Inscripcion.Persona.NumeroIdentificacion + "</td>" +
                                            "<td>" + _selectTipoAsistencia + "</td>" +
                                    "</tr>";
                                _contador++;
                            }
                            var _listaConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.ConfigurarCohorte.IdConfigurarCohorte == _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarCohorte.IdConfigurarCohorte && c.ConfigurarModuloDocente.Docente.IdDocente == _objDocente.IdDocente && c.Semestre.IdSemestre == _objFechaAsistencia.Horario.ConfigurarSemestre.Semestre.IdSemestre && c.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false).ToList();

                            DateTime _fechaInicioSemestre = _listaConfigurarSemestre.OrderBy(c => c.ConfigurarModuloDocente.FechaInicio).FirstOrDefault().ConfigurarModuloDocente.FechaInicio;
                            DateTime _fechaFinSemestre = _listaConfigurarSemestre.OrderByDescending(c => c.ConfigurarModuloDocente.FechaFin).FirstOrDefault().ConfigurarModuloDocente.FechaFin;


                            string _tablaFinal = "<br><div class='col-lg-12 row'>" +
                                                    "<div class='col-lg-12 text-center'><h4>MAESTRÍA DE " + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarCohorte.Cohorte.Maestria.Nombre.ToUpper() + "</h4></div>" +
                                                 "</div>" +
                                                 "<div class='col-lg-12 row'>" +
                                                    "<div class='col-lg-12 text-center'><h5>" + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarCohorte.Cohorte.Detalle.ToUpper() + " (" + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarCohorte.FechaInicio.ToShortDateString() + " - " + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarCohorte.FechaFin.ToShortDateString() + " ) " + "</h5></div>" +
                                                 "</div>" +
                                                "<div class='card'>" +
                                                  "<div class='card-header'>" +
                                                   "<table class='table'>" +
                                                        "<tr>" +
                                                            "<td colspan='1'><b>SEMESTRE: </b>" + _objFechaAsistencia.Horario.ConfigurarSemestre.Semestre.Descripcion.ToUpper() + " ( " + _fechaInicioSemestre.ToShortDateString() + " - " + _fechaFinSemestre.ToShortDateString() + " ) " + "</td>" +
                                                            "<td colspan='2'><b>MÓDULO: </b>" + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper() + " ( " + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarModuloDocente.FechaInicio.ToShortDateString() + " - " + _objFechaAsistencia.Horario.ConfigurarSemestre.ConfigurarModuloDocente.FechaFin.ToShortDateString() + " ) " + "</td>" +
                                                            "<td colspan='1'><b>DOCENTE:</b> " + _objDocente.Persona.Nombres.ToUpper() + "</td>" +
                                                        "</tr>" +
                                                   "</table>" +
                                                "</div>" +
                                   "<div class='card'>" +
                                  "<div class='card-header'>" +
                                    "<h3 class='card-title text-center'>Asistencia registrada en el sistema: "+ _objFechaAsistencia.Fecha.ToShortDateString() + " / (" + _objFechaAsistencia.Horario.HoraEntrada.ToString() + " - " + _objFechaAsistencia.Horario.HoraSalida.ToString() +" )</h3>" +
                                  "</div>" +
                                  "<div class='card-body table-responsive p-0'>" +
                                   "<table id='sd' class='table table-hover text-nowrap'>" +
                                    _cabecera +
                                      "<tbody >" +
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
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cambiarfechaasistenciaporconfigurarsemestre(string _idConfigurarSemestreEncriptado)
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
                else if (string.IsNullOrEmpty(_idConfigurarSemestreEncriptado) || _idConfigurarSemestreEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN MÓDULO</div>";
                }
                else
                {
                    int _idConfigurarSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarSemestreEncriptado));
                    var _objConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestrePorId(_idConfigurarSemestre).Where(c => c.Eliminado == false).FirstOrDefault();
                    if (_objConfigurarSemestre == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL OBJETO CONFIGURAR SEMETRE</div>";
                    }
                    else
                    {
                        var _listaFechaAsistencia = _objCatalogoFechaAsistencia.ConsultarFechaAsistenciaPorIConfigurarSemestre(_idConfigurarSemestre).Where(c => c.Eliminado == false).OrderBy(c => c.Fecha).ToList();
                        string _optionFecha = "<option value='0'>SELECCIONE FECHA Y HORARIO</option>";
                        foreach (var item in _listaFechaAsistencia)
                        {
                            _optionFecha = _optionFecha + "<option value='" + _objSeguridad.Encriptar(item.IdFechaAsistencia.ToString()) + "'>" + item.Fecha.ToShortDateString() + " / (" + item.Horario.HoraEntrada.ToString() + " - " + item.Horario.HoraSalida.ToString() + " )</option>";
                        }

                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, tabla = _optionFecha }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }
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
                    string _selectModulo = "<select onchange='cambiarFechaAsistenciaPorConfigurarSemestre();'  id='selectModulo' class='form-control'>" + _optionModulo + "</select>";
                    string _formSelectModulo = "<div class='form-group'>" +
                                                      "<label>Módulo:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectModulo +
                                                      "</div>" +
                                                    "</div>";

                    string _optionFecha = "<option value='0'>SELECCIONE FECHA Y HORARIO</option>";
                    string _selectFecha = "<select  id='selectFecha' class='form-control'>" + _optionFecha + "</select>";
                    string _formSelectFecha = "<div class='form-group'>" +
                                                      "<label>Fecha:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-calendar'></i></span>" +
                                                        "</div>" +
                                                        _selectFecha +
                                                      "</div>" +
                                                    "</div>";
                    string _buttonConsultar = "<button onclick='consultarAsistenciaEstudiantesPorModulo();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";


                    string _tabla = "<div class='row'>" +
                                    "<div class='col-md-6'>" + _formSelectMaestria + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectCohorte + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectSemestre + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectModulo + "</div>" +
                                    "<div class='col-md-12'>" + _formSelectFecha + "</div>" +
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