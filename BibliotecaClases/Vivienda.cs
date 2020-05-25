using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Vivienda
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int id_vivienda { get; set; }
        public string direccion { get; set; }
        public int num_habitaciones { get; set; }
        public int num_pisos { get; set; }
        public string constructora { get; set; }
        public int id_comuna { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        public string rut_cliente { get; set; }


        public Vivienda()
        {

        }
    }
}
