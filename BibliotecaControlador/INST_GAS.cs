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
    
    public partial class INST_GAS
    {
        public INST_GAS()
        {
            this.INSP_SERVICIOS = new HashSet<INSP_SERVICIOS>();
        }
    
        public decimal ID_GAS { get; set; }
        public string NOMBRE { get; set; }
    
        public virtual ICollection<INSP_SERVICIOS> INSP_SERVICIOS { get; set; }
    }
}
