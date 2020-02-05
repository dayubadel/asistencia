using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using H_AsistenciaPosgrado.Models.Catalogos;
using H_AsistenciaPosgrado.Models.Metodos;

namespace H_AsistenciaPosgrado.Controllers
{
    public class DocenteController : Controller
    {
        CatalogoDocente _objCatalogoDocente = new CatalogoDocente();
        Seguridad _objSeguridad = new Seguridad();
        [HttpPost]
        public ActionResult Cargartabladocentes()
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                var _listaModulos = _objCatalogoDocente.ConsultarDocente().Where(c => c.Eliminado == false).ToList();


                string _cabecera = "<thead>" +
                            "<tr>" +
                              "<th>#</th>" +
                              "<th>Cédula</th>" +
                              "<th>Docente</th>" +
                            "</tr>" +
                          "</thead>";

                string _filasCuerpo = "";
                int _contador = 1;
                string _buttonEliminar = "";
                foreach (var item in _listaModulos.OrderBy(c => c.Persona.Nombres))
                {
                    _filasCuerpo = _filasCuerpo +
                        "<tr id='" + _contador + "'>" +
                              "<td>" + _contador + "</td>" +
                              "<td>" + item.Persona.NumeroIdentificacion+ "</td>" +
                              "<td>" + item.Persona.Nombres.ToUpper() + "</td>" +
                        "</tr>";
                    _contador++;
                }

                string _tablaFinal = "<br><div class='card'>" +
                      "<div class='card-header'>" +
                        "<h3 class='card-title'>Docentes registrados en el sistema</h3>" +
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

    }
}