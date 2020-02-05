using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadConfigurarCohorte
    {
        public int IdConfigurarCohorte { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Eliminado { get; set; }
        public EntidadCohorte Cohorte { get; set; }
        public string Utilizado { get; set; }
    }
}