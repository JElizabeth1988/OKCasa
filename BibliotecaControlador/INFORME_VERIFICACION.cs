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
    
    public partial class INFORME_VERIFICACION
    {
        public int NUM_FORMULARIO { get; set; }
        public string ESTADO_SERVICIO { get; set; }
        public System.DateTime FECHA_INSP { get; set; }
        public string RESULTADO { get; set; }
        public int NUM_HABITACIONES { get; set; }
        public int NUM_PISOS { get; set; }
        public string OBSERVACION { get; set; }
        public string ALT_MINIMA { get; set; }
        public string BANIO_VENTANA { get; set; }
        public string LOC_HABIT { get; set; }
        public string AISL_TECHO { get; set; }
        public string MUROS_ALB { get; set; }
        public string AISL_PISOS { get; set; }
        public string TAB_EXT { get; set; }
        public string MUROS_C_FUEGO { get; set; }
        public string MUROS_ADOS { get; set; }
        public string MUROS_EST { get; set; }
        public string MURO_EXT_ALB { get; set; }
        public string MURO_INT_ALB { get; set; }
        public string MADERA_IMPREG { get; set; }
        public string TECHO_1X4 { get; set; }
        public string TABIQUE_2X4 { get; set; }
        public string ELECTRICA { get; set; }
        public string AGUA { get; set; }
        public string ALCANTARILLADO { get; set; }
        public string GAS { get; set; }
        public string RUT_CLIENTE { get; set; }
        public string RUT_TECNICO { get; set; }
        public int ID_TIPO { get; set; }
        public int ID_AGRUP { get; set; }
        public int ID_SOLICITUD { get; set; }
    
        public virtual AGRUPAMIENTO AGRUPAMIENTO { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual SOLICITUD SOLICITUD { get; set; }
        public virtual TECNICO TECNICO { get; set; }
        public virtual TIPO_VIVIENDA TIPO_VIVIENDA { get; set; }
    }
}
