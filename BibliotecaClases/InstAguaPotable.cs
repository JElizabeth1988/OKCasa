using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InstAguaPotable
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_agua_potable { get; set; }
        public string nombre { get; set; }


        public InstAguaPotable()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.INST_AGUA_POTABLE agua =
                    bdd.INST_AGUA_POTABLE.First(t => t.ID_AGUA_POTABLE == id_agua_potable);
                nombre = agua.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<InstAguaPotable> ReadAll()
        {
            try
            {
                List<InstAguaPotable> lista = new List<InstAguaPotable>();
                var lista_agua_bdd = bdd.INST_AGUA_POTABLE.ToList();
                foreach (INST_AGUA_POTABLE item in lista_agua_bdd)
                {
                    InstAguaPotable agua = new InstAguaPotable();
                    agua.id_agua_potable = item.ID_AGUA_POTABLE;//number no los toma el int
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
