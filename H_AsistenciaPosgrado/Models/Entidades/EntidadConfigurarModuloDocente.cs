using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadConfigurarModuloDocente
    {
        public int IdConfigurarModuloDocente { get; set; }
        public EntidadModulo Modulo { get; set; }
        public EntidadDocente Docente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Eliminado { get; set; }
        public string Utilizado { get; set; }
    }
}