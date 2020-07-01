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
    /// Lógica de interacción para DevolverInsumo.xaml
    /// </summary>
    public partial class DevolverInsumo : MetroWindow
    {
        OracleConnection conn = null;
        public DevolverInsumo()
        {
            InitializeComponent();

        }

        //-----------Cargar grilla------------------
        private void CargarGrilla()
        {
            try
            {
                int contador = 0;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                List<BibliotecaNegocio.Insumo.ListaInsumos2> lista = new List<BibliotecaNegocio.Insumo.ListaInsumos2>();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = "SP_INSUMOS_OCUPADOS";
                cmd.Parameters.Add(new OracleParameter("INSUMOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BibliotecaNegocio.Insumo.ListaInsumos2 i = new BibliotecaNegocio.Insumo.ListaInsumos2();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    i.id_insumo = int.Parse(dr.GetValue(0).ToString());
                    i.Nombre = dr.GetValue(1).ToString();
                    i.Equipo = dr.GetValue(2).ToString();

                    lista.Add(i);
                    contador = 1;
                }
                conn.Close();
                if (contador > 0)
                {
                    btnDevolver.Visibility = Visibility.Visible;
                    dgLista.ItemsSource = lista;
                    dgLista.Columns[0].Visibility = Visibility.Collapsed;

                }
                else
                {
                    btnDevolver.Visibility = Visibility.Hidden;
                    dgLista.ItemsSource = null;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Insumos:");
                    dt.Rows.Add("No hay Insumos Asignados a equipos");
                    dgLista.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
        }
        //---------Cargar Grilla--------------------------------------
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }
        //---------Devolver------------------------------------
        private async void btnDevolver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BibliotecaNegocio.Insumo.ListaInsumos2 cli = (BibliotecaNegocio.Insumo.ListaInsumos2)dgLista.SelectedItem;
                int id_insumo = cli.id_insumo;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_DEVOLVER_INSUMO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_ID_INSUMO", OracleDbType.Int32)).Value = id_insumo;
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA (si tiene)
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();

                await this.ShowMessageAsync("Mensaje:",
                    string.Format("Insumo Devuelto"));
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Logger.Mensaje(ex.Message);
                await this.ShowMessageAsync("Error:",
                    string.Format("No devuelto"));

            }
        }
        //------------Cerrar---------------------------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
