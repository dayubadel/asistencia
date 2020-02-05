using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoDocente
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadDocente> ConsultarDocente()
        {
            List<EntidadDocente> _lista = new List<EntidadDocente>();
            foreach (var item in _entitiesPosgrado.Sp_DocenteConsultar())
            {
                _lista.Add(new EntidadDocente()
                {
                    IdDocente=item.Id_Docente,
                    Eliminado=item.EliminadoDocente,
                    Persona=new EntidadPersona()
                    {
                        IdPersona=item.IdPersona,
                        Nombres=item.ApellidoPaterno+" "+item.ApellidoMaterno+" "+ item.Nombres+" "+item.SegundoNombre,
                        NumeroIdentificacion=item.Cedula
                    }
                });
            }
            return _lista;
        }
    }
}