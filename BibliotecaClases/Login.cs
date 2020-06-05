using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Login
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public string nombre_usuario { get; set; }
        public string password { get; set; }
        public string rut_cliente { get; set; }


        public Login()
        {

        }
    }
}
