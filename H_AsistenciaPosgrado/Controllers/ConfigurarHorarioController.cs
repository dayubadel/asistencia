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
    public class ConfigurarHorarioController : Controller
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
        Seguridad _objSeguridad = new Seguridad();
        [HttpPost]
        public ActionResult Consultarhorarioporconfigurarsemestre(string _idConfigurarSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_idConfigurarSemestreEncriptado))
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

                        var _listaHorario = _objCatalogoHorario.ConsultarHorario().Where(c => c.ConfigurarSemestre.IdConfigurarSemestre == _idConfigurarSemestre && c.Eliminado == false).ToList();
                        var _listaDias = _objCatalogoDia.ConsultarDia().Where(c => c.Eliminado == false).ToList();
                        string _contenidoTimeline = "";
                        foreach (var itemDia in _listaDias)
                        {
                            _contenidoTimeline = _contenidoTimeline + 
                                                "<div class='time-label'>" +
                                                    "<span class='bg-red'>" +
                                                       itemDia.Descripcion.ToUpper() +
                                                    "</span>" +
                                                "</div>";
                            var _listaHorarioPorDia = _listaHorario.Where(c => c.Dia.IdDia == itemDia.IdDia).ToList();
                            foreach (var itemHorario in _listaHorarioPorDia)
                            {
                                _contenidoTimeline = _contenidoTimeline +
                                                "<div>" +
                                                 "<i class='fa fa-envelope bg-blue'></i>" +
                                                  "<div class='timeline-item'>" +
                                                    "<span class='time'><i class='fa fa-clock-o'></i> 12:05</span>" +
                                                    "<h3 class='timeline-header'><a href='#'>Support Team</a> sent you an email</h3>" +
                                                    "<div class='timeline-body'>" +
                                                    "</div>" +
                                                    "<div class='timeline-footer'>" +
                                                      "<a class='btn btn-primary btn-xs'>Read more</a>" +
                                                      "<a class='btn btn-danger btn-xs'>Delete</a>" +
                                                    "</div>"+
                                                  "</div>"+
                                                "</div>" +
                                                "</div>" ;
                            }          
                        }
                        string _timeLine = "<div class='timeline'>" + _contenidoTimeline + "</div>";
                        string _tablaFinal = "<div class='row'><div class='col-md-12'>"+ _timeLine + "</div></div>";

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
        public ActionResult Cambiarmodulosporsemestre(string _idMaestriaEncriptado, string _idCohorteEncriptado, string _idSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_idMaestriaEncriptado))
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
                        var _listaModulos = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.Eliminado == false && c.Semestre.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte && c.Semestre.IdSemestre==_idSemestre).ToList();
                        string _optionModulo= "<option value='0'>SELECCIONE UN MÓDULO</option>";
                        foreach (var item in _listaModulos.OrderBy(c=>c.ConfigurarModuloDocente.Modulo.Descripcion))
                        {
                            _optionModulo = _optionModulo + "<option value='" + _objSeguridad.Encriptar(item.IdConfigurarSemestre.ToString()) + "'>" + item.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper()+" ( "+item.ConfigurarModuloDocente.Docente.Persona.Nombres.ToUpper()+" ) " + "</option>";
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
                if (string.IsNullOrEmpty(_idMaestriaEncriptado))
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
                        var _listaSemestres = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.Eliminado == false && c.Semestre.Eliminado==false && c.ConfigurarModuloDocente.Eliminado==false && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarCohorte.IdConfigurarCohorte).ToList()
                            .Select(x => new EntidadSemestre() { Identificador = x.Semestre.Identificador, IdSemestre = x.Semestre.IdSemestre, Descripcion = x.Semestre.Descripcion, Eliminado = x.Semestre.Eliminado })
                            .GroupBy(y => y.IdSemestre).Select(x => new EntidadSemestre() { IdSemestre = x.Key, Descripcion = x.Select(c => c.Descripcion).FirstOrDefault(), Identificador = x.Select(c => c.Identificador).FirstOrDefault(), Eliminado = x.Select(c => c.Eliminado).FirstOrDefault() }).OrderBy(y=>y.Identificador).ToList();
                        string _optionSemestre = "<option value='0'>SELECCIONE UN SEMESTRE</option>";
                        foreach (var item in _listaSemestres)
                        {
                            _optionSemestre = _optionSemestre + "<option value='" + _objSeguridad.Encriptar(item.IdSemestre.ToString()) + "'>" +item.Identificador +". " + item.Descripcion.ToUpper() + "</option>";
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
                if (string.IsNullOrEmpty(_idMaestriaEncriptado))
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
        [HttpPost]
        public ActionResult Cargarformulario()
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
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
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }
    }
}