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
            btnPasarInf.Visibility = Visibility.Hidden;
            btnRefrescarInf.Visibility = Visibility.Hidden;
            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutInf.Visibility = Visibility.Hidden;
            btnFiltrarEquipo.Visibility = Visibility.Visible;
            btnFiltrarEquipoInf.Visibility = Visibility.Hidden;

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
            
        }
        //--------Llamado desde Técnico------------------------------------
        public ListadoInspectores(Tecnico origen)
        {
            InitializeComponent();
            tec = origen;
            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Visible;//Btn no se ve
            btnPasarInf.Visibility = Visibility.Hidden;
            
            btnRefrescarInf.Visibility = Visibility.Hidden;
            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutInf.Visibility = Visibility.Hidden;
            btnFiltrarEquipo.Visibility = Visibility.Visible;
            btnFiltrarEquipoInf.Visibility = Visibility.Hidden;

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
            
        }
        //-----------Llamado desde Informe-----------------------------------
        public ListadoInspectores(FormularioInspeccion origen)
        {
            InitializeComponent();
            form = origen;
            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Hidden;//Btn no se ve
            
            btnPasarInf.Visibility = Visibility.Visible;
            btnRefrescar.Visibility = Visibility.Hidden;
            btnRefrescarInf.Visibility = Visibility.Visible;
            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutInf.Visibility = Visibility.Visible;
            btnFiltrarEquipo.Visibility = Visibility.Hidden;
            btnFiltrarEquipoInf.Visibility = Visibility.Visible;

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
                //se crea una lista 
                List<BibliotecaNegocio.Tecnico.ListaTecnico2> lista = new List<BibliotecaNegocio.Tecnico.ListaTecnico2>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_TECNICO_INF";

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
                    BibliotecaNegocio.Tecnico.ListaTecnico2 C = new BibliotecaNegocio.Tecnico.ListaTecnico2();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Equipo = dr.GetValue(2).ToString();
 
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
         
        }
        //-------------BOTÓN REFRESCAR-----------------------------
        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            BibliotecaNegocio.Tecnico cl = new BibliotecaNegocio.Tecnico();
            dgLista.ItemsSource = cl.ReadAll2();
            dgLista.Items.Refresh();
        }
        //-------Botón Salir--------------------------------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //-----------------Filtro equipo------------------------------
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
        //-------------Filtro Rut-----------------------------------------------
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
        
        //---------------Botón Pasar a Técnico--------------------------------------
        private async void btnPasar_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Visible;
            try
            {


                BibliotecaNegocio.Tecnico.ListaTecnico cl = (BibliotecaNegocio.Tecnico.ListaTecnico)dgLista.SelectedItem;
                string rutbuscar;
                rutbuscar = tec.txtRut + "-" + tec.txtDV;
                tec.txtRut.Text = cl.Rut;
                tec.Buscar();



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
        //-----------------Botón Pasar a Informe----------------------------------------
        private async void btnPasarInf_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Hidden;
            btnPasarInf.Visibility = Visibility.Visible;
            try
            {
                BibliotecaNegocio.Tecnico.ListaTecnico2 cl = (BibliotecaNegocio.Tecnico.ListaTecnico2)dgLista.SelectedItem;
                string rutbuscar;
                rutbuscar = form.txtRutTecnico.Text;
                form.txtRutTecnico.Text = cl.Rut;
                form.BuscarTec();


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
        //---------------Refrescar 2-----------------------------------------
        private void btnRefrescarInf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //se crea una lista 
                List<BibliotecaNegocio.Tecnico.ListaTecnico2> lista = new List<BibliotecaNegocio.Tecnico.ListaTecnico2>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_TECNICO_INF";

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
                    BibliotecaNegocio.Tecnico.ListaTecnico2 C = new BibliotecaNegocio.Tecnico.ListaTecnico2();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    C.Rut = dr.GetValue(0).ToString();
                    C.Nombre = dr.GetValue(1).ToString();
                    C.Equipo = dr.GetValue(2).ToString();

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
        }

        private async void btnFiltrarRutInf_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutInf.Visibility = Visibility.Visible;
            try
            {
                string rut = txtFiltroRut.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Tecnico.ListaTecnico2> clie = new List<BibliotecaNegocio.Tecnico.ListaTecnico2>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_TEC";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 20)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Tecnico.ListaTecnico2 c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Tecnico.ListaTecnico2();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Equipo = reader[2].ToString();

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
        //-----------Filtro x equipo--------------------------------------------------------
        private async void btnFiltrarEquipoInf_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarEquipo.Visibility = Visibility.Hidden;
            btnFiltrarEquipoInf.Visibility = Visibility.Visible;
            try
            {
                string nombre = cbEquipo.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Tecnico.ListaTecnico2> clie = new List<BibliotecaNegocio.Tecnico.ListaTecnico2>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_EQ";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_EQUIPO", OracleDbType.Varchar2, 20)).Value = nombre;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Tecnico.ListaTecnico2 c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Tecnico.ListaTecnico2();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Equipo = reader[2].ToString();

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
