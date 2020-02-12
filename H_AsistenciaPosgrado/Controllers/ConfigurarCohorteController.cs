using H_AsistenciaPosgrado.Models.Catalogos;
using H_AsistenciaPosgrado.Models.Entidades;
using H_AsistenciaPosgrado.Models.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class ConfigurarCohorteController : Controller
    {
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
        public ActionResult Eliminarconfigurarcohorte(string _idConfigurarCohorteEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (_idConfigurarCohorteEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarCohorteEncriptado))
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
        public ActionResult Eliminarconfigurarsemestreconfigurarmodulodocente(string _idConfigurarSemestreEncriptado,
                string _idConfigurarModuloDocenteEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (_idConfigurarSemestreEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarSemestreEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL IDENTIFICADOR DE LA CONFIGURACIÓN DEL SEMESTRE</div>";
                }
                else if (_idConfigurarModuloDocenteEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarModuloDocenteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL IDENTIFICADOR DE LA CONFIGURACIÓN DEL MÓDULO Y DEL DOCENTE</div>";
                }
                else
                {
                    int _idConfigurarSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarSemestreEncriptado));
                    int _idConfigurarModuloDocente = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarModuloDocenteEncriptado));
                    var _objConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.IdConfigurarSemestre == _idConfigurarSemestre).FirstOrDefault();
                    var _objConfigurarModuloDocente = _objConfigurarSemestre.ConfigurarModuloDocente;
                    if (_objConfigurarSemestre == null || _objConfigurarModuloDocente == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>EXISTE UN PROBLEMA CON LA CONFIGURACIÓN. POR FAVOR RECARGUE LA PÁGINA</div>";
                    }
                    else
                    {
                        if (_objConfigurarModuloDocente.Utilizado == "1" || _objConfigurarSemestre.Utilizado=="1")
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA CONFIGURACIÓN YA HA SIDO UTILIZADA, POR LO TANTO NO PUEDE SER ELIMINADA</div>";
                        }
                        else
                        {
                            _objCatalogoConfigurarSemestre.EliminarConfigurarSemestre(_idConfigurarSemestre);
                            _objCatalogoConfigurarModuloDocente.EliminarConfigurarModuloDocente(_idConfigurarModuloDocente);
                            _mensaje = "";
                            _validar = true;
                            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new
            {
                mensaje = _mensaje,
                validar = _validar
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Cargartablaconfigurarsemestre(string _idConfigurarCohorteEncriptado,
                   string _idSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (_idConfigurarCohorteEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL IDENTIFICADOR DE LA CONFIGURACIÓN</div>";
                }
                else if (string.IsNullOrEmpty(_idSemestreEncriptado) || _idSemestreEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN SEMESTRE</div>";
                }
                else
                {
                    int _idConfigurarCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarCohorteEncriptado));
                    int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                    var _listaConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestre().Where(c => c.ConfigurarCohorte.IdConfigurarCohorte == _idConfigurarCohorte && c.Semestre.IdSemestre == _idSemestre && c.Eliminado == false && c.ConfigurarModuloDocente.Eliminado == false).ToList();

                    string _cabecera = "<thead>" +
                                "<tr>" +
                                  "<th style='width: 10px' >#</th>" +
                                  "<th>Semestre</th>" +
                                  "<th>Modulo</th>" +
                                  "<th>Docente</th>" +
                                  "<th>Fecha Inicio</th>" +
                                  "<th>Fecha Fin</th>" +
                                  "<th>Acciones</th>" +
                                "</tr>" +
                              "</thead>";

                    string _filasCuerpo = "";
                    int _contador = 1;
                    foreach (var item in _listaConfigurarSemestre)
                    {
                        string _buttonEliminar = "";
                        if(item.ConfigurarModuloDocente.Utilizado=="0" && item.Utilizado=="0")
                        {
                            string _idConfigurarSemestreEncriptado = _objSeguridad.Encriptar(item.IdConfigurarSemestre.ToString());
                            string _idConfigurarModuloDocenteEncriptado = _objSeguridad.Encriptar(item.ConfigurarModuloDocente.IdConfigurarModuloDocente.ToString());
                            _buttonEliminar = "<button id='btn" + _contador + "' onclick='eliminarConfigurarSemestreConfigurarModuloDocente(\"" + _idConfigurarSemestreEncriptado + "\", \""+_idConfigurarModuloDocenteEncriptado+"\");' type='button' class='btn btn-outline-danger'><i class='fas fa-times'></i></button>";
                        }
                        _filasCuerpo = _filasCuerpo +
                            "<tr>" +
                                  "<td>"+_contador+"</td>" +
                                  "<td>"+item.Semestre.Descripcion.ToUpper()+"</td>" +
                                  "<td>"+item.ConfigurarModuloDocente.Modulo.Descripcion.ToUpper()+"</td>" +
                                  "<td>"+item.ConfigurarModuloDocente.Docente.Persona.Nombres+"</td>" +
                                  "<td>"+item.ConfigurarModuloDocente.FechaInicio.ToShortDateString()+"</td>" +
                                  "<td>"+item.ConfigurarModuloDocente.FechaFin.ToShortDateString()+"</td>" +
                                  "<td>"+_buttonEliminar+"</td>" +
                            "</tr>";
                        _contador++;
                    }

                    string _tablaFinal = "<br><div class='card'>"+
                          "<div class='card-header'>" +
                            "<h3 class='card-title'>Configuración registrada en el sistema</h3>" +
                          "</div>" +
                          "<div class='card-body p-0'>" +
                           "<table class='table table-striped'>" +
                            _cabecera+
                              "<tbody>" +
                                _filasCuerpo+                  
                              "</tbody> " +
                            "</table> " +
                          "</div> " +
                        "</div>";
                    
                    _mensaje = "";
                    _validar = true;
                    return Json(new { mensaje = _mensaje, validar = _validar, tabla =_tablaFinal }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Asignarmodulodocenteconfigurarsemestre(string _idConfigurarCohorteEncriptado,
            string _idSemestreEncriptado, string _idModuloEncriptado, string _idDocenteEncriptado,
            string _fechaInicioModulo, string _fechaFinModulo)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (_idConfigurarCohorteEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL IDENTIFICADOR DE LA CONFIGURACIÓN</div>";
                }
                else if (string.IsNullOrEmpty(_idSemestreEncriptado) || _idSemestreEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN SEMESTRE</div>";
                }
                else if (string.IsNullOrEmpty(_idModuloEncriptado) || _idModuloEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN MÓDULO</div>";
                }
                else if (string.IsNullOrEmpty(_idDocenteEncriptado) || _idDocenteEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN DOCENTE</div>";
                }
                else if (_fechaInicioModulo == "01/01/0001" || _fechaInicioModulo == "")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE UNA FECHA DE INICIO DEL MÓDULO</div>";
                }
                else if (_fechaFinModulo == "01/01/0001" || _fechaFinModulo == "")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE UNA FECHA DE FINALIZACIÓN DEL MÓDULO</div>";
                }
                else if (DateTime.Compare(Convert.ToDateTime(_fechaInicioModulo), Convert.ToDateTime(_fechaFinModulo)) >= 0)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA FECHA DE INICIO DEL MÓDULO DEBE SER MENOR A LA FECHA DE FINALIZACIÓN DEL MISMO</div>";
                }
                else
                {
                    int _idConfigurarCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarCohorteEncriptado));
                    var _objConfigurarCohorte = _objCatalogoConfigurarCohorte.ConsultarConfigurarCohorte().Where(c => c.IdConfigurarCohorte == _idConfigurarCohorte && c.Eliminado == false).FirstOrDefault();
                    if (_objConfigurarCohorte == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL OBJETO CONFIGURAR COHORTE. RECARGUE LA PÁGINA</div>";
                    }
                    else
                    {
                        if (DateTime.Compare(Convert.ToDateTime(_fechaInicioModulo), _objConfigurarCohorte.FechaInicio) < 0)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA FECHA DE INICIO DEL MÓDULO DEBE SER MAYOR O IGUAL A LA FECHA DE INICIO DE LA COHORTE</div>";
                        }
                        else if (DateTime.Compare(Convert.ToDateTime(_fechaFinModulo), _objConfigurarCohorte.FechaFin) > 0)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA FECHA DE FINALIZACIÓN DEL MÓDULO DEBE SER MENOR O IGUAL A LA FECHA DE FINALIZACIÓN DE LA COHORTE</div>";
                        }
                        else
                        {
                            int _idModulo = Convert.ToInt32(_objSeguridad.DesEncriptar(_idModuloEncriptado));
                            int _idDocente = Convert.ToInt32(_objSeguridad.DesEncriptar(_idDocenteEncriptado));
                            int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                            int _idConfigurarModuloDocente = _objCatalogoConfigurarModuloDocente.InsertarConfigurarModuloDocente(
                                new EntidadConfigurarModuloDocente()
                                {
                                    Modulo = new EntidadModulo() { IdModulo = _idModulo },
                                    Docente = new EntidadDocente() { IdDocente = _idDocente },
                                    FechaInicio = Convert.ToDateTime(_fechaInicioModulo),
                                    FechaFin = Convert.ToDateTime(_fechaFinModulo),
                                    Eliminado = false
                                });
                            if (_idConfigurarModuloDocente == 0)
                            {
                                _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR AL TRATAR DE INGRESAR LA CONFIGURACIÓN DEL MÓDULO. RECARGUE LA PÁGINA<div>";
                            }
                            else
                            {
                                int _idConfigurarSemestre = _objCatalogoConfigurarSemestre.InsertarConfigurarSemestre(
                                    new EntidadConfigurarSemestre()
                                    {
                                        ConfigurarCohorte = new EntidadConfigurarCohorte() { IdConfigurarCohorte = _idConfigurarCohorte },
                                        ConfigurarModuloDocente = new EntidadConfigurarModuloDocente() { IdConfigurarModuloDocente = _idConfigurarModuloDocente },
                                        Semestre = new EntidadSemestre() { IdSemestre = _idSemestre },
                                        Eliminado = false
                                    });
                                if (_idConfigurarSemestre == 0)
                                {
                                    _objCatalogoConfigurarModuloDocente.EliminarConfigurarModuloDocente(_idConfigurarModuloDocente);
                                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR AL TRATAR DE INGRESAR LA CONFIGURACIÓN DEL SEMESTRE. RECARGUE LA PÁGINA<div>";
                                }
                                else
                                {
                                    _mensaje = "";
                                    _validar = true;
                                    return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
                                }
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
        public ActionResult ConsultarModulosDocentes(string _idConfigurarCohorteEncriptado, string _idSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (_idConfigurarCohorteEncriptado == "0" || string.IsNullOrEmpty(_idConfigurarCohorteEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE ENCONTRÓ EL IDENTIFICADOR DE LA CONFIGURACIÓN</div>";
                }
                else if (string.IsNullOrEmpty(_idSemestreEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN SEMESTRE</div>";
                }
                else
                {
                    int _idConfigurarCohorte = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarCohorteEncriptado));
                    int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                    var _listaModulos = _objCatalogoModulo.ConsultarModulosNoConfiguradosPorConfigurarCohortePorSemestre(_idConfigurarCohorte,_idSemestre).Where(c => c.Eliminado == false).ToList();
                    string _optionModulo = "<option value='0'>SELECCIONE UN MÓDULO</option>";
                    foreach (var item in _listaModulos.OrderBy(c=>c.Descripcion))
                    {
                        _optionModulo = _optionModulo + "<option value='" + _objSeguridad.Encriptar(item.IdModulo.ToString()) + "'>" + item.Descripcion.ToUpper() + "</option>";
                    }
                    string _selectModulo = "<select id='selectModulo' class='form-control'>" + _optionModulo + "</select>";
                    string _formSelectModulo = "<div class='form-group'>" +
                                                      "<label>Módulo:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectModulo +
                                                      "</div>" +
                                                    "</div>";
                    var _listaDocente = _objCatalogoDocente.ConsultarDocente().Where(c => c.Eliminado == false).ToList();
                    string _optionDocente = "<option value='0'>SELECCIONE UN DOCENTE</option>";
                    foreach (var item in _listaDocente.OrderBy(c=>c.Persona.Nombres))
                    {
                        _optionDocente = _optionDocente + "<option value='" + _objSeguridad.Encriptar(item.IdDocente.ToString()) + "'>" + item.Persona.Nombres.ToUpper() + "</option>";
                    }
                    string _selectDocente = "<select id='selectDocente' class='form-control'>" + _optionDocente + "</select>";
                    string _formSelectDocente = "<div class='form-group'>" +
                                                      "<label>Docente:</label>" +
                                                      "<div class='input-group'>" +
                                                        "<div class='input-group-prepend'>" +
                                                          "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                        "</div>" +
                                                        _selectDocente +
                                                      "</div>" +
                                                    "</div>";
                    string _inputFechaInicio = "<input id='inputFechaInicioModulo' type='date' class='form-control' data-inputmask-alias='datetime' data-inputmask-inputformat='dd/mm/yyyy' data-mask='' im-insert='false'>";
                    string _inputFechaFin = "<input id='inputFechaFinModulo' type='date' class='form-control' data-inputmask-alias='datetime' data-inputmask-inputformat='dd/mm/yyyy' data-mask='' im-insert='false'>";
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
                                                 "</div>" +
                                               "</div>";
                    string _buttonAsignarModuloDocente = "<button onclick='asignarModuloDocenteConfigurarSemestre();' type='button' class='btn btn-block btn-outline-success'>Asignar Modulo y Docente</button>";

                    string _tabla = "<div class='row'>" +
                                       "<div class='col-md-6'>" + _formSelectModulo + "</div>" +
                                       "<div class='col-md-6'>" + _formSelectDocente + "</div>" +
                                       "<div class='col-md-6'>" + _formInputFechaInicio + "</div>" +
                                       "<div class='col-md-6'>" + _formInputFechaFin + "</div>" +
                                       "<div class='col-md-12'>" + _buttonAsignarModuloDocente + "</div>" +
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
        [HttpPost]
        public ActionResult Cargarlistadosemestre()
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                var _listaSemestre = _objCatalogoSemestre.ConsultarSemestre().Where(c=>c.Eliminado==false).ToList();
                string _optionSemestre = "<option value='0'>SELECCIONE UN SEMESTRE</option>";
                foreach (var item in _listaSemestre)
                {
                    _optionSemestre = _optionSemestre + "<option value='" + _objSeguridad.Encriptar(item.IdSemestre.ToString()) + "'>" + item.Descripcion.ToUpper() + "</option>";
                }
                string _selectSemestre = "<select onchange='consultarModulosDocentes();' id='selectSemestre' class='form-control'>" + _optionSemestre + "</select>";
                string _formSelectSemestre = "<div class='form-group'>" +
                                                  "<label>Semestre:</label>" +
                                                  "<div class='input-group'>" +
                                                    "<div class='input-group-prepend'>" +
                                                      "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                    "</div>" +
                                                    _selectSemestre +
                                                  "</div>" +
                                                "</div>";
               
                string _tabla = "<div class='row'>" +
                                   "<div class='col-md-12'>" + _formSelectSemestre + "</div>" +
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


        [HttpPost]
        public ActionResult GuardarconfigurarCohorte(string _idMaestriaEncriptado, string _idCohorteEncriptado, string _fechaInicio, string _fechaFin)
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
                else if(string.IsNullOrEmpty(_fechaInicio))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA FECHA DE INICIO</div>";
                }
                else if (string.IsNullOrEmpty(_fechaFin))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA FECHA FIN</div>";
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
                                FechaInicio=Convert.ToDateTime(_fechaInicio),
                                FechaFin=Convert.ToDateTime(_fechaFin),
                                Eliminado = false
                            });
                        string _inputFechaInicio = "<label id='inputFechaInicio' class='form-control'>" + _fechaInicio + "</label>";
                        string _inputFechaFin = "<label id='inputFechaFin' class='form-control' >" +_fechaFin + "</label>";
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
                                                       _inputIdConfigurarCohorte+
                                                     "</div>" +
                                                   "</div>";
                        string _contenedorFechas = "<div class='row'><div class='col-md-6'>" + _formInputFechaInicio + "</div>" +
                                        "<div class='col-md-6'>" + _formInputFechaFin + "</div></div>";

                        string   _fechas =
                                  "<div class='card' style='background: khaki;'>" +
                                        "<div class='card-header callout callout-warning'>" +
                                            "<h3 class='card-title'>Eliminar fechas de configuración</h3>" +
                                            "<div class='card-tools'>" +
                                                "<button type='button' onclick='eliminarConfigurarCohorte(\"" + _objSeguridad.Encriptar(_idConfigurarCohorte.ToString()) + "\");' class='btn btn-sm btn-warning'>" +
                                                    "<i class='fas fa-trash'></i>" +
                                                "</button>" +
                                            "</div>" +
                                        "</div>" +
                                      "<div class='card-body'>" +
                                            _contenedorFechas +
                                      "</div> " +
                                    "</div>";
                        
                        string _buttonConsultar = "<button onclick='consultarConfigurarCohorte();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";

                        string _inputFechas = "<div class='col-md-12'>" + _fechas + "</div>";
                        string _botones = "<div class='col-md-12'>" + _buttonConsultar + "</div>";
                        
                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, boton = _botones, fechas = _inputFechas }, JsonRequestBehavior.AllowGet);
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
                            _buttonGuardar = "<button onclick='cargarListadoSemestre();' type='button' class='btn btn-block btn-outline-primary'>Ver listado de semestres</button>";
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
                        if (_objConfigurarCohorte != null && _objConfigurarCohorte.Utilizado!="1")
                        {
                            _fechas =
                                  "<div class='card' style='background: khaki;'>" +
                                        "<div class='card-header callout callout-warning'>" +
                                            "<h3 class='card-title'>Eliminar fechas de configuración</h3>" +
                                            "<div class='card-tools'>" +
                                                "<button onclick='eliminarConfigurarCohorte(\""+_objSeguridad.Encriptar(_objConfigurarCohorte.IdConfigurarCohorte.ToString())+ "\");' type='button' class='btn btn-sm btn-warning'>" +
                                                    "<i class='fas fa-trash'></i>" +
                                                "</button>" +
                                            "</div>" +
                                        "</div>" +
                                      "<div class='card-body'>" +
                                            _fechas +
                                      "</div> " +
                                    "</div>";
                        }
                        string _inputFechas = "<div class='col-md-12'>"+  _fechas+ "</div>";
                        string _botones = "<div class='col-md-12'>" + _buttonGuardar + "</div>";
                                            
                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar, inputFechas = _inputFechas, botones= _botones }, JsonRequestBehavior.AllowGet);
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
                    var _listaCohorte = _objCatalogoCohorte.ConsultarCohorte().Where(c => c.Eliminado == false && c.Estado == "ACTIVA" && c.Maestria.IdMestria==_idMaestria).ToList();
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
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }
    }
}