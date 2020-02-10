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
        public ActionResult Eliminarhorario(string _contador, string _identificadorDia, string _idHorarioEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_contador))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN CONTADOR</div>";
                }
                else if (string.IsNullOrEmpty(_identificadorDia))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN DÍA</div>";
                }
                else if (string.IsNullOrEmpty(_idHorarioEncriptado) || _idHorarioEncriptado == "0")
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN HORARIO PARA ELIMINAR</div>";
                }
                else
                {
                    int _contadorEntero = Convert.ToInt32(_contador);
                    int _idHorario = Convert.ToInt32(_objSeguridad.DesEncriptar(_idHorarioEncriptado));
                    var _objHorario = _objCatalogoHorario.ConsultarHorario().Where(c => c.Eliminado == false && c.IdHorario == _idHorario).FirstOrDefault();
                    if (_objHorario == null)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>EL HORARIO SELECCIONADO NO EXISTE</div>";
                    }
                    else if (_objHorario.Utilizado=="1")
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>EL HORARIO YA HA SIDO UTILIZADO POR LO TANTO NO SE PODRÁ ELIMINAR</div>";
                    }
                    else
                    {
                        _objCatalogoHorario.EliminarHorario(_idHorario);
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
        public ActionResult Guardarhorario(string _color, string _contador, string _identificadorDia, string _idConfigurarSemestreEncriptado,
            string _horaEntrada, string _horaSalida)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_color))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN COLOR</div>";
                }
                else if (string.IsNullOrEmpty(_contador))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN CONTADOR</div>";
                }
                else if (string.IsNullOrEmpty(_identificadorDia))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UN DÍA</div>";
                }
                else if (string.IsNullOrEmpty(_idConfigurarSemestreEncriptado))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA CONFIGURACIÓN</div>";
                }
                else if (string.IsNullOrEmpty(_horaEntrada))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA HORA DE ENTRADA</div>";
                }
                else if (string.IsNullOrEmpty(_horaSalida))
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>SELECCIONE UNA HORA DE SALIDA</div>";
                }
                else
                {
                    int _contadorEntero = Convert.ToInt32(_contador);
                    TimeSpan _horaEntradaTime = TimeSpan.Parse(_horaEntrada);
                    TimeSpan _horaSalidaTime = TimeSpan.Parse(_horaSalida);
                    if (TimeSpan.Compare(_horaEntradaTime, _horaSalidaTime) >= 0)
                    {
                        _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA HORA DE SALIDA DEBE SER MAYOR A LA HORA DE ENTRADA</div>";
                    }
                    else
                    {
                        int _idConfigurarSemestre = Convert.ToInt32(_objSeguridad.DesEncriptar(_idConfigurarSemestreEncriptado));
                        var _objConfigurarSemestre = _objCatalogoConfigurarSemestre.ConsultarConfigurarSemestrePorId(_idConfigurarSemestre).Where(c => c.Eliminado == false && c.IdConfigurarSemestre == _idConfigurarSemestre).FirstOrDefault();
                        if (_objConfigurarSemestre == null)
                        {
                            _mensaje = "<div class='alert alert-danger text-center' role='alert'>EL IDENTIFICADOR DE LA CONFIGURACIÓN DEL CURSO NO ES VÁLIDO</div>";
                        }
                        else
                        {
                            int _identificadorDiaEntero = Convert.ToInt32(_identificadorDia);
                            var _objDia = _objCatalogoDia.ConsultarDia().Where(c => c.Eliminado == false && c.Identificador == _identificadorDiaEntero).FirstOrDefault();
                            if (_objDia == null)
                            {
                                _mensaje = "<div class='alert alert-danger text-center' role='alert'>EL DÍA INGRESADO NO ES VÁLIDO</div>";
                            }
                            else
                            {
                                int _idHorario = _objCatalogoHorario.InsertarHorario(new EntidadHorario()
                                {
                                    ConfigurarSemestre = new EntidadConfigurarSemestre() { IdConfigurarSemestre = _idConfigurarSemestre },
                                    Dia = new EntidadDia() { IdDia = _objDia.IdDia },
                                    HoraEntrada = _horaEntradaTime,
                                    HoraSalida = _horaSalidaTime,
                                    Eliminado = false
                                });
                                if (_idHorario == 0)
                                {
                                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR AL TRATAR DE INGRESAR EL HORARIO</div>";
                                }
                                else
                                {
                                    var _objHorario = _objCatalogoHorario.ConsultarHorario().Where(c => c.IdHorario == _idHorario && c.Eliminado == false).FirstOrDefault();
                                    string _btnEliminar = "";
                                    if (_objHorario.Utilizado != "1")
                                    {
                                        string _idHorarioEncriptado = _objSeguridad.Encriptar(_objHorario.IdHorario.ToString());
                                        _btnEliminar = "<button onclick='eliminarHorario(" + _contadorEntero + "," + _objDia.Identificador + ", \"" + _idHorarioEncriptado + "\");' class='btn btn-danger btn-xs'>Eliminar</button>";
                                    }
                                    string _horario = "<i class='fa fa-clock " + _color + "'></i>" +
                                                        "<div class='timeline-item'>" +
                                                         "<span class='time'><i class='fa fa-clock-o'></i>"+ _btnEliminar + "</span>" +
                                                         "<h3 class='timeline-header'>" + _objHorario.HoraEntrada.ToString() + " - " + _objHorario.HoraSalida.ToString() + "</h3>"+
                                                         "</div>";
                                    _contadorEntero++;
                                    string _nuevoHorario = "<div id='div" + _contadorEntero + "dia" + _objDia.Identificador + "'>" +
                                                "<i class='fa fa-clock " + _color + "'></i>" +
                                                     "<div  class='timeline-item'>" +
                                                           "<div class='timeline-body'>" +
                                                               "<div class='row'>" +
                                                                   "<div id='mensaje" + _objDia.Identificador + "' class='col-md-12'>" +
                                                                   "</div>" +
                                                                   "<div class='col-md-4'>" +
                                                                   "<input class='form-control' id='horaEntrada" + _objDia.Identificador + "' type='time' min='00:01' max='23:59'>" +
                                                                   "</div>" +
                                                                   "<div class='col-md-4'>" +
                                                                   "<input class='form-control' id='horaSalida" + _objDia.Identificador + "' type='time' min='00:01' max='23:59'>" +
                                                                   "</div>" +
                                                                   "<div class='col-md-4'>" +
                                                                       "<button onclick='guardarHorario(\"" + _color + "\"," + _contadorEntero + "," + _objDia.Identificador + ",\"" + _idConfigurarSemestreEncriptado + "\")' class='btn btn-sm btn-success form-control btn-block'>GUARDAR</button>" +
                                                                   "</div>" +
                                                               "</div>" +
                                                           "</div>" +
                                                       "</div>" +
                                               "</div>";
                                    _mensaje = "";
                                    _validar = true;
                                    return Json(new { mensaje = _mensaje, validar = _validar, tabla = _horario, nuevo=_nuevoHorario }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Consultarhorarioporconfigurarsemestre(string _idConfigurarSemestreEncriptado)
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {
                if (string.IsNullOrEmpty(_idConfigurarSemestreEncriptado)|| _idConfigurarSemestreEncriptado=="0")
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
                            _contenidoTimeline = _contenidoTimeline +
                                                "<div class='time-label'>" +
                                                    "<span class='" + _color + "'>" +
                                                       itemDia.Descripcion.ToUpper() +
                                                    "</span>" +
                                                "</div>";
                            var _listaHorarioPorDia = _listaHorario.Where(c => c.Dia.IdDia == itemDia.IdDia).ToList();
                            int _contador = 0;
                            foreach (var itemHorario in _listaHorarioPorDia)
                            {
                                string _btnEliminar = "";
                                if(itemHorario.Utilizado!="1")
                                {
                                    string _idHorarioEncriptado = _objSeguridad.Encriptar(itemHorario.IdHorario.ToString());
                                    _btnEliminar= "<button onclick='eliminarHorario(" + _contador + "," + itemDia.Identificador + ", \""+ _idHorarioEncriptado + "\");' class='btn btn-danger btn-xs'>Eliminar</button>";
                                }
                                _contenidoTimeline = _contenidoTimeline +
                                                "<div id='div" + _contador + "dia" + itemDia.Identificador + "' >" +
                                                 "<i class='fa fa-clock " + _color + "'></i>" +
                                                      "<div class='timeline-item'>" +
                                                        "<span class='time'><i class='fa fa-clock-o'></i>"+_btnEliminar+"</span>" +
                                                        "<h3 class='timeline-header'>" + itemHorario.HoraEntrada.ToString() + " - " + itemHorario.HoraSalida.ToString() + "</h3>" +
                                                        "</div>" +
                                                "</div>";
                                _contador++;
                            }
                            _contenidoTimeline = _contenidoTimeline +
                                               "<div id='div" + _contador + "dia" + itemDia.Identificador + "'>" +
                                                "<i class='fa fa-clock " + _color + "'></i>" +
                                                     "<div  class='timeline-item'>" +
                                                           "<div class='timeline-body'>" +
                                                               "<div class='row'>" +
                                                                    "<div id='mensaje" + itemDia.Identificador + "' class='col-md-12'>" +
                                                                   "</div>" +
                                                                   "<div class='col-md-4'>" +
                                                                   "<input class='form-control' id='horaEntrada" + itemDia.Identificador + "' type='time' min='00:01' max='23:59'>" +
                                                                   "</div>" +
                                                                   "<div class='col-md-4'>" +
                                                                   "<input class='form-control' id='horaSalida" + itemDia.Identificador + "' type='time' min='00:01' max='23:59'>" +
                                                                   "</div>" +
                                                                   "<div class='col-md-4'>" +
                                                                       "<button onclick='guardarHorario(\""+ _color +"\","+_contador+","+itemDia.Identificador+",\""+_idConfigurarSemestreEncriptado+"\")' class='btn btn-sm btn-success form-control btn-block'>GUARDAR</button>" +
                                                                   "</div>" +
                                                               "</div>" +
                                                           "</div>" +
                                                       "</div>" +
                                               "</div>";
                        }
                        string _tablaFinal = "<div class='row'><div class='col-md-2'></div><div class='col-md-8'><div class='timeline'>" + _contenidoTimeline + "</div></div><div class='col-md-2'></div></div>";

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