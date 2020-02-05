using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoCohorte
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadCohorte> ConsultarCohorte()
        {
            List<EntidadCohorte> _lista = new List<EntidadCohorte>();
            foreach (var item in _entitiesPosgrado.Sp_CohorteConsultar())
            {
                _lista.Add(new EntidadCohorte()
                {
                    IdCohorte=item.Id_Cohorte,
                    Detalle=item.Detalle_Cohorte,
                    Estado=item.Estado_Cohorte,
                    Fecha=Convert.ToDateTime(item.Fecha_Chohorte),
                    Eliminado=Convert.ToBoolean(item.Eliminado_Cohorte),
                    Maestria=new EntidadMaestria()
                    {
                        IdMestria = item.Id_Maestria,
                        Nombre = item.Nombre_Maestria,
                        Estado = item.Estado_Maestria,
                        Eliminado = Convert.ToBoolean(item.Eliminado_Maestria)
                    }
                });
            }
            return _lista;
        }
    }
}