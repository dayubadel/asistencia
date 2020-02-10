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
        public EntidadConfigurarSemestre ConfigurarSemestre { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public bool Eliminado { get; set; }
        public string Utilizado { get; set; }
    }
}