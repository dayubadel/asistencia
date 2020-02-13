using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult ConfigurarCohorte()
        {
            return View();
        }

        public ActionResult Modulo()
        {
            return View();
        }

        public ActionResult Semestre()
        {
            return View();
        }

        public ActionResult Docente()
        {
            return View();
        }

        public ActionResult ConfigurarHorario()
        {
            return View();
        }

        public ActionResult Asistencia()
        {
            return View();
        }
        public ActionResult AsistenciaDiaria()
        {
            return View();
        }
        public ActionResult ConfigurarFechaCohorte()
        {
            return View();
        }
    }
}