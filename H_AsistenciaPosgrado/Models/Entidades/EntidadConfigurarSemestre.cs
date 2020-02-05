using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadConfigurarSemestre
    {
        public int IdConfigurarSemestre { get; set; }
        public EntidadConfigurarCohorte ConfigurarCohorte { get; set; }
        public EntidadConfigurarModuloDocente ConfigurarModuloDocente { get; set; }
        public EntidadSemestre Semestre { get; set; }
        public bool Eliminado { get; set; }
        public string Utilizado { get; set; }
    }
}