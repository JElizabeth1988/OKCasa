using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InstGas
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_gas { get; set; }
        public string nombre { get; set; }


        public InstGas()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.INST_GAS gas =
                    bdd.INST_GAS.First(t => t.ID_GAS == id_gas);
                nombre = gas.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<InstGas> ReadAll()
        {
            try
            {
                List<InstGas> lista = new List<InstGas>();
                var lista_gas_bdd = bdd.INST_GAS.ToList();
                foreach (INST_GAS item in lista_gas_bdd)
                {
                    InstGas gas = new InstGas();
                    gas.id_gas = item.ID_GAS;//number no los toma el int
                    gas.nombre = item.NOMBRE;
                    lista.Add(gas);
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
