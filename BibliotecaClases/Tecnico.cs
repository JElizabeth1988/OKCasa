using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;

namespace BibliotecaNegocio
{
    public class Tecnico
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();
        //Capturar Errores
        DaoErrores err = new DaoErrores();
        public DaoErrores retornar() { return err; }

        private string _rut_tecnico;

        public string rut_tecnico
        {
            get { return _rut_tecnico; }
            set {
                    if (value != string.Empty && value.Length >= 9 && value.Length <= 10)
                    {
                        _rut_tecnico = value;
                    }
                    else
                    {
                        //throw new ArgumentException("Campo Rut no puede estar Vacío");
                        err.AgregarError("Campo Rut  no puede estar Vacío");
                    }
            }
        }

        private string _primer_nombre;

        public string primer_nombre
        {
            get { return _primer_nombre; }
            set
            {
                if (value != string.Empty)
                {
                    _primer_nombre = value;
                }
                else
                {
                    //throw new ArgumentException("Campo Rut no puede estar Vacío");
                    err.AgregarError("Campo Nombre no puede estar Vacío");
                }
            }
        }

        public string segundo_nombre { get; set; }

        private string _ap_paterno;

        public string ap_paterno
        {
            get { return _ap_paterno; }
            set
            {
                if (value != string.Empty)
                {
                    _ap_paterno = value;
                }
                else
                {
                    //throw new ArgumentException("Campo Rut no puede estar Vacío");
                    err.AgregarError("Campo Apellido Paterno no puede estar Vacío");
                }
            }
        }

        private string _ap_materno;

        public string ap_materno
        {
            get { return _ap_materno; }
            set
            {
                if (value != string.Empty)
                {
                    _ap_materno = value;
                }
                else
                {
                    //throw new ArgumentException("Campo Rut no puede estar Vacío");
                    err.AgregarError("Campo Apellido Materno no puede estar Vacío");
                }
            }
        }
        private string _direccion;

        public string direccion
        {
            get { return _direccion; }
            set
            {
                if (value != string.Empty)
                {
                    _direccion = value;
                }
                else
                {
                    //throw new ArgumentException("Campo Rut no puede estar Vacío");
                    err.AgregarError("Campo Direccion no puede estar Vacío");
                }
            }
        }
        public int telefono { get; set; }
        /* private int _telefono;

        public int telefono
        {
            get { return _telefono; }
            set
            {
                if (value != 0 && value > 90000000 && value < 10000000)
                {
                    _telefono = value;
                }
                else
                {
                    err.AgregarError("Campo Teléfono no puede estar Vacío y debe tener un largo de 9 dígitos");
                    //throw new ArgumentException("- Campo Teléfono no puede estar Vacío y debe tener un largo de 9 dígitos");
                }
            }
        }*/
        public string email { get; set; }
        public int id_equipo { get; set; }
        public int id_comuna { get; set; }


        public Tecnico()
        {

        }

        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla 
                TECNICO tec = new TECNICO();
                CommonBC.Syncronize(this, tec);
                bdd.TECNICO.Add(tec);
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
                TECNICO tec =
                bdd.TECNICO.Find(rut_tecnico);

                bdd.TECNICO.Remove(tec);
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
                TECNICO tec =
                bdd.TECNICO.First(cl => cl.RUT_TECNICO.Equals(rut_tecnico));

