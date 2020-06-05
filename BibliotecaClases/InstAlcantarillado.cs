using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InstAlcantarillado
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_alcantarillado { get; set; }
        public string nombre { get; set; }


        public InstAlcantarillado()
        {
                
        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.INST_ALCANTARILLADO alcantarillado =
                    bdd.INST_ALCANTARILLADO.First(t => t.ID_ALCANTARILLADO == id_alcantarillado);
                nombre = alcantarillado.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<InstAlcantarillado> ReadAll()
        {
            try
            {
                List<InstAlcantarillado> lista = new List<InstAlcantarillado>();
                var lista_al_bdd = bdd.INST_ALCANTARILLADO.ToList();
                foreach (INST_ALCANTARILLADO item in lista_al_bdd)
                {
                    InstAlcantarillado alc = new InstAlcantarillado();
                    alc.id_alcantarillado = item.ID_ALCANTARILLADO;//number no los toma el int
                    alc.nombre = item.NOMBRE;
                    lista.Add(alc);
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
