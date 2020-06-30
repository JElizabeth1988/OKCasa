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
        DaoErrores err = new DaoErrores();
        public DaoErrores retornar() { return err; }

        public long num_formulario { get; set; }
        public string estado_servicio { get; set; }
        public DateTime fecha_insp { get; set; }
        public string resultado { get; set; }
        public int num_habitaciones { get; set; }
        public int num_pisos { get; set; }
        public string direccion_vivienda { get; set; }
        public string constructora { get; set; }
        public string observacion { get; set; }
        public string habitabilidad { get; set; }
        public string termica { get; set; }
        public string fuego { get; set; }
        public int area_regis { get; set; }
        public int area_real { get; set; }
        public int sup_constr_regis { get; set; }
        public int sup_constr_real { get; set; }
        public int emisividad { get; set; }
        public int temp_reflejada { get; set; }
        public int distancia { get; set; }
        public int humedad_rel { get; set; }
        public int temp_atmosferica { get; set; }

        //public string rut_cliente { get; set; }
        private string _rut_cliente;

        public string rut_cliente
        {
            get { return _rut_cliente; }
            set {
                    if (value != string.Empty && value.Length >= 9 && value.Length <= 10)
                    {
                        _rut_cliente = value;
                    }
                    else
                    {
                        //throw new ArgumentException("Campo Rut no puede estar Vacío");
                        err.AgregarError("Campo Rut  no puede estar Vacío");
                    }
            }
        }

        public string rut_tecnico { get; set; }
        public int id_tipo { get; set; }
        public int id_agrup { get; set; }
        //public int id_solicitud { get; set; }
        private int _id_solicitud;

        public int id_solicitud
        {
            get { return _id_solicitud; }
            set
            {
                if (value != 0)
                {
                    _id_solicitud = value;
                }
                else
                {
                    //throw new ArgumentException("Campo Rut no puede estar Vacío");
                    err.AgregarError("Debe Buscar la Solicitud de Servicio");
                }
            }
        }



        public int id_alcantarillado { get; set; }
        public int id_gas { get; set; }
        public int id_electrica { get; set; }
        public int id_agua { get; set; }
        public int id_agua_potable { get; set; }
        public int id_comuna { get; set; }

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
                            resultado = info.RESULTADO,
                            num_habitaciones = info.NUM_HABITACIONES,
                            num_pisos = info.NUM_PISOS,
                            direccion_vivienda = info.DIRECCION_VIVIENDA,
                            constructora = info.CONSTRUCTORA,
                            fecha_insp = info.FECHA_INSP,
                            observacion = info.OBSERVACION,
                            habitabilidad = info.HABITABILIDAD,
                            termica = info.TERMICA,
                            fuego = info.FUEGO,
                            area_regis = int.Parse(info.AREA_REGIS.ToString()),
                            area_real = int.Parse(info.AREA_REAL.ToString()),
                            sup_constr_regis = int.Parse(info.SUP_CONSTR_REGIS.ToString()),
                            sup_constr_real = int.Parse(info.SUP_CONSTR_REAL.ToString()),
                            emisividad = int.Parse(info.EMISIVIDAD.ToString()),
                            temp_reflejada= int.Parse(info.TEMP_REFLEJADA.ToString()),
                            distancia = int.Parse(info.DISTANCIA.ToString()),
                            humedad_rel= int.Parse(info.HUMEDAD_REL.ToString()),
                            temp_atmosferica= int.Parse(info.TEMP_ATMOSFERICA.ToString()),
                            rut_cliente = info.RUT_CLIENTE,
                            rut_tecnico = info.RUT_TECNICO,
                            id_tipo = info.ID_TIPO,
                            id_agrup = info.ID_AGRUP,
                            id_solicitud = info.ID_SOLICITUD,
                            id_alcantarillado = info.ID_ALCANTARILLADO,
                            id_gas = info.ID_GAS,
                            id_electrica = info.ID_ELECTRICA,
                            id_agua = info.ID_AGUA,
                            id_agua_potable = info.ID_AGUA_POTABLE,
                            id_comuna = info.ID_COMUNA

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        //Read all 2 (se llena con método ListaCliente que está abajo)
      /*  public List<ListaInforme> ReadAll2()
        {
            try
            {
                var c = from info in bdd.INFORME
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
                        join tipo in bdd.TIPO_VIVIENDA
                          on info.ID_TIPO equals tipo.ID_TIPO
                        join agr in bdd.AGRUPAMIENTO
                          on info.ID_AGRUP equals agr.ID_AGRUP
                        join sol in bdd.SOLICITUD
                          on info.ID_SOLICITUD equals sol.ID_SOLICITUD
                        join co in bdd.COMUNA
                            on info.ID_COMUNA equals co.ID_COMUNA
                        select new ListaInforme()
                        {
                            Numero = info.NUM_FORMULARIO,
                            Estado = info.ESTADO_SERVICIO,
                            Resultado = info.RESULTADO,
                            Habitaciones = info.NUM_HABITACIONES,
                            Pisos = info.NUM_PISOS,
                            Dirección = info.DIRECCION_VIVIENDA,
                            Constructora = info.CONSTRUCTORA,
                            Fecha = info.FECHA_INSP,
                            Observacion = info.OBSERVACION,
                            habitabilidad = info.HABITABILIDAD,
                            termica = info.TERMICA,
                            fuego = info.FUEGO,
                            area_regis = int.Parse(info.AREA_REGIS.ToString()),
                            area_real = int.Parse(info.AREA_REAL.ToString()),
                            sup_constr_regis = int.Parse(info.SUP_CONSTR_REGIS.ToString()),
                            sup_constr_real = int.Parse(info.SUP_CONSTR_REAL.ToString()),
                            emisividad = int.Parse(info.EMISIVIDAD.ToString()),
                            temp_reflejada = int.Parse(info.TEMP_REFLEJADA.ToString()),
                            distancia = int.Parse(info.DISTANCIA.ToString()),
                            humedad_rel = int.Parse(info.HUMEDAD_REL.ToString()),
                            temp_atmosferica = int.Parse(info.TEMP_ATMOSFERICA.ToString()),
                            
                            RutCliente = info.RUT_CLIENTE,
                            RutTecnico = info.RUT_TECNICO,
                            TipoVivienda = tipo.NOMBRE_TIPO,
                            Agrupamiento = agr.NOMBRE_AGR,
                            Solicitud = sol.ID_SOLICITUD,
                            Alcantarillado = alc.NOMBRE,
                            Gas = gas.NOMBRE,
                            Electrica = ele.NOMBRE,
                            Agua = ag.NOMBRE,
                            AguaPotable = agp.NOMBRE,
                            Comuna = co.NOMBRE

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
            var cl = from info in bdd.INFORME
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
                     join tipo in bdd.TIPO_VIVIENDA
                       on info.ID_TIPO equals tipo.ID_TIPO
                     join agr in bdd.AGRUPAMIENTO
                       on info.ID_AGRUP equals agr.ID_AGRUP
                     join co in bdd.COMUNA
                       on info.ID_COMUNA equals co.ID_COMUNA
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
                         Dirección = info.DIRECCION_VIVIENDA,
                         Constructora = info.CONSTRUCTORA,
                         Fecha = info.FECHA_INSP,
                         Observacion = info.OBSERVACION,
                         habitabilidad = info.HABITABILIDAD,
                         termica = info.TERMICA,
                         fuego = info.FUEGO,
                         area_regis = int.Parse(info.AREA_REGIS.ToString()),
                         area_real = int.Parse(info.AREA_REAL.ToString()),
                         sup_constr_regis = int.Parse(info.SUP_CONSTR_REGIS.ToString()),
                         sup_constr_real = int.Parse(info.SUP_CONSTR_REAL.ToString()),
                         emisividad = int.Parse(info.EMISIVIDAD.ToString()),
                         temp_reflejada = int.Parse(info.TEMP_REFLEJADA.ToString()),
                         distancia = int.Parse(info.DISTANCIA.ToString()),
                         humedad_rel = int.Parse(info.HUMEDAD_REL.ToString()),
                         temp_atmosferica = int.Parse(info.TEMP_ATMOSFERICA.ToString()),
                         
                         RutCliente = info.RUT_CLIENTE,
                         RutTecnico = info.RUT_TECNICO,
                         TipoVivienda = tipo.NOMBRE_TIPO,
                         Agrupamiento = agr.NOMBRE_AGR,
                         Solicitud = sol.ID_SOLICITUD,
                         Alcantarillado = alc.NOMBRE,
                         Gas = gas.NOMBRE,
                         Electrica = ele.NOMBRE,
                         Agua = ag.NOMBRE,
                         AguaPotable = agp.NOMBRE,
                         Comuna = co.NOMBRE
                     };

            return cl.ToList();
        }*/


        //Lista para read all2 trae nombres no id
        public class ListaInforme
        {
            public long Numero { get; set; }
            public string Estado_Servicio { get; set; }
            public DateTime Fecha_Inspeccion { get; set; }
            public string Rut_Cliente { get; set; }
            public string Cliente { get; set; }
            public string Dirección { get; set; }
            public string Comuna { get; set; }
            public string Constructora { get; set; }
            public int N_Habitaciones { get; set; }
            public int N_Pisos { get; set; }
            public string Tipo_Vivienda { get; set; }
            public string Tipo_Agrupamiento { get; set; }
            public string Rut_Tecnico { get; set; }
            public string Técnico { get; set; }
            public string Equipo { get; set; }
            public string Resultado { get; set; }
            public string Observacion { get; set; }
            public string habitabilidad { get; set; }
            public string Resistencia_Térmica { get; set; }
            public string Resistencia_Fuego { get; set; }
            public int Area_regis { get; set; }
            public int Area_real { get; set; }
            public int sup_construida_reg { get; set; }
            public int Sup_construida_real { get; set; }
            public int Emisividad { get; set; }
            public int Temp_reflejada { get; set; }
            public int Distancia { get; set; }
            public int Humedad_relativa { get; set; }
            public int temp_atmosferica { get; set; }
            public string Inst_Agua_Potable { get; set; }
            public string Inst_Alcantarillado { get; set; }
            public string Inst_Gas { get; set; }
            public string Inst_Electrica { get; set; }
            public string Red_Agua { get; set; }
            public int Solicitud { get; set; }
            public DateTime Fecha_solicitud { get; set; }


            public ListaInforme()
            {

            }
        }
    }
}
