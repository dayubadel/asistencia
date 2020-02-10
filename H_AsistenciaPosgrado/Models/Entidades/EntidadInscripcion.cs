using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadInscripcion
    {
        public int IdInscripcion { get; set; }
        public EntidadPersona Persona { get; set; }
    }
}