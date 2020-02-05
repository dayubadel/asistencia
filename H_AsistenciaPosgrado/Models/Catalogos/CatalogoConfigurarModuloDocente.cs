using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoConfigurarModuloDocente
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public int InsertarConfigurarModuloDocente(EntidadConfigurarModuloDocente _objConfigurarModuloDocente)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_ConfigurarModuloDocenteInsertar(_objConfigurarModuloDocente.Modulo.IdModulo,_objConfigurarModuloDocente.Docente.IdDocente,_objConfigurarModuloDocente.FechaInicio,_objConfigurarModuloDocente.FechaFin,_objConfigurarModuloDocente.Eliminado).Select(x=>x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void EliminarConfigurarModuloDocente(int _idConfigurarModuloDocente)
        {
            _entitiesPosgrado.Sp_ConfigurarModuloDocenteEliminar(_idConfigurarModuloDocente);
        }
    }
}