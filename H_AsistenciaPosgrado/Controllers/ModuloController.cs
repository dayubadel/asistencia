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
    public class ModuloController : Controller
    {
        CatalogoModulo _objCatalogoModulo = new CatalogoModulo();
        Seguridad _objSeguridad = new Seguridad();
        [HttpPost]
        public ActionResult Eliminarmodulo(string _numFila, string _idModuloEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_numFila))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL NUMERO DE LA FILA</div>";
                }
                else if (string.IsNullOrEmpty(_idModuloEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL IDENTIFICADOR DEL MÓDULO QUE DESEA ELIMINAR</div>";
                }
                else
                {
                    int _idModulo = Convert.ToInt32(_objSeguridad.DesEncriptar(_idModuloEncriptado));
                    var _objModulo = _objCatalogoModulo.ConsultarModulos().Where(c => c.IdModulo == _idModulo).FirstOrDefault();
                    if (_objModulo == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO EXISTE EL MÓDULO QUE INTENTA ELIMINAR</div>";
                    }
                    else if(_objModulo.Utilizado=="1")
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>ESTE MÓDULO YA HA SIDO UTILIZADO, POR LO TANTO NO LO PUEDE ELIMINAR</div>";
                    }
                    else
                    {
                        _objCatalogoModulo.EliminarModulo(_idModulo);
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
        public ActionResult Guardarmodulo(string _descripcionModulo)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if(string.IsNullOrEmpty(_descripcionModulo))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL NOMBRE DEL MÓDULO</div>";
                }
                else if(_objCatalogoModulo.ConsultarModulos().Where(c=>c.Eliminado==false && c.Descripcion == _descripcionModulo.Trim()).ToList().Count!=0)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>ESE MÓDULO YA EXISTE, REVISE EN LA LISTA</div>";
                }
                else
                {
                    int _idModulo = _objCatalogoModulo.InsertarModulo(new EntidadModulo() { Descripcion = _descripcionModulo.Trim() });
                    if (_idModulo == 0)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN PROBLEMA AL INTENTAR GUARDAR EL MÓDULO</div>";
                    }
                    else
                    {
                        _mensaje = "";
                        _validar = true;
                        return Json(new { mensaje = _mensaje, validar = _validar}, JsonRequestBehavior.AllowGet);
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
        public ActionResult Cargartablamodulos()
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                var _listaModulos = _objCatalogoModulo.ConsultarModulos().Where(c => c.Eliminado == false).ToList();


                string _cabecera = "<thead>" +
                            "<tr>" +
                              "<th>#</th>" +
                              "<th>Modulo</th>" +
                              "<th>Acciones</th>" +
                            "</tr>" +
                          "</thead>";

                string _filasCuerpo = "";
                int _contador = 1;
                string _buttonEliminar = "";
                foreach (var item in _listaModulos.OrderBy(c => c.Descripcion))
                {
                    _buttonEliminar = "";
                    if (item.Utilizado == "0")
                    {
                        string _idModuloEncriptado = _objSeguridad.Encriptar(item.IdModulo.ToString());
                        _buttonEliminar = "<button id='btn"+_contador+"' onclick='eliminarModulo("+_contador+ ", \""+_idModuloEncriptado+"\");' type='button' class='btn btn-outline-danger'><i class='fas fa-times'></i></button>";
                    }
                    _filasCuerpo = _filasCuerpo +
                        "<tr id='"+_contador+"'>" +
                              "<td>" + _contador + "</td>" +
                              "<td>" + item.Descripcion.ToUpper() + "</td>" +
                              "<td>"+ _buttonEliminar + "</td>" +
                        "</tr>";
                    _contador++;
                }

                string _tablaFinal = "<br><div class='card'>" +
                      "<div class='card-header'>" +
                        "<h3 class='card-title'>Módulos registrados en el sistema</h3>" +
                      "</div>" +
                      "<div class='card-body p-0'>" +
                       "<table id='sd' class='table table-striped'>" +
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
                string _inputDescripcion = "<input id='inputModulo' type='text' class='form-control'>";
                string _buttonGuardar = "<button onclick='guardarModulo();' type='button' class='btn btn-block btn-outline-success'>Guardar</button>";

                string _formInputDescripcion = "<div class='form-group'>" +
                                                     "<label>Módulo:</label>" +
                                                     "<div class='input-group'>" +
                                                       "<div class='input-group-prepend'>" +
                                                         "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                       "</div>" +
                                                       _inputDescripcion +
                                                     "</div>" +
                                                   "</div>";
                string _tabla = "<div class='row'>" +
                                "<div class='col-md-12'>" + _formInputDescripcion + "</div>" +
                                "<div class='col-md-12'>" + _buttonGuardar + "</div>" +
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