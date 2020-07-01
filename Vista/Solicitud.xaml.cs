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
    /// Lógica de interacción para Solicitud.xaml
    /// </summary>
    public partial class Solicitud : MetroWindow
    {
        OracleConnection conn = null;
        public Solicitud()
        {

            InitializeComponent();
            CargarGrilla();
            //----ComboBox-------
            foreach (BibliotecaNegocio.Servicio item in new BibliotecaNegocio.Servicio().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_servicio;
                cb.nombre = item.nombre;
                cbFiltro.Items.Add(cb);
            }

            cbFiltro.SelectedIndex = 0;


        }
    
        private void CargarGrilla()
        {
            try
            {
                btnConfirmar.Visibility = Visibility.Hidden;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                int contador = 0;
                List<BibliotecaNegocio.Solicitud.ListaSolicitud2> lista = new List<BibliotecaNegocio.Solicitud.ListaSolicitud2>();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = "SP_LISTAR_SOLICITUD2";
                cmd.Parameters.Add(new OracleParameter("SOLICITUDES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BibliotecaNegocio.Solicitud.ListaSolicitud2 sol = new BibliotecaNegocio.Solicitud.ListaSolicitud2();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    sol.id = int.Parse(dr.GetValue(0).ToString());
                    sol.Rut = dr.GetValue(1).ToString();
                    sol.Nombre = dr.GetValue(2).ToString();
                    sol.Fecha = dr.GetValue(3).ToString();
                    sol.Dirección = dr.GetValue(4).ToString();
                    sol.Comuna = dr.GetValue(5).ToString();
                    sol.Estado = dr.GetValue(6).ToString();
                    sol.Fecha__agendada = dr.GetValue(7).ToString();
                    sol.Hora = dr.GetValue(8).ToString();

                    lista.Add(sol);
                    contador ++;//Si trajo resultados a la lista el contador aumento
                }
                conn.Close();
                if ( contador >0)//Si el contador es mayor a 0 muestra el botón confirmar y llena el grid
                {
                    btnConfirmar.Visibility = Visibility.Visible;
                    dgResultado.ItemsSource = lista;
                    dgResultado.Columns[0].Visibility = Visibility.Collapsed;
                    
                }
                else
                {   //Sino
                    btnConfirmar.Visibility = Visibility.Hidden;
                    dgResultado.ItemsSource = null;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Solicitudes:");
                    dt.Rows.Add("No existen solicitudes Pendientes");
                    dgResultado.ItemsSource = dt.DefaultView;
                }



            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BibliotecaNegocio.Solicitud.ListaSolicitud2 cli = (BibliotecaNegocio.Solicitud.ListaSolicitud2)dgResultado.SelectedItem;
                int id = cli.id;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_CONFIRMAR";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_SOLICITUD", OracleDbType.Int32)).Value = id;
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA (si tiene)
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();

                await this.ShowMessageAsync("Mensaje:",
                    string.Format("Solicitud Confirmada"));
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Logger.Mensaje(ex.Message);

            }

        }

        private async void btnFiltro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = cbFiltro.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Solicitud.ListaSolicitud2> clie = new List<BibliotecaNegocio.Solicitud.ListaSolicitud2>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_FILTRO_SOLICITUD";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_SERVICIO", OracleDbType.Varchar2, 50)).Value = nombre;
                CMD.Parameters.Add(new OracleParameter("SOLICITUDES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Solicitud.ListaSolicitud2 sol = null;
                while (reader.Read())
                {
                    sol = new BibliotecaNegocio.Solicitud.ListaSolicitud2();
                    sol.id = int.Parse(reader[0].ToString());
                    sol.Rut = reader[1].ToString();
                    sol.Nombre = reader[2].ToString();
                    sol.Fecha = reader[3].ToString();
                    sol.Dirección = reader[4].ToString();
                    sol.Comuna = reader[5].ToString();
                    sol.Estado = reader[6].ToString();
                    sol.Fecha__agendada = reader[7].ToString();
                    sol.Hora = reader[8].ToString();

                    clie.Add(sol);

                }
                conn.Close();
                dgResultado.ItemsSource = clie;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);

                dgResultado.Items.Refresh();
            }
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }
    }
}
