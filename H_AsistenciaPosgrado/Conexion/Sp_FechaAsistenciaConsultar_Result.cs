//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace H_AsistenciaPosgrado.Conexion
{
    using System;
    
    public partial class Sp_FechaAsistenciaConsultar_Result
    {
        public int Id_Fecha_Asistencia { get; set; }
        public System.DateTime Fecha_Asistencia { get; set; }
        public bool Eliminado_FechaAsistencia { get; set; }
        public int Id_Horario { get; set; }
        public System.TimeSpan Hora_Entrada { get; set; }
        public System.TimeSpan Hora_Salida { get; set; }
        public bool Eliminado_Horario { get; set; }
        public string UtilizadoHorario { get; set; }
        public int Id_Dia { get; set; }
        public string Descripcion_Dia { get; set; }
        public int Identificador_Dia { get; set; }
        public bool Eliminado_Dia { get; set; }
        public int Id_Configurar_Semestre { get; set; }
        public System.DateTime Fecha_Registro_Configurar_Semestre { get; set; }
        public bool Eliminado_Configurar_Semestre { get; set; }
        public string Utilizado_Configurar_Semestre { get; set; }
        public int Id_Semestre { get; set; }
        public string Descripcion_Semestre { get; set; }
        public int Identificador_Semestre { get; set; }
        public bool Eliminado_Semestre { get; set; }
        public int Id_Configurar_Cohorte { get; set; }
        public System.DateTime Fecha_Inicio_Configurar_Cohorte { get; set; }
        public System.DateTime Fecha_Fin_Configurar_Cohorte { get; set; }
        public bool Eliminado_Configurar_Cohorte { get; set; }
        public int Id_Cohorte { get; set; }
        public string Detalle_Cohorte { get; set; }
        public string Estado_Cohorte { get; set; }
        public Nullable<System.DateTime> Fecha_Cohorte { get; set; }
        public Nullable<bool> Eliminado_Cohorte { get; set; }
        public int IdMaestria { get; set; }
        public string Estado_Maestria { get; set; }
        public string Nombre_Maestria { get; set; }
        public Nullable<bool> Eliminado_Maestria { get; set; }
        public int Id_Configurar_Modulo_Docente { get; set; }
        public System.DateTime Fecha_Inicio_Configurar_Modulo_Docente { get; set; }
        public System.DateTime Fecha_Fin_Configurar_Modulo_Docente { get; set; }
        public System.DateTime Fecha_Registro_Configurar_Modulo_Docente { get; set; }
        public bool Eliminado_Configurar_Modulo_Docente { get; set; }
        public string Utilizado_Configurar_Modulo_Docente { get; set; }
        public int Id_Modulo { get; set; }
        public string Descripcion_Modulo { get; set; }
        public bool Eliminado_Modulo { get; set; }
        public int Id_Docente { get; set; }
        public bool Eliminado_Docente { get; set; }
        public int Id_Persona { get; set; }
        public string Nombres_Persona { get; set; }
        public string Apellido_Paterno_Persona { get; set; }
        public string Apellido_Materno_Persona { get; set; }
        public string Cedula_Persona { get; set; }
    }
}
