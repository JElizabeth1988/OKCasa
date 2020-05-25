using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Agenda
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        //Creacion de los atributos
        public int id_agenda { get; set; }
        public DateTime dia { get; set; }
        public string hora { get; set; }
        public int id_equipo { get; set; }


        public Agenda()
        {

        }

    }

}
