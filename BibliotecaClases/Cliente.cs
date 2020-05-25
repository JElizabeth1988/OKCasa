using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;



namespace BibliotecaNegocio
{
    public class Cliente
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public string rut_cliente { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }
        public string hipotecario { get; set; }
        public int id_comuna { get; set; }


        public Cliente()
        {

        }
    }
}
