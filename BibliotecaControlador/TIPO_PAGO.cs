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
    
    public partial class TIPO_PAGO
    {
        public TIPO_PAGO()
        {
            this.PAGO = new HashSet<PAGO>();
        }
    
        public decimal ID_TIPO { get; set; }
        public string NOMBRE { get; set; }
        public decimal ID_BANCO { get; set; }
    
        public virtual ENTIDAD_BANCARIA ENTIDAD_BANCARIA { get; set; }
        public virtual ICollection<PAGO> PAGO { get; set; }
    }
}
