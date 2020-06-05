using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class EquipoTecnico
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_equipo { get; set; }
        public string nombre { get; set; }
        

        public EquipoTecnico()
        {

        }

        public bool Read()
        {
            try
            {
                BibliotecaDALC.EQUIPO_TECNICO equipo =
                    bdd.EQUIPO_TECNICO.First(t => t.ID_EQUIPO == id_equipo);
                nombre = equipo.NOMBRE;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EquipoTecnico> ReadAll()
        {
            try
            {
                List<EquipoTecnico> lista = new List<EquipoTecnico>();
                var lista_equi_bdd = bdd.EQUIPO_TECNICO.ToList();
                foreach (EQUIPO_TECNICO item in lista_equi_bdd)
                {
                    EquipoTecnico eq = new EquipoTecnico();
                    eq.id_equipo = item.ID_EQUIPO;//number no los toma el int
                    eq.nombre = item.NOMBRE;
                    lista.Add(eq);
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
