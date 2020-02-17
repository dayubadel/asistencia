using H_AsistenciaPosgrado.Conexion;
using H_AsistenciaPosgrado.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H_AsistenciaPosgrado.Models.Catalogos
{

    public class CatalogoLogin
    {
        i_posgradoEntities _entitiesPosgrado = new i_posgradoEntities();
        public List<EntidadUsuarioTipo> Login(string _usuario, string _clave)
        {
            List<EntidadUsuarioTipo> _lista = new List<EntidadUsuarioTipo>();
            foreach (var item in _entitiesPosgrado.spValidarIngresoNSMIED(_usuario, _clave))
            {
                _lista.Add(new EntidadUsuarioTipo()
                {
                    idTipoUsuario = item.idTipoUsuario,
                    Usuario = new EntidadUsuario()
                    {
                        IdUsuario = item.Id_Usuario,
                        Usuario = item.Usuario,
                        Clave = item.Clave,
                        Persona = new EntidadPersona()
                        {
                            IdPersona = Convert.ToInt32(item.Id_Persona)
                        },
                        Foto = item.Foto,
                    },
                    Tipo = new EntidadTipo()
                    {
                        idTipo = item.idTipo,
                        roll = item.Roll
                    }
                });
            }
            return _lista;

        }
    }
}