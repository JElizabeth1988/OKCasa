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
    
    public partial class INFORME_INSPECCION
    {
        public int NUM_FORMULARIO { get; set; }
        public string ESTADO_SERVICIO { get; set; }
        public System.DateTime FECHA_INSP { get; set; }
        public string RESULTADO { get; set; }
        public int NUM_HABITACIONES { get; set; }
        public int NUM_PISOS { get; set; }
        public string OBSERVACION { get; set; }
        public int ID_ALCANTARILLADO { get; set; }
        public int ID_GAS { get; set; }
        public int ID_ELECTRICA { get; set; }
        public int ID_AGUA { get; set; }
        public int ID_AGUA_POTABLE { get; set; }
        public int ID_ARTICULO { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string RUT_TECNICO { get; set; }
        public int ID_TIPO { get; set; }
        public int ID_AGRUP { get; set; }
        public int ID_SOLICITUD { get; set; }
    
        public virtual AGRUPAMIENTO AGRUPAMIENTO { get; set; }
        public virtual ART_SANITARIO ART_SANITARIO { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual INST_AGUA_POTABLE INST_AGUA_POTABLE { get; set; }
        public virtual INST_ALCANTARILLADO INST_ALCANTARILLADO { get; set; }
        public virtual INST_ELECTRICA INST_ELECTRICA { get; set; }
        public virtual INST_GAS INST_GAS { get; set; }
        public virtual RED_AGUA RED_AGUA { get; set; }
        public virtual TECNICO TECNICO { get; set; }
        public virtual TIPO_VIVIENDA TIPO_VIVIENDA { get; set; }
        public virtual SOLICITUD SOLICITUD { get; set; }
    }
}
