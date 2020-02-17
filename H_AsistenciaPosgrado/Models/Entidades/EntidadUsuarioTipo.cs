using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Entidades
{
    public class EntidadUsuarioTipo
    {
        public int idTipoUsuario { get; set; }
        public EntidadUsuario Usuario { get; set; }
        public EntidadTipo Tipo { get; set; }
        public bool eliminado { get; set; }
    }
}