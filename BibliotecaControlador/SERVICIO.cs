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
    
    public partial class SERVICIO
    {
        public SERVICIO()
        {
            this.SOLICITUD = new HashSet<SOLICITUD>();
        }
    
        public int ID_SERVICIO { get; set; }
        public string NOMBRE { get; set; }
    
        public virtual ICollection<SOLICITUD> SOLICITUD { get; set; }
    }
}
