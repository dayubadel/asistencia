using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoAsistenciaTipo
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadAsistenciaTipo> ConsultarAsistenciaTipo()
        {
            List<EntidadAsistenciaTipo> _lista = new List<EntidadAsistenciaTipo>();
            foreach (var item in _entitiesPosgrado.Sp_AsistenciaTipoConsultar())
            {
                _lista.Add(new EntidadAsistenciaTipo()
                {
                    IdAsistenciaTipo = item.Id_TipoAsistencia,
                    Identificador = item.Identificador,
                    Descripcion = item.Descripcion,
                    Eliminado = item.Eliminado
                });
            }
            return _lista;
        }
    }
}