                CommonBC.Syncronize(tec, this);//arregló this

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
                TECNICO tec = bdd.TECNICO.Find(rut_tecnico);
                CommonBC.Syncronize(this, tec);
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
                TECNICO tec = bdd.TECNICO.Find(rut_tecnico);
                CommonBC.Syncronize(tec, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<Tecnico> ReadAll()
        {
            try
            {
                var c = from tec in bdd.TECNICO
                        select new Tecnico()
                        {
                            rut_tecnico = tec.RUT_TECNICO,
                            primer_nombre = tec.PRIMER_NOMBRE,
                            segundo_nombre = tec.SEGUNDO_NOMBRE,
                            ap_paterno = tec.AP_PATERNO,
                            ap_materno = tec.AP_MATERNO,
                            direccion = tec.DIRECCION,
                            telefono = tec.TELEFONO,
                            email = tec.EMAIL,
                            id_equipo = tec.ID_EQUIPO,
                            id_comuna = tec.ID_COMUNA

                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        //Read all 2 (se llena con método ListaTecnico que está abajo)
        public List<ListaTecnico> ReadAll2()
        {
            try
            {
                var c = from tec in bdd.TECNICO
                        join comu in bdd.COMUNA //Join con Comuna para traer el nombre de la comuna y no su id igualando el dato en común (id_comuna)
                          on tec.ID_COMUNA equals comu.ID_COMUNA
                        join equi in bdd.EQUIPO_TECNICO//Join con equipo
                            on tec.ID_EQUIPO equals equi.ID_EQUIPO
                        select new ListaTecnico()
                        {
                            Rut = tec.RUT_TECNICO,
                            Nombre = tec.PRIMER_NOMBRE,
                            Segundo_Nombre = tec.SEGUNDO_NOMBRE,
                            ApellidoPaterno = tec.AP_PATERNO,
                            ApellidoMaterno = tec.AP_MATERNO,
                            Dirección = tec.DIRECCION,
                            Teléfono = tec.TELEFONO,
                            Email = tec.EMAIL,
                            Equipo = equi.NOMBRE,//Traigo el nombre no el id
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
        public List<ListaTecnico> FiltroRut(string rut)
        {
            var cl = from tec in bdd.TECNICO
                     join comu in bdd.COMUNA
                     on tec.ID_COMUNA equals comu.ID_COMUNA
                     join equi in bdd.EQUIPO_TECNICO
                     on tec.ID_EQUIPO equals equi.ID_EQUIPO
                     where tec.RUT_TECNICO == rut
                     select new ListaTecnico()
                     {
                         Rut = tec.RUT_TECNICO,
                         Nombre = tec.PRIMER_NOMBRE,
                         Segundo_Nombre = tec.SEGUNDO_NOMBRE,
                         ApellidoPaterno = tec.AP_PATERNO,
                         ApellidoMaterno = tec.AP_MATERNO,
                         Dirección = tec.DIRECCION,
                         Teléfono = tec.TELEFONO,
                         Email = tec.EMAIL,
                         Equipo = equi.NOMBRE,//Traigo el nombre no el id
                         Comuna = comu.NOMBRE//Traigo el nombre no el id
                     };

            return cl.ToList();
        }

        //Filtro por equipo
        public List<ListaTecnico> FiltroEquipo(string equipo)
        {
            var eq = from tec in bdd.TECNICO
                     join comu in bdd.COMUNA
                          on tec.ID_COMUNA equals comu.ID_COMUNA
                     join equi in bdd.EQUIPO_TECNICO
                          on tec.ID_EQUIPO equals equi.ID_EQUIPO
                     where equi.NOMBRE == equipo
                     select new ListaTecnico()
                     {
                         Rut = tec.RUT_TECNICO,
                         Nombre = tec.PRIMER_NOMBRE,
                         Segundo_Nombre = tec.SEGUNDO_NOMBRE,
                         ApellidoPaterno = tec.AP_PATERNO,
                         ApellidoMaterno = tec.AP_MATERNO,
                         Dirección = tec.DIRECCION,
                         Teléfono = tec.TELEFONO,
                         Email = tec.EMAIL,
                         Equipo = equi.NOMBRE,//Traigo el nombre no el id
                         Comuna = comu.NOMBRE//Traigo el nombre no el id
                     };

            return eq.ToList();
        }


        //Lista para read all2
        public class ListaTecnico
        {
            public string Rut { get; set; }
            public string Nombre { get; set; }
            public string Segundo_Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Dirección { get; set; }
            public int Teléfono { get; set; }
            public string Email { get; set; }
            public string Equipo { get; set; }//Nombre no id
            public string Comuna { get; set; }//Nombre no id

            public ListaTecnico()
            {

            }
        }
    }
}
