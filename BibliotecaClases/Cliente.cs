using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaDALC;



namespace BibliotecaNegocio
{
    public class Cliente
    {
        //Crear objeto de la Bdd
        private OkCasa_Entities bdd = new OkCasa_Entities();

        //Capturar Errores
        DaoErrores err = new DaoErrores();
        public DaoErrores retornar() { return err; }

        private string _rut_cliente;

        public string rut_cliente
        {
            get { return _rut_cliente; }
            set
            {
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
        private string _primer_nombre;

        public string primer_nombre
        {
            get { return _primer_nombre; }
            set {
                    if (value != string.Empty )
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
            set {
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
            set {
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
            set {
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

        private int _telefono;

        public int telefono
        {
            get { return _telefono; }
            set {
                    if (value != 0 && value >90000000 && value <10000000)
                    {
                        _telefono = value;
                    }
                    else
                    {
                        err.AgregarError("Campo Teléfono no puede estar Vacío y debe tener un largo de 9 dígitos");
                        //throw new ArgumentException("- Campo Teléfono no puede estar Vacío y debe tener un largo de 9 dígitos");
                    }
            }
        }

        public string email { get; set; }

        
        public int id_comuna { get; set; }


        public Cliente()
        {

        }


        //CRUD
        //Guardar
        public Boolean Guardar()
        {
            try
            {
                //creo un modelo de la tabla cliente
                CLIENTE cli = new CLIENTE();
                CommonBC.Syncronize(this, cli);
                bdd.CLIENTE.Add(cli);
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
                CLIENTE cli =
                //bdd.CLIENTE.First(cli => cli.rut_cliente.Equals(rut_cliente));
                bdd.CLIENTE.Find(rut_cliente);

                bdd.CLIENTE.Remove(cli);
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
                CLIENTE cli =
                bdd.CLIENTE.First(cl => cl.RUT_CLIENTE.Equals(rut_cliente));

                CommonBC.Syncronize(cli, this);//arregló this

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
                //creo un modelo de la tabla cliente
                CLIENTE cli = bdd.CLIENTE.Find(rut_cliente);
                CommonBC.Syncronize(this, cli);
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
                CLIENTE cli = bdd.CLIENTE.Find(rut_cliente);
                CommonBC.Syncronize(cli, this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }

        }
        public List<Cliente> ReadAll()
        {
            try
            {
                var c = from cli in bdd.CLIENTE
                        select new Cliente()
                        {
                            rut_cliente = cli.RUT_CLIENTE,
                            primer_nombre = cli.PRIMER_NOMBRE,
                            segundo_nombre = cli.SEGUNDO_NOMBRE,
                            ap_paterno = cli.AP_PATERNO,
                            ap_materno = cli.AP_MATERNO,
                            direccion = cli.DIRECCION,
                            telefono = cli.TELEFONO,
                            email = cli.EMAIL,
                            id_comuna = cli.ID_COMUNA
                            
                        };
                return c.ToList();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
        //Read all 2 (se llena con método ListaCliente que está abajo)
        public List<ListaClientes> ReadAll2()
        {
            try
            {
                var c = from cli in bdd.CLIENTE
                        join comu in bdd.COMUNA //Join con Comuna para traer el nombre de la comuna y no su id igualando el dato en común (id_comuna)
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
                         Comuna = comu.NOMBRE//Traigo el nombre no el id
                     };

            return cl.ToList();
        }


        //Lista para read all2
        public class ListaClientes
        {
            public string Rut { get; set; }
            public string PrimerNombre { get; set; }
            public string SegundoNombre { get; set; }
            public string ApPaterno { get; set; }
            public string ApMaterno { get; set; }
            public string Direccion { get; set; }
            public int Telefono { get; set; }
            public string Mail { get; set; }
            public string Comuna { get; set; }//Nombre no id

            public ListaClientes()
            {

            }
        }

    }
}
