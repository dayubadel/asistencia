using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoConfigurarCohorte
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadConfigurarCohorte> ConsultarConfigurarCohorte()
        {
            List<EntidadConfigurarCohorte> _lista = new List<EntidadConfigurarCohorte>();
            foreach (var item in _entitiesPosgrado.Sp_ConfigurarCohorteConsultar())
            {
                _lista.Add(new EntidadConfigurarCohorte()
                {
                    IdConfigurarCohorte = item.Id_Configurar_Cohorte,
                    FechaInicio = item.Fecha_Inicio,
                    FechaFin = item.Fecha_Fin,
                    Utilizado=item.Utilizado_Configurar_Cohorte,
                    Eliminado = Convert.ToBoolean(item.Eliminado_Configurar_Cohorte),
                    Cohorte = new EntidadCohorte()
                    {
                        IdCohorte = item.Id_Cohorte,
                        Detalle = item.Detalle_Cohorte,
                        Estado = item.Estado_Cohorte,
                        Fecha = Convert.ToDateTime(item.Fecha_Chohorte),
                        Eliminado = Convert.ToBoolean(item.Eliminado_Cohorte),
                        Maestria = new EntidadMaestria()
                        {
                            IdMestria = item.Id_Maestria,
                            Nombre = item.Nombre_Maestria,
                            Estado = item.Estado_Maestria,
                            Eliminado = Convert.ToBoolean(item.Eliminado_Maestria)
                        }
                    }
                });
            }
            return _lista;
        }

        public int InsertarConfigurarCohorte(EntidadConfigurarCohorte _objConfigurarCohorte)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_ConfigurarCohorteInsertar(_objConfigurarCohorte.Cohorte.IdCohorte, _objConfigurarCohorte.FechaInicio, _objConfigurarCohorte.FechaFin, _objConfigurarCohorte.Eliminado).Select(x=>x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void EliminarConfigurarCohorte(int _idConfigurarCohorte)
        {
            _entitiesPosgrado.Sp_ConfigurarCohorteEliminar(_idConfigurarCohorte);
        }
    }
}