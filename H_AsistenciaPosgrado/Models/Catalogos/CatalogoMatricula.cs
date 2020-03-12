using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoMatricula
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadMatricula> ConsultarMatricula()
        {
            List<EntidadMatricula> _lista = new List<EntidadMatricula>();
            foreach (var item in _entitiesPosgrado.Sp_MatriculaConsultar())
            {
                _lista.Add(new EntidadMatricula()
                {
                    IdMatricula=item.Id_Matricula,
                    FechaRegistro=item.FechaRegistroMatricula,
                    MatriculaVigente=item.Matricula_Vigente,
                    Preseleccionado=new EntidadPreseleccionado()
                    {
                        IdPreseleccionado=item.Id_preseleccionado,
                        Inscripcion=new EntidadInscripcion()
                        {
                            IdInscripcion=item.IdInscripcion,
                            Persona=new EntidadPersona()
                            {
                                IdPersona=item.IdPersona,
                                Nombres=item.ApellidoPaterno+" "+item.ApellidoMaterno+" "+item.Nombres+" "+item.SegundoNombre,
                                NumeroIdentificacion=item.Cedula
                                
                            }
                        }
                    },
                    TipoMatricula=new EntidadTipoMatricula()
                    {
                        IdTipoMatricula=item.Id_TipoMatricula,
                        Descripcion=item.DescripcionTipoMatricula,
                        Identificador=item.IdentificadorTipoMatricula,
                        Eliminado=item.EliminadoTipoMatricula
                    },
                    ConfigurarCohorte= new EntidadConfigurarCohorte()
                    {
                        IdConfigurarCohorte=item.Id_ConfiguracionCohorte
                    }
                });
            }
            return _lista;
        }

        public List<EntidadMatricula> ConsultarMatriculaPorIdPersona(int _idPersona)
        {
            List<EntidadMatricula> _lista = new List<EntidadMatricula>();
            foreach (var item in _entitiesPosgrado.Sp_MatriculaConsultarPorPersona(_idPersona))
            {
                _lista.Add(new EntidadMatricula()
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
                        IdConfigurarCohorte = item.Id_Configurar_Cohorte,
                        FechaInicio = item.Fecha_Inicio,
                        FechaFin = item.Fecha_Fin,
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
                    }
                });
            }
            return _lista;
        }

        public List<EntidadMatricula> ConsultarMatriculaPorIdConfigurarCohorte(int _idConfigurarCohorte)
        {
            List<EntidadMatricula> _lista = new List<EntidadMatricula>();
            foreach (var item in _entitiesPosgrado.Sp_MatriculaConsultar().Where(c=>c.Id_ConfiguracionCohorte==_idConfigurarCohorte))
            {
                _lista.Add(new EntidadMatricula()
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
                                NumeroIdentificacion = item.Cedula,
                                TelefonoC = item.TelefonoC,
                                TelefonoD=item.TelefonoD,
                                Email=item.Email,
                                EmailInstitucional=item.EmailInstitucional
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
                });
            }
            return _lista;
        }
    }
}