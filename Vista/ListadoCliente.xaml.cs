using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using BibliotecaNegocio;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using System.Configuration;
using System.Data;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ListadoCliente.xaml
    /// </summary>
    public partial class ListadoCliente : MetroWindow
    {
        //This
        Cliente cli;
        FormularioInspeccion form;
        ListadoFormulario liForm;

        OracleConnection conn = null;

        public ListadoCliente()
        {
            InitializeComponent();

            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Hidden;//el botón traspasar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;//no se ve

            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;

            btnRefrescar2.Visibility = Visibility.Hidden;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista de tipo cine
                List<BibliotecaNegocio.Cliente.ListaClientes> lista = new List<BibliotecaNegocio.Cliente.ListaClientes>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_CLIENTE2";

                //cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Cliente.ListaClientes C = new BibliotecaNegocio.Cliente.ListaClientes();
                    
                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Segundo_Nombre = dr.GetValue(2).ToString();
                    C.Apellido_paterno = dr.GetValue(3).ToString();
                    C.Apellido_Materno = dr.GetValue(4).ToString();
                    C.Dirección = dr.GetValue(5).ToString();
                    C.Teléfono = int.Parse(dr.GetValue(6).ToString());
                    C.Email = dr.GetValue(7).ToString();                   
                    C.Comuna = dr.GetValue(8).ToString();


                    lista.Add(C);
                }
                conn.Close();
                
                dgLista.ItemsSource = lista;
                dgLista.Columns[0].Visibility = Visibility.Collapsed;//Esconder campo id
                


            }
            catch (Exception ex)
            {
                
                Logger.Mensaje(ex.Message);
            }

            /*try-----------> Código sin procedure
            {
                BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }*/


        }
        //-----------------Llamado desde Informe---------------------------------
        //-----------------------------------------------------------------------
        //Llama solo los parámetros que necesito para el informe
        public ListadoCliente(FormularioInspeccion origen)
        {
            InitializeComponent();
            form = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasar.Visibility = Visibility.Hidden;

            btnRefrescar.Visibility = Visibility.Hidden;
            btnRefrescar2.Visibility = Visibility.Visible;

            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutFor.Visibility = Visibility.Visible;


            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista de tipo cine
                List<BibliotecaNegocio.Solicitud.ListaSolicitud> lista = new List<BibliotecaNegocio.Solicitud.ListaSolicitud>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_CLIENTE_INF";

                //cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Solicitud.ListaSolicitud C = new BibliotecaNegocio.Solicitud.ListaSolicitud();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Direccion = dr.GetValue(2).ToString();
                    C.Constructora = dr.GetValue(3).ToString();
                    C.Fecha = DateTime.Parse(dr.GetValue(4).ToString());
                    C.id_solicitud = int.Parse(dr.GetValue(5).ToString());
                    C.Comuna = dr.GetValue(6).ToString();


                    lista.Add(C);
                }
                conn.Close();
                dgLista.ItemsSource = lista;
                dgLista.Items.Refresh();
                //dgLista.Columns[3].Visibility = Visibility.Collapsed;//Esconder campo id---> No funciona :/
                
                btnRefrescar.Visibility = Visibility.Hidden;



            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }


            /*try
            {
                BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }*/

        }


        //-----------------Llamado desde Adm. Clientes---------------------------------
        //-----------------------------------------------------------------------
        public ListadoCliente(Cliente origen)
        {
            InitializeComponent();
            cli = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;
            btnRefrescar.Visibility = Visibility.Visible;
            btnRefrescar2.Visibility = Visibility.Hidden;

            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista de tipo cine
                List<BibliotecaNegocio.Cliente.ListaClientes> lista = new List<BibliotecaNegocio.Cliente.ListaClientes>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_CLIENTE2";

                //cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Cliente.ListaClientes C = new BibliotecaNegocio.Cliente.ListaClientes();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Segundo_Nombre = dr.GetValue(2).ToString();
                    C.Apellido_paterno = dr.GetValue(3).ToString();
                    C.Apellido_Materno = dr.GetValue(4).ToString();
                    C.Dirección = dr.GetValue(5).ToString();
                    C.Teléfono = int.Parse(dr.GetValue(6).ToString());
                    C.Email = dr.GetValue(7).ToString();
                    C.Comuna = dr.GetValue(8).ToString();


                    lista.Add(C);
                }
                conn.Close();

                dgLista.ItemsSource = lista;
                //dgLista.Columns[0].Visibility = Visibility.Collapsed;//Esconder campo id



            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
           /* try//----> CommonBC
            {
                BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }*/
        }

        //-----------Botón Refrescar readAll2-------------------------------
        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
            dgLista.ItemsSource = cl.ReadAll2();
            dgLista.Items.Refresh();

            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;
        }
        //-----------Refrescar 2---------------------------------------------------------
        private void btnRefrescar2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista de tipo cine
                List<BibliotecaNegocio.Solicitud.ListaSolicitud> lista = new List<BibliotecaNegocio.Solicitud.ListaSolicitud>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_CLIENTE_INF";

                //cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Solicitud.ListaSolicitud C = new BibliotecaNegocio.Solicitud.ListaSolicitud();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Direccion = dr.GetValue(2).ToString();
                    C.Constructora = dr.GetValue(3).ToString();
                    C.Fecha = DateTime.Parse(dr.GetValue(4).ToString());
                    C.id_solicitud = int.Parse(dr.GetValue(5).ToString());
                    C.Comuna = dr.GetValue(6).ToString();


                    lista.Add(C);
                }
                conn.Close();
                dgLista.ItemsSource = lista;
                dgLista.Items.Refresh();
                //dgLista.Columns[3].Visibility = Visibility.Collapsed;//Esconder campo id---> No funciona :/
                btnRefrescar.Visibility = Visibility.Hidden;
                btnPasar.Visibility = Visibility.Hidden;
                btnPasarAForm.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Hidden;

                btnFiltrarRut.Visibility = Visibility.Hidden;
                btnFiltrarRutFor.Visibility = Visibility.Visible;



            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
           
        }
        //--------------Salir---------------------------------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //-----------------Botón pasar a Cliente---------------------
        private async void btnPasar_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Visible;
            try
            {
                BibliotecaNegocio.Cliente.ListaClientes cl = (BibliotecaNegocio.Cliente.ListaClientes)dgLista.SelectedItem;
                string rutbuscar;
                rutbuscar = cli.txtRut + "-" + cli.txtDV;
                cli.txtRut.Text = cl.Rut;
                cli.Buscar();


                Close();
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al traspasar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);
            }
        }
        //---------Método Eliminar-----------------------------------------------
        public bool Eliminar(BibliotecaNegocio.Cliente.ListaClientes client)
        {
            try
            {
                BibliotecaNegocio.Cliente.ListaClientes cli = (BibliotecaNegocio.Cliente.ListaClientes)dgLista.SelectedItem;
                string rut = cli.Rut;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nunca una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ELIMINAR_CLIENTE";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_RUT_CLIENTE", OracleDbType.Varchar2, 20)).Value = rut;

                //se abre la conexion
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA en caso de tener
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);

            }
        }
        //-------------Botón Eliminar------------------------------------------------------
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                BibliotecaNegocio.Cliente.ListaClientes cli = new BibliotecaNegocio.Cliente.ListaClientes();
                var x = await this.ShowMessageAsync("Eliminar Datos de Cliente " + cli.Rut,
                         "¿Desea eliminar al Cliente?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {
                    bool resp = Eliminar(cli);
                    if (resp == true)
                    {
                        await this.ShowMessageAsync("Éxito:",
                          string.Format("Cliente Eliminado"));
                        /*MessageBox.Show("Cliente eliminado"); */
                        //dgLista.ItemsSource =
                        //cli.ReadAll2();
                        dgLista.Items.Refresh();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Error:",
                          string.Format("No Eliminado"));
                        /*MessageBox.Show("No se eliminó al Cliente");*/
                    }

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                          string.Format("Operación Cancelada"));
                    /*MessageBox.Show("Operación Cancelada");*/
                }
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Eliminar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);
            }
        }
        //---------------Botón Pasar a Formulario (Traspasa la info del cliente y solicitud al formulario)
        private async void btnPasarAForm_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Hidden;
            btnPasarAForm.Visibility = Visibility.Visible;
            try
            {
                BibliotecaNegocio.Solicitud.ListaSolicitud cl = (BibliotecaNegocio.Solicitud.ListaSolicitud)dgLista.SelectedItem;
                string rutbuscar;
                rutbuscar = form.txtRutCliente.Text;
                form.txtRutCliente.Text = cl.Rut;
                form.Buscar();


                Close();
            }
            catch (Exception ex)
            {

                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al traspasar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);
            }
        }
        //---------------Filtro para Cliente-----------(No sirve para formulario)
        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string rut = txtFiltroRut.Text;

                List<BibliotecaNegocio.Cliente.ListaClientes> lc = new BibliotecaNegocio.Cliente().FiltroRut(rut);
                dgLista.ItemsSource = lc;
                dgLista.Items.Refresh();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);

                dgLista.Items.Refresh();
            }
        }

        private async void btnFiltrarRutFor_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutFor.Visibility = Visibility.Visible;
            try
            {
                string rut = txtFiltroRut.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Solicitud.ListaSolicitud> clie = new List<BibliotecaNegocio.Solicitud.ListaSolicitud>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_CLI";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 20)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Solicitud.ListaSolicitud c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Solicitud.ListaSolicitud();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Fecha = DateTime.Parse(reader[2].ToString());
                    c.id_solicitud = int.Parse(reader[3].ToString());
                    c.Direccion = reader[4].ToString();
                    c.Constructora = reader[5].ToString();
                    c.Comuna= reader[6].ToString();

                    clie.Add(c);

                }
                conn.Close();
                dgLista.ItemsSource = clie;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);

                dgLista.Items.Refresh();
            }
        }
    }
}
