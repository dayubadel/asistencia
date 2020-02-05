using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoModulo
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadModulo> ConsultarModulos()
        {
            List<EntidadModulo> _lista = new List<EntidadModulo>();
            foreach (var item in _entitiesPosgrado.Sp_ModuloConsultar())
            {
                _lista.Add(new EntidadModulo()
                {
                    IdModulo = item.Id_Modulo,
                    Descripcion = item.Descripcion,
                    Eliminado = item.Eliminado,
                    Utilizado=item.UtilizadoModulo
                });
            }
            return _lista;
        }
        public List<EntidadModulo> ConsultarModulosNoConfiguradosPorConfigurarCohortePorSemestre(int _idConfigurarCohorte, int _idSemestre)
        {
            List<EntidadModulo> _lista = new List<EntidadModulo>();
            foreach (var item in _entitiesPosgrado.Sp_ModuloConsultarNoConfiguradosPorConfigurarCohortePorSemestre(_idConfigurarCohorte,_idSemestre))
            {
                _lista.Add(new EntidadModulo()
                {
                    IdModulo = item.Id_Modulo,
                    Descripcion = item.Descripcion,
                    Eliminado = item.Eliminado
                });
            }
            return _lista;
        }
        public int InsertarModulo(EntidadModulo _objModulo)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_ModuloInsertar(_objModulo.Descripcion, _objModulo.Eliminado).Select(x=>x.Value.ToString()).FirstOrDefault()); ;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ModificarModulo(EntidadModulo _objModulo)
        {
            try
            {
                _entitiesPosgrado.Sp_ModuloActualizar(_objModulo.IdModulo, _objModulo.Descripcion, _objModulo.Eliminado);
                return _objModulo.IdModulo; 
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public void EliminarModulo(int _idModulo)
        {
            _entitiesPosgrado.Sp_ModuloEliminar(_idModulo);
        }

    }
}