using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InstElectrica
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_electrica { get; set; }
        public string nombre { get; set; }


        public InstElectrica()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.INST_ELECTRICA electrica =
                    bdd.INST_ELECTRICA.First(t => t.ID_ELECTRICA == id_electrica);
                nombre = electrica.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<InstElectrica> ReadAll()
        {
            try
            {
                List<InstElectrica> lista = new List<InstElectrica>();
                var lista_ele_bdd = bdd.INST_ELECTRICA.ToList();
                foreach (INST_ELECTRICA item in lista_ele_bdd)
                {
                    InstElectrica electr = new InstElectrica();
                    electr.id_electrica = item.ID_ELECTRICA;//number no los toma el int
                    electr.nombre = item.NOMBRE;
                    lista.Add(electr);
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
