using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoConfigurarSemestre
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public int InsertarConfigurarSemestre(EntidadConfigurarSemestre _objConfigurarSemestre)
        {
            try
            {
                return int.Parse(_entitiesPosgrado.Sp_ConfigurarSemestreInsertar(_objConfigurarSemestre.ConfigurarCohorte.IdConfigurarCohorte,_objConfigurarSemestre.ConfigurarModuloDocente.IdConfigurarModuloDocente,_objConfigurarSemestre.Semestre.IdSemestre,_objConfigurarSemestre.Eliminado).Select(x=>x.Value.ToString()).FirstOrDefault());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<EntidadConfigurarSemestre> ConsultarConfigurarSemestre()
        {
            List<EntidadConfigurarSemestre> _lista = new List<EntidadConfigurarSemestre>();
            foreach (var item in _entitiesPosgrado.Sp_ConfigurarSemestreConsultar())
            {
                _lista.Add(new EntidadConfigurarSemestre()
                {
                    IdConfigurarSemestre=item.Id_Configurar_Semestre,
                    Eliminado=item.Eliminado_Configurar_Semestre,
                    Utilizado=item.Utilizado_Configurar_Semestre,
                    Semestre=new EntidadSemestre()
                    {
                        IdSemestre=item.Id_Semestre,
                        Descripcion=item.Descripcion_Semestre,
                        Identificador=item.Identificador_Semestre,
                        Eliminado=item.Eliminado_Semestre
                    },
                    ConfigurarCohorte=new EntidadConfigurarCohorte()
                    {
                        IdConfigurarCohorte=item.Id_Configurar_Cohorte,
                        Eliminado=item.Eliminado_Configurar_Cohorte,
                        Cohorte=new EntidadCohorte()
                        {
                            IdCohorte=item.Id_Cohorte,
                            Fecha=Convert.ToDateTime(item.Fecha_Cohorte),
                            Eliminado = Convert.ToBoolean(item.Eliminado_Cohorte),
                            Detalle=item.Detalle_Cohorte
                        }
                    },
                    ConfigurarModuloDocente=new EntidadConfigurarModuloDocente()
                    {
                        IdConfigurarModuloDocente=item.Id_Configurar_Modulo_Docente,
                        Eliminado=item.Eliminado_Configurar_Modulo_Docente,
                        FechaInicio=item.Fecha_Inicio_Configurar_Modulo_Docente,
                        FechaFin=item.Fecha_Fin_Configurar_Modulo_Docente,
                        Utilizado=item.Utilizado_Configurar_Modulo_Docente,
                        Docente=new EntidadDocente()
                        {
                            IdDocente=item.Id_Docente,
                            Eliminado=item.Eliminado_Docente,
                            Persona = new EntidadPersona()
                            {
                                IdPersona = item.Id_Persona,
                                Nombres = item.Apellido_Paterno_Persona + " " + item.Apellido_Materno_Persona + " " + item.Nombres_Persona,
                                NumeroIdentificacion = item.Cedula_Persona
                            }
                        },
                        Modulo=new EntidadModulo()
                        {
                            IdModulo=item.Id_Modulo,
                            Descripcion=item.Descripcion_Modulo,
                            Eliminado=item.Eliminado_Modulo
                        }
                    }
                });
            }
            return _lista;
        }

        public void EliminarConfigurarSemestre(int _idConfigurarSemestre)
        {
            _entitiesPosgrado.Sp_ConfigurarSemestreEliminar(_idConfigurarSemestre);
        }

        public List<EntidadConfigurarSemestre> ConsultarConfigurarSemestrePorId(int _idConfigurarSemestre)
        {
            List<EntidadConfigurarSemestre> _lista = new List<EntidadConfigurarSemestre>();
            foreach (var item in _entitiesPosgrado.Sp_ConfigurarSemestreConsultar().Where(c=>c.Id_Configurar_Semestre== _idConfigurarSemestre).ToList())
            {
                _lista.Add(new EntidadConfigurarSemestre()
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
                        Cohorte = new EntidadCohorte()
                        {
                            IdCohorte = item.Id_Cohorte,
                            Fecha = Convert.ToDateTime(item.Fecha_Cohorte),
                            Eliminado = Convert.ToBoolean(item.Eliminado_Cohorte),
                            Detalle = item.Detalle_Cohorte
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
                });
            }
            return _lista;
        }

    }
}