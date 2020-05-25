using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Informe
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int num_formulario { get; set; }
        public string estado_servicio { get; set; }
        public string resultado { get; set; }
        public string observacion { get; set; }
        public DateTime fecha_insp { get; set; }
        public string hora_insp { get; set; }
        public int id_vivienda { get; set; }
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_termografia { get; set; }
        public int id_medicion { get; set; }
        public int id_inspeccion { get; set; }
        public int id_verificacion { get; set; }


        public Informe()
        {

        }


    }
}
