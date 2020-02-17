using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadUsuario
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public EntidadPersona Persona { get; set; }
        public string Foto { get; set; }
        public int Click { get; set; }
        public bool Eliminado { get; set; }
    }
}