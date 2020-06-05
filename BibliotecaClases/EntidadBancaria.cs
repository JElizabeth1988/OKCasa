using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class EntidadBancaria
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_banco { get; set; }
        public string nombre { get; set; }


        public EntidadBancaria()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.ENTIDAD_BANCARIA entidad =
                    bdd.ENTIDAD_BANCARIA.First(t => t.ID_BANCO == id_banco);
                nombre = entidad.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EntidadBancaria> ReadAll()
        {
            try
            {
                List<EntidadBancaria> lista = new List<EntidadBancaria>();
                var lista_ent_bdd = bdd.ENTIDAD_BANCARIA.ToList();
                foreach (ENTIDAD_BANCARIA item in lista_ent_bdd)
                {
                    EntidadBancaria enti = new EntidadBancaria();
                    enti.id_banco = item.ID_BANCO;//number no los toma el int
                    enti.nombre = item.NOMBRE;
                    lista.Add(enti);
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
