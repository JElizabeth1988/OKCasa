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
        public DbSet<INFORME> INFORME { get; set; }
        public DbSet<INST_AGUA_POTABLE> INST_AGUA_POTABLE { get; set; }
        public DbSet<INST_ALCANTARILLADO> INST_ALCANTARILLADO { get; set; }
        public DbSet<INST_ELECTRICA> INST_ELECTRICA { get; set; }
        public DbSet<INST_GAS> INST_GAS { get; set; }
        public DbSet<INSUMO> INSUMO { get; set; }
        public DbSet<LOGIN> LOGIN { get; set; }
        public DbSet<PAGO> PAGO { get; set; }
        public DbSet<RED_AGUA> RED_AGUA { get; set; }
        public DbSet<SERVICIO> SERVICIO { get; set; }
        public DbSet<SOLICITUD> SOLICITUD { get; set; }
        public DbSet<TECNICO> TECNICO { get; set; }
        public DbSet<TIPO_PAGO> TIPO_PAGO { get; set; }
        public DbSet<TIPO_VIVIENDA> TIPO_VIVIENDA { get; set; }
    
        public virtual int SP_LISTAR_COMUNA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_LISTAR_COMUNA");
        }
    }
}
