using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Catalogos
{
    public class CatalogoPersona
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public EntidadPersona ConsultarPersonaPorId(int _idPersona)
        {
            EntidadPersona _objPersona = new EntidadPersona();
            foreach (var item in _entitiesPosgrado.spPersonaConsultarXIDNSMIT(_idPersona))
            {
                _objPersona = new EntidadPersona()
                {
                    IdPersona=item.Id_Persona,
                    Nombres = item.ApellidoPaterno + " " + item.ApellidoMaterno + " " + item.Nombres + " " + item.SegundoNombre,
                    NumeroIdentificacion = item.Cedula,                   
                    TipoIdentificacion = new EntidadTipoIdentificacion()
                    {
                        IdTipoIdentificacion = Convert.ToInt32(item.tipoDocumentoId),
                        Etiqueta = item.descTipoDocumento
                    },
                    Sexo = new EntidadSexo()
                    {
                        IdSexo = Convert.ToInt32(item.sexoId),
                        Descripcion = item.descSexo
                    },
                    Eliminado=item.Eliminado
                };
            }
            return _objPersona;
        }
    }
}