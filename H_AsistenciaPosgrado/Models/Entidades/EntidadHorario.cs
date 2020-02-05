using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadHorario
    {
        public int IdHorario { get; set; }
        public string IdHorarioEncriptado { get; set; }
        public EntidadDia Dia { get; set; }
        public EntidadConfigurarModuloDocente ConfigurarModuloDocente { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public bool Eliminado { get; set; }
    }
}