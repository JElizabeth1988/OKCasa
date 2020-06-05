using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class RedAgua
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_agua { get; set; }
        public string nombre { get; set; }


        public RedAgua()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.RED_AGUA agua =
                    bdd.RED_AGUA.First(t => t.ID_AGUA == id_agua);
                nombre = agua.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<RedAgua> ReadAll()
        {
            try
            {
                List<RedAgua> lista = new List<RedAgua>();
                var lista_agua_bdd = bdd.RED_AGUA.ToList();
                foreach (RED_AGUA item in lista_agua_bdd)
                {
                    RedAgua agua = new RedAgua();
                    agua.id_agua = item.ID_AGUA;//number no los toma el int
                    agua.nombre = item.NOMBRE;
                    lista.Add(agua);
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
