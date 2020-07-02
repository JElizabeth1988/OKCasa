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
using BibliotecaDALC;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using Oracle.ManagedDataAccess.Client;

using System.Data;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para AsignarInsumo.xaml
    /// </summary>
    public partial class AsignarInsumo : MetroWindow
    {

        OracleConnection conn = null;
        public AsignarInsumo()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
            cbEquipo.Focus();
            btnAsignar.Visibility = Visibility.Hidden;
            //llenar el combo box 
            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }
            cbEquipo.SelectedIndex = 0;
        }
        //------------Cargar Grilla---------------
        private void CargarGrilla()
        {
            try
            {
                int contador = 0;
                List<BibliotecaNegocio.Insumo.ListaInsumos> lista = new List<BibliotecaNegocio.Insumo.ListaInsumos>();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = "SP_INSUMOS_DISPONIBLES";
                cmd.Parameters.Add(new OracleParameter("INSUMOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BibliotecaNegocio.Insumo.ListaInsumos i = new BibliotecaNegocio.Insumo.ListaInsumos();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    i.id = int.Parse(dr.GetValue(0).ToString());
                    i.Nombre = dr.GetValue(1).ToString();

                    lista.Add(i);
                    contador = 1;
                }
                conn.Close();
                if (contador > 0)
                {
                    btnAsignar.Visibility = Visibility.Visible;
                    dgLista.ItemsSource = lista;
                    dgLista.Columns[0].Visibility = Visibility.Collapsed;

                }
                else
                {
                    btnAsignar.Visibility = Visibility.Hidden;
                    dgLista.ItemsSource = null;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Insumos:");
                    dt.Rows.Add("No hay Insumos Disponibles");
                    dgLista.ItemsSource = dt.DefaultView;
                }
                
            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
        }
        //---------Limpiar
        private void Limpiar()
        {
            CargarGrilla();
            cbEquipo.SelectedIndex = 0;
            cbEquipo.Focus();
        }
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }

        
        private async void btnAsignar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BibliotecaNegocio.Insumo.ListaInsumos cli = (BibliotecaNegocio.Insumo.ListaInsumos)dgLista.SelectedItem;
                int id_insumo = cli.id;
                int id_equipo = ((comboBoxItem1)cbEquipo.SelectedItem).id;//Guardo el id
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ASIGNAR_INSUMO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_ID_INSUMO", OracleDbType.Int32)).Value = id_insumo;
                CMD.Parameters.Add(new OracleParameter("P_ID_EQUIPO", OracleDbType.Int32)).Value = id_equipo;
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA (si tiene)
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();

                await this.ShowMessageAsync("Mensaje:",
                    string.Format("Insumo Asignado"));
                CargarGrilla();
            }
            catch (Exception ex)
            {
                Logger.Mensaje(ex.Message);
                await this.ShowMessageAsync("Error:",
                    string.Format("No Asignado"));

            }
        }
        //---------------Cancelar------------------------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
