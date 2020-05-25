using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Servicio
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int id_servicio { get; set; }
        public string nombre { get; set; }
        public int valor { get; set; }


        public Servicio()
        {

        }
    }
}
