using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InformeTermografia
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        public int num_formulario { get; set; }
        public string estado_servicio { get; set; }
        public DateTime fecha_insp { get; set; }
        public string resultado { get; set; }
        public int num_habitaciones { get; set; }
        public int num_pisos { get; set; }
        public string observacion { get; set; }
        public int emisividad { get; set; }
        public int temp_reflejada { get; set; }
        public int distancia { get; set; }
        public int humedad_rel { get; set; }
        public int temp_atmosferica { get; set; }
        public int temp_externa { get; set; }
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        public int id_solicitud { get; set; }

        public InformeTermografia()
        {

        }

        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla
                INFORME_TERMOGRAFIA inf = new INFORME_TERMOGRAFIA();
                CommonBC.Syncronize(this, inf);
                bdd.INFORME_TERMOGRAFIA.Add(inf);
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
                INFORME_TERMOGRAFIA inf =
                bdd.INFORME_TERMOGRAFIA.Find(num_formulario);

                bdd.INFORME_TERMOGRAFIA.Remove(inf);
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
                INFORME_TERMOGRAFIA info =
                bdd.INFORME_TERMOGRAFIA.First(inf => inf.NUM_FORMULARIO.Equals(num_formulario));

                CommonBC.Syncronize(info, this);

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
                INFORME_TERMOGRAFIA info = bdd.INFORME_TERMOGRAFIA.Find(num_formulario);
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
                INFORME_TERMOGRAFIA info = bdd.INFORME_TERMOGRAFIA.Find(num_formulario);
                CommonBC.Syncronize(info, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<InformeTermografia> ReadAll()
        {
            try
            {
                var c = from info in bdd.INFORME_TERMOGRAFIA
                        select new InformeTermografia()
                        {
                            num_formulario = info.NUM_FORMULARIO,
                            estado_servicio = info.ESTADO_SERVICIO,
                            resultado = info.RESULTADO,
                            num_habitaciones = info.NUM_HABITACIONES,
                            num_pisos = info.NUM_PISOS,
                            fecha_insp = info.FECHA_INSP,
                            observacion = info.OBSERVACION,
                            emisividad = info.EMISIVIDAD,
                            temp_reflejada = info.TEMP_REFLEJADA,
                            distancia = info.DISTANCIA,
                            humedad_rel = info.HUMEDAD_REL,
                            temp_atmosferica = info.TEMP_ATMOSFERICA,
                            temp_externa = info.TEMP_EXTERNA,
                            rut_cliente = info.RUT_CLIENTE,
                            rut_tecnico = info.RUT_TECNICO,
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
        }
        //Read all 2 (se llena con método ListaCliente que está abajo)
        public List<ListaInforme> ReadAll2()
        {
            try
            {
                var c = from info in bdd.INFORME_TERMOGRAFIA
                        join tipo in bdd.TIPO_VIVIENDA
                          on info.ID_TIPO equals tipo.ID_TIPO
                        join agr in bdd.AGRUPAMIENTO
                          on info.ID_AGRUP equals agr.ID_AGRUP
                        join sol in bdd.SOLICITUD
                          on info.ID_SOLICITUD equals sol.ID_SOLICITUD
                        select new ListaInforme()
                        {
                            Numero = info.NUM_FORMULARIO,
                            Estado = info.ESTADO_SERVICIO,
                            Resultado = info.RESULTADO,
                            Habitaciones = info.NUM_HABITACIONES,
                            Pisos = info.NUM_PISOS,
                            Fecha = info.FECHA_INSP,
                            Observacion = info.OBSERVACION,
                            Emisividad = info.EMISIVIDAD,
                            TemperaturaReflejada = info.TEMP_REFLEJADA,
                            Distancia = info.DISTANCIA,
                            HumedadRelativa = info.HUMEDAD_REL,
                            TempAtmosferica = info.TEMP_ATMOSFERICA,
                            TempExterna = info.TEMP_EXTERNA,
                            RutCliente = info.RUT_CLIENTE,
                            RutTecnico = info.RUT_TECNICO,
                            TipoVivienda = tipo.NOMBRE_TIPO,
                            Agrupamiento = agr.NOMBRE_AGR,
                            Solicitud = sol.ID_SOLICITUD

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Filtro por Rut
        public List<ListaInforme> FiltroRut(string rut)
        {
            var cl = from info in bdd.INFORME_TERMOGRAFIA
                     join tipo in bdd.TIPO_VIVIENDA
                       on info.ID_TIPO equals tipo.ID_TIPO
                     join agr in bdd.AGRUPAMIENTO
                       on info.ID_AGRUP equals agr.ID_AGRUP
                     join sol in bdd.SOLICITUD
                       on info.ID_SOLICITUD equals sol.ID_SOLICITUD
                     where info.RUT_CLIENTE == rut
                     select new ListaInforme()
                     {
                         Numero = info.NUM_FORMULARIO,
                         Estado = info.ESTADO_SERVICIO,
                         Resultado = info.RESULTADO,
                         Habitaciones = info.NUM_HABITACIONES,
                         Pisos = info.NUM_PISOS,
                         Fecha = info.FECHA_INSP,
                         Observacion = info.OBSERVACION,
                         Emisividad = info.EMISIVIDAD,
                         TemperaturaReflejada = info.TEMP_REFLEJADA,
                         Distancia = info.DISTANCIA,
                         HumedadRelativa = info.HUMEDAD_REL,
                         TempAtmosferica = info.TEMP_ATMOSFERICA,
                         TempExterna = info.TEMP_EXTERNA,
                         RutCliente = info.RUT_CLIENTE,
                         RutTecnico = info.RUT_TECNICO,
                         TipoVivienda = tipo.NOMBRE_TIPO,
                         Agrupamiento = agr.NOMBRE_AGR,
                         Solicitud = sol.ID_SOLICITUD
                     };

            return cl.ToList();
        }


        //Lista para read all2 trae nombres no id
        public class ListaInforme
        {
            public int Numero { get; set; }
            public string Estado { get; set; }
            public DateTime Fecha { get; set; }
            public string Resultado { get; set; }
            public int Habitaciones { get; set; }
            public int Pisos { get; set; }
            public string Observacion { get; set; }
            public int Emisividad { get; set; }
            public int TemperaturaReflejada { get; set; }
            public int Distancia { get; set; }
            public int HumedadRelativa { get; set; }
            public int TempAtmosferica { get; set; }
            public int TempExterna { get; set; }

            public string RutCliente { get; set; }
            public string RutTecnico { get; set; }
            public string TipoVivienda { get; set; }
            public string Agrupamiento { get; set; }
            public int Solicitud { get; set; }

            public ListaInforme()
            {

            }
        }

    }
}
