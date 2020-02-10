using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadAsistenciaTipo
    {
        public int IdAsistenciaTipo { get; set; }
        public int Identificador { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
    }
}