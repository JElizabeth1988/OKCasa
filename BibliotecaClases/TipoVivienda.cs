using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class TipoVivienda
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_tipo { get; set; }
        public string nombre_tipo { get; set; }

        public TipoVivienda()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.TIPO_VIVIENDA tipo =
                    bdd.TIPO_VIVIENDA.First(t => t.ID_TIPO == id_tipo);
                nombre_tipo = tipo.NOMBRE_TIPO;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TipoVivienda> ReadAll()
        {
            try
            {
                List<TipoVivienda> lista = new List<TipoVivienda>();
                var lista_tipo_bdd = bdd.TIPO_VIVIENDA.ToList();
                foreach (TIPO_VIVIENDA item in lista_tipo_bdd)
                {
                    TipoVivienda tipo = new TipoVivienda();
                    tipo.id_tipo = item.ID_TIPO;//number no los toma el int
                    tipo.nombre_tipo = item.NOMBRE_TIPO;
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
