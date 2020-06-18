using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class InformeMedicion
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

        public int area_regis { get; set; }
        public int area_real { get; set; }
        public int sup_util_regis { get; set; }
        public int sup_util_real { get; set; }
        public int sup_constr_regis { get; set; }
        public int sup_constr_real { get; set; }
        public int sup_elem_regis { get; set; }
        public int sup_elem_real { get; set; }
        
        public string rut_cliente { get; set; }
        public string rut_tecnico { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        public int id_solicitud { get; set; }

        public InformeMedicion()
        {

        }

        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla
                INFORME_MEDICION inf = new INFORME_MEDICION();
                CommonBC.Syncronize(this, inf);
                bdd.INFORME_MEDICION.Add(inf);
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
                INFORME_MEDICION inf =
                bdd.INFORME_MEDICION.Find(num_formulario);

                bdd.INFORME_MEDICION.Remove(inf);
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
                INFORME_MEDICION info =
                bdd.INFORME_MEDICION.First(inf => inf.NUM_FORMULARIO.Equals(num_formulario));

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
                INFORME_MEDICION info = bdd.INFORME_MEDICION.Find(num_formulario);
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
                INFORME_MEDICION info = bdd.INFORME_MEDICION.Find(num_formulario);
                CommonBC.Syncronize(info, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<InformeMedicion> ReadAll()
        {
            try
            {
                var c = from info in bdd.INFORME_MEDICION
                        select new InformeMedicion()
                        {
                            num_formulario = info.NUM_FORMULARIO,
                            estado_servicio = info.ESTADO_SERVICIO,
                            resultado = info.RESULTADO,
                            num_habitaciones = info.NUM_HABITACIONES,
                            num_pisos = info.NUM_PISOS,
                            fecha_insp = info.FECHA_INSP,
                            observacion = info.OBSERVACION,
                            area_regis = info.AREA_REGIS,
                            area_real = info.AREA_REAL,
                            sup_util_regis = info.SUP_UTIL_REGIS,
                            sup_util_real = info.SUP_UTIL_REAL,
                            sup_constr_regis = info.SUP_CONSTR_REGIS,
                            sup_constr_real = info.SUP_CONSTR_REAL,
                            sup_elem_regis = info.SUP_ELEM_REGIS,
                            sup_elem_real = int.Parse(info.SUP_ELEM_REAL.ToString()),
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
                var c = from info in bdd.INFORME_MEDICION
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
                            AreaRegistrada = info.AREA_REGIS,
                            AreaReal = info.AREA_REAL,
                            SuperfUtilRegistrada = info.SUP_UTIL_REGIS,
                            SuperfUtilReal = info.SUP_UTIL_REAL,
                            SupConstrRegistrada = info.SUP_CONSTR_REGIS,
                            SupConstrReal = info.SUP_CONSTR_REAL,
                            SupElemComunRegist = info.SUP_ELEM_REGIS,
                            SupElemComunReal = int.Parse(info.SUP_ELEM_REAL.ToString()),
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
            var cl = from info in bdd.INFORME_MEDICION
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
                         AreaRegistrada = info.AREA_REGIS,
                         AreaReal = info.AREA_REAL,
                         SuperfUtilRegistrada = info.SUP_UTIL_REGIS,
                         SuperfUtilReal = info.SUP_UTIL_REAL,
                         SupConstrRegistrada = info.SUP_CONSTR_REGIS,
                         SupConstrReal = info.SUP_CONSTR_REAL,
                         SupElemComunRegist = info.SUP_ELEM_REGIS,
                         SupElemComunReal = int.Parse(info.SUP_ELEM_REAL.ToString()),
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
            public int AreaRegistrada { get; set; }
            public int AreaReal { get; set; }
            public int SuperfUtilRegistrada { get; set; }
            public int SuperfUtilReal { get; set; }
            public int SupConstrRegistrada { get; set; }
            public int SupConstrReal { get; set; }
            public int SupElemComunRegist { get; set; }
            public int SupElemComunReal { get; set; }

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
