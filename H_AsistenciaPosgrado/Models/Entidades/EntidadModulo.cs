using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadModulo
    {
        public int IdModulo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        public string Utilizado { get; set; }
    }
}