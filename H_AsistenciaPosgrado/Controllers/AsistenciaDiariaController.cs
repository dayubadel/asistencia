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
    public class AsistenciaDiariaController : Controller
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
        public ActionResult Consultarasistenciadiaria(string _idAsistenciaFechaEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_idAsistenciaFechaEncriptado) || _idAsistenciaFechaEncriptado == "0")
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
                        var _listaAsistencia = _objCatalogoAsistencia.ConsultarAsistenciaPorIdFechaAsistencia(_idFechaAsistencia).Where(c => c.Eliminado == false).OrderBy(c => c.Matricula.Preseleccionado.Inscripcion.Persona.Nombres).ToList();
                        string _cabecera = "<thead>" +
                                                 "<tr>" +
                                                   "<th>#</th>" +
                                                   "<th>ESTUDIANTE</th>" +
                                                   "<th>ASISTENCIA</th>" +
                                                 "</tr>" +
                                               "</thead>";

                        string _filasCuerpo = "";
                        int _contador = 1;
                        foreach (var item in _listaAsistencia)
                        {
                            _filasCuerpo = _filasCuerpo +
                                "<tr id='" + _contador + "'>" +
                                      "<td>" + _contador + "</td>" +
                                      "<td>" + item.Matricula.Preseleccionado.Inscripcion.Persona.Nombres.ToUpper() + "</td>" +
                                        "<td>" + item.AsistenciaTipo.Descripcion + "</td>" +
                                "</tr>";
                            _contador++;
                        }

                        string _tablaFinal = "<div class='card'>" +
                              "<div class='card-header'>" +
                                "<h3 class='card-title text-center'>Asistencia registrada en el sistema</h3>" +
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
        public ActionResult Cambiarfechaasistenciaporconfigurarsemestre(string _idConfigurarSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_idConfigurarSemestreEncriptado) || _idConfigurarSemestreEncriptado == "0")
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
                        var _listaFechaAsistencia = _objCatalogoFechaAsistencia.ConsultarFechaAsistenciaPorIConfigurarSemestre(_idConfigurarSemestre).Where(c => c.Eliminado == false).OrderBy(c=>c.Fecha).ToList();
                        string _optionFecha = "<option value='0'>SELECCIONE FECHA Y HORARIO</option>";
                        foreach (var item in _listaFechaAsistencia)
                        {
                            _optionFecha= _optionFecha + "<option value='"+_objSeguridad.Encriptar(item.IdFechaAsistencia.ToString())+"'>"+item.Fecha.ToShortDateString()+" / ("+item.Horario.HoraEntrada.ToString()+" - "+item.Horario.HoraSalida.ToString()+" )</option>";
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
                string _selectFecha = "<select  id='selectFecha' onchange='cambiarHorarioPorFechaAsistencia();' class='form-control'>" + _optionFecha + "</select>";
                string _formSelectFecha = "<div class='form-group'>" +
                                                  "<label>Fecha:</label>" +
                                                  "<div class='input-group'>" +
                                                    "<div class='input-group-prepend'>" +
                                                      "<span class='input-group-text'><i class='fa fa-calendar'></i></span>" +
                                                    "</div>" +
                                                    _selectFecha +
                                                  "</div>" +
                                                "</div>";
                string _buttonConsultar = "<button onclick='consultarAsistenciaDiaria();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";


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
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }
            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }
    }
}