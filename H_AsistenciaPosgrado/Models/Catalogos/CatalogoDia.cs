using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoDia
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadDia> ConsultarDia()
        {
            List<EntidadDia> _lista = new List<EntidadDia>();
            foreach (var item in _entitiesPosgrado.Sp_DiaConsultar())
            {
                _lista.Add(new EntidadDia()
                {
                    IdDia = item.Id_Dia,
                    Identificador = item.Identificador,
                    Descripcion = item.Descripcion,
                    Eliminado = item.Eliminado
                });
            }
            return _lista;
        }
    }
}