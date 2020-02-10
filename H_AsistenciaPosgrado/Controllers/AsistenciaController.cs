﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H_AsistenciaPosgrado.Models.Catalogos;
using H_AsistenciaPosgrado.Models.Entidades;
using H_AsistenciaPosgrado.Models.Metodos;
namespace H_AsistenciaPosgrado.Controllers
{
    public class AsistenciaController : Controller
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
        public ActionResult GenerarAsistencia(string _idConfigurarSemestreEncriptado)
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
                        var _listaHorario = _objCatalogoHorario.ConsultarHorario().Where(c => c.ConfigurarSemestre.IdConfigurarSemestre == _idConfigurarSemestre && c.Eliminado == false).ToList();
                        if (_listaHorario.Count == 0)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>ES NECESARIO QUE CONFIGURE UN HORARIO PARA ESTE MÓDULO</div>";
                        }
                        else
                        {
                            var _listaMatriculados = _objCatalogoMatricula.ConsultarMatricula().Where(c => c.MatriculaVigente == true && c.ConfigurarCohorte.IdConfigurarCohorte == _objConfigurarSemestre.ConfigurarCohorte.IdConfigurarCohorte).ToList();
                            if (_listaMatriculados.Count == 0)
                            {
                                _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO SE HAN REGISTRADO MATRÍCULAS EN LA MAESTRÍA Y COHORTE SELECCIONADOS</div>";
                            }
                            else
                            {
                                var _objAsistenciaTipo = _objCatalogoAsistenciaTipo.ConsultarAsistenciaTipo().Where(c => c.Eliminado == false && c.Identificador == 1).FirstOrDefault();
                                if (_objAsistenciaTipo == null)
                                {
                                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>ES NECESARIO QUE SE REGISTRE EL TIPO DE ASISTENCIA AUTOMÁTICO. CONTÁCTESE CON EL ADMINISTRADOR.</div>";
                                }
                                else
                                {
                                    for (var i = _objConfigurarSemestre.ConfigurarModuloDocente.FechaInicio; i <= _objConfigurarSemestre.ConfigurarModuloDocente.FechaFin; i =i.AddDays(1))
                                    {
                                        DateTime _fechaActual = Convert.ToDateTime(i);
                                        int _identificadorDia =(int)_fechaActual.DayOfWeek;                                        
                                        foreach (var itemHorario in _listaHorario)
                                        {
                                            if(itemHorario.Dia.Identificador== _identificadorDia)
                                            {
                                                int _idFechaAsistencia = _objCatalogoFechaAsistencia.InsertarFechaAsistencia(new EntidadFechaAsistencia() { Horario = new EntidadHorario() { IdHorario = itemHorario.IdHorario }, Fecha = _fechaActual, Eliminado = false });
                                                if (_idFechaAsistencia != 0)
                                                {
                                                    foreach (var itemMatricula in _listaMatriculados)
                                                    {
                                                        int _idAsistencia = _objCatalogoAsistencia.InsertarAsistencia(new EntidadAsistencia() { FechaAsistencia = new EntidadFechaAsistencia() { IdFechaAsistencia = _idFechaAsistencia }, AsistenciaTipo = new EntidadAsistenciaTipo() { IdAsistenciaTipo = _objAsistenciaTipo.IdAsistenciaTipo }, Matricula = new EntidadMatricula() { IdMatricula = itemMatricula.IdMatricula }, Eliminado = false });
                                                    }
                                                }
                                            }
                                        }
                                    }
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
                string _buttonConsultar = "<button onclick='generarAsistencia();' type='button' class='btn btn-block btn-outline-primary'>Consultar</button>";


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