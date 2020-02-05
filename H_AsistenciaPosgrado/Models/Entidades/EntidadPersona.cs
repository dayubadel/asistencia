using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadPersona
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; }
        public string NumeroIdentificacion { get; set; }
        public EntidadTipoIdentificacion TipoIdentificacion { get; set; }
        public EntidadSexo Sexo { get; set; }
        public bool Eliminado { get; set; }
    }
}