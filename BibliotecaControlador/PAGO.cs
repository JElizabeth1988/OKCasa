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
    
    public partial class PAGO
    {
        public decimal ID_PAGO { get; set; }
        public decimal VALOR { get; set; }
        public decimal DESCUENTO { get; set; }
        public decimal ID_TIPO { get; set; }
        public string RUT_CLIENTE { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual TIPO_PAGO TIPO_PAGO { get; set; }
    }
}
