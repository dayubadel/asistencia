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
    }
}