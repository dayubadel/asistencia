using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoAsistencia
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();

        public int InsertarAsistencia(EntidadAsistencia _objAsistencia)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_AsistenciaInsertar(_objAsistencia.Horario.IdHorario, _objAsistencia.Matricula.IdMatricula, _objAsistencia.AsistenciaTipo.IdAsistenciaTipo, _objAsistencia.FechaAsistencia, _objAsistencia.Eliminado).Select(x => x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}