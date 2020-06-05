﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Tecnico
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public string rut_tecnico { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string direccion { get; set; }
        public int telefono { get; set; }
        public string email { get; set; }
        public int id_equipo { get; set; }
        public int id_comuna { get; set; }


        public Tecnico()
        {

        }

    }
}
