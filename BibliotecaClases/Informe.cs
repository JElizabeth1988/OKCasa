using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Informe
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int num_formulario { get; set; }
        public string estado_servicio { get; set; }
        public string resultado { get; set; }
        public string observacion { get; set; }
        public DateTime fecha_insp { get; set; }
        public string hora_insp { get; set; }
        public int id_vivienda { get; set; }
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_termografia { get; set; }
        public int id_medicion { get; set; }
        public int id_inspeccion { get; set; }
        public int id_verificacion { get; set; }


        public Informe()
        {

        }

        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla
                INFORME inf = new INFORME();
                CommonBC.Syncronize(this, inf);
                bdd.INFORME.Add(inf);
                bdd.SaveChanges();

                return true;


            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        //Eliminar
        public bool Eliminar()
        {
            try
            {
                INFORME inf =
                bdd.INFORME.Find(num_formulario);

                bdd.INFORME.Remove(inf);
                bdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        //Buscar
        public bool Buscar()
        {
            try
            {
                INFORME info =
                bdd.INFORME.First(inf => inf.NUM_FORMULARIO.Equals(num_formulario));

                CommonBC.Syncronize(info, this);//arregló this

                return true;

            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);
            }
        }

        //Modificar
        public bool Modificar()
        {
            try
            {
                //creo un modelo de la tabla 
                INFORME info = bdd.INFORME.Find(num_formulario);
                CommonBC.Syncronize(this, info);
                bdd.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {

                return false;
            }
        }

        //Read
        public bool Read()
        {
            try
            {
                INFORME info = bdd.INFORME.Find(num_formulario);
                CommonBC.Syncronize(info, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
     /*   public List<Informe> ReadAll()
        {
            try
            {
                var c = from inf in bdd.INFORME
                        select new Informe()
                        {
                            num_formulario = inf.NUM_FORMULARIO,
                            estado_servicio = inf.ESTADO_SERVICIO,
                            resultado = inf.RESULTADO,
                            observacion = inf.OBSERVACION,
                            fecha_insp = inf.FECHA_INSP,
                            hora_insp = inf.HORA_INSP,
                            id_vivienda = inf.ID_VIVIENDA,
                            rut_cliente = inf.RUT_CLIENTE,
                            rut_tecnico = inf.RUT_TECNICO,
                            id_termografia = inf.ID_TERMOGRAFIA,
                            id_medicion = inf.ID_MEDICION,
                            id_inspeccion = inf.ID_INSPECCION,
                            id_verificacion = inf.ID_VERIFICACION

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        //Read all 2 (se llena con método ListaCliente que está abajo)
        public List<ListaInforme> ReadAll2()
        {
            try
            {
                var c = from inf in bdd.INFORME
                        join comu in bdd.COMUNA 
                          on cli.ID_COMUNA equals comu.ID_COMUNA
                        select new ListaClientes()
                        {
                            Rut = cli.RUT_CLIENTE,
                            PrimerNombre = cli.PRIMER_NOMBRE,
                            SegundoNombre = cli.SEGUNDO_NOMBRE,
                            ApPaterno = cli.AP_PATERNO,
                            ApMaterno = cli.AP_MATERNO,
                            Direccion = cli.DIRECCION,
                            Telefono = cli.TELEFONO,
                            Mail = cli.EMAIL,
                            Hipotecario = cli.HIPOTECARIO,
                            Comuna = comu.NOMBRE//Traigo el nombre no el id

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Filtro por Rut
        public List<ListaClientes> FiltroRut(string rut)
        {
            var cl = from cli in bdd.CLIENTE
                     join comu in bdd.COMUNA
                     on cli.ID_COMUNA equals comu.ID_COMUNA
                     where cli.RUT_CLIENTE == rut
                     select new ListaClientes()
                     {
                         Rut = cli.RUT_CLIENTE,
                         PrimerNombre = cli.PRIMER_NOMBRE,
                         SegundoNombre = cli.SEGUNDO_NOMBRE,
                         ApPaterno = cli.AP_PATERNO,
                         ApMaterno = cli.AP_MATERNO,
                         Direccion = cli.DIRECCION,
                         Telefono = cli.TELEFONO,
                         Mail = cli.EMAIL,
                         Hipotecario = cli.HIPOTECARIO,
                         Comuna = comu.NOMBRE//Traigo el nombre no el id
                     };

            return cl.ToList();
        }


        //Lista para read all2
        public class ListaInforme
        {
            public int Numero { get; set; }
            public string Estado { get; set; }
            public string Resultado { get; set; }
            public string Observacion { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string Vivienda { get; set; }
            public string RutCliente { get; set; }
            public string RutTecnico { get; set; }
            public int Termografia { get; set; }//Dudas!!!!!!
            public int Medicion { get; set; }//Dudas!!!!!!
            public int Inspeccion { get; set; }//Dudas!!!!!!
            public int Verificacion { get; set; }//Dudas!!!!!!

            public ListaInforme()
            {

            }
        }*/

    }
}
