﻿using System;
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
        public List<Informe> ReadAll()
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
                            observ_servicios = info.OBSERV_SERVICIOS,
                            observ_terminacion = info.OBSERV_TERMINACION,
                            observ_termografia = info.OBSERV_TERMOGRAFIA,
                            habitabilidad = info.HABITABILIDAD,
                            termica = info.TERMICA,
                            resist_fuego = info.RESIST_FUEGO,
                            electrica = info.ELECTRICA,
                            agua = info.AGUA,
                            alcantarillado = info.ALCANTARILLADO,
                            gas = info.GAS,
                            emisividad = info.EMISIVIDAD ,//TT___TT
                            temp_reflejada = info.TEMP_REFLEJADA,//TT___TT
                            humedad = info.HUMEDAD,//TT___TT
                            rut_cliente = info.RUT_CLIENTE,
                            rut_tecnico = info.RUT_TECNICO,
                            id_agua_potable = info.ID_AGUA_POTABLE,
                            id_articulo = info.ID_ARTICULO,
                            id_gas = info.ID_GAS,
                            id_alcantarillado = info.ID_ALCANTARILLADO,
                            id_agua = info.ID_AGUA,
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
        }
        //Read all 2 (se llena con método ListaCliente que está abajo)
        public List<ListaInforme> ReadAll2()
        {
            try
            {
                var c = from inf in bdd.INFORME
                        join ap in bdd.INST_AGUA_POTABLE 
                          on inf.ID_AGUA_POTABLE equals ap.ID_AGUA_POTABLE
                        join art in bdd.ART_SANITARIO
                          on inf.ID_ARTICULO equals art.ID_ARTICULO
                        join gas in bdd.INST_GAS
                          on inf.ID_GAS equals gas.ID_GAS
                        join alc in bdd.INST_ALCANTARILLADO
                          on inf.ID_ALCANTARILLADO equals alc.ID_ALCANTARILLADO
                        join agu in bdd.RED_AGUA
                          on inf.ID_AGUA equals agu.ID_AGUA
                        join ele in bdd.INST_ELECTRICA
                          on inf.ID_ELECTRICA equals ele.ID_ELECTRICA
                        join tipo in bdd.TIPO_PAGO
                          on inf.ID_TIPO equals tipo.ID_TIPO
                        join agr in bdd.AGRUPAMIENTO
                          on inf.ID_AGRUP equals agr.ID_AGRUP
                        join sol in bdd.SOLICITUD
                          on inf.ID_SOLICITUD equals sol.ID_SOLICITUD
                        select new ListaInforme()
                        {
                            Numero = inf.NUM_FORMULARIO,
                            Estado = inf.ESTADO_SERVICIO,
                            Habitaciones = inf.NUM_HABITACIONES,
                            Pisos = inf.NUM_PISOS,
                            Resultado = inf.RESULTADO,
                            Fecha = inf.FECHA_INSP,
                            Hora = inf.HORA_INSP,
                            ObsFinal = inf.OBSERV_FINAL,
                            ObsMedicion = inf.OBSERV_MEDICION,
                            ObsServicio = inf.OBSERV_SERVICIOS,
                            ObsTerminaciones = inf.OBSERV_TERMINACION,
                            ObsTermografia = inf.OBSERV_TERMOGRAFIA,
                            Habitabilidad = inf.HABITABILIDAD,
                            Termica = inf.TERMICA,
                            Resistencia = inf.RESIST_FUEGO,
                            Electrica = inf.ELECTRICA,
                            Agua = inf.AGUA,
                            Alcantarillado = inf.ALCANTARILLADO,
                            Gas = inf.GAS,
                            Emisividad = inf.EMISIVIDAD,//TT___TT
                            Temperatura = inf.TEMP_REFLEJADA,//TT___TT
                            Humedad = inf.HUMEDAD,//TT___TT
                            RutCliente = inf.RUT_CLIENTE,
                            RutTecnico = inf.RUT_TECNICO,
                            AguaPotable = ap.NOMBRE,
                            Articulo = art.NOMBRE,
                            InstGas = gas.NOMBRE,
                            InstAlcantarillado = alc.NOMBRE,
                            InstAgua = agu.NOMBRE,
                            InstElectrica = ele.NOMBRE,
                            TipoVivienda = tipo.NOMBRE,
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
            var cl = from inf in bdd.INFORME
                     join ap in bdd.INST_AGUA_POTABLE 
                          on inf.ID_AGUA_POTABLE equals ap.ID_AGUA_POTABLE
                        join art in bdd.ART_SANITARIO
                          on inf.ID_ARTICULO equals art.ID_ARTICULO
                        join gas in bdd.INST_GAS
                          on inf.ID_GAS equals gas.ID_GAS
                        join alc in bdd.INST_ALCANTARILLADO
                          on inf.ID_ALCANTARILLADO equals alc.ID_ALCANTARILLADO
                        join agu in bdd.RED_AGUA
                          on inf.ID_AGUA equals agu.ID_AGUA
                        join ele in bdd.INST_ELECTRICA
                          on inf.ID_ELECTRICA equals ele.ID_ELECTRICA
                        join tipo in bdd.TIPO_PAGO
                          on inf.ID_TIPO equals tipo.ID_TIPO
                        join agr in bdd.AGRUPAMIENTO
                          on inf.ID_AGRUP equals agr.ID_AGRUP
                        join sol in bdd.SOLICITUD
                          on inf.ID_SOLICITUD equals sol.ID_SOLICITUD
                     where inf.RUT_CLIENTE == rut
                     select new ListaInforme()
                     {
                         Numero = inf.NUM_FORMULARIO,
                         Estado = inf.ESTADO_SERVICIO,
                         Habitaciones = inf.NUM_HABITACIONES,
                         Pisos = inf.NUM_PISOS,
                         Resultado = inf.RESULTADO,
                         Fecha = inf.FECHA_INSP,
                         Hora = inf.HORA_INSP,
                         ObsFinal = inf.OBSERV_FINAL,
                         ObsMedicion = inf.OBSERV_MEDICION,
                         ObsServicio = inf.OBSERV_SERVICIOS,
                         ObsTerminaciones = inf.OBSERV_TERMINACION,
                         ObsTermografia = inf.OBSERV_TERMOGRAFIA,
                         Habitabilidad = inf.HABITABILIDAD,
                         Termica = inf.TERMICA,
                         Resistencia = inf.RESIST_FUEGO,
                         Electrica = inf.ELECTRICA,
                         Agua = inf.AGUA,
                         Alcantarillado = inf.ALCANTARILLADO,
                         Gas = inf.GAS,
                         Emisividad = inf.EMISIVIDAD,//TT___TT
                         Temperatura = inf.TEMP_REFLEJADA,//TT___TT
                         Humedad = inf.HUMEDAD,//TT___TT
                         RutCliente = inf.RUT_CLIENTE,
                         RutTecnico = inf.RUT_TECNICO,
                         AguaPotable = ap.NOMBRE,
                         Articulo = art.NOMBRE,
                         InstGas = gas.NOMBRE,
                         InstAlcantarillado = alc.NOMBRE,
                         InstAgua = agu.NOMBRE,
                         InstElectrica = ele.NOMBRE,
                         TipoVivienda = tipo.NOMBRE,
                         Agrupamiento = agr.NOMBRE_AGR,
                         Solicitud = sol.ID_SOLICITUD
                     };

            return cl.ToList();
        }


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
