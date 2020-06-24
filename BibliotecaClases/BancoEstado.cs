using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class BancoEstado
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_banco { get; set; }
        public string rut { get; set; }
        public string nombre { get; set; }
        public string Descripción { get; set; }


        public BancoEstado()
        {

        }



    }
}
