//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BibliotecaControlador
{
    using System;
    using System.Collections.Generic;
    
    public partial class AGENDA
    {
        public AGENDA()
        {
            this.EQUIPO_TECNICO = new HashSet<EQUIPO_TECNICO>();
            this.SOLICITUD = new HashSet<SOLICITUD>();
        }
    
        public decimal ID_AGENDA { get; set; }
        public System.DateTime DIA { get; set; }
        public string HORA { get; set; }
    
        public virtual ICollection<EQUIPO_TECNICO> EQUIPO_TECNICO { get; set; }
        public virtual ICollection<SOLICITUD> SOLICITUD { get; set; }
    }
}
