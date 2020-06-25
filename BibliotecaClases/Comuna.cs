﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;

namespace BibliotecaNegocio
{
    public class Comuna
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_comuna { get; set; }
        public string nombre { get; set; }


        public Comuna()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.COMUNA com =
                    bdd.COMUNA.First(t => t.ID_COMUNA == id_comuna);
                nombre = com.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Comuna> ReadAll()
        {
           try
             {
                 List<Comuna> lista = new List<Comuna>();
                 var lista_comu_bdd = bdd.COMUNA.ToList();
                 foreach (COMUNA item in lista_comu_bdd)
                 {
                     Comuna comu = new Comuna();
                     comu.id_comuna = item.ID_COMUNA;
                     comu.nombre = item.NOMBRE;
                     lista.Add(comu);
                 }
                 return lista;

             }
             catch (Exception ex)
             {
                 return null;
             }
        }


    }
}
