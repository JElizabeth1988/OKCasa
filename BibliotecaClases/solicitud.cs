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
        public int id_agenda { get; set; }
        public int id_pago { get; set; }
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
                var c = from cli in bdd.SOLICITUD
                        select new Solicitud()
                        {
                            id_solicitud = cli.ID_SOLICITUD,
                            fecha_solicitud = cli.FECHA_SOLICITUD,
                            
                            direccion_vivienda = cli.DIRECCION_VIVIENDA,
                            constructora = cli.CONSTRUCTORA,
                            id_agenda = cli.ID_AGENDA,
                            id_pago = cli.ID_PAGO,
                            id_comuna = cli.ID_COMUNA,
                            id_servicio = cli.ID_SERVICIO

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
