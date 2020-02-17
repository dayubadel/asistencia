using H_AsistenciaPosgrado.Models.Metodos;
using H_AsistenciaPosgrado.Models.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class InicioController : Controller
    {
        Seguridad _objSeguridad = new Seguridad();
        CatalogoLogin _objCatalogoLogin = new CatalogoLogin();
        CatalogoPersona _objCatalogoPersona = new CatalogoPersona();

        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Salir()
        {
            return View();
        }



        public string ValidarLogin(string _usuarioEncriptado, string _claveEncriptada, string _roll)
        {

            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            if (string.IsNullOrEmpty(_usuarioEncriptado))
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>EL USUARIO NO EXISTE</div>";
            }
            else if (string.IsNullOrEmpty(_claveEncriptada))
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>LA CLAVE NO EXISTE</div>";

            }
            else if (string.IsNullOrEmpty(_roll))
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>EL ROL NO EXISTE</div>";

            }
            else
            {
                int _idRol = Convert.ToInt32(_objSeguridad.DesEncriptar(_roll));
                string _usuario = _objSeguridad.DesEncriptar(_usuarioEncriptado);
                var _objLogin = _objCatalogoLogin.Login(_usuario, _claveEncriptada).Where(c => c.Tipo.idTipo == _idRol).FirstOrDefault();
                if (_objLogin == null)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>USUARIO O CONTRASEÑA INCORRECTA</div>";
                }
                else
                {                    
                    Session.Add("idtipousuario", _objLogin.idTipoUsuario);
                    Session.Add("idusuario", _objLogin.Usuario.IdUsuario);
                    Session.Add("idPersona", _objLogin.Usuario.Persona.IdPersona);

                    var persona = _objCatalogoPersona.ConsultarPersonaPorId(Convert.ToInt32(_objLogin.Usuario.Persona.IdPersona));
                    string nombre = persona.Nombres;
                    Session.Add("nombre", nombre);
                    string _rutaFoto = Url.Content("~/Content/img/profile-pic.jpg");
                    if (_objLogin.Usuario.Foto != null && _objLogin.Usuario.Foto.Trim() != "")
                    {
                        _rutaFoto = "http://gestionacademica.espam.edu.ec/img/fotos/" + _objLogin.Usuario.Foto;
                    }

                    Session.Add("fotoperfil", _rutaFoto);

                    Session.Add("roll", _idRol);
                    _mensaje = "";

                }
            }
            return _mensaje;
        }


        [HttpPost]
        public ActionResult Login()
        {
            string _mensaje = "<div class='alert alert-danger text-center' role='alert'>OCURRIÓ UN ERROR INESPERADO</div>";
            bool _validar = false;
            try
            {

                string _usuarioEncriptado = "";
                string _contrasenaEncriptada = "";
                string _rolGalletaEncriptado = "";
                bool _validarAcceso = false;
                if (Request.Cookies["espam"] != null)
                {
                    _usuarioEncriptado = Server.HtmlEncode(Request.Cookies["espam"]["kus"]);
                    _contrasenaEncriptada = Server.HtmlEncode(Request.Cookies["espam"]["kpa"]);
                    _rolGalletaEncriptado = Server.HtmlEncode(Request.Cookies["espam"]["kri"]);
                    _validarAcceso = true;
                }
                if (_validarAcceso == false)
                {
                    _mensaje = "<div class='alert alert-danger text-center' role='alert'>NO TIENE ACCESO A ESTA PARTE DEL SISTEMA</div>";
                }
                else
                {
                    _mensaje = ValidarLogin(_usuarioEncriptado, _contrasenaEncriptada, _rolGalletaEncriptado);
                    if (_mensaje == "")
                    {
                        _mensaje = "<div class='alert alert-success text-center' role='alert'>LOGEADO CORRECTAMENTE REDIRECCIONANDO...</div>";
                        _validar = true;
                    }
                }

            }
            catch (Exception ex)
            {
                _mensaje = "<div class='alert alert-danger text-center' role='alert'>ERROR INTERNO DEL SISTEMA: " + ex.Message + "</div>";
            }

            return Json(new { mensaje = _mensaje, validar = _validar }, JsonRequestBehavior.AllowGet);
        }


        public void Logout()
        {
            Session.RemoveAll();
            Response.Redirect(Url.Action("Salir", "Inicio"));
        }
    }
}