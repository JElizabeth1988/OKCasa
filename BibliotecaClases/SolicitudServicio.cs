using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class SolicitudServicio
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_solicitud { get; set; }
        public int id_servicio { get; set; }


        public SolicitudServicio()
        {

        }
    }
}
