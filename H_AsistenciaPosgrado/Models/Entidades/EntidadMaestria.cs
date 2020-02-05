using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadMaestria
    {
        public int IdMestria { get; set; }
        public string Nombre { get; set; }
        public string  Estado { get; set; }
        public bool Eliminado { get; set; }
    }
}