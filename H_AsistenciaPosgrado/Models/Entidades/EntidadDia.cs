using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadDia
    {
        public int IdDia { get; set; }
        public string IdDiaEncriptado { get; set; }
        public int Identificador { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
    }
}