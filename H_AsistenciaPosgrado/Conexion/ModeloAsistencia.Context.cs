﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class i_posgradoEntities : DbContext
    {
        public i_posgradoEntities()
            : base("name=i_posgradoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<Nullable<decimal>> Sp_AsistenciaInsertar(Nullable<int> idMatricula, Nullable<int> idTipoAsistencia, Nullable<int> idFechaAsistencia, Nullable<bool> eliminado)
        {
            var idMatriculaParameter = idMatricula.HasValue ?
                new ObjectParameter("IdMatricula", idMatricula) :
                new ObjectParameter("IdMatricula", typeof(int));
    
            var idTipoAsistenciaParameter = idTipoAsistencia.HasValue ?
                new ObjectParameter("IdTipoAsistencia", idTipoAsistencia) :
                new ObjectParameter("IdTipoAsistencia", typeof(int));
    
            var idFechaAsistenciaParameter = idFechaAsistencia.HasValue ?
                new ObjectParameter("IdFechaAsistencia", idFechaAsistencia) :
                new ObjectParameter("IdFechaAsistencia", typeof(int));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_AsistenciaInsertar", idMatriculaParameter, idTipoAsistenciaParameter, idFechaAsistenciaParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_AsistenciaTipoConsultar_Result> Sp_AsistenciaTipoConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_AsistenciaTipoConsultar_Result>("Sp_AsistenciaTipoConsultar");
        }
    
        public virtual ObjectResult<Sp_CohorteConsultar_Result> Sp_CohorteConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_CohorteConsultar_Result>("Sp_CohorteConsultar");
        }
    
        public virtual ObjectResult<Sp_ConfigurarCohorteConsultar_Result> Sp_ConfigurarCohorteConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_ConfigurarCohorteConsultar_Result>("Sp_ConfigurarCohorteConsultar");
        }
    
        public virtual int Sp_ConfigurarCohorteEliminar(Nullable<int> idConfigurarCohorte)
        {
            var idConfigurarCohorteParameter = idConfigurarCohorte.HasValue ?
                new ObjectParameter("IdConfigurarCohorte", idConfigurarCohorte) :
                new ObjectParameter("IdConfigurarCohorte", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_ConfigurarCohorteEliminar", idConfigurarCohorteParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_ConfigurarCohorteInsertar(Nullable<int> idCohorte, Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin, Nullable<bool> eliminado)
        {
            var idCohorteParameter = idCohorte.HasValue ?
                new ObjectParameter("IdCohorte", idCohorte) :
                new ObjectParameter("IdCohorte", typeof(int));
    
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("FechaFin", fechaFin) :
                new ObjectParameter("FechaFin", typeof(System.DateTime));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_ConfigurarCohorteInsertar", idCohorteParameter, fechaInicioParameter, fechaFinParameter, eliminadoParameter);
        }
    
        public virtual int Sp_ConfigurarModuloDocenteEliminar(Nullable<int> idConfigurarModuloDocente)
        {
            var idConfigurarModuloDocenteParameter = idConfigurarModuloDocente.HasValue ?
                new ObjectParameter("IdConfigurarModuloDocente", idConfigurarModuloDocente) :
                new ObjectParameter("IdConfigurarModuloDocente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_ConfigurarModuloDocenteEliminar", idConfigurarModuloDocenteParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_ConfigurarModuloDocenteInsertar(Nullable<int> idModulo, Nullable<int> idDocente, Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin, Nullable<bool> eliminado)
        {
            var idModuloParameter = idModulo.HasValue ?
                new ObjectParameter("IdModulo", idModulo) :
                new ObjectParameter("IdModulo", typeof(int));
    
            var idDocenteParameter = idDocente.HasValue ?
                new ObjectParameter("IdDocente", idDocente) :
                new ObjectParameter("IdDocente", typeof(int));
    
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("FechaInicio", fechaInicio) :
                new ObjectParameter("FechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_ConfigurarModuloDocenteInsertar", idModuloParameter, idDocenteParameter, fechaInicioParameter, fechaFinParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_ConfigurarSemestreConsultar_Result> Sp_ConfigurarSemestreConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_ConfigurarSemestreConsultar_Result>("Sp_ConfigurarSemestreConsultar");
        }
    
        public virtual int Sp_ConfigurarSemestreEliminar(Nullable<int> idConfigurarSemestre)
        {
            var idConfigurarSemestreParameter = idConfigurarSemestre.HasValue ?
                new ObjectParameter("IdConfigurarSemestre", idConfigurarSemestre) :
                new ObjectParameter("IdConfigurarSemestre", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_ConfigurarSemestreEliminar", idConfigurarSemestreParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_ConfigurarSemestreInsertar(Nullable<int> idConfigurarCohorte, Nullable<int> idConfigurarModuloDocente, Nullable<int> idSemestre, Nullable<bool> eliminado)
        {
            var idConfigurarCohorteParameter = idConfigurarCohorte.HasValue ?
                new ObjectParameter("IdConfigurarCohorte", idConfigurarCohorte) :
                new ObjectParameter("IdConfigurarCohorte", typeof(int));
    
            var idConfigurarModuloDocenteParameter = idConfigurarModuloDocente.HasValue ?
                new ObjectParameter("IdConfigurarModuloDocente", idConfigurarModuloDocente) :
                new ObjectParameter("IdConfigurarModuloDocente", typeof(int));
    
            var idSemestreParameter = idSemestre.HasValue ?
                new ObjectParameter("IdSemestre", idSemestre) :
                new ObjectParameter("IdSemestre", typeof(int));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_ConfigurarSemestreInsertar", idConfigurarCohorteParameter, idConfigurarModuloDocenteParameter, idSemestreParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_DiaConsultar_Result> Sp_DiaConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_DiaConsultar_Result>("Sp_DiaConsultar");
        }
    
        public virtual ObjectResult<Sp_DocenteConsultar_Result> Sp_DocenteConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_DocenteConsultar_Result>("Sp_DocenteConsultar");
        }
    
        public virtual ObjectResult<Sp_FechaAsistenciaConsultar_Result> Sp_FechaAsistenciaConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_FechaAsistenciaConsultar_Result>("Sp_FechaAsistenciaConsultar");
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_FechaAsistenciaInsertar(Nullable<int> idHorario, Nullable<System.DateTime> fechaAsistencia, Nullable<bool> eliminado)
        {
            var idHorarioParameter = idHorario.HasValue ?
                new ObjectParameter("IdHorario", idHorario) :
                new ObjectParameter("IdHorario", typeof(int));
    
            var fechaAsistenciaParameter = fechaAsistencia.HasValue ?
                new ObjectParameter("FechaAsistencia", fechaAsistencia) :
                new ObjectParameter("FechaAsistencia", typeof(System.DateTime));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_FechaAsistenciaInsertar", idHorarioParameter, fechaAsistenciaParameter, eliminadoParameter);
        }
    
        public virtual int Sp_HorarioEliminar(Nullable<int> idHorario)
        {
            var idHorarioParameter = idHorario.HasValue ?
                new ObjectParameter("IdHorario", idHorario) :
                new ObjectParameter("IdHorario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_HorarioEliminar", idHorarioParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_HorarioInsertar(Nullable<int> idConfigurarSemestre, Nullable<int> idDia, Nullable<System.TimeSpan> horaEntrada, Nullable<System.TimeSpan> horaSalida, Nullable<bool> eliminado)
        {
            var idConfigurarSemestreParameter = idConfigurarSemestre.HasValue ?
                new ObjectParameter("IdConfigurarSemestre", idConfigurarSemestre) :
                new ObjectParameter("IdConfigurarSemestre", typeof(int));
    
            var idDiaParameter = idDia.HasValue ?
                new ObjectParameter("IdDia", idDia) :
                new ObjectParameter("IdDia", typeof(int));
    
            var horaEntradaParameter = horaEntrada.HasValue ?
                new ObjectParameter("HoraEntrada", horaEntrada) :
                new ObjectParameter("HoraEntrada", typeof(System.TimeSpan));
    
            var horaSalidaParameter = horaSalida.HasValue ?
                new ObjectParameter("HoraSalida", horaSalida) :
                new ObjectParameter("HoraSalida", typeof(System.TimeSpan));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_HorarioInsertar", idConfigurarSemestreParameter, idDiaParameter, horaEntradaParameter, horaSalidaParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_MaestriaConsultar_Result> Sp_MaestriaConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_MaestriaConsultar_Result>("Sp_MaestriaConsultar");
        }
    
        public virtual ObjectResult<Sp_MatriculaConsultar_Result> Sp_MatriculaConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_MatriculaConsultar_Result>("Sp_MatriculaConsultar");
        }
    
        public virtual int Sp_ModuloActualizar(Nullable<int> idModulo, string descripcion, Nullable<bool> eliminado)
        {
            var idModuloParameter = idModulo.HasValue ?
                new ObjectParameter("IdModulo", idModulo) :
                new ObjectParameter("IdModulo", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_ModuloActualizar", idModuloParameter, descripcionParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_ModuloConsultar_Result> Sp_ModuloConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_ModuloConsultar_Result>("Sp_ModuloConsultar");
        }
    
        public virtual ObjectResult<Sp_ModuloConsultarNoConfiguradosPorConfigurarCohortePorSemestre_Result> Sp_ModuloConsultarNoConfiguradosPorConfigurarCohortePorSemestre(Nullable<int> idConfigurarCohorte, Nullable<int> idSemestre)
        {
            var idConfigurarCohorteParameter = idConfigurarCohorte.HasValue ?
                new ObjectParameter("idConfigurarCohorte", idConfigurarCohorte) :
                new ObjectParameter("idConfigurarCohorte", typeof(int));
    
            var idSemestreParameter = idSemestre.HasValue ?
                new ObjectParameter("idSemestre", idSemestre) :
                new ObjectParameter("idSemestre", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_ModuloConsultarNoConfiguradosPorConfigurarCohortePorSemestre_Result>("Sp_ModuloConsultarNoConfiguradosPorConfigurarCohortePorSemestre", idConfigurarCohorteParameter, idSemestreParameter);
        }
    
        public virtual int Sp_ModuloEliminar(Nullable<int> idModulo)
        {
            var idModuloParameter = idModulo.HasValue ?
                new ObjectParameter("IdModulo", idModulo) :
                new ObjectParameter("IdModulo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_ModuloEliminar", idModuloParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_ModuloInsertar(string descripcion, Nullable<bool> eliminado)
        {
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_ModuloInsertar", descripcionParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_SemestreConsultar_Result> Sp_SemestreConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_SemestreConsultar_Result>("Sp_SemestreConsultar");
        }
    
        public virtual int Sp_SemestreEliminar(Nullable<int> idSemestre)
        {
            var idSemestreParameter = idSemestre.HasValue ?
                new ObjectParameter("IdSemestre", idSemestre) :
                new ObjectParameter("IdSemestre", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_SemestreEliminar", idSemestreParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> Sp_SemestreInsertar(string descripcion, Nullable<int> identificador, Nullable<bool> eliminado)
        {
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var identificadorParameter = identificador.HasValue ?
                new ObjectParameter("Identificador", identificador) :
                new ObjectParameter("Identificador", typeof(int));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("Sp_SemestreInsertar", descripcionParameter, identificadorParameter, eliminadoParameter);
        }
    
        public virtual int Sp_SemestreModificar(Nullable<int> idSemestre, string descripcion, Nullable<int> identificador, Nullable<bool> eliminado)
        {
            var idSemestreParameter = idSemestre.HasValue ?
                new ObjectParameter("IdSemestre", idSemestre) :
                new ObjectParameter("IdSemestre", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var identificadorParameter = identificador.HasValue ?
                new ObjectParameter("Identificador", identificador) :
                new ObjectParameter("Identificador", typeof(int));
    
            var eliminadoParameter = eliminado.HasValue ?
                new ObjectParameter("Eliminado", eliminado) :
                new ObjectParameter("Eliminado", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Sp_SemestreModificar", idSemestreParameter, descripcionParameter, identificadorParameter, eliminadoParameter);
        }
    
        public virtual ObjectResult<Sp_AsistenciaConsultar_Result> Sp_AsistenciaConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_AsistenciaConsultar_Result>("Sp_AsistenciaConsultar");
        }
    
        public virtual ObjectResult<Sp_HorarioConsultar_Result> Sp_HorarioConsultar()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_HorarioConsultar_Result>("Sp_HorarioConsultar");
        }
    }
}
