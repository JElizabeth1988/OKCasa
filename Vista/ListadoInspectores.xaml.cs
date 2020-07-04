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
            conn = new Conexion().Getcone();
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
            CargarGrilla();
            
            
        }
        //-------------Cargar Grilla------------------------------
        private void CargarGrilla()
        {
            try
            {
                List<BibliotecaNegocio.Tecnico.ListaTecnico> lista = new List<BibliotecaNegocio.Tecnico.ListaTecnico>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_TECNICO2";

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
        //-----Cargar Grilla para informe-----------------
        private void CargarInforme()
        {
            try
            {
                List<BibliotecaNegocio.Tecnico.ListaTecnico2> lista = new List<BibliotecaNegocio.Tecnico.ListaTecnico2>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_TECNICO_INF";
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
        //--------Llamado desde Técnico------------------------------------
        public ListadoInspectores(Tecnico origen)
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
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
            CargarGrilla();
            
        }
       
        //-----------Llamado desde Informe-----------------------------------
        public ListadoInspectores(FormularioInspeccion origen)
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
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
            CargarInforme();
         
        }
        //-------------BOTÓN REFRESCAR-----------------------------
        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }
        //-------Botón Salir--------------------------------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //-----------------Filtro equipo------------------------------
        private async void btnFiltrarEquipo_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarEquipo.Visibility = Visibility.Visible;
            btnFiltrarEquipoInf.Visibility = Visibility.Hidden;
            try
            {
                int equipo = ((comboBoxItem1)cbEquipo.SelectedItem).id;//Recupero el id
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Tecnico.ListaTecnico> clie = new List<BibliotecaNegocio.Tecnico.ListaTecnico>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_FILTRAR_TECNICO_EQUIPO";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_ID_EQUIPO", OracleDbType.Int32)).Value = equipo;
                CMD.Parameters.Add(new OracleParameter("TECNICOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Tecnico.ListaTecnico c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Tecnico.ListaTecnico();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Segundo_Nombre = reader[2].ToString();
                    c.ApellidoPaterno = reader[3].ToString();
                    c.ApellidoMaterno = reader[4].ToString();
                    c.Dirección = reader[5].ToString();
                    c.Teléfono = int.Parse(reader[6].ToString());
                    c.Email = reader[7].ToString();
                    c.Equipo = reader[8].ToString();
                    c.Comuna = reader[9].ToString();
                   
                    clie.Add(c);

                }
                dgLista.ItemsSource = clie;
                conn.Close();

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
            CargarInforme();
        }

        private async void btnFiltrarRutInf_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarRut.Visibility = Visibility.Hidden;
            btnFiltrarRutInf.Visibility = Visibility.Visible;
            try
            {
                string rut = txtFiltroRut.Text;
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
        //------------Filtrar por rut------------------------------------------
        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            btnFiltrarRut.Visibility = Visibility.Visible;
            btnFiltrarRutInf.Visibility = Visibility.Hidden;
            try
            {
                string rut = txtFiltroRut.Text;
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Tecnico.ListaTecnico> clie = new List<BibliotecaNegocio.Tecnico.ListaTecnico>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_FILTRAR_TECNICO_RUT";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2,20)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("TECNICOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Tecnico.ListaTecnico c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Tecnico.ListaTecnico();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Segundo_Nombre = reader[2].ToString();
                    c.ApellidoPaterno = reader[3].ToString();
                    c.ApellidoMaterno = reader[4].ToString();
                    c.Dirección = reader[5].ToString();
                    c.Teléfono = int.Parse(reader[6].ToString());
                    c.Email = reader[7].ToString();
                    c.Equipo = reader[8].ToString();
                    c.Comuna = reader[9].ToString();

                    clie.Add(c);

                }
                dgLista.ItemsSource = clie;
                conn.Close();

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
