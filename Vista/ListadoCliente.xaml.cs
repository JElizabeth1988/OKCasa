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

            btnCrear.Visibility = Visibility.Hidden; //No se ve

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

        public ListadoCliente(FormularioInspeccion origen)
        {
            InitializeComponent();
            form = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasar.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;

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
               // dgLista.Columns[0].Visibility = Visibility.Collapsed;//Esconder campo id



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


        //Llamado desde cliente
        public ListadoCliente(Cliente origen)
        {
            InitializeComponent();
            cli = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;
           /* try
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
            }*/
            try
            {
                BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }
        }

        public ListadoCliente(ListadoFormulario origen)
        {
            InitializeComponent();
            liForm = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;
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



        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();
            cli.ShowDialog();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
            dgLista.ItemsSource = cl.ReadAll2();
            dgLista.Items.Refresh();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnPasar_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Visible;
            try
            {

                if (cli == null)
                {
                    BibliotecaNegocio.Cliente.ListaClientes cl = (BibliotecaNegocio.Cliente.ListaClientes)dgLista.SelectedItem;
                    form.txtRutCliente.Text = cl.Rut;
                    //form.Buscar();
                }
                else
                {
                    BibliotecaNegocio.Cliente.ListaClientes cl = (BibliotecaNegocio.Cliente.ListaClientes)dgLista.SelectedItem;
                    string rutbuscar;
                    rutbuscar = cli.txtRut + "-" + cli.txtDV;
                    cli.txtRut.Text = cl.Rut;
                    cli.Buscar();

                }
                /* ListaClientes clie = (ListaClientes)dgLista.SelectedItem;
                 lc.txtfiltroRut.Text = clie.Rut;
                 lc.BuscarCliente();*/

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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPasarAForm_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string rut = txtFiltroRut.Text;

                List<BibliotecaNegocio.Cliente.ListaClientes> lc = new BibliotecaNegocio.Cliente().FiltroRut(rut);
                dgLista.ItemsSource = lc;
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
