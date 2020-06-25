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
using BibliotecaNegocio;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using System.Configuration;
using System.Data;
namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ListadoInspectores.xaml
    /// </summary>
    public partial class ListadoInspectores : MetroWindow
    {
        //This
        Tecnico tec;
        FormularioInspeccion form;
        ListadoFormulario liForm;

        OracleConnection conn = null;

        public ListadoInspectores()
        {
            InitializeComponent();
            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Hidden;//Btn no se ve

            //llenar CB
            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }

            cbEquipo.SelectedIndex = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista de tipo cine
                List<BibliotecaNegocio.Tecnico.ListaTecnico> lista = new List<BibliotecaNegocio.Tecnico.ListaTecnico>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_TECNICO";

                //cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("TECNICOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Tecnico.ListaTecnico C = new BibliotecaNegocio.Tecnico.ListaTecnico();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Segundo_Nombre = dr.GetValue(2).ToString();
                    C.ApellidoPaterno = dr.GetValue(3).ToString();
                    C.ApellidoMaterno = dr.GetValue(4).ToString();
                    C.Dirección = dr.GetValue(5).ToString();
                    C.Teléfono = int.Parse(dr.GetValue(6).ToString());
                    C.Email = dr.GetValue(7).ToString();
                    C.Equipo= dr.GetValue(8).ToString();
                    C.Comuna = dr.GetValue(9).ToString();


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
            /*try
            {
                BibliotecaNegocio.Tecnico cl = new BibliotecaNegocio.Tecnico();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }*/
        }

        public ListadoInspectores(Tecnico origen)
        {
            InitializeComponent();
            tec = origen;
            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Visible;//Btn no se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve

            //llenar CB
            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }

            cbEquipo.SelectedIndex = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista de tipo cine
                List<BibliotecaNegocio.Tecnico.ListaTecnico> lista = new List<BibliotecaNegocio.Tecnico.ListaTecnico>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_TECNICO";

                //cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("TECNICOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Tecnico.ListaTecnico C = new BibliotecaNegocio.Tecnico.ListaTecnico();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Segundo_Nombre = dr.GetValue(2).ToString();
                    C.ApellidoPaterno = dr.GetValue(3).ToString();
                    C.ApellidoMaterno = dr.GetValue(4).ToString();
                    C.Dirección = dr.GetValue(5).ToString();
                    C.Teléfono = int.Parse(dr.GetValue(6).ToString());
                    C.Email = dr.GetValue(7).ToString();
                    C.Equipo = dr.GetValue(8).ToString();
                    C.Comuna = dr.GetValue(9).ToString();


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
            /*try
            {
                BibliotecaNegocio.Tecnico cl = new BibliotecaNegocio.Tecnico();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }*/
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            BibliotecaNegocio.Tecnico cl = new BibliotecaNegocio.Tecnico();
            dgLista.ItemsSource = cl.ReadAll2();
            dgLista.Items.Refresh();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            Tecnico tec = new Tecnico();
            tec.ShowDialog();
        }

        private async void btnFiltrarEquipo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                comboBoxItem1 eq = (comboBoxItem1)cbEquipo.SelectedItem;
                List<BibliotecaNegocio.Tecnico.ListaTecnico> lf = new BibliotecaNegocio.Tecnico().FiltroEquipo(eq.nombre);
                dgLista.ItemsSource = lf;
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

        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string rut = txtFiltroRut.Text;

                List<BibliotecaNegocio.Tecnico.ListaTecnico> lc = new BibliotecaNegocio.Tecnico().FiltroRut(rut);
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

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnPasar_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Visible;
            try
            {

                if (tec == null)
                {
                    BibliotecaNegocio.Tecnico.ListaTecnico cl = (BibliotecaNegocio.Tecnico.ListaTecnico)dgLista.SelectedItem;
                    form.txtRutTecnico.Text = cl.Rut;
                    //form.Buscar();
                }
                else
                {
                    BibliotecaNegocio.Tecnico.ListaTecnico cl = (BibliotecaNegocio.Tecnico.ListaTecnico)dgLista.SelectedItem;
                    string rutbuscar;
                    rutbuscar = tec.txtRut + "-" + tec.txtDV;
                    tec.txtRut.Text = cl.Rut;
                    tec.Buscar();

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
    }
}
