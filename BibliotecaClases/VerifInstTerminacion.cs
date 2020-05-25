using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class VerifInstTerminacion
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int id_verificacion { get; set; }
        public string altura_min { get; set; }
        public string ventana_banio_coc { get; set; }
        public string ventana_loc_hab { get; set; }
        public string aisl_techo_60mm { get; set; }
        public string muros_alba_bloque { get; set; }
        public string aisl_pisos_50mm { get; set; }
        public string tab_ext_30mm { get; set; }
        public string sup_vent_muro { get; set; }
        public string muros_contrafuego { get; set; }
        public string muros_adosam { get; set; }
        public string muros_estruct { get; set; }
        public string techo_cielo_falso { get; set; }
        public string muros_alb_ext { get; set; }
        public string muros_alb_ext_1 { get; set; }
        public string madera_est_1x4 { get; set; }
        public string pilar_ha_15x15 { get; set; }
        public string madera_impreg { get; set; }
        public string mad_tabiq_2x4 { get; set; }
        public string entremado_piso_2x6 { get; set; }
        public string inst_electrica { get; set; }
        public string inst_agua { get; set; }
        public string inst_alcantarillado { get; set; }
        public string int_gas { get; set; }
        public string otro { get; set; }
        public string observaciones { get; set; }


        public VerifInstTerminacion()
        {

        }
    }
}
