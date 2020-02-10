using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadMatricula
    {
        public int IdMatricula { get; set; }
        public EntidadConfigurarCohorte ConfigurarCohorte { get; set; }
        public EntidadTipoMatricula TipoMatricula { get; set; }
        public EntidadPreseleccionado Preseleccionado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool MatriculaVigente { get; set; }
    }
}