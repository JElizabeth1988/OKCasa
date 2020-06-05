using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class TipoPago
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();
        public int id_tipo { get; set; }
        public string nombre { get; set; }


        public TipoPago()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.TIPO_PAGO tipo =
                    bdd.TIPO_PAGO.First(t => t.ID_TIPO == id_tipo);
                nombre = tipo.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TipoPago> ReadAll()
        {
            try
            {
                List<TipoPago> lista = new List<TipoPago>();
                var lista_tipo_bdd = bdd.TIPO_PAGO.ToList();
                foreach (TIPO_PAGO item in lista_tipo_bdd)
                {
                    TipoPago tipo = new TipoPago();
                    tipo.id_tipo = item.ID_TIPO;//number no los toma el int
                    tipo.nombre = item.NOMBRE;
                    lista.Add(tipo);
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
