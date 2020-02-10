using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadFechaAsistencia
    {
        public int IdFechaAsistencia { get; set; }
        public EntidadHorario Horario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Eliminado { get; set; }
    }
}