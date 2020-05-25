using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InspServicios
    {
        //Crear objeto de la Bdd
        private EntitiesOKCasa bdd = new EntitiesOKCasa();

        public int id_inspeccion { get; set; }
        public int id_articulo { get; set; }
        public int id_alcantarillado { get; set; }
        public int id_electrica { get; set; }
        public int id_agua_potable { get; set; }
        public int id_gas { get; set; }
        public int id_agua { get; set; }
        public string observacion { get; set; }


        public InspServicios()
        {

        }
    }
}
