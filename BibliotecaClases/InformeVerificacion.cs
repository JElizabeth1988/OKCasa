using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InformeVerificacion
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

        public string alt_minima { get; set; }
        public string banio_ventana { get; set; }
        public string loc_habit { get; set; }
        public string aisl_techo { get; set; }
        public string muros_alb { get; set; }
        public string aisl_pisos { get; set; }
        public string tab_ext { get; set; }
        public string muros_c_fuego { get; set; }
        public string muros_ados { get; set; }
        public string muros_est { get; set; }
        public string muro_ext_alb { get; set; }
        public string muro_int_alb { get; set; }
        public string madera_impreg { get; set; }
        public string techo_1x4 { get; set; }
        public string tabique_2x4 { get; set; }
        public string electrica { get; set; }
        public string agua { get; set; }
        public string alcantarillado { get; set; }
        public string gas { get; set; }
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        public int id_solicitud { get; set; }


        public InformeVerificacion()
        {

        }

        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla
                INFORME_VERIFICACION inf = new INFORME_VERIFICACION();
                CommonBC.Syncronize(this, inf);
                bdd.INFORME_VERIFICACION.Add(inf);
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
                INFORME_VERIFICACION inf =
                bdd.INFORME_VERIFICACION.Find(num_formulario);

                bdd.INFORME_VERIFICACION.Remove(inf);
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
                INFORME_VERIFICACION info =
                bdd.INFORME_VERIFICACION.First(inf => inf.NUM_FORMULARIO.Equals(num_formulario));

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
                INFORME_VERIFICACION info = bdd.INFORME_VERIFICACION.Find(num_formulario);
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
                INFORME_VERIFICACION info = bdd.INFORME_VERIFICACION.Find(num_formulario);
                CommonBC.Syncronize(info, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<InformeVerificacion> ReadAll()
        {
            try
            {
                var c = from info in bdd.INFORME_VERIFICACION
                        select new InformeVerificacion()
                        {
                            num_formulario = info.NUM_FORMULARIO,
                            estado_servicio = info.ESTADO_SERVICIO,
                            resultado = info.RESULTADO,
                            num_habitaciones = info.NUM_HABITACIONES,
                            num_pisos = info.NUM_PISOS,
                            fecha_insp = info.FECHA_INSP,
                            observacion = info.OBSERVACION,
                            alt_minima = info.ALT_MINIMA,
                            banio_ventana = info.BANIO_VENTANA,
                            loc_habit = info.LOC_HABIT,
                            aisl_techo = info.AISL_TECHO,
                            muros_alb = info.MUROS_ALB,
                            aisl_pisos = info.AISL_PISOS,
                            tab_ext = info.TAB_EXT,
                            muros_c_fuego = info.MUROS_C_FUEGO,
                            muros_ados = info.MUROS_ADOS,
                            muros_est = info.MUROS_EST,
                            muro_ext_alb = info.MURO_EXT_ALB,
                            muro_int_alb = info.MURO_INT_ALB,
                            madera_impreg = info.MADERA_IMPREG,
                            techo_1x4 = info.TECHO_1X4,
                            tabique_2x4 = info.TABIQUE_2X4,
                            electrica = info.ELECTRICA,
                            agua = info.AGUA,
                            alcantarillado = info.ALCANTARILLADO,
                            gas = info.GAS,
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
                var c = from info in bdd.INFORME_VERIFICACION
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
                            AlturaMin = info.ALT_MINIMA,
                            BanioVentana = info.BANIO_VENTANA,
                            LocHabitables = info.LOC_HABIT,
                            AislTecho = info.AISL_TECHO,
                            MurosAlb = info.MUROS_ALB,
                            AislPisos = info.AISL_PISOS,
                            TabiqueExt = info.TAB_EXT,
                            MurosContraF = info.MUROS_C_FUEGO,
                            MurosAdosados = info.MUROS_ADOS,
                            MuroEst = info.MUROS_EST,
                            MuroExterior = info.MURO_EXT_ALB,
                            MuroInterior = info.MURO_INT_ALB,
                            MaderaImpregnada = info.MADERA_IMPREG,
                            Techo1x4 = info.TECHO_1X4,
                            Tabique2x4 = info.TABIQUE_2X4,
                            Electrica = info.ELECTRICA,
                            Agua = info.AGUA,
                            Alcantarillado = info.ALCANTARILLADO,
                            Gas = info.GAS,
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
            var cl = from info in bdd.INFORME_VERIFICACION
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
                         AlturaMin = info.ALT_MINIMA,
                         BanioVentana = info.BANIO_VENTANA,
                         LocHabitables = info.LOC_HABIT,
                         AislTecho = info.AISL_TECHO,
                         MurosAlb = info.MUROS_ALB,
                         AislPisos = info.AISL_PISOS,
                         TabiqueExt = info.TAB_EXT,
                         MurosContraF = info.MUROS_C_FUEGO,
                         MurosAdosados = info.MUROS_ADOS,
                         MuroEst = info.MUROS_EST,
                         MuroExterior = info.MURO_EXT_ALB,
                         MuroInterior = info.MURO_INT_ALB,
                         MaderaImpregnada = info.MADERA_IMPREG,
                         Techo1x4 = info.TECHO_1X4,
                         Tabique2x4 = info.TABIQUE_2X4,
                         Electrica = info.ELECTRICA,
                         Agua = info.AGUA,
                         Alcantarillado = info.ALCANTARILLADO,
                         Gas = info.GAS,
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
            public string AlturaMin { get; set; }
            public string BanioVentana { get; set; }
            public string LocHabitables { get; set; }
            public string AislTecho { get; set; }
            public string MurosAlb { get; set; }
            public string AislPisos { get; set; }
            public string TabiqueExt { get; set; }
            public string MurosContraF { get; set; }
            public string MurosAdosados { get; set; }
            public string MuroEst { get; set; }
            public string MuroExterior { get; set; }
            public string MuroInterior { get; set; }
            public string MaderaImpregnada { get; set; }
            public string Techo1x4 { get; set; }
            public string Tabique2x4 { get; set; }
            public string Electrica { get; set; }
            public string Agua { get; set; }
            public string Alcantarillado { get; set; }
            public string Gas { get; set; }
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
