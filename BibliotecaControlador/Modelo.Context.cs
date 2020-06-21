﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BibliotecaDALC
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class OkCasa_Entities : DbContext
    {
        public OkCasa_Entities()
            : base("name=OkCasa_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AGENDA> AGENDA { get; set; }
        public DbSet<AGRUPAMIENTO> AGRUPAMIENTO { get; set; }
        public DbSet<ART_SANITARIO> ART_SANITARIO { get; set; }
        public DbSet<BANCO_ESTADO> BANCO_ESTADO { get; set; }
        public DbSet<CLIENTE> CLIENTE { get; set; }
        public DbSet<COMUNA> COMUNA { get; set; }
        public DbSet<ENTIDAD_BANCARIA> ENTIDAD_BANCARIA { get; set; }
        public DbSet<EQUIPO_TECNICO> EQUIPO_TECNICO { get; set; }
        public DbSet<INST_AGUA_POTABLE> INST_AGUA_POTABLE { get; set; }
        public DbSet<INST_ALCANTARILLADO> INST_ALCANTARILLADO { get; set; }
        public DbSet<INST_ELECTRICA> INST_ELECTRICA { get; set; }
        public DbSet<INST_GAS> INST_GAS { get; set; }
        public DbSet<INSUMO> INSUMO { get; set; }
        public DbSet<PAGO> PAGO { get; set; }
        public DbSet<RED_AGUA> RED_AGUA { get; set; }
        public DbSet<SERVICIO> SERVICIO { get; set; }
        public DbSet<SOLICITUD> SOLICITUD { get; set; }
        public DbSet<TECNICO> TECNICO { get; set; }
        public DbSet<TIPO_PAGO> TIPO_PAGO { get; set; }
        public DbSet<TIPO_VIVIENDA> TIPO_VIVIENDA { get; set; }
        public DbSet<INFORME_INSPECCION> INFORME_INSPECCION { get; set; }
        public DbSet<INFORME_MEDICION> INFORME_MEDICION { get; set; }
        public DbSet<INFORME_TERMOGRAFIA> INFORME_TERMOGRAFIA { get; set; }
        public DbSet<TIPO_USUARIO> TIPO_USUARIO { get; set; }
        public DbSet<USUARIO> USUARIO { get; set; }
        public DbSet<INFORME_VERIFICACION> INFORME_VERIFICACION { get; set; }
    
        public virtual int SP_LISTAR_COMUNA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_COMUNA");
        }
    
        public virtual int SP_AGREGAR_CLIENTE(string rUT_CLIENTE, string pRIMER_NOMBRE, string sEGUNDO_NOMBRE, string aP_PATERNO, string aP_MATERNO, string dIRECCION, Nullable<decimal> tELEFONO, string eMAIL, Nullable<decimal> iD_COMUNA)
        {
            var rUT_CLIENTEParameter = rUT_CLIENTE != null ?
                new ObjectParameter("RUT_CLIENTE", rUT_CLIENTE) :
                new ObjectParameter("RUT_CLIENTE", typeof(string));
    
            var pRIMER_NOMBREParameter = pRIMER_NOMBRE != null ?
                new ObjectParameter("PRIMER_NOMBRE", pRIMER_NOMBRE) :
                new ObjectParameter("PRIMER_NOMBRE", typeof(string));
    
            var sEGUNDO_NOMBREParameter = sEGUNDO_NOMBRE != null ?
                new ObjectParameter("SEGUNDO_NOMBRE", sEGUNDO_NOMBRE) :
                new ObjectParameter("SEGUNDO_NOMBRE", typeof(string));
    
            var aP_PATERNOParameter = aP_PATERNO != null ?
                new ObjectParameter("AP_PATERNO", aP_PATERNO) :
                new ObjectParameter("AP_PATERNO", typeof(string));
    
            var aP_MATERNOParameter = aP_MATERNO != null ?
                new ObjectParameter("AP_MATERNO", aP_MATERNO) :
                new ObjectParameter("AP_MATERNO", typeof(string));
    
            var dIRECCIONParameter = dIRECCION != null ?
                new ObjectParameter("DIRECCION", dIRECCION) :
                new ObjectParameter("DIRECCION", typeof(string));
    
            var tELEFONOParameter = tELEFONO.HasValue ?
                new ObjectParameter("TELEFONO", tELEFONO) :
                new ObjectParameter("TELEFONO", typeof(decimal));
    
            var eMAILParameter = eMAIL != null ?
                new ObjectParameter("EMAIL", eMAIL) :
                new ObjectParameter("EMAIL", typeof(string));
    
            var iD_COMUNAParameter = iD_COMUNA.HasValue ?
                new ObjectParameter("ID_COMUNA", iD_COMUNA) :
                new ObjectParameter("ID_COMUNA", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AGREGAR_CLIENTE", rUT_CLIENTEParameter, pRIMER_NOMBREParameter, sEGUNDO_NOMBREParameter, aP_PATERNOParameter, aP_MATERNOParameter, dIRECCIONParameter, tELEFONOParameter, eMAILParameter, iD_COMUNAParameter);
        }
    
        public virtual int SP_AGREGAR_SOLICITUD(Nullable<decimal> iD_SOLICITUD, Nullable<System.DateTime> fECHA_SOLICITUD, string hORA_SOLICITUD, string dIRECCION_VIVIENDA, string cONSTRUCTORA, string rUT_CLIENTE, Nullable<decimal> iD_AGENDA, Nullable<decimal> iD_PAGO, Nullable<decimal> iD_COMUNA, Nullable<decimal> iD_SERVICIO)
        {
            var iD_SOLICITUDParameter = iD_SOLICITUD.HasValue ?
                new ObjectParameter("ID_SOLICITUD", iD_SOLICITUD) :
                new ObjectParameter("ID_SOLICITUD", typeof(decimal));
    
            var fECHA_SOLICITUDParameter = fECHA_SOLICITUD.HasValue ?
                new ObjectParameter("FECHA_SOLICITUD", fECHA_SOLICITUD) :
                new ObjectParameter("FECHA_SOLICITUD", typeof(System.DateTime));
    
            var hORA_SOLICITUDParameter = hORA_SOLICITUD != null ?
                new ObjectParameter("HORA_SOLICITUD", hORA_SOLICITUD) :
                new ObjectParameter("HORA_SOLICITUD", typeof(string));
    
            var dIRECCION_VIVIENDAParameter = dIRECCION_VIVIENDA != null ?
                new ObjectParameter("DIRECCION_VIVIENDA", dIRECCION_VIVIENDA) :
                new ObjectParameter("DIRECCION_VIVIENDA", typeof(string));
    
            var cONSTRUCTORAParameter = cONSTRUCTORA != null ?
                new ObjectParameter("CONSTRUCTORA", cONSTRUCTORA) :
                new ObjectParameter("CONSTRUCTORA", typeof(string));
    
            var rUT_CLIENTEParameter = rUT_CLIENTE != null ?
                new ObjectParameter("RUT_CLIENTE", rUT_CLIENTE) :
                new ObjectParameter("RUT_CLIENTE", typeof(string));
    
            var iD_AGENDAParameter = iD_AGENDA.HasValue ?
                new ObjectParameter("ID_AGENDA", iD_AGENDA) :
                new ObjectParameter("ID_AGENDA", typeof(decimal));
    
            var iD_PAGOParameter = iD_PAGO.HasValue ?
                new ObjectParameter("ID_PAGO", iD_PAGO) :
                new ObjectParameter("ID_PAGO", typeof(decimal));
    
            var iD_COMUNAParameter = iD_COMUNA.HasValue ?
                new ObjectParameter("ID_COMUNA", iD_COMUNA) :
                new ObjectParameter("ID_COMUNA", typeof(decimal));
    
            var iD_SERVICIOParameter = iD_SERVICIO.HasValue ?
                new ObjectParameter("ID_SERVICIO", iD_SERVICIO) :
                new ObjectParameter("ID_SERVICIO", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AGREGAR_SOLICITUD", iD_SOLICITUDParameter, fECHA_SOLICITUDParameter, hORA_SOLICITUDParameter, dIRECCION_VIVIENDAParameter, cONSTRUCTORAParameter, rUT_CLIENTEParameter, iD_AGENDAParameter, iD_PAGOParameter, iD_COMUNAParameter, iD_SERVICIOParameter);
        }
    
        public virtual int SP_AGREGAR_USUARIO(Nullable<decimal> cODIGO, string nOMBRE_USUARIO, string cONTRASENIA, string rUT_CLIENTE, Nullable<decimal> iD_TIPO_USUARIO)
        {
            var cODIGOParameter = cODIGO.HasValue ?
                new ObjectParameter("CODIGO", cODIGO) :
                new ObjectParameter("CODIGO", typeof(decimal));
    
            var nOMBRE_USUARIOParameter = nOMBRE_USUARIO != null ?
                new ObjectParameter("NOMBRE_USUARIO", nOMBRE_USUARIO) :
                new ObjectParameter("NOMBRE_USUARIO", typeof(string));
    
            var cONTRASENIAParameter = cONTRASENIA != null ?
                new ObjectParameter("CONTRASENIA", cONTRASENIA) :
                new ObjectParameter("CONTRASENIA", typeof(string));
    
            var rUT_CLIENTEParameter = rUT_CLIENTE != null ?
                new ObjectParameter("RUT_CLIENTE", rUT_CLIENTE) :
                new ObjectParameter("RUT_CLIENTE", typeof(string));
    
            var iD_TIPO_USUARIOParameter = iD_TIPO_USUARIO.HasValue ?
                new ObjectParameter("ID_TIPO_USUARIO", iD_TIPO_USUARIO) :
                new ObjectParameter("ID_TIPO_USUARIO", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_AGREGAR_USUARIO", cODIGOParameter, nOMBRE_USUARIOParameter, cONTRASENIAParameter, rUT_CLIENTEParameter, iD_TIPO_USUARIOParameter);
        }
    
        public virtual int SP_LISTAR_CLIENTE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_CLIENTE");
        }
    
        public virtual int SP_LISTAR_CLIENTES()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_CLIENTES");
        }
    
        public virtual int SP_LISTAR_INFORME_INSPECCION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_INFORME_INSPECCION");
        }
    
        public virtual int SP_LISTAR_INFORME_MEDICION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_INFORME_MEDICION");
        }
    
        public virtual int SP_LISTAR_INFORME_TERMOGRAFIA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_INFORME_TERMOGRAFIA");
        }
    
        public virtual int SP_LISTAR_INFORME_VERIFICACION()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_INFORME_VERIFICACION");
        }
    
        public virtual int SP_LISTAR_INSUMOS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_INSUMOS");
        }
    
        public virtual int SP_LISTAR_SOLICITUDES()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_SOLICITUDES");
        }
    
        public virtual int SP_LISTAR_TECNICO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_TECNICO");
        }
    
        public virtual int SP_BANCOESTADO(string rUT)
        {
            var rUTParameter = rUT != null ?
                new ObjectParameter("RUT", rUT) :
                new ObjectParameter("RUT", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_BANCOESTADO", rUTParameter);
        }
    
        public virtual int SP_GUARDAR_INSUMO(Nullable<decimal> p_ID_INSUMO, string p_NOMBRE)
        {
            var p_ID_INSUMOParameter = p_ID_INSUMO.HasValue ?
                new ObjectParameter("P_ID_INSUMO", p_ID_INSUMO) :
                new ObjectParameter("P_ID_INSUMO", typeof(decimal));
    
            var p_NOMBREParameter = p_NOMBRE != null ?
                new ObjectParameter("P_NOMBRE", p_NOMBRE) :
                new ObjectParameter("P_NOMBRE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GUARDAR_INSUMO", p_ID_INSUMOParameter, p_NOMBREParameter);
        }
    }
}
