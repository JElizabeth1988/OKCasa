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
    
    public partial class ART_SANITARIO
    {
        public ART_SANITARIO()
        {
            this.INSP_SERVICIOS = new HashSet<INSP_SERVICIOS>();
        }
    
        public decimal ID_ARTICULO { get; set; }
        public string NOMBRE { get; set; }
    
        public virtual ICollection<INSP_SERVICIOS> INSP_SERVICIOS { get; set; }
    }
}
