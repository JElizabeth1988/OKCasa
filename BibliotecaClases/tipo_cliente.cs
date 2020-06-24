using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibliotecaDALC;

using Oracle.ManagedDataAccess.Client;

namespace BibliotecaNegocio
{
    public class tipo_cliente
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();
        public int id_tipo_cliente { get; set; }
        public string nombre { get; set; }

        public tipo_cliente()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.TIPO_CLIENTE tipo =
                    bdd.TIPO_CLIENTE.First(t => t.ID_TIPO_CLIENTE == id_tipo_cliente);
                nombre = tipo.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<tipo_cliente> ReadAll()
        {
            try
            {
                List<tipo_cliente> lista = new List<tipo_cliente>();
                var lista_tipo_bdd = bdd.TIPO_CLIENTE.ToList();
                foreach (TIPO_CLIENTE item in lista_tipo_bdd)
                {
                    tipo_cliente tip = new tipo_cliente();
                    tip.id_tipo_cliente = item.ID_TIPO_CLIENTE;
                    tip.nombre = item.NOMBRE;
                    lista.Add(tip);
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
