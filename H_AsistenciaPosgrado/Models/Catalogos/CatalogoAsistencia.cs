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
                return int.Parse(_entitiesPosgrado.Sp_AsistenciaInsertar(_objAsistencia.Matricula.IdMatricula, _objAsistencia.AsistenciaTipo.IdAsistenciaTipo, _objAsistencia.FechaAsistencia.IdFechaAsistencia, _objAsistencia.Eliminado).Select(x => x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public List<EntidadAsistencia> ConsultarAsistencia()
        {
            List<EntidadAsistencia> _lista = new List<EntidadAsistencia>();
            foreach (var item in _entitiesPosgrado.Sp_AsistenciaConsultar())
            {
                _lista.Add(new EntidadAsistencia()
                {
                    IdAsistencia = item.Id_Asistencia, 
                    Eliminado=item.EliminadoAsistencia,
                    AsistenciaTipo= new EntidadAsistenciaTipo()
                    {
                        IdAsistenciaTipo = item.Id_TipoAsistencia,
                        Identificador = item.IdentificadorAsistenciaTipo,
                        Descripcion = item.DescripcionAsistenciaTipo,
                        Eliminado = item.EliminadoAsistenciaTipo
                    },
                    FechaAsistencia = new EntidadFechaAsistencia()
                    {
                        IdFechaAsistencia=item.Id_Fecha_Asistencia
                    },
                    Matricula=new EntidadMatricula()
                    {

                        IdMatricula = item.Id_Matricula,
                        FechaRegistro = item.FechaRegistroMatricula,
                        MatriculaVigente = item.Matricula_Vigente,
                        Preseleccionado = new EntidadPreseleccionado()
                        {
                            IdPreseleccionado = item.Id_preseleccionado,
                            Inscripcion = new EntidadInscripcion()
                            {
                                IdInscripcion = item.IdInscripcion,
                                Persona = new EntidadPersona()
                                {
                                    IdPersona = item.IdPersona,
                                    Nombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.Nombres + " " + item.SegundoNombre,
                                    NumeroIdentificacion = item.Cedula

                                }
                            }
                        },
                        TipoMatricula = new EntidadTipoMatricula()
                        {
                            IdTipoMatricula = item.Id_TipoMatricula,
                            Descripcion = item.DescripcionTipoMatricula,
                            Identificador = item.IdentificadorTipoMatricula,
                            Eliminado = item.EliminadoTipoMatricula
                        },
                        ConfigurarCohorte = new EntidadConfigurarCohorte()
                        {
                            IdConfigurarCohorte = item.Id_ConfiguracionCohorte
                        }
                    }
                });
            }
            return _lista;
        }

        public List<EntidadAsistencia> ConsultarAsistenciaPorIdFechaAsistencia(int _idFechaAsistencia)
        {
            List<EntidadAsistencia> _lista = new List<EntidadAsistencia>();
            foreach (var item in _entitiesPosgrado.Sp_AsistenciaConsultar().Where(c=>c.Id_Fecha_Asistencia== _idFechaAsistencia).ToList())
            {
                _lista.Add(new EntidadAsistencia()
                {
                    IdAsistencia = item.Id_Asistencia,
                    Eliminado = item.EliminadoAsistencia,
                    AsistenciaTipo = new EntidadAsistenciaTipo()
                    {
                        IdAsistenciaTipo = item.Id_TipoAsistencia,
                        Identificador = item.IdentificadorAsistenciaTipo,
                        Descripcion = item.DescripcionAsistenciaTipo,
                        Eliminado = item.EliminadoAsistenciaTipo
                    },
                    FechaAsistencia = new EntidadFechaAsistencia()
                    {
                        IdFechaAsistencia = item.Id_Fecha_Asistencia
                    },
                    Matricula = new EntidadMatricula()
                    {

                        IdMatricula = item.Id_Matricula,
                        FechaRegistro = item.FechaRegistroMatricula,
                        MatriculaVigente = item.Matricula_Vigente,
                        Preseleccionado = new EntidadPreseleccionado()
                        {
                            IdPreseleccionado = item.Id_preseleccionado,
                            Inscripcion = new EntidadInscripcion()
                            {
                                IdInscripcion = item.IdInscripcion,
                                Persona = new EntidadPersona()
                                {
                                    IdPersona = item.IdPersona,
                                    Nombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.Nombres + " " + item.SegundoNombre,
                                    NumeroIdentificacion = item.Cedula

                                }
                            }
                        },
                        TipoMatricula = new EntidadTipoMatricula()
                        {
                            IdTipoMatricula = item.Id_TipoMatricula,
                            Descripcion = item.DescripcionTipoMatricula,
                            Identificador = item.IdentificadorTipoMatricula,
                            Eliminado = item.EliminadoTipoMatricula
                        },
                        ConfigurarCohorte = new EntidadConfigurarCohorte()
                        {
                            IdConfigurarCohorte = item.Id_ConfiguracionCohorte
                        }
                    }
                });
            }
            return _lista;
        }
    }
}