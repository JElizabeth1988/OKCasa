using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Insumo
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        DaoErrores err = new DaoErrores();
        public DaoErrores retornar() { return err; }

        public int id_insumo { get; set; }
        public string nombre { get; set; }
        public string estado { get; set; }
        public int id_equipo { get; set; }


        public Insumo()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.INSUMO insumo =
                    bdd.INSUMO.First(t => t.ID_INSUMO == id_insumo);
                nombre = insumo.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /* public List<Insumo> ReadAll()
         {
             try
             {
                 List<Insumo> lista = new List<Insumo>();
                 var lista_in_bdd = bdd.INSUMO.ToList();
                 foreach (INSUMO item in lista_in_bdd)
                 {
                     Insumo insu = new Insumo();
                     insu.id_insumo = item.ID_INSUMO;//number no los toma el int
                     insu.nombre = item.NOMBRE;
                     lista.Add(insu);
                 }
                 return lista;

             }
             catch (Exception ex)
             {
                 return null;
             }
         }*/
        public class ListaInsumos
        {
            public int id { get; set; }
            public string Nombre { get; set; }
            

            public ListaInsumos()
            {

            }
        }
        public class ListaInsumos2
        {
            public int id_insumo { get; set; }
            public string Nombre { get; set; }
            public string Equipo { get; set; }


            public ListaInsumos2()
            {

            }
        }
    }
}
