﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class solicitud
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_solicitud { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public string hora_solicitud { get; set; }
        public string direccion_vivienda { get; set; }
        public string constructora { get; set; }
        public string rut_cliente { get; set; }
        public int id_agenda { get; set; }
        public int id_pago { get; set; }


        public solicitud()
        {

        }

    }
}
