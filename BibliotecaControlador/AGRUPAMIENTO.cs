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
    
    public partial class AGRUPAMIENTO
    {
        public AGRUPAMIENTO()
        {
            this.INFORME_MEDICION = new HashSet<INFORME_MEDICION>();
            this.INFORME_TERMOGRAFIA = new HashSet<INFORME_TERMOGRAFIA>();
            this.INFORME_INSPECCION = new HashSet<INFORME_INSPECCION>();
            this.INFORME_VERIFICACION = new HashSet<INFORME_VERIFICACION>();
        }
    
        public int ID_AGRUP { get; set; }
        public string NOMBRE_AGR { get; set; }
    
        public virtual ICollection<INFORME_MEDICION> INFORME_MEDICION { get; set; }
        public virtual ICollection<INFORME_TERMOGRAFIA> INFORME_TERMOGRAFIA { get; set; }
        public virtual ICollection<INFORME_INSPECCION> INFORME_INSPECCION { get; set; }
        public virtual ICollection<INFORME_VERIFICACION> INFORME_VERIFICACION { get; set; }
    }
}
