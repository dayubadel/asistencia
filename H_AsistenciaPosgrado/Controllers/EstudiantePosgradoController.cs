using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H_AsistenciaPosgrado.Controllers
{
    public class EstudiantePosgradoController : Controller
    {
        // GET: EstudiantePosgrado
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Horario()
        {
            return View();
        }
    }
}