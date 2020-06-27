using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Solicitud
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int id_solicitud { get; set; }
        public DateTime fecha_solicitud { get; set; }

        public string direccion_vivienda { get; set; }
        public string constructora { get; set; }
        public string rut_cliente { get; set; }

        public int pago { get; set; }
        public int descuento { get; set; }
        public int id_agenda { get; set; }
        public int id_comuna { get; set; }
        public int id_servicio { get; set; }


        public Solicitud()
        {

        }

        //CRUD

        public List<Solicitud> ReadAll()
        {
            try
            {
                var c = from sol in bdd.SOLICITUD
                        select new Solicitud()
                        {
                            id_solicitud = sol.ID_SOLICITUD,
                            fecha_solicitud = DateTime.Parse(sol.FECHA_SOLICITUD.ToString()),
                            direccion_vivienda = sol.DIRECCION_VIVIENDA,
                            constructora = sol.CONSTRUCTORA,
                            rut_cliente = sol.RUT_CLIENTE,
                            pago = sol.PAGO,
                            descuento = int.Parse(sol.DESCUENTO.ToString()),
                            id_agenda = sol.ID_AGENDA,
                            id_comuna = sol.ID_COMUNA,
                            id_servicio = sol.ID_SERVICIO

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //Read all 3 (se llena con método ListaCliente2 que está abajo)
        public List<ListaSolicitud> ReadAll2()
        {
            try
            {
                var c = from sol in bdd.SOLICITUD
                        join cli in bdd.CLIENTE //Join con Comuna para traer el nombre de la comuna y no su id igualando el dato en común (id_comuna)
                          on sol.RUT_CLIENTE equals cli.RUT_CLIENTE
                        select new ListaSolicitud()
                        {
                            Rut = sol.RUT_CLIENTE,
                            Nombre = cli.PRIMER_NOMBRE,
                            Direccion = sol.DIRECCION_VIVIENDA,
                            Constructora = sol.CONSTRUCTORA,
                            Fecha = DateTime.Parse(sol.FECHA_SOLICITUD.ToString()),
                            id_solicitud = sol.ID_SOLICITUD
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

      
        public class ListaSolicitud
        {
            public string Rut { get; set; }
            public string Nombre { get; set; }
            public DateTime Fecha { get; set; }
            public int id_solicitud { get; set; }
            public string Direccion { get; set; }
            public string Constructora { get; set; }



            public ListaSolicitud()
            {

            }

        }
    }
}
