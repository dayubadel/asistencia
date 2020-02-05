using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoSemestre
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadSemestre> ConsultarSemestre()
        {
            List<EntidadSemestre> _lista = new List<EntidadSemestre>();
            foreach (var item in _entitiesPosgrado.Sp_SemestreConsultar())
            {
                _lista.Add(new EntidadSemestre()
                {
                    IdSemestre = item.Id_Semestre,
                    Identificador= item.Identificador,
                    Descripcion = item.Descripcion,
                    Eliminado = item.Eliminado,
                    Utilizado = item.UtilizadoSemestre
                });
            }
            return _lista;
        }

        public int InsertarSemestre(EntidadSemestre _objSemestre)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_SemestreInsertar(_objSemestre.Descripcion,_objSemestre.Identificador, _objSemestre.Eliminado).Select(y=>y.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ModificarSemeste(EntidadSemestre _objSemestre)
        {
            try
            {
                _entitiesPosgrado.Sp_SemestreModificar(_objSemestre.IdSemestre, _objSemestre.Descripcion, _objSemestre.Identificador, _objSemestre.Eliminado);
                return _objSemestre.IdSemestre;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void EliminarSemestre(int _idSemestre)
        {
            _entitiesPosgrado.Sp_SemestreEliminar(_idSemestre);
        }
    }
}