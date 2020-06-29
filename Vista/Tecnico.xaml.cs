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
    /// Lógica de interacción para Tecnico.xaml
    /// </summary>
    public partial class Tecnico : MetroWindow
    {
        OracleConnection conn = null;
        public Tecnico()
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

            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }
            
            cboComuna.SelectedIndex = 0;
            cbEquipo.SelectedIndex = 0;
            txtTelefono.Text = "0";
        }

        //--------------Validación Solo se aceptan valores numéricos
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
        //-----------Botón Cancelar
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //Llama al listado para traspasar información para ser modificada en caso que se desconozca rut
        private void btnPregunta_Click(object sender, RoutedEventArgs e)
        {
            ListadoInspectores list = new ListadoInspectores(this);
            list.ShowDialog();
        }

        //--------------Este buscar se usa para el Llamado del botón traspasar
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

                List<BibliotecaNegocio.Tecnico> tec = new List<BibliotecaNegocio.Tecnico>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_TECNICO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al tiposito.ID
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 10)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("TECNICOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Tecnico c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Tecnico();

                    c.rut_tecnico = reader[0].ToString();
                    c.primer_nombre = reader[1].ToString();
                    c.segundo_nombre = reader[2].ToString();
                    c.ap_paterno = reader[3].ToString();
                    c.ap_materno = reader[4].ToString();
                    c.direccion = reader[5].ToString();
                    c.telefono = int.Parse(reader[6].ToString());
                    c.email = reader[7].ToString();
                    c.id_equipo = int.Parse(reader[8].ToString());
                    c.id_comuna = int.Parse(reader[9].ToString());

                    tec.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    txtRut.Text = c.rut_tecnico.Substring(0, 8);
                    txtDV.Text = c.rut_tecnico.Substring(9, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;

                    txtNombre.Text = c.primer_nombre;
                    txtSegNombre.Text = c.segundo_nombre;
                    txtApeMaterno.Text = c.ap_paterno;
                    txtApPaterno.Text = c.ap_materno;
                    txtEmail.Text = c.email;
                    txtDireccion.Text = c.direccion;
                    txtTelefono.Text = c.telefono.ToString();
                    EquipoTecnico eq = new EquipoTecnico();
                    eq.id_equipo = c.id_equipo;
                    eq.Read();
                    cbEquipo.Text = eq.nombre;//Cambiar a nombre

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


        //----------------Botón Limpiar
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
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
            cbEquipo.SelectedIndex = 0;

            btnModificar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;//botón guardar aparece
            btnEliminar.Visibility = Visibility.Hidden;
            txtRut.IsEnabled = true;

            txtRut.Focus();//Mover el cursor a la poscición Rut
        }
        //-----------------------CRUD con Procedimientos----------------------------------------------
        //--------------------------------------------------------------------------------------------
        //Botón Buscar (de administrar)
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

                List<BibliotecaNegocio.Tecnico> tec = new List<BibliotecaNegocio.Tecnico>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_TECNICO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al tiposito.ID
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 10)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("TECNICOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Tecnico c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Tecnico();

                    c.rut_tecnico = reader[0].ToString();
                    c.primer_nombre = reader[1].ToString();
                    c.segundo_nombre = reader[2].ToString();
                    c.ap_paterno = reader[3].ToString();
                    c.ap_materno = reader[4].ToString();
                    c.direccion = reader[5].ToString();
                    c.telefono = int.Parse(reader[6].ToString());
                    c.email = reader[7].ToString();
                    c.id_equipo = int.Parse(reader[8].ToString());
                    c.id_comuna = int.Parse(reader[9].ToString());

                    tec.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    txtRut.Text = c.rut_tecnico.Substring(0, 8);
                    txtDV.Text = c.rut_tecnico.Substring(9, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;

                    txtNombre.Text = c.primer_nombre;
                    txtSegNombre.Text = c.segundo_nombre;
                    txtApeMaterno.Text = c.ap_paterno;
                    txtApPaterno.Text = c.ap_materno;
                    txtEmail.Text = c.email;
                    txtDireccion.Text = c.direccion;
                    txtTelefono.Text = c.telefono.ToString();
                    EquipoTecnico eq = new EquipoTecnico();
                    eq.id_equipo = c.id_equipo;
                    eq.Read();
                    cbEquipo.Text = eq.nombre;//Cambiar a nombre

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
       //--------------Método Guardar---------------------------
        public bool Agregar(BibliotecaNegocio.Tecnico client)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                //NO INGRESA YA QUE TIENE EL ESTADO Y DEVUELVE 0 o 1 DE FILAS AFECTADAS
                //SI CAE EN LA EXCEPCION DEVUELVE 0 Y SI NO CAE DEVUELVE 1
                CMD.CommandText = "SP_AGREGAR_TECNICO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al tiposito.ID
                CMD.Parameters.Add(new OracleParameter("P_RUT_TECNICO", OracleDbType.Varchar2, 10)).Value = client.rut_tecnico;
                CMD.Parameters.Add(new OracleParameter("P_PRIMER_NOMBRE", OracleDbType.Varchar2, 20)).Value = client.primer_nombre;
                CMD.Parameters.Add(new OracleParameter("P_SEGUNDO_NOMBRE", OracleDbType.Varchar2, 20)).Value = client.segundo_nombre;
                CMD.Parameters.Add(new OracleParameter("P_AP_PATERNO", OracleDbType.Varchar2, 20)).Value = client.ap_paterno;
                CMD.Parameters.Add(new OracleParameter("P_AP_MATERNO", OracleDbType.Varchar2, 20)).Value = client.ap_materno;
                CMD.Parameters.Add(new OracleParameter("P_DIRECCION", OracleDbType.Varchar2, 50)).Value = client.direccion;
                CMD.Parameters.Add(new OracleParameter("P_TELEFONO", OracleDbType.Int32)).Value = client.telefono;
                CMD.Parameters.Add(new OracleParameter("P_EMAIL", OracleDbType.Varchar2, 50)).Value = client.email;
                CMD.Parameters.Add(new OracleParameter("P_ID_EQUIPO", OracleDbType.Int32)).Value = client.id_equipo;
                CMD.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleDbType.Int32)).Value = client.id_comuna;
                

                //asi se indica que es parametro de salida// parametro de direccion, y hacia donde es
                //CMD.Parameters.Add(new OracleParameter("P_RESP", OracleDbType.Int32)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA UNA DE SALIDA
                CMD.ExecuteNonQuery();
                // SE CONVIERTE EL P_RESP EN INT 32
                //int cantidad = Convert.ToInt32(CMD.Parameters["P_RESP"].Value);
                //se cierra la conexioin
                conn.Close();
                //se ven las filas afectadas
                //return cantidad > 0;
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //--------------Botón Guardar---------------------------
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                if (rut.Length == 9)
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
                int Equipo = ((comboBoxItem1)cbEquipo.SelectedItem).id;//Guardo el id
                
                BibliotecaNegocio.Tecnico t = new BibliotecaNegocio.Tecnico()
                {
                    rut_tecnico = rut,
                    primer_nombre = Nombre,
                    segundo_nombre = segNombre,
                    ap_paterno = apPaterno,
                    ap_materno = apMaterno,
                    direccion = direccion,
                    telefono = telefono,
                    email = mail,
                    id_equipo = Equipo,
                    id_comuna = Comuna
                    

                };
                bool resp = Agregar(t);
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));
                

                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {
                    DaoErrores de = t.retornar();
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
                    cbEquipo.SelectedIndex = 0;

                    btnModificar.Visibility = Visibility.Hidden;
                    btnGuardar.Visibility = Visibility.Visible;//botón guardar aparece
                    btnEliminar.Visibility = Visibility.Hidden;
                    txtRut.IsEnabled = true;

                    txtRut.Focus();//Mover el cursor a la poscición Rut

                }


                //-----------------------------------------------------------------------------------------------


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

        //-----------Método Modificar----------------------------------       
        public bool Actualizar(BibliotecaNegocio.Tecnico tec)
        {
            try
            {
                string rut = txtRut.Text + "-" + txtDV.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                //NO INGRESA YA QUE TIENE EL ESTADO Y DEVUELVE 0 o 1 DE FILAS AFECTADAS
                //SI CAE EN LA EXCEPCION DEVUELVE 0 Y SI NO CAE DEVUELVE 1
                CMD.CommandText = "SP_ACTUALIZAR_TECNICO";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al tiposito.ID
                CMD.Parameters.Add(new OracleParameter("P_RUT_TECNICO", OracleDbType.Varchar2, 10)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("P_PRIMER_NOMBRE", OracleDbType.Varchar2, 20)).Value = tec.primer_nombre;
                CMD.Parameters.Add(new OracleParameter("P_SEGUNDO_NOMBRE", OracleDbType.Varchar2, 20)).Value = tec.segundo_nombre;
                CMD.Parameters.Add(new OracleParameter("P_AP_PATERNO", OracleDbType.Varchar2, 20)).Value = tec.ap_paterno;
                CMD.Parameters.Add(new OracleParameter("P_AP_MATERNO", OracleDbType.Varchar2, 20)).Value = tec.ap_materno;
                CMD.Parameters.Add(new OracleParameter("P_DIRECCION", OracleDbType.Varchar2, 50)).Value = tec.direccion;
                CMD.Parameters.Add(new OracleParameter("P_TELEFONO", OracleDbType.Int32)).Value = tec.telefono;
                CMD.Parameters.Add(new OracleParameter("P_EMAIL", OracleDbType.Varchar2, 50)).Value = tec.email;
                CMD.Parameters.Add(new OracleParameter("P_ID_EQUIPO", OracleDbType.Int32)).Value = tec.id_equipo;
                CMD.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleDbType.Int32)).Value = tec.id_comuna;

                //asi se indica que es parametro de salida// parametro de direccion, y hacia donde es
                //CMD.Parameters.Add(new OracleParameter("P_RESP", OracleDbType.Int32)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA
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
        //----------Botón modificar-------------------------------
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
                int Equipo = ((comboBoxItem1)cbEquipo.SelectedItem).id;
                BibliotecaNegocio.Tecnico c = new BibliotecaNegocio.Tecnico()
                {
                    rut_tecnico = rut,
                    primer_nombre = nombre,
                    segundo_nombre = segNombre,
                    ap_paterno = apPaterno,
                    ap_materno = apMaterno,
                    direccion = direccion,
                    telefono = telefono,
                    email = mail,
                    id_equipo = Equipo,
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
                        cbEquipo.SelectedIndex = 0;

                        btnModificar.Visibility = Visibility.Hidden;
                        btnGuardar.Visibility = Visibility.Visible;//botón guardar aparece
                        btnEliminar.Visibility = Visibility.Hidden;
                        txtRut.IsEnabled = true;

                        txtRut.Focus();//Mover el cursor a la poscición Rut
                    
                }


                //-----------------------------------------------------------------------------------------------

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
       

        //-----------------Formato Rut (calculo DV)-------------
        //--------------------------------------------------------------

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

        //---------Método Eliminar-----------------------------------------------
        public bool Eliminar(BibliotecaNegocio.Tecnico client)
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
                CMD.CommandText = "SP_ELIMINAR_TECNICO";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_RUT_TECNICO", OracleDbType.Varchar2, 20)).Value = rut;

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

                BibliotecaNegocio.Tecnico tec = new BibliotecaNegocio.Tecnico();
                string nombre = txtNombre.Text+" "+txtApPaterno.Text;
                var x = await this.ShowMessageAsync("Eliminar Datos:",
                         "¿Está Seguro de eliminar a " + nombre + "?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {
                    bool resp = Eliminar(tec);
                    if (resp == true)
                    {
                        await this.ShowMessageAsync("Éxito:",
                          string.Format("Inspector Técnico Eliminado"));
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
                        cbEquipo.SelectedIndex = 0;

                        btnModificar.Visibility = Visibility.Hidden;
                        btnGuardar.Visibility = Visibility.Visible;//botón guardar aparece
                        btnEliminar.Visibility = Visibility.Hidden;
                        txtRut.IsEnabled = true;

                        txtRut.Focus();//Mover el cursor a la poscición Rut
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
    }
}
