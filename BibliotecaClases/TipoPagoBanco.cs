using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class TipoPagoBanco
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_tipo { get; set; }
        public int id_banco { get; set; }


        public TipoPagoBanco()
        {

        }
    }

}