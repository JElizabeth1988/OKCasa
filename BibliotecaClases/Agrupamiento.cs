using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaDALC;
namespace BibliotecaNegocio
{
    public class Agrupamiento
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_agrup { get; set; }
        public string nombre_agr { get; set; }

        public Agrupamiento()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.AGRUPAMIENTO agrup =
                    bdd.AGRUPAMIENTO.First(t => t.ID_AGRUP == id_agrup);
                nombre_agr = agrup.NOMBRE_AGR;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Agrupamiento> ReadAll()
        {
            try
            {
                List<Agrupamiento> lista = new List<Agrupamiento>();
                var lista_agru_bdd = bdd.AGRUPAMIENTO.ToList();
                foreach (AGRUPAMIENTO item in lista_agru_bdd)
                {
                    Agrupamiento agr = new Agrupamiento();
                    agr.id_agrup = item.ID_AGRUP;
                    agr.nombre_agr = item.NOMBRE_AGR;
                    lista.Add(agr);
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
