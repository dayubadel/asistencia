using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadPreseleccionado
    {
        public int IdPreseleccionado { get; set; }
        public EntidadInscripcion Inscripcion { get; set; }
    }
}