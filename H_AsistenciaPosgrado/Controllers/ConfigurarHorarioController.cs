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
                var _listaSemestre = _objCatalogoSemestre.ConsultarSemestre().Where(c => c.Eliminado == false).ToList();
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
                                "<div class='col-md-6'>" + _formSelectMaestria + "</div>" +
                                "<div class='col-md-6'>" + _formSelectCohorte + "</div>" +
                                "<div class='col-md-6'>" + _formSelectSemestre + "</div>" +
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