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
                            fecha_solicitud =DateTime.Parse( sol.FECHA_SOLICITUD.ToString()),                            
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

    }
}
