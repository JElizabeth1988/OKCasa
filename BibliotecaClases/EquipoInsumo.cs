using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class EquipoInsumo
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_equipo { get; set; }
        public int id_insumo { get; set; }


        public EquipoInsumo()
        {

        }
    }
}
