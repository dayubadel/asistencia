using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoHorario
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadHorario> ConsultarHorario()
        {
            List<EntidadHorario> _lista = new List<EntidadHorario>();
            foreach (var item in _entitiesPosgrado.Sp_HorarioConsultar())
            {
                _lista.Add(new EntidadHorario()
                {
                    IdHorario = item.Id_Horario,
                    HoraEntrada =item.Hora_Entrada,
                    HoraSalida = item.Hora_Salida,
                    Eliminado = item.Eliminado_Horario,
                    Utilizado=item.UtilizadoHorario,
                    ConfigurarSemestre = new EntidadConfigurarSemestre() { IdConfigurarSemestre = item.Id_Configurar_Semestre },
                    Dia= new EntidadDia()
                    {
                        IdDia = item.Id_Dia,
                        Identificador = item.Identificador_Dia,
                        Descripcion = item.Descripcion_Dia,
                        Eliminado = item.Eliminado_Dia
                    }
                });
            }
            return _lista;
        }

        public int InsertarHorario(EntidadHorario _objHorario)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_HorarioInsertar(_objHorario.ConfigurarSemestre.IdConfigurarSemestre,_objHorario.Dia.IdDia,_objHorario.HoraEntrada,_objHorario.HoraSalida,_objHorario.Eliminado).Select(x=>x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void EliminarHorario(int _idHorario)
        {
            _entitiesPosgrado.Sp_HorarioEliminar(_idHorario);
        }
    }
}