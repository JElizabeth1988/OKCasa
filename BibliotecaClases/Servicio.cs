using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Servicio
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_servicio { get; set; }
        public string nombre { get; set; }
        public int valor { get; set; }


        public Servicio()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.SERVICIO servicio =
                    bdd.SERVICIO.First(t => t.ID_SERVICIO == id_servicio);
                nombre = servicio.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Servicio> ReadAll()
        {
            try
            {
                List<Servicio> lista = new List<Servicio>();
                var lista_serv_bdd = bdd.SERVICIO.ToList();
                foreach (SERVICIO item in lista_serv_bdd)
                {
                    Servicio serv = new Servicio();
                    serv.id_servicio = item.ID_SERVICIO;//number no los toma el int
                    serv.nombre = item.NOMBRE;
                    serv.valor = item.VALOR;
                    lista.Add(serv);
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
