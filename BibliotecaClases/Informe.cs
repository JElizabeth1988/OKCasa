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
        public int num_habitaciones { get; set; }
        public int num_pisos { get; set; }
        public string resultado { get; set; }
        public DateTime fecha_insp { get; set; }
        public string hora_insp { get; set; }
        public string observ_final { get; set; }
        public string observ_medicion { get; set; }
        public string observ_servicios{ get; set; }
        public string observ_terminacion { get; set; }
        public string observ_termografia { get; set; }
        public string habitabilidad { get; set; }
        public string termica { get; set; }
        public string resist_fuego { get; set; }
        public string electrica { get; set; }
        public string agua { get; set; }
        public string alcantarillado { get; set; }
        public string gas { get; set; }
        public int emisividad { get; set; }
        public int temp_reflejada { get; set; }
        public int humedad { get; set; }
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_agua_potable { get; set; }
        public int id_articulo { get; set; }
        public int id_gas{ get; set; }
        public int id_alcantarillado { get; set; }
        public int id_agua { get; set; }
        public int id_electrica { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        public int id_solicitud { get; set; }


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
        /*public List<Informe> ReadAll()
        {
            try
            {
                var c = from info in bdd.INFORME
                        select new Informe()
                        {
                            num_formulario = info.NUM_FORMULARIO,
                            estado_servicio = info.ESTADO_SERVICIO,
                            num_habitaciones = info.NUM_HABITACIONES,
                            num_pisos = info.NUM_PISOS,
                            resultado = info.RESULTADO,
                            fecha_insp = info.FECHA_INSP,
                            hora_insp = info.HORA_INSP,
                            observ_final = info.OBSERV_FINAL,
                            observ_medicion = info.OBSERV_MEDICION,
                            observ_servicios = info.OBSERV_MEDICION,
                            observ_terminacion = info.OBSERV_TERMINACION,
                            observ_termografia = info.OBSERV_TERMOGRAFIA,
                            habitabilidad = info.HABITABILIDAD,
                            termica = info.TERMICA,
                            resist_fuego = info.RESIST_FUEGO,
                            electrica = info.ELECTRICA,
                            agua = info.AGUA,
                            alcantarillado = info.ALCANTARILLADO,
                            gas = info.GAS,
                            emisividad = info.EMISIVIDAD ,
                            temp_reflejada = info.TEMP_REFLEJADA,
                            humedad = info.HUMEDAD,
                            rut_cliente = info.RUT_CLIENTE,
                            rut_tecnico = info.RUT_TECNICO,
                            id_agua_potable = info.ID_AGUA_POTABLE,
                            id_articulo = info.ID_ARTICULO,
                            id_gas = info.ID_GAS,
                            id_electrica = info.ID_ELECTRICA,
                            id_tipo = info.ID_TIPO,
                            id_agrup = info.ID_AGRUP,
                            id_solicitud = info.ID_SOLICITUD


                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }*/
        //Read all 2 (se llena con método ListaCliente que está abajo)
        /*public List<ListaInforme> ReadAll2()
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
        }*/


        //Lista para read all2
        public class ListaInforme
        {
            public int Numero { get; set; }
            public string Estado { get; set; }
            public int Habitaciones { get; set; }
            public int Pisos { get; set; }
            public string Resultado { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string ObsFinal { get; set; }
            public string ObsMedicion { get; set; }
            public string ObsServicio { get; set; }
            public string ObsTerminaciones { get; set; }
            public string ObsTermografia { get; set; }
            public string Habitabilidad { get; set; }
            public string Termica { get; set; }
            public string Resistencia { get; set; }
            public string Electrica { get; set; }
            public string Agua { get; set; }
            public string Alcantarillado { get; set; }
            public string Gas { get; set; }
            public int Emisividad { get; set; }
            public int Temperatura { get; set; }
            public int Humedad { get; set; }
            public string RutCliente { get; set; }
            public string RutTecnico { get; set; }
            public string AguaPotable { get; set; }
            public string Articulo { get; set; }
            public string InstGas { get; set; }
            public string InstAlcantarillado { get; set; }
            public string InstAgua { get; set; }
            public string InstElectrica { get; set; }
            public string TipoVivienda { get; set; }
            public string Agrupamiento { get; set; }
            public int Solicitud { get; set; }

            public ListaInforme()
            {

            }
        }

    }
}
