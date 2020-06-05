using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class ArtSanitario
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_articulo { get; set; }
        public string nombre { get; set; }

        public ArtSanitario()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.ART_SANITARIO articulo =
                    bdd.ART_SANITARIO.First(t => t.ID_ARTICULO == id_articulo);
                nombre = articulo.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ArtSanitario> ReadAll()
        {
            try
            {
                List<ArtSanitario> lista = new List<ArtSanitario>();
                var lista_art_bdd = bdd.ART_SANITARIO.ToList();
                foreach (ART_SANITARIO item in lista_art_bdd)
                {
                    ArtSanitario art = new ArtSanitario();
                    art.id_articulo = item.ID_ARTICULO;//number no los toma el int
                    art.nombre = item.NOMBRE;
                    lista.Add(art);
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
