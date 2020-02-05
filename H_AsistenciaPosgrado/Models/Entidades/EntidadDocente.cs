using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadDocente
    {
        public int IdDocente { get; set; }
        public EntidadPersona Persona { get; set; }
        public bool Eliminado { get; set; }
    }
}