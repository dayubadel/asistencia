using H_AsistenciaPosgrado.Models.Catalogos;
using H_AsistenciaPosgrado.Models.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class AsistenciaGeneralController : Controller
    {
        CatalogoDia _objCatalogoDia = new CatalogoDia();
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
        CatalogoMatricula _objCatalogoMatricula = new CatalogoMatricula();
        CatalogoAsistencia _objCatalogoAsistencia = new CatalogoAsistencia();
        Seguridad _objSeguridad = new Seguridad();
        Correo _objCorreo = new Correo();


        [HttpPost]
        public ActionResult Notificarestudiantefaltas(string _destinatario, string _asunto, string _contenido)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "28")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else
                {
                    _mensaje = _objCorreo.EnviarCorreo(_destinatario, _asunto, _contenido);
                    if (_mensaje == "0")
                    {
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Consultarasistenciageneral(string _idConfigurarSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "28")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else if (string.IsNullOrEmpty(_idConfigurarSemestreEncriptado)|| _idConfigurarSemestreEncriptado=="0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA MÓDULO</div>";
                }
                else
                {
                    int _idConfigurarSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarSemestreEncriptado));
                    var _objConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestrePorId(_idConfigurarSemestre).Where(c => c.Eliminado == false).FirstOrDefault();
                    if (_objConfigurarSemestre == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL MÓDULO CONFIGURADO</div>";
                    }
                    else
                    {
                        var _listaFechaAsistenciaPorConfigurarSemestre = _objCatalogoFechaAsistencia.ConsultarFechaAsistenciaPorIConfigurarSemestre(_idConfigurarSemestre).Where(c => c.Eliminado == false).ToList();
                        int _totalFechasAsistenciasPorConfigurarSemestre = _listaFechaAsistenciaPorConfigurarSemestre.Count;
                        var _listaMatriculados = _objCatalogoMatricula.ConsultarMatriculaPorIdConfigurarCohorte(_objConfigurarSemestre.ConfigurarCohorte.IdConfigurarCohorte).Where(c => c.MatriculaVigente == true).ToList();
                        string _filasCuerpo = "";
                        int _contador = 1;
                        string _correoDestinatario = "";
                        
                        string _asuntoCorreo = "IMPORTANTE! POSGRADO DE LA ESPAM MFL";
                        foreach (var item in _listaMatriculados)
                        {
                            string _botonEnviarCorreo = "";
                            var _listaAsistenciaPorMatriculado = _objCatalogoAsistencia.ConsultarAsistenciaPorIdMatricula(item.IdMatricula).Where(c => c.Eliminado == false).ToList();
                            var _listaFechaAsistenciaPorMatriculado = _listaAsistenciaPorMatriculado.Join(_listaFechaAsistenciaPorConfigurarSemestre,
                                asistenciaMatriculados => asistenciaMatriculados.FechaAsistencia.IdFechaAsistencia,
                                fechaAsistencia => fechaAsistencia.IdFechaAsistencia,
                                (asistenciaMatriculados, fechaAsistencia) => new { AsistenciaMatriculados = asistenciaMatriculados, FechaAsistencia = fechaAsistencia }).ToList();
                            int _totalAsistencia = _listaFechaAsistenciaPorMatriculado.Where(c => c.AsistenciaMatriculados.AsistenciaTipo.Identificador == 1).Count();
                            int _totalFaltaJustificadas = _listaFechaAsistenciaPorMatriculado.Where(c => c.AsistenciaMatriculados.AsistenciaTipo.Identificador == 2).Count();
                            int _totalFaltaInustificadas = _listaFechaAsistenciaPorMatriculado.Where(c => c.AsistenciaMatriculados.AsistenciaTipo.Identificador == 3).Count();

                            decimal _porcentajeFaltasJustificadas = 0;
                            decimal _porcentajeFaltasInjustificadas = 0;
                            if (_totalFechasAsistenciasPorConfigurarSemestre > 0)
                            {
                                _porcentajeFaltasJustificadas = (Decimal.Divide(_totalFaltaJustificadas, _totalFechasAsistenciasPorConfigurarSemestre)) * 100;
                                _porcentajeFaltasInjustificadas = (Decimal.Divide(_totalFaltaInustificadas, _totalFechasAsistenciasPorConfigurarSemestre)) * 100;
                            }
                            if(_porcentajeFaltasJustificadas >= 15 || _porcentajeFaltasInjustificadas>=10)
                            {
                                _correoDestinatario = item.Preseleccionado.Inscripcion.Persona.EmailInstitucional;
                                if (_correoDestinatario != "")
                                {
                                    //string _mensajeEnviar = "<ul>" +
                                    //        "<li>NOTIFICACIÓN POR INASISTENCIA AL MÓDULO " + _objConfigurarSemestre.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper() + "</li>" +
                                    //        "<li>DIRECCIÓN DE POSGRADO</li>" +
                                    //        "<li>CÉDULA: " + item.Preseleccionado.Inscripcion.Persona.NumeroIdentificacion + "</li>" +
                                    //        "<li>NOMBRES: " + item.Preseleccionado.Inscripcion.Persona.Nombres.ToUpper() + "</li>" +
                                    //        "<li>SE LE NOTIFICA QUE</li>" +
                                    //    "</ul>";

                                    string _mensajeEnviar = "NOTIFICACIÓN POR INASISTENCIA AL MÓDULO " + _objConfigurarSemestre.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper();
                                    _botonEnviarCorreo = "<button id='btnCorreo"+_contador+"' onclick='notificarEstudianteFaltas("+_contador+ ", \"" + _correoDestinatario+ "\", \"" + _asuntoCorreo + "\", \"" + _mensajeEnviar + "\"); ' type='button' class='btn btn-block btn-outline-warning'>NOTIFICAR</button>";
                                }

                            }

                         

                            _filasCuerpo = _filasCuerpo +
                                "<tr id='" + _contador + "'>" +
                                      "<td>" + _contador + "</td>" +
                                      "<td>" + item.Preseleccionado.Inscripcion.Persona.Nombres.ToUpper() + "</td>" +
                                        "<td>" + _totalFechasAsistenciasPorConfigurarSemestre.ToString() + "</td>" +
                                        "<td>" + _totalAsistencia.ToString() + "</td>" +
                                        "<td>" + _totalFaltaJustificadas.ToString() + " ( "+_porcentajeFaltasJustificadas.ToString("0.##") + "% )</td>" +
                                        "<td>" + _totalFaltaInustificadas.ToString() + " ( " + _porcentajeFaltasInjustificadas.ToString("0.##") + "% )</td>" +
                                        "<td>"+_botonEnviarCorreo+"</td>" +
                                "</tr>";
                            _contador++;

                        }

                        string _cabecera = "<thead>" +
                                           "<tr>" +
                                             "<th>#</th>" +
                                             "<th>ESTUDIANTE</th>" +
                                             "<th>TOTAL</th>" +
                                             "<th>ASISTENCIAS</th>" +
                                             "<th>FALTAS JUSTIFICADAS</th>" +
                                             "<th>FALTAS INJUSTIFICADAS</th>" +
                                             "<th>NOTIFICAR AL ESTUDIANTE</th>" +
                                           "</tr>" +
                                         "</thead>";

                        string _tablaFinal = "<div class='card'>" +
                                "<div class='card-header'>" +
                                  "<h3 class='card-title text-center'>Porcentaje de asistencia de los estudiantes</h3>" +
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
                else if (Session["roll"].ToString() != "28")
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
                    string _buttonConsultar = "<button onclick='consultarAsistenciaGeneral();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";


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