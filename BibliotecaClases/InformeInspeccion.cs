using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InformeInspeccion
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
        public int id_alcantarillado { get; set; }
        public int id_gas { get; set; }
        public int id_electrica { get; set; }
        public int id_agua { get; set; }
        public int id_agua_potable { get; set; }
        public int id_articulo { get; set; }
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        public int id_solicitud { get; set; }

        public InformeInspeccion()
        {

        }

        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla
                INFORME_INSPECCION inf = new INFORME_INSPECCION();
                CommonBC.Syncronize(this, inf);
                bdd.INFORME_INSPECCION.Add(inf);
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
                INFORME_INSPECCION inf =
                bdd.INFORME_INSPECCION.Find(num_formulario);

                bdd.INFORME_INSPECCION.Remove(inf);
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
                INFORME_INSPECCION info =
                bdd.INFORME_INSPECCION.First(inf => inf.NUM_FORMULARIO.Equals(num_formulario));

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
                INFORME_INSPECCION info = bdd.INFORME_INSPECCION.Find(num_formulario);
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
                INFORME_INSPECCION info = bdd.INFORME_INSPECCION.Find(num_formulario);
                CommonBC.Syncronize(info, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<InformeInspeccion> ReadAll()
        {
            try
            {
                var c = from info in bdd.INFORME_INSPECCION
                        select new InformeInspeccion()
                        {
                            num_formulario = info.NUM_FORMULARIO,
                            estado_servicio = info.ESTADO_SERVICIO,
                            resultado = info.RESULTADO,
                            num_habitaciones = info.NUM_HABITACIONES,
                            num_pisos = info.NUM_PISOS,
                            fecha_insp = info.FECHA_INSP,
                            observacion = info.OBSERVACION,
                            id_alcantarillado = info.ID_ALCANTARILLADO,
                            id_gas = info.ID_GAS,
                            id_electrica = info.ID_ELECTRICA,
                            id_agua = info.ID_AGUA,
                            id_agua_potable = info.ID_AGUA_POTABLE,
                            id_articulo = info.ID_ARTICULO,
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
                var c = from info in bdd.INFORME_INSPECCION
                        join alc in bdd.INST_ALCANTARILLADO
                          on info.ID_ALCANTARILLADO equals alc.ID_ALCANTARILLADO
                        join gas in bdd.INST_GAS
                          on info.ID_GAS equals gas.ID_GAS
                        join ele in bdd.INST_ELECTRICA
                          on info.ID_ELECTRICA equals ele.ID_ELECTRICA
                        join ag in bdd.RED_AGUA
                          on info.ID_AGUA equals ag.ID_AGUA
                        join agp in bdd.INST_AGUA_POTABLE
                          on info.ID_AGUA_POTABLE equals agp.ID_AGUA_POTABLE
                        join art in bdd.ART_SANITARIO
                          on info.ID_ARTICULO equals art.ID_ARTICULO
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
                            Alcantarillado = alc.NOMBRE,
                            Gas = gas.NOMBRE,
                            Electrica = ele.NOMBRE,
                            Agua = ag.NOMBRE,
                            AguaPotable = agp.NOMBRE,
                            Articulo = art.NOMBRE,
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
            var cl = from info in bdd.INFORME_INSPECCION
                     join alc in bdd.INST_ALCANTARILLADO
                          on info.ID_ALCANTARILLADO equals alc.ID_ALCANTARILLADO
                     join gas in bdd.INST_GAS
                       on info.ID_GAS equals gas.ID_GAS
                     join ele in bdd.INST_ELECTRICA
                       on info.ID_ELECTRICA equals ele.ID_ELECTRICA
                     join ag in bdd.RED_AGUA
                       on info.ID_AGUA equals ag.ID_AGUA
                     join agp in bdd.INST_AGUA_POTABLE
                       on info.ID_AGUA_POTABLE equals agp.ID_AGUA_POTABLE
                     join art in bdd.ART_SANITARIO
                       on info.ID_ARTICULO equals art.ID_ARTICULO
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
                         Alcantarillado = alc.NOMBRE,
                         Gas = gas.NOMBRE,
                         Electrica = ele.NOMBRE,
                         Agua = ag.NOMBRE,
                         AguaPotable = agp.NOMBRE,
                         Articulo = art.NOMBRE,
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
            public string Alcantarillado { get; set; }
            public string Gas { get; set; }
            public string Electrica { get; set; }
            public string Agua { get; set; }
            public string AguaPotable { get; set; }
            public string Articulo { get; set; }

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
