//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class EQUIPO_TECNICO
    {
        public EQUIPO_TECNICO()
        {
            this.AGENDA = new HashSet<AGENDA>();
            this.TECNICO = new HashSet<TECNICO>();
            this.INSUMO = new HashSet<INSUMO>();
        }
    
        public decimal ID_EQUIPO { get; set; }
        public string NOMBRE { get; set; }
    
        public virtual ICollection<AGENDA> AGENDA { get; set; }
        public virtual ICollection<TECNICO> TECNICO { get; set; }
        public virtual ICollection<INSUMO> INSUMO { get; set; }
    }
}
