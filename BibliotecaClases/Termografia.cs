using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Termografia
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int id_termografia { get; set; }
        public double emisividad { get; set; }
        public double temp_reflejada { get; set; }
        public double distancia { get; set; }
        public double hum_relativa { get; set; }
        public double temp_atmosferica { get; set; }
        public string observacion { get; set; }


        public Termografia()
        {
                
        }
    }
}
