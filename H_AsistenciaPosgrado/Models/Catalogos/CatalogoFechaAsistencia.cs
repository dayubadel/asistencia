using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoFechaAsistencia
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public int InsertarFechaAsistencia(EntidadFechaAsistencia _objFechaAsistencia)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_FechaAsistenciaInsertar(_objFechaAsistencia.Horario.IdHorario, _objFechaAsistencia.Fecha, _objFechaAsistencia.Eliminado).Select(x => x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<EntidadFechaAsistencia> ConsultarFechaAsistencia()
        {
            List<EntidadFechaAsistencia> _lista = new List<EntidadFechaAsistencia>();
            foreach (var item in _entitiesPosgrado.Sp_FechaAsistenciaConsultar())
            {
                _lista.Add(new EntidadFechaAsistencia()
                {
                    IdFechaAsistencia = item.Id_Fecha_Asistencia,
                    Fecha = item.Fecha_Asistencia,
                    Horario = new EntidadHorario()
                    {
                        IdHorario = item.Id_Horario,
                        HoraEntrada = item.Hora_Entrada,
                        HoraSalida = item.Hora_Salida,
                        Eliminado = item.Eliminado_Horario,
                        Utilizado = item.UtilizadoHorario,
                        ConfigurarSemestre = new EntidadConfigurarSemestre()
                        {
                            IdConfigurarSemestre = item.Id_Configurar_Semestre,
                            Eliminado = item.Eliminado_Configurar_Semestre,
                            Utilizado = item.Utilizado_Configurar_Semestre,
                            Semestre = new EntidadSemestre()
                            {
                                IdSemestre = item.Id_Semestre,
                                Descripcion = item.Descripcion_Semestre,
                                Identificador = item.Identificador_Semestre,
                                Eliminado = item.Eliminado_Semestre
                            },
                            ConfigurarCohorte = new EntidadConfigurarCohorte()
                            {
                                IdConfigurarCohorte = item.Id_Configurar_Cohorte,
                                Eliminado = item.Eliminado_Configurar_Cohorte,
                                FechaInicio = item.Fecha_Inicio_Configurar_Cohorte,
                                FechaFin = item.Fecha_Fin_Configurar_Cohorte,
                                Cohorte = new EntidadCohorte()
                                {
                                    IdCohorte = item.Id_Cohorte,
                                    Fecha = Convert.ToDateTime(item.Fecha_Cohorte),
                                    Eliminado = Convert.ToBoolean(item.Eliminado_Cohorte),
                                    Detalle = item.Detalle_Cohorte,
                                    Maestria = new EntidadMaestria()
                                    {
                                        IdMestria=item.IdMaestria,
                                        Nombre=item.Nombre_Maestria,
                                        Estado =item.Estado_Maestria,
                                        Eliminado = Convert.ToBoolean(item.Eliminado_Maestria)
                                    }
                                }
                            },
                            ConfigurarModuloDocente = new EntidadConfigurarModuloDocente()
                            {
                                IdConfigurarModuloDocente = item.Id_Configurar_Modulo_Docente,
                                Eliminado = item.Eliminado_Configurar_Modulo_Docente,
                                FechaInicio = item.Fecha_Inicio_Configurar_Modulo_Docente,
                                FechaFin = item.Fecha_Fin_Configurar_Modulo_Docente,
                                Utilizado = item.Utilizado_Configurar_Modulo_Docente,
                                Docente = new EntidadDocente()
                                {
                                    IdDocente = item.Id_Docente,
                                    Eliminado = item.Eliminado_Docente,
                                    Persona = new EntidadPersona()
                                    {
                                        IdPersona = item.Id_Persona,
                                        Nombres = item.Apellido_Paterno_Persona + " " + item.Apellido_Materno_Persona + " " + item.Nombres_Persona,
                                        NumeroIdentificacion = item.Cedula_Persona
                                    }
                                },
                                Modulo = new EntidadModulo()
                                {
                                    IdModulo = item.Id_Modulo,
                                    Descripcion = item.Descripcion_Modulo,
                                    Eliminado = item.Eliminado_Modulo
                                }
                            }
                        },
                        Dia = new EntidadDia()
                        {
                            IdDia = item.Id_Dia,
                            Identificador = item.Identificador_Dia,
                            Descripcion = item.Descripcion_Dia,
                            Eliminado = item.Eliminado_Dia
                        }
                    },
                });
            }
            return _lista;
        }

        public List<EntidadFechaAsistencia> ConsultarFechaAsistenciaPorIConfigurarSemestre(int _idConfigurarSemestre)
        {
            List<EntidadFechaAsistencia> _lista = new List<EntidadFechaAsistencia>();
            foreach (var item in _entitiesPosgrado.Sp_FechaAsistenciaConsultar().Where(c=>c.Id_Configurar_Semestre==_idConfigurarSemestre).ToList())
            {
                _lista.Add(new EntidadFechaAsistencia()
                {
                    IdFechaAsistencia = item.Id_Fecha_Asistencia,
                    Fecha = item.Fecha_Asistencia,
                    Horario = new EntidadHorario()
                    {
                        IdHorario = item.Id_Horario,
                        HoraEntrada = item.Hora_Entrada,
                        HoraSalida = item.Hora_Salida,
                        Eliminado = item.Eliminado_Horario,
                        Utilizado = item.UtilizadoHorario,
                        ConfigurarSemestre = new EntidadConfigurarSemestre()
                        {
                            IdConfigurarSemestre = item.Id_Configurar_Semestre,
                            Eliminado = item.Eliminado_Configurar_Semestre,
                            Utilizado = item.Utilizado_Configurar_Semestre,
                            Semestre = new EntidadSemestre()
                            {
                                IdSemestre = item.Id_Semestre,
                                Descripcion = item.Descripcion_Semestre,
                                Identificador = item.Identificador_Semestre,
                                Eliminado = item.Eliminado_Semestre
                            },
                            ConfigurarCohorte = new EntidadConfigurarCohorte()
                            {
                                IdConfigurarCohorte = item.Id_Configurar_Cohorte,
                                Eliminado = item.Eliminado_Configurar_Cohorte,
                                FechaInicio = item.Fecha_Inicio_Configurar_Cohorte,
                                FechaFin = item.Fecha_Fin_Configurar_Cohorte,
                                Cohorte = new EntidadCohorte()
                                {
                                    IdCohorte = item.Id_Cohorte,
                                    Fecha = Convert.ToDateTime(item.Fecha_Cohorte),
                                    Eliminado = Convert.ToBoolean(item.Eliminado_Cohorte),
                                    Detalle = item.Detalle_Cohorte,
                                    Maestria = new EntidadMaestria()
                                    {
                                        IdMestria = item.IdMaestria,
                                        Nombre = item.Nombre_Maestria,
                                        Estado = item.Estado_Maestria,
                                        Eliminado = Convert.ToBoolean(item.Eliminado_Maestria)
                                    }
                                }
                            },
                            ConfigurarModuloDocente = new EntidadConfigurarModuloDocente()
                            {
                                IdConfigurarModuloDocente = item.Id_Configurar_Modulo_Docente,
                                Eliminado = item.Eliminado_Configurar_Modulo_Docente,
                                FechaInicio = item.Fecha_Inicio_Configurar_Modulo_Docente,
                                FechaFin = item.Fecha_Fin_Configurar_Modulo_Docente,
                                Utilizado = item.Utilizado_Configurar_Modulo_Docente,
                                Docente = new EntidadDocente()
                                {
                                    IdDocente = item.Id_Docente,
                                    Eliminado = item.Eliminado_Docente,
                                    Persona = new EntidadPersona()
                                    {
                                        IdPersona = item.Id_Persona,
                                        Nombres = item.Apellido_Paterno_Persona + " " + item.Apellido_Materno_Persona + " " + item.Nombres_Persona,
                                        NumeroIdentificacion = item.Cedula_Persona
                                    }
                                },
                                Modulo = new EntidadModulo()
                                {
                                    IdModulo = item.Id_Modulo,
                                    Descripcion = item.Descripcion_Modulo,
                                    Eliminado = item.Eliminado_Modulo
                                }
                            }
                        },
                        Dia = new EntidadDia()
                        {
                            IdDia = item.Id_Dia,
                            Identificador = item.Identificador_Dia,
                            Descripcion = item.Descripcion_Dia,
                            Eliminado = item.Eliminado_Dia
                        }
                    },
                });
            }
            return _lista;
        }
        public List<EntidadFechaAsistencia> ConsultarFechaAsistenciaPorId(int _idFechaAsistencia)
        {
            List<EntidadFechaAsistencia> _lista = new List<EntidadFechaAsistencia>();
            foreach (var item in _entitiesPosgrado.Sp_FechaAsistenciaConsultar().Where(c => c.Id_Fecha_Asistencia == _idFechaAsistencia).ToList())
            {
                _lista.Add(new EntidadFechaAsistencia()
                {
                    IdFechaAsistencia = item.Id_Fecha_Asistencia,
                    Fecha = item.Fecha_Asistencia,
                    Horario = new EntidadHorario()
                    {
                        IdHorario = item.Id_Horario,
                        HoraEntrada = item.Hora_Entrada,
                        HoraSalida = item.Hora_Salida,
                        Eliminado = item.Eliminado_Horario,
                        Utilizado = item.UtilizadoHorario,
                        ConfigurarSemestre = new EntidadConfigurarSemestre()
                        {
                            IdConfigurarSemestre = item.Id_Configurar_Semestre,
                            Eliminado = item.Eliminado_Configurar_Semestre,
                            Utilizado = item.Utilizado_Configurar_Semestre,
                            Semestre = new EntidadSemestre()
                            {
                                IdSemestre = item.Id_Semestre,
                                Descripcion = item.Descripcion_Semestre,
                                Identificador = item.Identificador_Semestre,
                                Eliminado = item.Eliminado_Semestre
                            },
                            ConfigurarCohorte = new EntidadConfigurarCohorte()
                            {
                                IdConfigurarCohorte = item.Id_Configurar_Cohorte,
                                FechaInicio = item.Fecha_Inicio_Configurar_Cohorte,
                                FechaFin = item.Fecha_Fin_Configurar_Cohorte,
                                Eliminado = item.Eliminado_Configurar_Cohorte,
                                Cohorte = new EntidadCohorte()
                                {
                                    IdCohorte = item.Id_Cohorte,
                                    Fecha = Convert.ToDateTime(item.Fecha_Cohorte),
                                    Eliminado = Convert.ToBoolean(item.Eliminado_Cohorte),
                                    Detalle = item.Detalle_Cohorte,
                                    Maestria = new EntidadMaestria()
                                    {
                                        IdMestria = item.IdMaestria,
                                        Nombre = item.Nombre_Maestria,
                                        Estado = item.Estado_Maestria,
                                        Eliminado = Convert.ToBoolean(item.Eliminado_Maestria)
                                    }
                                }
                            },
                            ConfigurarModuloDocente = new EntidadConfigurarModuloDocente()
                            {
                                IdConfigurarModuloDocente = item.Id_Configurar_Modulo_Docente,
                                Eliminado = item.Eliminado_Configurar_Modulo_Docente,
                                FechaInicio = item.Fecha_Inicio_Configurar_Modulo_Docente,
                                FechaFin = item.Fecha_Fin_Configurar_Modulo_Docente,
                                Utilizado = item.Utilizado_Configurar_Modulo_Docente,
                                Docente = new EntidadDocente()
                                {
                                    IdDocente = item.Id_Docente,
                                    Eliminado = item.Eliminado_Docente,
                                    Persona = new EntidadPersona()
                                    {
                                        IdPersona = item.Id_Persona,
                                        Nombres = item.Apellido_Paterno_Persona + " " + item.Apellido_Materno_Persona + " " + item.Nombres_Persona,
                                        NumeroIdentificacion = item.Cedula_Persona
                                    }
                                },
                                Modulo = new EntidadModulo()
                                {
                                    IdModulo = item.Id_Modulo,
                                    Descripcion = item.Descripcion_Modulo,
                                    Eliminado = item.Eliminado_Modulo
                                }
                            }
                        },
                        Dia = new EntidadDia()
                        {
                            IdDia = item.Id_Dia,
                            Identificador = item.Identificador_Dia,
                            Descripcion = item.Descripcion_Dia,
                            Eliminado = item.Eliminado_Dia
                        }
                    },
                });
            }
            return _lista;
        }
    }
}