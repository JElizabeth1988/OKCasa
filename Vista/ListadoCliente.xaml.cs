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
using BibliotecaDALC;

using Oracle.ManagedDataAccess.Client;

using System.Data;

namespace Vista
{
    
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
            conn = new Conexion().Getcone();

            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Hidden;//el botón traspasar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;//no se ve

            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;

            btnRefrescar2.Visibility = Visibility.Hidden;
            CargarGrilla();
           

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

        //------------Cargar Grilla---------------------
        private void CargarGrilla()
        {
            try
            {
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
                // dgLista.Columns[0].Visibility = Visibility.Collapsed;//Esconder campo id



            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
        }


        //-----------------Llamado desde Informe---------------------------------
        //-----------------------------------------------------------------------
        //Llama solo los parámetros que necesito para el informe
        public ListadoCliente(FormularioInspeccion origen)
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
            form = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            
            btnPasar.Visibility = Visibility.Hidden;

            btnRefrescar.Visibility = Visibility.Hidden;
            btnRefrescar2.Visibility = Visibility.Visible;

            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutFor.Visibility = Visibility.Visible;
            CargarInforme();            

        }
        //----------Cargar Grilla para el informe(Sólo clientes con solicitudes)-----------
        private void CargarInforme()
        {
            try
            {
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
        }
        //-----------------Llamado desde Adm. Clientes---------------------------------
        //-----------------------------------------------------------------------
        public ListadoCliente(Cliente origen)
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
            cli = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
           
            btnPasarAForm.Visibility = Visibility.Hidden;
            btnRefrescar.Visibility = Visibility.Visible;
            btnRefrescar2.Visibility = Visibility.Hidden;

            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;
            CargarGrilla();
        }

        //-----------Botón Refrescar readAll2-------------------------------
        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;

            CargarGrilla();
        }
        //-----------Refrescar 2---------------------------------------------------------
        private void btnRefrescar2_Click(object sender, RoutedEventArgs e)
        {

            btnRefrescar.Visibility = Visibility.Hidden;
            btnPasar.Visibility = Visibility.Hidden;
            btnPasarAForm.Visibility = Visibility.Visible;

            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutFor.Visibility = Visibility.Visible;

            CargarInforme();
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

            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutFor.Visibility = Visibility.Hidden;
            try
            {
                string rut = txtFiltroRut.Text;
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Cliente.ListaClientes> clie = new List<BibliotecaNegocio.Cliente.ListaClientes>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_FILTRAR_RUT";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 20)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Cliente.ListaClientes c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Cliente.ListaClientes();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Segundo_Nombre = reader[2].ToString();
                    c.Apellido_paterno = reader[3].ToString();
                    c.Apellido_Materno = reader[4].ToString();
                    c.Dirección = reader[5].ToString();
                    c.Teléfono = int.Parse(reader[6].ToString());
                    c.Email = reader[7].ToString();
                    c.Comuna = reader[8].ToString();

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

        private async void btnFiltrarRutFor_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutFor.Visibility = Visibility.Visible;
            try
            {
                string rut = txtFiltroRut.Text;
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
