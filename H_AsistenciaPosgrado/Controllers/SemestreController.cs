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
    public class SemestreController : Controller
    {

        CatalogoSemestre _objCatalogoSemestre = new CatalogoSemestre();
        Seguridad _objSeguridad = new Seguridad();
        [HttpPost]
        public ActionResult Eliminarsemestre(string _numFila, string _idSemestreEncriptado)
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
                else if (string.IsNullOrEmpty(_numFila))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL NUMERO DE LA FILA</div>";
                }
                else if (string.IsNullOrEmpty(_idSemestreEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL IDENTIFICADOR DEL SEMESTRE QUE DESEA ELIMINAR</div>";
                }
                else
                {
                    int _idSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idSemestreEncriptado));
                    var _objSemestre = _objCatalogoSemestre.ConsultarSemestre().Where(c => c.IdSemestre == _idSemestre).FirstOrDefault();
                    if (_objSemestre == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO EXISTE EL SEMESTRE QUE INTENTA ELIMINAR</div>";
                    }
                    else if (_objSemestre.Utilizado == "1")
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>ESTE SEMESTRE YA HA SIDO UTILIZADO, POR LO TANTO NO LO PUEDE ELIMINAR</div>";
                    }
                    else
                    {
                        _objCatalogoSemestre.EliminarSemestre(_idSemestre);
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
        public ActionResult Guardarsemestre(string _descripcionSemestre, string _identificadorSemestre)
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
                else if (string.IsNullOrEmpty(_descripcionSemestre))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL NOMBRE DEL SEMESTRE</div>";
                }
                else if (string.IsNullOrEmpty(_identificadorSemestre))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>INGRESE EL IDENTIFICADOR DEL SEMESTRE</div>";
                }
                else if (_objCatalogoSemestre.ConsultarSemestre().Where(c => c.Eliminado == false && c.Descripcion == _descripcionSemestre.Trim()).ToList().Count != 0)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>ESE SEMESTRE YA EXISTE, REVISE EN LA LISTA</div>";
                }
                else if (_objCatalogoSemestre.ConsultarSemestre().Where(c => c.Eliminado == false && c.Identificador == Convert.ToInt32(_identificadorSemestre)).ToList().Count != 0)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>YA EXISTE UN SEMESTRE CON EL MISMO IDENTIFICADOR, REVISE EN LA LISTA</div>";
                }
                else
                {
                    int _idSemestre = _objCatalogoSemestre.InsertarSemestre(new EntidadSemestre() { Descripcion = _descripcionSemestre.Trim(), Identificador=Convert.ToInt32(_identificadorSemestre) });
                    if (_idSemestre == 0)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN PROBLEMA AL INTENTAR GUARDAR EL SEMESTRE</div>";
                    }
                    else
                    {
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
        public ActionResult Cargartablasemestres()
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
                    var _listaSemestres = _objCatalogoSemestre.ConsultarSemestre().Where(c => c.Eliminado == false).ToList();
                    string _cabecera = "<thead>" +
                                "<tr>" +
                                  "<th>#</th>" +
                                  "<th>Semestre</th>" +
                                  "<th>Acciones</th>" +
                                "</tr>" +
                              "</thead>";

                    string _filasCuerpo = "";
                    int _contador = 1;
                    string _buttonEliminar = "";
                    foreach (var item in _listaSemestres.OrderBy(c => c.Identificador))
                    {
                        _buttonEliminar = "";
                        if (item.Utilizado == "0")
                        {
                            string _idSemestreEncriptado = _objSeguridad.Encriptar(item.IdSemestre.ToString());
                            _buttonEliminar = "<button id='btn" + _contador + "' onclick='eliminarSemestre(" + _contador + ", \"" + _idSemestreEncriptado + "\");' type='button' class='btn btn-outline-danger'><i class='fas fa-times'></i></button>";
                        }
                        _filasCuerpo = _filasCuerpo +
                            "<tr id='" + _contador + "'>" +
                                  "<td>" + _contador + "</td>" +
                                  "<td>" + item.Descripcion.ToUpper() + "</td>" +
                                  "<td>" + _buttonEliminar + "</td>" +
                            "</tr>";
                        _contador++;
                    }

                    string _tablaFinal = "<br><div class='card'>" +
                          "<div class='card-header'>" +
                            "<h3 class='card-title'>Semestres registrados en el sistema</h3>" +
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
                    string _inputDescripcion = "<input id='inputSemestre' type='text' class='form-control'>";
                    string _inputIdentificador = "<input id='inputIdentificador' type='number' min='0' class='form-control'>";
                    string _buttonGuardar = "<button onclick='guardarSemestre();' type='button' class='btn btn-block btn-outline-success'>Guardar</button>";

                    string _formInputDescripcion = "<div class='form-group'>" +
                                                         "<label>Semestre:</label>" +
                                                         "<div class='input-group'>" +
                                                           "<div class='input-group-prepend'>" +
                                                             "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                           "</div>" +
                                                           _inputDescripcion +
                                                         "</div>" +
                                                       "</div>";
                    string _formInputIdentificador = "<div class='form-group'>" +
                                                         "<label>Identificador:</label>" +
                                                         "<div class='input-group'>" +
                                                           "<div class='input-group-prepend'>" +
                                                             "<span class='input-group-text'><i class='fa fa-book'></i></span>" +
                                                           "</div>" +
                                                           _inputIdentificador +
                                                         "</div>" +
                                                       "</div>";
                    string _tabla = "<div class='row'>" +
                                    "<div class='col-md-6'>" + _formInputDescripcion + "</div>" +
                                    "<div class='col-md-6'>" + _formInputIdentificador + "</div>" +
                                    "<div class='col-md-12'>" + _buttonGuardar + "</div>" +
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