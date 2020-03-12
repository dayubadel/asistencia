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
    public class HorarioController : Controller
    {
        CatalogoDia _objCatalogoDia = new CatalogoDia();
        CatalogoHorario _objCatalogoHorario = new CatalogoHorario();
        CatalogoMaestria _objCatalogoMaestria = new CatalogoMaestria();
        CatalogoCohorte _objCatalogoCohorte = new CatalogoCohorte();
        CatalogoConfigurarCohorte _objCatalogoConfigurarCohorte = new CatalogoConfigurarCohorte();
        CatalogoSemestre _objCatalogoSemestre = new CatalogoSemestre();
        CatalogoModulo _objCatalogoModulo = new CatalogoModulo();
        CatalogoDocente _objCatalogoDocente = new CatalogoDocente();
        CatalogoMatricula _objCatalogoMatricula = new CatalogoMatricula();
        CatalogoConfigurarModuloDocente _objCatalogoConfigurarModuloDocente = new CatalogoConfigurarModuloDocente();
        CatalogoConfigurarSemestre _objCatalogoConfigurarSemestre = new CatalogoConfigurarSemestre();
        CatalogoFechaAsistencia _objCatalogoFechaAsistencia = new CatalogoFechaAsistencia();
        CatalogoAsistencia _objCatalogoAsistencia = new CatalogoAsistencia();
        Seguridad _objSeguridad = new Seguridad();
        [HttpPost]
        public ActionResult Consultarhorarioporconfigurarsemestre(string _idConfigurarSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "39")
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
                        int _idPersona = Convert.ToInt32(Session["idPersona"]);
                        var _objMatricula = _objCatalogoMatricula.ConsultarMatriculaPorIdPersona(_idPersona).Where(c => c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarSemestre.ConfigurarCohorte.IdConfigurarCohorte).FirstOrDefault();
                        if (_objMatricula == null)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>UD. NO HA SIDO MATRICULADO(A) EN ESTA COHORTE</div>";
                        }
                        else
                        {
                            var _listaHorario = _objCatalogoHorario.ConsultarHorario().Where(c => c.ConfigurarSemestre.IdConfigurarSemestre == _idConfigurarSemestre && c.Eliminado == false).ToList();
                            string _contenidoTimeline = "";
                            if (_listaHorario.Count > 0)
                            {
                                var _listaDias = _objCatalogoDia.ConsultarDia().Where(c => c.Eliminado == false).ToList();
                                foreach (var itemDia in _listaDias)
                                {
                                    string _color = "bg-blue";
                                    if (itemDia.Identificador == 1)
                                    {
                                        _color = "bg-maroon";
                                    }
                                    if (itemDia.Identificador == 2)
                                    {
                                        _color = "bg-yellow";
                                    }
                                    else if (itemDia.Identificador == 4)
                                    {
                                        _color = "bg-purple";
                                    }
                                    else if (itemDia.Identificador == 6)
                                    {
                                        _color = "bg-green";
                                    }
                                    else if (itemDia.Identificador == 7)
                                    {
                                        _color = "bg-gray";
                                    }
                                    var _listaHorarioPorDia = _listaHorario.Where(c => c.Dia.IdDia == itemDia.IdDia).ToList();
                                    if (_listaHorarioPorDia.Count > 0)
                                    {
                                        _contenidoTimeline = _contenidoTimeline +
                                                            "<div class='time-label'>" +
                                                                "<span class='" + _color + "'>" +
                                                                   itemDia.Descripcion.ToUpper() +
                                                                "</span>" +
                                                            "</div>";
                                    }

                                    int _contador = 0;
                                    foreach (var itemHorario in _listaHorarioPorDia)
                                    {
                                        _contenidoTimeline = _contenidoTimeline +
                                                        "<div id='div" + _contador + "dia" + itemDia.Identificador + "' >" +
                                                         "<i class='fa fa-clock " + _color + "'></i>" +
                                                              "<div class='timeline-item'>" +
                                                                "<span class='time'><i class='fa fa-clock-o'></i></span>" +
                                                                "<h3 class='timeline-header'>" + itemHorario.HoraEntrada.ToString() + " - " + itemHorario.HoraSalida.ToString() + "</h3>" +
                                                                "</div>" +
                                                        "</div>";
                                        _contador++;
                                    }
                                }
                            }
                            else
                            {
                                _contenidoTimeline = _contenidoTimeline + "<div class='time-label'>" +
                                                            "<span class='bg-gray'>SIN HORARIO</span>" +
                                                        "</div>" +
                                                         "<div>" +
                                                         "<i class='fa fa-clock bg-red'></i>" +
                                                              "<div class='timeline-item'>" +
                                                                "<span class='time'><i class='fa fa-clock-o'></i></span>" +
                                                                "<h3 class='timeline-header'>NO SE HA CONFIGURADO EL HORARIO DE CLASES</h3>" +
                                                                "</div>" +
                                                        "</div>";
                            }
                            var _listaConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarSemestre.ConfigurarCohorte.IdConfigurarCohorte && c.Semestre.IdSemestre == _objConfigurarSemestre.Semestre.IdSemestre).ToList();

                            DateTime _fechaInicioSemestre = _listaConfigurarSemestre.OrderBy(c => c.ConfigurarModuloDocente.FechaInicio).FirstOrDefault().ConfigurarModuloDocente.FechaInicio;
                            DateTime _fechaFinSemestre = _listaConfigurarSemestre.OrderByDescending(c => c.ConfigurarModuloDocente.FechaFin).FirstOrDefault().ConfigurarModuloDocente.FechaFin;


                            var _listaFechaAsistenciaPorConfigurarSemestre = _objCatalogoFechaAsistencia.ConsultarFechaAsistenciaPorIConfigurarSemestre(_idConfigurarSemestre).Where(c => c.Eliminado == false).ToList();
                            int _totalFechasAsistenciasPorConfigurarSemestre = _listaFechaAsistenciaPorConfigurarSemestre.Count;

                            var _listaAsistenciaPorMatriculado = _objCatalogoAsistencia.ConsultarAsistenciaPorIdMatricula(_objMatricula.IdMatricula).Where(c => c.Eliminado == false).ToList();
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


                            _contenidoTimeline = _contenidoTimeline + "<div><i class='fas fa-feather-alt bg-gray'></i></div>";

                            string _encabezado = "<div class='col-lg-12 row'>" +
                                                        "<div class='col-lg-12 text-center'><h4>MAESTRÍA DE " + _objConfigurarSemestre.ConfigurarCohorte.Cohorte.Maestria.Nombre.ToUpper() + "</h4></div>" +
                                                     "</div>" +
                                                     "<div class='col-lg-12 row'>" +
                                                        "<div class='col-lg-12 text-center'><h5>" + _objConfigurarSemestre.ConfigurarCohorte.Cohorte.Detalle.ToUpper() + " (" + _objConfigurarSemestre.ConfigurarCohorte.FechaInicio.ToShortDateString() + " - " + _objConfigurarSemestre.ConfigurarCohorte.FechaFin.ToShortDateString() + " ) " + "</h5></div>" +
                                                     "</div>" +
                                                       "<table class='table'>" +
                                                      "<tbdoy>"+
                                                            "<tr>" +
                                                                "<td colspan='2'><b>SEMESTRE: </b>" + _listaConfigurarSemestre.FirstOrDefault().Semestre.Descripcion.ToUpper() + " ( " + _fechaInicioSemestre.ToShortDateString() + " - " + _fechaFinSemestre.ToShortDateString() + " ) " + "</td>" +
                                                                "<td colspan='2'><b>MÓDULO: </b>" + _objConfigurarSemestre.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper() + " ( " + _objConfigurarSemestre.ConfigurarModuloDocente.FechaInicio.ToShortDateString() + " - " + _objConfigurarSemestre.ConfigurarModuloDocente.FechaFin.ToShortDateString() + " ) " + "</td>" +
                                                                "<td colspan='2'><b>DOCENTE:</b> " + _objConfigurarSemestre.ConfigurarModuloDocente.Docente.Persona.Nombres.ToUpper() + "</td>" +
                                                            "</tr>" +
                                                             "<tr>" +
                                                                "<td colspan='1'><b>HORAS TOTALES: </b>" + _totalFechasAsistenciasPorConfigurarSemestre.ToString() + "</td>" +
                                                                "<td colspan='1'><b>H ASISTENCIAS: </b>" + _totalAsistencia.ToString() + "</td>" +
                                                                "<td colspan='2'><b>H FALTAS JUSTIFICADAS:</b> " + _totalFaltaJustificadas.ToString() + " ( " + _porcentajeFaltasJustificadas + "% ) "+ "</td>" +
                                                                "<td colspan='2'><b>H FALTAS INJUSTIFICADAS:</b> " + _totalFaltaInustificadas.ToString() + " ( " + _porcentajeFaltasInjustificadas +"% ) " + "</td>" +
                                                            "</tr>" +
                                                        "</tbdoy>" +
                                                       "</table>" +
                                                    "</div>";

                            string _tablaFinal = "<br>" + _encabezado + "<br><br><div class='row'><div class='col-md-2'></div><div class='col-md-8'><div class='timeline'>" + _contenidoTimeline + "</div></div><div class='col-md-2'></div></div>";

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
                else if (Session["roll"].ToString() != "39")
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
                        int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                        var _listaModulos = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.Eliminado == false && c.Semestre.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte && c.Semestre.IdSemestre == _idSemestre).ToList();
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
        public ActionResult Cambiarsemestreporcohorte(string _idMaestriaEncriptado, string _idCohorteEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "39")
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
                        var _listaSemestres = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.Eliminado == false && c.Semestre.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte).ToList()
                            .Select(x => new EntidadSemestre() { Identificador = x.Semestre.Identificador, IdSemestre = x.Semestre.IdSemestre, Descripcion = x.Semestre.Descripcion, Eliminado = x.Semestre.Eliminado })
                            .GroupBy(y => y.IdSemestre).Select(x => new EntidadSemestre() { IdSemestre = x.Key, Descripcion = x.Select(c => c.Descripcion).FirstOrDefault(), Identificador = x.Select(c => c.Identificador).FirstOrDefault(), Eliminado = x.Select(c => c.Eliminado).FirstOrDefault() }).OrderBy(y => y.Identificador).ToList();
                        string _optionSemestre = "<option value='0'>SELECCIONE UN SEMESTRE</option>";
                        foreach (var item in _listaSemestres)
                        {
                            _optionSemestre = _optionSemestre + "<option value='" + _objSeguridad.Encriptar(item.IdSemestre.ToString()) + "'>" + item.Identificador + ". " + item.Descripcion.ToUpper() + "</option>";
                        }
                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, tabla = _optionSemestre }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Cambiarcohortepormaestria(string _idMaestriaEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (Session["roll"] == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA SESIÓN HA EXPIRADO, POR FAVOR RECARGUE LA PÁGINA</div>";
                }
                else if (Session["roll"].ToString() != "39")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else if (string.IsNullOrEmpty(_idMaestriaEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA MAESTRÍA</div>";
                }
                else
                {
                    int _idMaestria = Convert.ToInt32(_objSeguridad.DesEncriptar(_idMaestriaEncriptado));
                    var _listaCohorte = _objCatalogoCohorte.ConsultarCohorte().Where(c => c.Eliminado == false && c.Estado == "ACTIVA" && c.Maestria.IdMestria == _idMaestria).ToList();
                    string _optionCohorte = "<option value='0'>SELECCIONE UNA COHORTE</option>";
                    foreach (var item in _listaCohorte)
                    {
                        _optionCohorte = _optionCohorte + "<option value='" + _objSeguridad.Encriptar(item.IdCohorte.ToString()) + "'>" + item.Detalle.ToUpper() + "</option>";
                    }
                    _mensaje = "";
                    _validar = true;
                    return Json(new { mensaje = _mensaje, validar = _validar, tabla = _optionCohorte }, JsonRequestBehavior.AllowGet);
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
                else if (Session["roll"].ToString() != "39")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else
                {
                    int _idPersona = Convert.ToInt32(Session["idPersona"]);
                    var _listaMatriculas = _objCatalogoMatricula.ConsultarMatriculaPorIdPersona(_idPersona).Where(c => c.MatriculaVigente == true && c.ConfigurarCohorte.Eliminado==false && c.ConfigurarCohorte.Cohorte.Maestria.Estado == "ACTIVA").ToList();

                    var _listaMaestria = _listaMatriculas.Select(c => new EntidadMaestria() { IdMestria = c.ConfigurarCohorte.Cohorte.Maestria.IdMestria, Nombre = c.ConfigurarCohorte.Cohorte.Maestria.Nombre })
                                                        .GroupBy(c => c.IdMestria).Select(c => new EntidadMaestria() { IdMestria = c.Key, Nombre = c.Select(x => x.Nombre.ToString()).FirstOrDefault() }).ToList();
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
                    string _buttonConsultar = "<button onclick='consultarHorarioPorConfigurarSemestre();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";


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