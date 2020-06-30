﻿using System;
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

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using System.Configuration;
using System.Data;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using BibliotecaNegocio;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : MetroWindow
    {
        OracleConnection conn = null;
        public Cliente()
        {
            InitializeComponent();

            txtDV.IsEnabled = false;
            btnModificar.Visibility = Visibility.Hidden;//el botón Modificar no se ve
            btnEliminar.Visibility = Visibility.Hidden;
            txtRut.Focus();

            //llenar el combo box 
            foreach (Comuna item in new Comuna().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_comuna;
                cb.nombre = item.nombre;
                cboComuna.Items.Add(cb);
            }
            
            cboComuna.SelectedIndex = 0;
            txtTelefono.Text = "0";

            
        }
        //----------Validación Solo acepta valores numéricos
        private void txtNumeros_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }

        }
        //-----------Botón Cancelar-------------------
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //-----------Botón Pregunta Llama al listado de Clientes en caso de que se desconozca el Rut-------------------
        private void btnPregunta_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }

        //---------Método Limpiar--------------------
        private void Limpiar()
        {
            txtRut.Clear();
            txtDV.Clear();
            txtNombre.Clear();
            txtSegNombre.Clear();
            txtApPaterno.Clear();
            txtApeMaterno.Clear();
            txtDireccion.Clear();
            txtTelefono.Text = "0";
            txtEmail.Clear();
            cboComuna.SelectedIndex = 0;
            txtRut.IsEnabled = true;

            btnModificar.Visibility = Visibility.Hidden;//Botón modificar se esconde
            btnGuardar.Visibility = Visibility.Visible;//botón guardar aparece
            btnEliminar.Visibility = Visibility.Hidden;

            txtRut.Focus();//Mover el cursor a la poscición Rut
        }
        //-----------Botón Limpiar-------------------
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();

        }
        //------------------------CRUD----------------------------------------------------------------
        //--------------------------------------------------------------------------------------------

        //----------------Método agregar----------------------
        public bool Agregar(BibliotecaNegocio.Cliente client)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nunca una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_AGREGAR_CLIENTE";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_RUT_CLIENTE", OracleDbType.Varchar2, 10)).Value = client.rut_cliente;
                CMD.Parameters.Add(new OracleParameter("P_PRIMER_NOMBRE", OracleDbType.Varchar2, 20)).Value = client.primer_nombre;
                CMD.Parameters.Add(new OracleParameter("P_SEGUNDO_NOMBRE", OracleDbType.Varchar2, 20)).Value = client.segundo_nombre;
                CMD.Parameters.Add(new OracleParameter("P_AP_PATERNO", OracleDbType.Varchar2, 20)).Value = client.ap_paterno;
                CMD.Parameters.Add(new OracleParameter("P_AP_MATERNO", OracleDbType.Varchar2, 20)).Value = client.ap_materno;
                CMD.Parameters.Add(new OracleParameter("P_DIRECCION", OracleDbType.Varchar2, 50)).Value = client.direccion;
                CMD.Parameters.Add(new OracleParameter("P_TELEFONO", OracleDbType.Int32)).Value = client.telefono;
                CMD.Parameters.Add(new OracleParameter("P_EMAIL", OracleDbType.Varchar2, 50)).Value = client.email;
                CMD.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleDbType.Int32)).Value = client.id_comuna;
                

                //asi se indica que es parametro de salida// parametro de direccion, y hacia donde es
                //CMD.Parameters.Add(new OracleParameter("P_RESP", OracleDbType.Int32)).Direction = System.Data.ParameterDirection.Output;
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


        //----------------Botón Guardar-----------------------
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                if (rut.Length == 9)//Si el rut tiene solo 9 caracteres se le agrega cero al comienzo para que quede de 10
                {
                    rut = "0" + txtRut.Text + "-" + txtDV.Text;
                }
                String Nombre = txtNombre.Text;
                String segNombre = txtSegNombre.Text;
                String apPaterno = txtApPaterno.Text;
                String apMaterno = txtApeMaterno.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;    
                int telefono = int.Parse(txtTelefono.Text); 
                int Comuna = ((comboBoxItem1)cboComuna.SelectedItem).id;//Guardo el id

                             
                BibliotecaNegocio.Cliente c = new BibliotecaNegocio.Cliente()
                {
                    rut_cliente = rut,
                    primer_nombre = Nombre,
                    segundo_nombre = segNombre,
                    ap_paterno = apPaterno,
                    ap_materno = apMaterno,
                    direccion = direccion,
                    telefono = telefono,
                    email = mail,
                    id_comuna = Comuna
                    
                    
                    
                };
                bool resp = Agregar(c);
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));
                /*MessageBox.Show(resp ? "Guardado" : "No Guardado");*/
                
                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES (validación de la clase)
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {
                    DaoErrores de = c.retornar();
                    string li = "";
                    foreach (string item in de.ListarErrores())
                    {
                        li += item + " \n";
                    }
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format(li));
                }
                else
                {
                    Limpiar();
                }
            }
            catch (ArgumentException exa)//mensajes de reglas de negocios
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error de ingreso de datos"));
                /*MessageBox.Show("Error de ingreso de datos");*/
                Logger.Mensaje(ex.Message);

            }
        }
        //------------Método Actualizar------------------------------------------
        public bool Actualizar(BibliotecaNegocio.Cliente client)
        {
            try
            {
                string rut = txtRut.Text+"-"+txtDV.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ACTUALIZAR_CLIENTE";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_RUT_CLIENTE", OracleDbType.Varchar2, 10)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("P_PRIMER_NOMBRE", OracleDbType.Varchar2, 20)).Value = client.primer_nombre;
                CMD.Parameters.Add(new OracleParameter("P_SEGUNDO_NOMBRE", OracleDbType.Varchar2, 20)).Value = client.segundo_nombre;
                CMD.Parameters.Add(new OracleParameter("P_AP_PATERNO", OracleDbType.Varchar2, 20)).Value = client.ap_paterno;
                CMD.Parameters.Add(new OracleParameter("P_AP_MATERNO", OracleDbType.Varchar2, 20)).Value = client.ap_materno;
                CMD.Parameters.Add(new OracleParameter("P_DIRECCION", OracleDbType.Varchar2, 50)).Value = client.direccion;
                CMD.Parameters.Add(new OracleParameter("P_TELEFONO", OracleDbType.Int32)).Value = client.telefono;
                CMD.Parameters.Add(new OracleParameter("P_EMAIL", OracleDbType.Varchar2, 50)).Value = client.email;
                CMD.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleDbType.Int32)).Value = client.id_comuna;

                //asi se indica que es parametro de salida// parametro de direccion, y hacia donde es
                //CMD.Parameters.Add(new OracleParameter("P_RESP", OracleDbType.Int32)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA (si tiene)
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
        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                String nombre = txtNombre.Text;
                String segNombre = txtSegNombre.Text;
                String apPaterno = txtApPaterno.Text;
                String apMaterno = txtApeMaterno.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;
                int telefono = int.Parse(txtTelefono.Text);
                int Comuna = ((comboBoxItem1)cboComuna.SelectedItem).id;
                
                BibliotecaNegocio.Cliente c = new BibliotecaNegocio.Cliente()
                {
                    rut_cliente = rut,
                    primer_nombre = nombre,
                    segundo_nombre = segNombre,
                    ap_paterno = apPaterno,
                    ap_materno = apMaterno,
                    direccion = direccion,
                    telefono = telefono,
                    email = mail,               
                    id_comuna = Comuna

                };
                bool resp = Actualizar(c);
                await this.ShowMessageAsync("Mensaje:",
                     string.Format(resp ? "Actualizado" : "No Actualizado"));
                /*MessageBox.Show(resp ? "Actualizado" : "No Actualizado, (El rut no se debe modificar)");*/

                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {

                    DaoErrores de = c.retornar();
                    string li = "";
                    foreach (string item in de.ListarErrores())
                    {
                        li += item + " \n";
                    }
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format(li));
                }
                else
                {
                    Limpiar();
                }

            }
            catch (ArgumentException exa)//mensajes de reglas de negocios
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Actualizar Datos"));
                /*MessageBox.Show("Error al Actualizar");*/
                Logger.Mensaje(ex.Message);

            }
        }

        //------------------Llamado del botón traspasar--------------
        public async void Buscar()
         {
             try
             {
                string rut = txtRut.Text + "-" + txtDV.Text;

                if (rut.Length == 9)
                {
                    rut = "0" + txtRut.Text + "-" + txtDV.Text;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Cliente> clie = new List<BibliotecaNegocio.Cliente>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_CLIENTE";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 10)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Cliente c = null;
                while (reader.Read())//Mientras lee
                {
                    c = new BibliotecaNegocio.Cliente();

                    c.rut_cliente = reader[0].ToString();
                    c.primer_nombre = reader[1].ToString();
                    c.segundo_nombre = reader[2].ToString();
                    c.ap_paterno = reader[3].ToString();
                    c.ap_materno = reader[4].ToString();
                    c.direccion = reader[5].ToString();
                    c.telefono = int.Parse(reader[6].ToString());
                    c.email = reader[7].ToString();
                    c.id_comuna = int.Parse(reader[8].ToString());

                    clie.Add(c);

                }
                conn.Close();
                if (c != null)//Si la lista no esta vacía entrego parámetros a los textBox y CB
                {
                    txtRut.Text = c.rut_cliente.Substring(0, 8);
                    txtDV.Text = c.rut_cliente.Substring(9, 1);
                    txtRut.IsEnabled = false;//Rut no se modifica
                    txtDV.IsEnabled = false;//DV tampoco

                    txtNombre.Text = c.primer_nombre;
                    txtSegNombre.Text = c.segundo_nombre;
                    txtApeMaterno.Text = c.ap_paterno;
                    txtApPaterno.Text = c.ap_materno;
                    txtEmail.Text = c.email;
                    txtDireccion.Text = c.direccion;
                    txtTelefono.Text = c.telefono.ToString();
                    //-------Cambiar a nombre
                    Comuna co = new Comuna();
                    co.id_comuna = c.id_comuna;
                    co.Read();
                    cboComuna.Text = co.nombre;//Cambiar a nombre
                    //--------------------
                    btnModificar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;//Guardar desaparece
                    btnEliminar.Visibility = Visibility.Visible;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format("No se encontraron resultados!"));
                    /*MessageBox.Show("No se encontraron resultados!");*/
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Buscar Información! "));
                /*MessageBox.Show("error al buscar");*/
                Logger.Mensaje(ex.Message);

            }
        }


       
        //----------------------Botón Buscar (de administrar cliente)---------------
        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rut = txtRut.Text + "-" + txtDV.Text;

                if (rut.Length == 9)
                {
                    rut = "0" + txtRut.Text + "-" + txtDV.Text;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                
                List<BibliotecaNegocio.Cliente> clie = new List<BibliotecaNegocio.Cliente>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_CLIENTE";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 10)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("CLIENTES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Cliente c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Cliente();

                    c.rut_cliente = reader[0].ToString();
                    c.primer_nombre = reader[1].ToString();
                    c.segundo_nombre = reader[2].ToString();
                    c.ap_paterno = reader[3].ToString();
                    c.ap_materno = reader[4].ToString();
                    c.direccion = reader[5].ToString();
                    c.telefono = int.Parse(reader[6].ToString());
                    c.email = reader[7].ToString();
                    c.id_comuna = int.Parse(reader[8].ToString());

                    clie.Add(c);

                }
                    conn.Close();
                   
                    
                if (c !=null)
                {
                    txtRut.Text = c.rut_cliente.Substring(0, 8);
                    txtDV.Text = c.rut_cliente.Substring(9, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;

                    txtNombre.Text = c.primer_nombre;
                    txtSegNombre.Text = c.segundo_nombre;
                    txtApeMaterno.Text = c.ap_paterno;
                    txtApPaterno.Text = c.ap_materno;
                    txtEmail.Text = c.email;
                    txtDireccion.Text = c.direccion;
                    txtTelefono.Text = c.telefono.ToString();

                    Comuna co = new Comuna();
                    co.id_comuna = c.id_comuna;
                    co.Read();
                    cboComuna.Text = co.nombre;//Cambiar a nombre

                    btnModificar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;
                    btnEliminar.Visibility = Visibility.Visible;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format("No se encontraron resultados!"));
                    /*MessageBox.Show("No se encontraron resultados!");*/
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Buscar Información! "));
                /*MessageBox.Show("error al buscar");*/
                Logger.Mensaje(ex.Message);

            }
        }



        //---------Método Eliminar-----------------------------------------------
        public bool Eliminar(BibliotecaNegocio.Cliente client)
        {
            try
            {
                string rut = txtRut.Text+"-"+txtDV.Text;
                if (rut.Length == 9)
                {
                    rut = "0" + txtRut.Text + "-" + txtDV.Text;
                }
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nunca una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ELIMINAR_CLIENTE";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_RUT_CLIENTE", OracleDbType.Varchar2, 20)).Value = rut;

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
                BibliotecaNegocio.Cliente cli = new BibliotecaNegocio.Cliente();
                string nombre = txtNombre.Text + " " + txtApPaterno.Text;
                var x = await this.ShowMessageAsync("Eliminar Datos: ",
                         "¿Está Seguro de eliminar a "+nombre +"?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {
                    bool resp = Eliminar(cli);
                    if (resp == true)
                    {
                        await this.ShowMessageAsync("Éxito:",
                          string.Format("Cliente Eliminado"));
                        /*MessageBox.Show("Cliente eliminado"); */
                        Limpiar();

                    }
                    else
                    {
                        await this.ShowMessageAsync("Error:",
                          string.Format("No Eliminado"));
                        /*MessageBox.Show("No se eliminó al Cliente");*/
                    }

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                          string.Format("Operación Cancelada"));
                    /*MessageBox.Show("Operación Cancelada");*/
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

        //-------------------------------------------------------------------------------------
        //----------------------añadir formato al rut-----------------------------------------
        //--------------------------------------------------------------------------------------
        private void txtRut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text.Length >= 7 && txtRut.Text.Length <= 8)
            {
                string v = new Verificar().ValidarRut(txtRut.Text);
                txtDV.Text = v;
                try
                {
                    string rutSinFormato = txtRut.Text;

                    //si el rut ingresado tiene "." o "," o "-" son ratirados para realizar la formula 
                    rutSinFormato = rutSinFormato.Replace(",", "");
                    rutSinFormato = rutSinFormato.Replace(".", "");
                    rutSinFormato = rutSinFormato.Replace("-", "");
                    string rutFormateado = String.Empty;

                    //obtengo la parte numerica del RUT
                    //string rutTemporal = rutSinFormato.Substring(0, rutSinFormato.Length - 1);
                    string rutTemporal = rutSinFormato;
                    //obtengo el Digito Verificador del RUT
                    //string dv = rutSinFormato.Substring(rutSinFormato.Length - 1, 1);

                    Int64 rut;

                    //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                    if (!Int64.TryParse(rutTemporal, out rut))
                    {
                        rut = 0;
                    }

                    //este comando es el que formatea con los separadores de miles
                    //rutFormateado = rut.ToString("N0"); (11.111.111-1)
                    rutFormateado = rut.ToString();

                    if (rutFormateado.Equals("0"))
                    {
                        rutFormateado = string.Empty;
                    }
                    else
                    {
                        //si no hubo problemas con el formateo agrego el DV a la salida
                        // rutFormateado += "-" + dv;

                        //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                        rutFormateado = rutFormateado.Replace(",", ".");
                    }

                    //se pasa a mayuscula si tiene letra k
                    rutFormateado = rutFormateado.ToUpper();

                    //Si se uso rutFormateado = rut.ToString("N0"); la salida esperada para el ejemplo es 99.999.999-K
                    txtRut.Text = rutFormateado;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                txtRut.Text = "";
            }
        }
    }

}
