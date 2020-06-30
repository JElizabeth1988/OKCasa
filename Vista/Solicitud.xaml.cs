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
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");

                List<BibliotecaNegocio.Solicitud.ListaSolicitud> lista = new List<BibliotecaNegocio.Solicitud.ListaSolicitud>();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = "SP_LISTAR_SOLICITUD2";
                cmd.Parameters.Add(new OracleParameter("SOLICITUDES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BibliotecaNegocio.Solicitud.ListaSolicitud sol = new BibliotecaNegocio.Solicitud.ListaSolicitud();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    sol.id_solicitud = int.Parse(dr.GetValue(0).ToString());
                    sol.Rut = dr.GetValue(1).ToString();
                    sol.Nombre = dr.GetValue(2).ToString();
                    sol.Fecha = DateTime.Parse(dr.GetValue(3).ToString());
                    sol.Direccion = dr.GetValue(4).ToString();
                    sol.Constructora = dr.GetValue(5).ToString();
                    sol.Comuna = dr.GetValue(6).ToString();

                    lista.Add(sol);
                }
                conn.Close();

                dgResultado.ItemsSource = lista;
                dgResultado.Columns[0].Visibility = Visibility.Collapsed;//Esconder campo id



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
                BibliotecaNegocio.Solicitud.ListaSolicitud cli = (BibliotecaNegocio.Solicitud.ListaSolicitud)dgResultado.SelectedItem;
                int id = cli.id_solicitud;
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

                List<BibliotecaNegocio.Solicitud.ListaSolicitud> clie = new List<BibliotecaNegocio.Solicitud.ListaSolicitud>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_FILTRO_SOLICITUD";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_SERVICIO", OracleDbType.Varchar2, 20)).Value = nombre;
                CMD.Parameters.Add(new OracleParameter("SOLICITUDES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Solicitud.ListaSolicitud sol = null;
                while (reader.Read())
                {
                    sol = new BibliotecaNegocio.Solicitud.ListaSolicitud();

                    sol.id_solicitud = int.Parse(reader[0].ToString());
                    sol.Rut = reader[1].ToString();
                    sol.Nombre = reader[2].ToString();
                    sol.Fecha = DateTime.Parse(reader[3].ToString());
                    sol.Direccion = reader[4].ToString(); ;
                    sol.Constructora = reader[5].ToString();
                    sol.Comuna = reader[6].ToString();

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
    }
}
