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
    public class ConfigurarFechaCohorteController : Controller
    {
        CatalogoMaestria _objCatalogoMaestria = new CatalogoMaestria();
        CatalogoCohorte _objCatalogoCohorte = new CatalogoCohorte();
        CatalogoConfigurarCohorte _objCatalogoConfigurarCohorte = new CatalogoConfigurarCohorte();
        Seguridad _objSeguridad = new Seguridad();
        [HttpPost]
        public ActionResult Eliminarconfigurarcohorte(string _idConfigurarCohorteEncriptado)
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
                else if (_idConfigurarCohorteEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL IDENTIFICADOR DE LA CONFIGURACIÓN DE LA COHORTE</div>";
                }
                else
                {
                    int _idConfigurarCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarCohorteEncriptado));
                    var _objConfigurarCohorte = _objCatalogoConfigurarCohorte.ConsultarConfigurarCohorte().Where(c => c.IdConfigurarCohorte == _idConfigurarCohorte && c.Eliminado == false).FirstOrDefault();
                    if (_objConfigurarCohorte == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ LA CONFIGURACIÓN DE LA COHORTE EN EL SISTEMA</div>";
                    }
                    else if (_objConfigurarCohorte.Utilizado == "1")
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE PUEDE ELIMINAR LA CONFIGURACIÓN DE LA COHORTE PORQUE YA HA SIDO UTILIZADA</div>";
                    }
                    {
                        _objCatalogoConfigurarCohorte.EliminarConfigurarCohorte(_idConfigurarCohorte);
                        _mensaje = "";
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
        public ActionResult Cargartablaconfigurarfechacohorte()
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
                    var _listaConfigurar = _objCatalogoConfigurarCohorte.ConsultarConfigurarCohorte().Where(c => c.Eliminado == false).ToList();
                    string _cabecera = "<thead>" +
                                "<tr>" +
                                  "<th>#</th>" +
                                  "<th>Maestría</th>" +
                                  "<th>Cohorte</th>" +
                                  "<th>Fecha Inicio</th>" +
                                  "<th>Fecha Fin</th>" +
                                  "<th>Acciones</th>" +
                                "</tr>" +
                              "</thead>";

                    string _filasCuerpo = "";
                    int _contador = 1;
                    foreach (var item in _listaConfigurar.OrderBy(c => c.Cohorte.Maestria.Nombre))
                    {
                        string _buttonEliminar = "";
                        if (item.Utilizado == "0")
                        {
                            _buttonEliminar = "<button id='btn" + _contador + "' onclick='eliminarConfigurarCohorte(" + _contador + ",\"" + _objSeguridad.Encriptar(item.IdConfigurarCohorte.ToString()) + "\");' type='button' class='btn btn-outline-danger'><i class='fas fa-times'></i></button>";
                        }

                        _filasCuerpo = _filasCuerpo +
                            "<tr id='" + _contador + "'>" +
                                  "<td>" + _contador + "</td>" +
                                  "<td>" + item.Cohorte.Maestria.Nombre + "</td>" +
                                  "<td>" + item.Cohorte.Detalle.ToUpper() + "</td>" +
                                  "<td>" + item.FechaInicio.ToShortDateString() + "</td>" +
                                  "<td>" + item.FechaFin.ToShortDateString() + "</td>" +
                                  "<td>" + _buttonEliminar + "</td>" +
                            "</tr>";
                        _contador++;
                    }

                    string _tablaFinal = "<br>" +
                           "<table id='sd' class='table table-striped'>" +
                            _cabecera +
                              "<tbody >" +
                                _filasCuerpo +
                              "</tbody> " +
                            "</table> ";
                    _mensaje = "";
                    _validar = true;
                    return Json(new { mensaje = _mensaje, validar = _validar, tabla = _tablaFinal }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GuardarconfigurarCohorte(string _idMaestriaEncriptado, string _idCohorteEncriptado, string _fechaInicio, string _fechaFin)
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
                else if (string.IsNullOrEmpty(_idMaestriaEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA MAESTRÍA</div>";
                }
                else if (string.IsNullOrEmpty(_idCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA COHORTE</div>";
                }
                else if (string.IsNullOrEmpty(_fechaInicio))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA FECHA DE INICIO</div>";
                }
                else if (string.IsNullOrEmpty(_fechaFin))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA FECHA FIN</div>";
                }
                else if(DateTime.Compare(Convert.ToDateTime(_fechaInicio), Convert.ToDateTime(_fechaFin))>=0)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA FECHA DE INICIO DEBE SER MENOR A LA FECHA FIN</div>";
                }
                else
                {
                    int _idMaestria = Convert.ToInt32(_objSeguridad.DesEncriptar(_idMaestriaEncriptado));
                    int _idCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idCohorteEncriptado));
                    var _objCohorte = _objCatalogoCohorte.ConsultarCohorte().Where(c => c.Eliminado == false && c.Estado == "ACTIVA" && c.Maestria.IdMestria == _idMaestria && c.IdCohorte == _idCohorte).FirstOrDefault();
                    if (_objCohorte == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA COHORTE VÁLIDA</div>";
                    }
                    else
                    {
                        int _idConfigurarCohorte = _objCatalogoConfigurarCohorte.InsertarConfigurarCohorte(
                            new EntidadConfigurarCohorte()
                            {
                                Cohorte = new EntidadCohorte()
                                {
                                    IdCohorte = _objCohorte.IdCohorte
                                },
                                FechaInicio = Convert.ToDateTime(_fechaInicio),
                                FechaFin = Convert.ToDateTime(_fechaFin),
                                Eliminado = false
                            });
                        string _inputFechaInicio = "<label id='inputFechaInicio' class='form-control'>" + _fechaInicio + "</label>";
                        string _inputFechaFin = "<label id='inputFechaFin' class='form-control' >" + _fechaFin + "</label>";
                        string _inputIdConfigurarCohorte = "<input id='idConfigurarCohorteEncriptado' value='" + _objSeguridad.Encriptar(_idConfigurarCohorte.ToString()) + "' type='hidden' class='form-control' >";
                        string _formInputFechaInicio = "<div class='form-group'>" +
                                                "<label>Fecha Inicio:</label>" +
                                                "<div class='input-group'>" +
                                                  "<div class='input-group-prepend'>" +
                                                    "<span class='input-group-text'><i class='far fa-calendar-alt'></i></span>" +
                                                  "</div>" +
                                                  _inputFechaInicio +
                                                "</div>" +
                                              "</div>";
                        string _formInputFechaFin = "<div class='form-group'>" +
                                                     "<label>Fecha Fin:</label>" +
                                                     "<div class='input-group'>" +
                                                       "<div class='input-group-prepend'>" +
                                                         "<span class='input-group-text'><i class='far fa-calendar-alt'></i></span>" +
                                                       "</div>" +
                                                       _inputFechaFin +
                                                       _inputIdConfigurarCohorte +
                                                     "</div>" +
                                                   "</div>";
                        string _contenedorFechas = "<div class='row'><div class='col-md-6'>" + _formInputFechaInicio + "</div>" +
                                        "<div class='col-md-6'>" + _formInputFechaFin + "</div></div>";
                        
                        string _inputFechas = "<div class='col-md-12'>" + _contenedorFechas + "</div>";

                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar,  fechas = _inputFechas }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Cargarformulariofechasconfigurarcohorte(string _idMaestriaEncriptado, string _idCohorteEncriptado)
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
                    int _idMaestria = Convert.ToInt32(_objSeguridad.DesEncriptar(_idMaestriaEncriptado));
                    int _idCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idCohorteEncriptado));
                    var _objCohorte = _objCatalogoCohorte.ConsultarCohorte().Where(c => c.Eliminado == false && c.Estado == "ACTIVA" && c.Maestria.IdMestria == _idMaestria && c.IdCohorte == _idCohorte).FirstOrDefault();
                    if (_objCohorte == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA COHORTE VÁLIDA</div>";
                    }
                    else
                    {
                        string _inputIdConfigurarCohorte = "";
                        string _inputFechaInicio = "<input id='inputFechaInicio' type='date' class='form-control' data-inputmask-alias='datetime' data-inputmask-inputformat='dd/mm/yyyy' data-mask='' im-insert='false'>";
                        string _inputFechaFin = "<input id='inputFechaFin' type='date' class='form-control' data-inputmask-alias='datetime' data-inputmask-inputformat='dd/mm/yyyy' data-mask='' im-insert='false'>";
                        string _buttonGuardar = "<button onclick='guardarConfigurarCohorte();' type='button' class='btn btn-block btn-outline-success'>Guardar</button>";
                        var _objConfigurarCohorte = _objCatalogoConfigurarCohorte.ConsultarConfigurarCohorte().Where(c => c.Cohorte.IdCohorte == _idCohorte && c.Eliminado == false).FirstOrDefault();

                        if (_objConfigurarCohorte != null)
                        {
                            _inputIdConfigurarCohorte = "<input id='idConfigurarCohorteEncriptado' value='" + _objSeguridad.Encriptar(_objConfigurarCohorte.IdConfigurarCohorte.ToString()) + "' type='hidden' class='form-control' >";
                            _inputFechaInicio = "<label id='inputFechaInicio' class='form-control'>" + _objConfigurarCohorte.FechaInicio.ToShortDateString() + "</label>";
                            _inputFechaFin = "<label id='inputFechaFin' class='form-control' >" + _objConfigurarCohorte.FechaFin.ToShortDateString() + "</label>";
                            _buttonGuardar = "";
                        }

                        string _formInputFechaInicio = "<div class='form-group'>" +
                                                 "<label>Fecha Inicio:</label>" +
                                                 "<div class='input-group'>" +
                                                   "<div class='input-group-prepend'>" +
                                                     "<span class='input-group-text'><i class='far fa-calendar-alt'></i></span>" +
                                                   "</div>" +
                                                   _inputFechaInicio +
                                                 "</div>" +
                                               "</div>";
                        string _formInputFechaFin = "<div class='form-group'>" +
                                                     "<label>Fecha Fin:</label>" +
                                                     "<div class='input-group'>" +
                                                       "<div class='input-group-prepend'>" +
                                                         "<span class='input-group-text'><i class='far fa-calendar-alt'></i></span>" +
                                                       "</div>" +
                                                       _inputFechaFin +
                                                       _inputIdConfigurarCohorte +
                                                     "</div>" +
                                                   "</div>";
                        string _fechas = "<div class='row'><div class='col-md-6'>" + _formInputFechaInicio + "</div>" +
                                        "<div class='col-md-6'>" + _formInputFechaFin + "</div></div>";
                       
                        string _inputFechas = "<div class='col-md-12'>" + _fechas + "</div>";
                        string _botones = "<div class='col-md-12'>" + _buttonGuardar + "</div>";

                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, inputFechas = _inputFechas, botones = _botones }, JsonRequestBehavior.AllowGet);
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
                    string _selectCohorte = "<select onchange='cargarFormularioFechasConfigurarCohorte();' id='selectCohorte' class='form-control'>" + _optionCohorte + "</select>";
                    string _formSelectCohorte = "<div class='form-group'>" +
                                                      "<label>Cohorte:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectCohorte +
                                                      "</div>" +
                                                    "</div>";
                    string _tabla = "<div class='row'>" +
                                    "<div class='col-md-6'>" + _formSelectMaestria + "</div>" +
                                    "<div class='col-md-6'>" + _formSelectCohorte + "</div>" +
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