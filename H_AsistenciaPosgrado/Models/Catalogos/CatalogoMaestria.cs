using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;


namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoMaestria
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadMaestria> ConsultarMaestria()
        {
            List<EntidadMaestria> _lista = new List<EntidadMaestria>();
            foreach (var item in _entitiesPosgrado.Sp_MaestriaConsultar())
            {
                _lista.Add(new EntidadMaestria()
                {
                    IdMestria = item.Id_Maestria,
                    Nombre= item.Nombre_Maestria,
                    Estado=item.Estado_Maestria,
                    Eliminado=Convert.ToBoolean(item.Eliminado_Maestria)
                });
            }
            return _lista;
        }
    }
}