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
    
    public partial class SOLICITUD
    {
        public SOLICITUD()
        {
            this.INFORME = new HashSet<INFORME>();
        }
    
        public int ID_SOLICITUD { get; set; }
        public Nullable<System.DateTime> FECHA_SOLICITUD { get; set; }
        public string DIRECCION_VIVIENDA { get; set; }
        public string CONSTRUCTORA { get; set; }
        public string RUT_CLIENTE { get; set; }
        public int PAGO { get; set; }
        public Nullable<int> DESCUENTO { get; set; }
        public string ESTADO { get; set; }
        public int ID_AGENDA { get; set; }
        public int ID_COMUNA { get; set; }
        public int ID_SERVICIO { get; set; }
        public string TIPO_PAGO { get; set; }
    
        public virtual AGENDA AGENDA { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual COMUNA COMUNA { get; set; }
        public virtual ICollection<INFORME> INFORME { get; set; }
        public virtual SERVICIO SERVICIO { get; set; }
    }
}
