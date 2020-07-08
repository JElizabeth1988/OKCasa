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
    public partial class Insumo : MetroWindow
    {
        OracleConnection conn = null;
        public Insumo()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();//Conexión
            txtNombre.Focus();

            btnActualizar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;
        }
        //-----------Cargar Grid---------------------------------------
        private void CargarGrilla()
        {
            try
            {
                List<BibliotecaNegocio.Insumo.ListaInsumos> lista = new List<BibliotecaNegocio.Insumo.ListaInsumos>();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = "SP_LISTAR_INSUMO";
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
                }
                conn.Close();
                dgLista.ItemsSource = lista;
                dgLista.Columns[0].Visibility = Visibility.Collapsed;//Id no se ve
                btnActualizar.Visibility = Visibility.Visible;
                btnEliminar.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                Logger.Mensaje(ex.Message);
            }
        }
        //---------Limpiar-------------
        private void Limpiar()
        {
            CargarGrilla();
            txtNombre.Clear();
            txtNombre.Focus();
        }
        //-------Salir--------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //----------------Método agregar----------------------
        public bool Agregar(BibliotecaNegocio.Insumo ins)
        {
            try
            {
                OracleCommand CMD = new OracleCommand();
                //que tipo de comando voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_AGREGAR_INSUMO";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, 50)).Value = ins.nombre;
                //se abre la conexion
                conn.Open();
                //se ejecuta la query 
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();
                //Return
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Logger.Mensaje(ex.Message);
            }
        }


        //----------------Botón Guardar-----------------------
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String Nombre = txtNombre.Text;
                BibliotecaNegocio.Insumo c = new BibliotecaNegocio.Insumo()
                {
                    nombre = Nombre
                };
                bool resp = Agregar(c);
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));

                if (resp == true)
                {
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error de ingreso de datos"));
                Logger.Mensaje(ex.Message);
            }
        }

        //------------Método Actualizar------------------------------------------
        public bool Actualizar(BibliotecaNegocio.Insumo client)
        {
            try
            {
                BibliotecaNegocio.Insumo.ListaInsumos cli = (BibliotecaNegocio.Insumo.ListaInsumos)dgLista.SelectedItem;
                int id = cli.id;

                OracleCommand CMD = new OracleCommand();
                //que tipo de comand voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ACTUALIZAR_INSUMO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = id;
                CMD.Parameters.Add(new OracleParameter("P_NOMBRE", OracleDbType.Varchar2, 50)).Value = client.nombre;

                //se abre la conexion
                conn.Open();
                //se ejecuta la query 
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //--------------Botón modificar------------------------------------------------
        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BibliotecaNegocio.Insumo.ListaInsumos cli = (BibliotecaNegocio.Insumo.ListaInsumos)dgLista.SelectedItem;
                int id = cli.id;
                string nomb = cli.Nombre;

                BibliotecaNegocio.Insumo c = new BibliotecaNegocio.Insumo()
                {
                    id_insumo = id,
                    nombre = nomb

                };
                bool resp = Actualizar(c);
                await this.ShowMessageAsync("Mensaje:",
                     string.Format(resp ? "Actualizado" : "No Actualizado"));
                if (resp == true)
                {
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Actualizar Datos"));
                Logger.Mensaje(ex.Message);
            }
        }
        //---------Método Eliminar-----------------------------------------------
        public bool Eliminar(BibliotecaNegocio.Insumo serv)
        {
            try
            {
                BibliotecaNegocio.Insumo.ListaInsumos cli = (BibliotecaNegocio.Insumo.ListaInsumos)dgLista.SelectedItem;
                int num = cli.id;

                OracleCommand CMD = new OracleCommand();
                //que tipo de comando voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ELIMINAR_INSUMO";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_ID", OracleDbType.Int32)).Value = num;

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
                BibliotecaNegocio.Insumo cli = new BibliotecaNegocio.Insumo();
                string nombre = cli.nombre;

                var x = await this.ShowMessageAsync("Eliminar Datos: ",
                         "¿Está Seguro de eliminar a "+nombre+"?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {
                    bool resp = Eliminar(cli);
                    if (resp == true)
                    {
                        await this.ShowMessageAsync("Éxito:",
                          string.Format("Insumo Eliminado"));
                        Limpiar();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Error:",
                          string.Format("No Eliminado"));
                    }
                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                          string.Format("Operación Cancelada"));
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
        //---------Ver listado-------------------
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }
    }
}
