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
    
    public partial class VIVIENDA
    {
        public VIVIENDA()
        {
            this.INFORME = new HashSet<INFORME>();
        }
    
        public decimal ID_VIVIENDA { get; set; }
        public string DIRECCIÓN { get; set; }
        public decimal NUM_HABITACIONES { get; set; }
        public decimal NUM_PISOS { get; set; }
        public string CONSTRUCTORA { get; set; }
        public decimal ID_COMUNA { get; set; }
        public decimal ID_TIPO { get; set; }
        public decimal ID_AGRUP { get; set; }
        public string RUT_CLIENTE { get; set; }
    
        public virtual AGRUPAMIENTO AGRUPAMIENTO { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual COMUNA COMUNA { get; set; }
        public virtual ICollection<INFORME> INFORME { get; set; }
        public virtual TIPO_VIVIENDA TIPO_VIVIENDA { get; set; }
    }
}
