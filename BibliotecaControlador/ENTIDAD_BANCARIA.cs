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
    
    public partial class ENTIDAD_BANCARIA
    {
        public ENTIDAD_BANCARIA()
        {
            this.TIPO_PAGO = new HashSet<TIPO_PAGO>();
        }
    
        public decimal ID_BANCO { get; set; }
        public string NOMBRE { get; set; }
    
        public virtual ICollection<TIPO_PAGO> TIPO_PAGO { get; set; }
    }
}
