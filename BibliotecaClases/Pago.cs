using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Pago
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_pago { get; set; }
        public int valor { get; set; }
        public double descuento { get; set; }
        public int id_tipo { get; set; }
        public string rut_cliente { get; set; }


        public Pago()
        {

        }

    }
}
