using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class MedicionM2
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int id_medicion { get; set; }
        public double area_total_real { get; set; }
        public double area_total_reg { get; set; }
        public double sup_util_real { get; set; }
        public double sup_util_reg { get; set; }
        public double sup_const_real { get; set; }
        public double sup_const_reg { get; set; }
        public double sup_ele_com_real { get; set; }
        public double sup_ele_com_reg { get; set; }
        public string observacion { get; set; }


        public MedicionM2()
        {

        }



    }
}
