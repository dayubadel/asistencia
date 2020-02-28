using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class DocentePosgradoController : Controller
    {
        // GET: DocentePosgrado
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult ModulosPorDocente()
        {
            return View();
        }

        public ActionResult EstudiantesPorModulos()
        {
            return View();
        }
    }
}