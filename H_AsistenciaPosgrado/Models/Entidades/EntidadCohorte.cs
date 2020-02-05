using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadCohorte
    {
        public int IdCohorte { get; set; }
        public string Detalle { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public bool Eliminado { get; set; }
        public EntidadMaestria Maestria { get; set; }
    }
}