using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadAsistencia
    {
        public int IdAsistencia { get; set; }
        public EntidadMatricula Matricula { get; set; }
        public EntidadAsistenciaTipo AsistenciaTipo { get; set; }
        public EntidadFechaAsistencia FechaAsistencia { get; set; }
        public bool Eliminado { get; set; }
    }
}