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
    /// Lógica de interacción para FormularioInspeccion.xaml
    /// </summary>
    public partial class FormularioInspeccion : MetroWindow
    {
        OracleConnection conn = null;
        public FormularioInspeccion()
        {
            InitializeComponent();
            this.DataContext = this;
            txtRutCliente.Focus();

            lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");

            btnActualizar.Visibility = Visibility.Hidden;

            //---------llenar el combo box 
            foreach (InstAlcantarillado item in new InstAlcantarillado().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_alcantarillado;
                cb.nombre = item.nombre;
                cbAlcanta.Items.Add(cb);
            }

            foreach (InstGas item in new InstGas().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_gas;
                cb.nombre = item.nombre;
                cbGas.Items.Add(cb);
            }

            foreach (InstElectrica item in new InstElectrica().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_electrica;
                cb.nombre = item.nombre;
                cbElectrica.Items.Add(cb);
            }

            foreach (RedAgua item in new RedAgua().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_agua;
                cb.nombre = item.nombre;
                cbRedAgua.Items.Add(cb);
            }

            foreach (InstAguaPotable item in new InstAguaPotable().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_agua_potable;
                cb.nombre = item.nombre;
                cbInstAgua.Items.Add(cb);
            }


            foreach (TipoVivienda item in new TipoVivienda().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_tipo;
                cb.nombre = item.nombre_tipo;
                cbTipoV.Items.Add(cb);
            }

            foreach (Agrupamiento item in new Agrupamiento().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_agrup;
                cb.nombre = item.nombre_agr;
                cbTipoAg.Items.Add(cb);
            }
            //_------------------------------

            cbAlcanta.SelectedIndex = 0;
            cbGas.SelectedIndex = 0;
            cbElectrica.SelectedIndex = 0;
            cbRedAgua.SelectedIndex = 0;
            cbInstAgua.SelectedIndex = 0;
            cbTipoV.SelectedIndex = 0;
            cbTipoAg.SelectedIndex = 0;
            txtPisos.Text = "0";
            txtHabitac.Text = "0";
            txtTotalReal.Text = "0";
            txtTotalReg.Text = "0";
            txtIConstReal.Text = "0";
            txtConsReg.Text = "0";
            txtDistancia.Text = "0";
            txtEmisividad.Text = "0";
            txtTempRefle.Text = "0";
            txtTempAtmo.Text = "0";
            txtHumedad.Text = "0";
            lblIdSolicitud.Visibility = Visibility.Hidden;
        }
        //--------BOTÓN CANCELAR
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //-------BOTÓN PREGUNTA BUSCAR CLIENTE (LLAMA AL LISTADO DE CLIENTES)----------
        private void btnListarCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }
        //----REVISAR!!!!!!!!!!!!!!!!!
        //-------BOTÓN PREGUNTA BUSCAR FORMULARIO (LLAMA AL LISTADO DE CLIENTES?)----------
        private void btnListarCli_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }
        //-------BOTÓN PREGUNTA BUSCAR FORMULARIO (LLAMA AL LISTADO DE FORMULARIOS)----------
        private void btnListarForm_Click(object sender, RoutedEventArgs e)
        {
            ListadoFormulario liForm = new ListadoFormulario(this);
            liForm.ShowDialog();
        }
        //-------BOTÓN PREGUNTA BUSCAR INSPECTORES (LLAMA AL LISTADO DE TÉCNICOS)----------
        private void btnLsitarInsp_Click(object sender, RoutedEventArgs e)
        {
            ListadoInspectores lii = new ListadoInspectores();
            lii.ShowDialog();
        }

        //-------------Validación campo solo numerico---------------------
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
        //-------------BOTÓN LIMPIAR----------------------------
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");
            dtfechaIns.SelectedDate = DateTime.Now;
            dtfechaSol.SelectedDate = DateTime.Now;
            txtRutCliente.Focus();
            rbPrimera.IsChecked = true;
            rbSegunda.IsChecked = false;
            rbCierre.IsChecked = false;

            txtNFormBuscar.Clear();
            txtRutCliente.Clear();
            txtRutTecnico.Clear();
            txtPisos.Text = "0";
            txtObserv.Clear();
            txtNombreCliente.Clear();
            txtHabitac.Text = "0";
            txtEquipo.Clear();
            txtDireccion.Clear();
            txtConstr.Clear();
            txtNombreTec.Clear();
            cbAlcanta.SelectedIndex = 0;
            cbElectrica.SelectedIndex = 0;
            cbGas.SelectedIndex = 0;
            cbInstAgua.SelectedIndex = 0;
            cbRedAgua.SelectedIndex = 0;
            cbTipoAg.SelectedIndex = 0;
            cbTipoV.SelectedIndex = 0;

            RbNoFuego.IsChecked = true;
            RbSiFuego.IsChecked = false;
            RbNoHab.IsChecked = true;
            RbSiHab.IsChecked = false;
            RbNoTerm.IsChecked = true;
            RbSiTerm.IsChecked = false;

            txtTotalReal.Text = "0";
            txtTotalReg.Text = "0";
            txtIConstReal.Text = "0";
            txtConsReg.Text = "0";
            txtDistancia.Text = "0";
            txtEmisividad.Text = "0";
            txtTempRefle.Text = "0";
            txtTempAtmo.Text = "0";
            txtHumedad.Text = "0";


            rbBueno.IsChecked = true;
            rbDeficiente.IsChecked = false;
            rbMalo.IsChecked = false;
            rbRegular.IsChecked = false;

            lblIdSolicitud.Visibility = Visibility.Hidden;
            btnActualizar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;


        }

        //---------------------CRUD-------------------------------------------------
        //--------------------------------------------------------------------------
        //----------------Método agregar----------------------
        public bool Agregar(BibliotecaNegocio.Informe inf)
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
                CMD.CommandText = "SP_AGREGAR_INFORME";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_NUM_FORMULARIO", OracleDbType.Int64)).Value = inf.num_formulario;
                CMD.Parameters.Add(new OracleParameter("P_ESTADO_SERVICIO", OracleDbType.Varchar2, 20)).Value = inf.estado_servicio;
                CMD.Parameters.Add(new OracleParameter("P_FECHA_INSP", OracleDbType.Date)).Value = inf.fecha_insp;
                CMD.Parameters.Add(new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, 20)).Value = inf.resultado;
                CMD.Parameters.Add(new OracleParameter("P_NUM_HABITACIONES", OracleDbType.Int32)).Value = inf.num_habitaciones;
                CMD.Parameters.Add(new OracleParameter("P_NUM_PISOS", OracleDbType.Int32)).Value = inf.num_pisos;
                CMD.Parameters.Add(new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2,2000)).Value = inf.observacion;
                CMD.Parameters.Add(new OracleParameter("P_HABITABILIDAD", OracleDbType.Varchar2, 5)).Value = inf.habitabilidad;
                CMD.Parameters.Add(new OracleParameter("P_TERMICA", OracleDbType.Varchar2,5)).Value = inf.termica;
                CMD.Parameters.Add(new OracleParameter("P_FUEGO", OracleDbType.Varchar2,5)).Value = inf.fuego;
                CMD.Parameters.Add(new OracleParameter("P_AREA_REGIS", OracleDbType.Int32)).Value = inf.area_regis;
                CMD.Parameters.Add(new OracleParameter("P_AREA_REAL", OracleDbType.Int32)).Value = inf.area_real;
                CMD.Parameters.Add(new OracleParameter("P_SUP_CONSTR_REGIS", OracleDbType.Int32)).Value = inf.sup_constr_regis;
                CMD.Parameters.Add(new OracleParameter("P_SUP_CONSTR_REAL", OracleDbType.Int32)).Value = inf.sup_constr_real;
                CMD.Parameters.Add(new OracleParameter("P_EMISIVIDAD", OracleDbType.Int32)).Value = inf.emisividad;
                CMD.Parameters.Add(new OracleParameter("P_TEMP_REFLEJADA", OracleDbType.Int32)).Value = inf.temp_reflejada;
                CMD.Parameters.Add(new OracleParameter("P_DISTANCIA", OracleDbType.Int32)).Value = inf.distancia;
                CMD.Parameters.Add(new OracleParameter("P_HUMEDAD_REL", OracleDbType.Int32)).Value = inf.humedad_rel;
                CMD.Parameters.Add(new OracleParameter("P_TEMP_ATMOSFERICA", OracleDbType.Int32)).Value = inf.temp_atmosferica;
                CMD.Parameters.Add(new OracleParameter("P_RUT_CLIENTE", OracleDbType.Varchar2,20)).Value = inf.rut_cliente;
                CMD.Parameters.Add(new OracleParameter("P_RUT_TECNICO", OracleDbType.Varchar2,20)).Value = inf.rut_tecnico;
                CMD.Parameters.Add(new OracleParameter("P_ID_TIPO", OracleDbType.Int32)).Value = inf.id_tipo;
                CMD.Parameters.Add(new OracleParameter("P_ID_AGRUP", OracleDbType.Int32)).Value = inf.id_agrup;
                CMD.Parameters.Add(new OracleParameter("P_ID_SOLICITUD", OracleDbType.Int32)).Value = inf.id_solicitud;
                CMD.Parameters.Add(new OracleParameter("P_ID_ALCANTARILLADO", OracleDbType.Int32)).Value = inf.id_alcantarillado;
                CMD.Parameters.Add(new OracleParameter("P_ID_GAS", OracleDbType.Int32)).Value = inf.id_gas;
                CMD.Parameters.Add(new OracleParameter("P_ID_ELECTRICA", OracleDbType.Int32)).Value = inf.id_electrica;
                CMD.Parameters.Add(new OracleParameter("P_ID_AGUA", OracleDbType.Int32)).Value = inf.id_agua;
                CMD.Parameters.Add(new OracleParameter("P_ID_AGUA_POTABLE", OracleDbType.Int32)).Value = inf.id_agua_potable;
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

        //---------GUARDAR---------------------
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long Numero = long.Parse(lblNumForm.Content.ToString());
                string Estado = "";
                if (rbPrimera.IsChecked==true)
                {
                    Estado = "Primera Revisión";
                }
                if(rbSegunda.IsChecked == true)
                {
                    Estado = "Segunda Revisión";
                }
                if (rbCierre.IsChecked==true)
                {
                    Estado = "Cierre";
                }
                DateTime Fecha = dtfechaIns.SelectedDate.Value.Date;
                string Result = "";
                if (rbBueno.IsChecked==true)
                {
                    Result = "Bueno";
                }
                if (rbDeficiente.IsChecked==true)
                {
                    Result = "Deficiente";
                }
                if (rbRegular.IsChecked==true)
                {
                    Result = "Regular";
                }
                if (rbMalo.IsChecked==true)
                {
                    Result = "Malo";
                }
                int Habitaciones =int.Parse(txtHabitac.Text);
                int Pisos = int.Parse(txtPisos.Text);
                string Observ = txtObserv.Text;
                string Habitabilidad ="";
                if (RbSiHab.IsChecked == true)
                {
                    Habitabilidad = "Si";
                }
                else
                {
                    Habitabilidad = "No";
                }
                string Termica = "";
                if (RbSiTerm.IsChecked == true)
                {
                    Termica = "Si";
                }
                else
                {
                    Termica = "No";
                }
                string Fuego = "";
                if (RbSiFuego.IsChecked == true)
                {
                    Fuego = "Si";
                }
                else
                {
                    Fuego = "No";
                }
                int AreaRegist = int.Parse(txtTotalReg.Text);
                int AreaReal = int.Parse(txtTotalReal.Text);
                int SupReg = int.Parse(txtConsReg.Text);
                int SupReal = int.Parse(txtIConstReal.Text);
                int Emisividad = int.Parse(txtEmisividad.Text);
                int TempReflejada = int.Parse(txtTempRefle.Text);
                int Distancia = int.Parse(txtDistancia.Text);
                int Humedad = int.Parse(txtHumedad.Text);
                int TempAtm = int.Parse(txtTempAtmo.Text);
                string RutCliente = txtRutCliente.Text;
                string RutTecnico = txtRutTecnico.Text;
                int Tipo = ((comboBoxItem1)cbTipoV.SelectedItem).id;//Guardo el id
                int Agrup = ((comboBoxItem1)cbTipoAg.SelectedItem).id;//Guardo el id
                int Solicitud = int.Parse(lblIdSolicitud.Content.ToString());
                int Alc = ((comboBoxItem1)cbAlcanta.SelectedItem).id;//Guardo el id
                int Gas = ((comboBoxItem1)cbGas.SelectedItem).id;//Guardo el id
                int Electric = ((comboBoxItem1)cbElectrica.SelectedItem).id;//Guardo el id
                int Agua = ((comboBoxItem1)cbRedAgua.SelectedItem).id;//Guardo el id
                int Potable = ((comboBoxItem1)cbInstAgua.SelectedItem).id;//Guardo el id

                BibliotecaNegocio.Informe c = new BibliotecaNegocio.Informe()
                {
                    num_formulario = Numero,
                    estado_servicio = Estado,
                    fecha_insp = Fecha,
                    resultado = Result,
                    num_habitaciones = Habitaciones,
                    num_pisos = Pisos,
                    observacion = Observ,
                    habitabilidad = Habitabilidad,
                    termica = Termica,
                    fuego = Fuego,
                    area_regis = AreaRegist,
                    area_real = AreaReal,
                    sup_constr_regis = SupReg,
                    sup_constr_real = SupReal,
                    emisividad = Emisividad,
                    temp_reflejada = TempReflejada,
                    distancia = Distancia,
                    humedad_rel = Humedad,
                    temp_atmosferica = TempAtm,
                    rut_cliente = RutCliente,
                    rut_tecnico = RutTecnico,
                    id_tipo = Tipo,
                    id_agrup = Agrup,
                    id_solicitud = Solicitud,
                    id_alcantarillado = Alc,
                    id_gas = Gas,
                    id_electrica = Electric,
                    id_agua = Agua,
                    id_agua_potable = Potable

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
                //-----------------------------------------------------------------------------------------------
                if (resp == true)
                {
                    lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");
                    dtfechaIns.SelectedDate = DateTime.Now;
                    dtfechaSol.SelectedDate = DateTime.Now;
                    txtRutCliente.Focus();
                    rbPrimera.IsChecked = true;
                    rbSegunda.IsChecked = false;
                    rbCierre.IsChecked = false;

                    txtNFormBuscar.Clear();
                    txtRutCliente.Clear();
                    txtRutTecnico.Clear();
                    txtPisos.Text = "0";
                    txtObserv.Clear();
                    txtNombreCliente.Clear();
                    txtHabitac.Text = "0";
                    txtEquipo.Clear();
                    txtDireccion.Clear();
                    txtConstr.Clear();
                    txtNombreTec.Clear();
                    cbAlcanta.SelectedIndex = 0;
                    cbElectrica.SelectedIndex = 0;
                    cbGas.SelectedIndex = 0;
                    cbInstAgua.SelectedIndex = 0;
                    cbRedAgua.SelectedIndex = 0;
                    cbTipoAg.SelectedIndex = 0;
                    cbTipoV.SelectedIndex = 0;

                    RbNoFuego.IsChecked = true;
                    RbSiFuego.IsChecked = false;
                    RbNoHab.IsChecked = true;
                    RbSiHab.IsChecked = false;
                    RbNoTerm.IsChecked = true;
                    RbSiTerm.IsChecked = false;

                    txtTotalReal.Text = "0";
                    txtTotalReg.Text = "0";
                    txtIConstReal.Text = "0";
                    txtConsReg.Text = "0";
                    txtDistancia.Text = "0";
                    txtEmisividad.Text = "0";
                    txtTempRefle.Text = "0";
                    txtTempAtmo.Text = "0";
                    txtHumedad.Text = "0";


                    rbBueno.IsChecked = true;
                    rbDeficiente.IsChecked = false;
                    rbMalo.IsChecked = false;
                    rbRegular.IsChecked = false;

                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    btnActualizar.Visibility = Visibility.Hidden;
                    btnGuardar.Visibility = Visibility.Visible;
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
        public bool Actualizar(BibliotecaNegocio.Informe inf)
        {
            try
            {
                string numero = lblNumForm.Content.ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ACTUALIZAR_INFORME";
                //////////se crea un nuevo de tipo parametro//P_ID//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_NUM_FORMULARIO", OracleDbType.Int64)).Value = numero;
                CMD.Parameters.Add(new OracleParameter("P_ESTADO_SERVICIO", OracleDbType.Varchar2, 20)).Value = inf.estado_servicio;
                CMD.Parameters.Add(new OracleParameter("P_FECHA_INSP", OracleDbType.Date)).Value = inf.fecha_insp;
                CMD.Parameters.Add(new OracleParameter("P_RESULTADO", OracleDbType.Varchar2, 20)).Value = inf.resultado;
                CMD.Parameters.Add(new OracleParameter("P_NUM_HABITACIONES", OracleDbType.Int32)).Value = inf.num_habitaciones;
                CMD.Parameters.Add(new OracleParameter("P_NUM_PISOS", OracleDbType.Int32)).Value = inf.num_pisos;
                CMD.Parameters.Add(new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2, 2000)).Value = inf.observacion;
                CMD.Parameters.Add(new OracleParameter("P_HABITABILIDAD", OracleDbType.Varchar2, 5)).Value = inf.habitabilidad;
                CMD.Parameters.Add(new OracleParameter("P_TERMICA", OracleDbType.Varchar2, 5)).Value = inf.termica;
                CMD.Parameters.Add(new OracleParameter("P_FUEGO", OracleDbType.Varchar2, 5)).Value = inf.fuego;
                CMD.Parameters.Add(new OracleParameter("P_AREA_REGIS", OracleDbType.Int32)).Value = inf.area_regis;
                CMD.Parameters.Add(new OracleParameter("P_AREA_REAL", OracleDbType.Int32)).Value = inf.area_real;
                CMD.Parameters.Add(new OracleParameter("P_SUP_CONSTR_REGIS", OracleDbType.Int32)).Value = inf.sup_constr_regis;
                CMD.Parameters.Add(new OracleParameter("P_SUP_CONSTR_REAL", OracleDbType.Int32)).Value = inf.sup_constr_real;
                CMD.Parameters.Add(new OracleParameter("P_EMISIVIDAD", OracleDbType.Int32)).Value = inf.emisividad;
                CMD.Parameters.Add(new OracleParameter("P_TEMP_REFLEJADA", OracleDbType.Int32)).Value = inf.temp_reflejada;
                CMD.Parameters.Add(new OracleParameter("P_DISTANCIA", OracleDbType.Int32)).Value = inf.distancia;
                CMD.Parameters.Add(new OracleParameter("P_HUMEDAD_REL", OracleDbType.Int32)).Value = inf.humedad_rel;
                CMD.Parameters.Add(new OracleParameter("P_TEMP_ATMOSFERICA", OracleDbType.Int32)).Value = inf.temp_atmosferica;
                CMD.Parameters.Add(new OracleParameter("P_RUT_CLIENTE", OracleDbType.Varchar2, 20)).Value = inf.rut_cliente;
                CMD.Parameters.Add(new OracleParameter("P_RUT_TECNICO", OracleDbType.Varchar2, 20)).Value = inf.rut_tecnico;
                CMD.Parameters.Add(new OracleParameter("P_ID_TIPO", OracleDbType.Int32)).Value = inf.id_tipo;
                CMD.Parameters.Add(new OracleParameter("P_ID_AGRUP", OracleDbType.Int32)).Value = inf.id_agrup;
                CMD.Parameters.Add(new OracleParameter("P_ID_SOLICITUD", OracleDbType.Int32)).Value = inf.id_solicitud;
                CMD.Parameters.Add(new OracleParameter("P_ID_ALCANTARILLADO", OracleDbType.Int32)).Value = inf.id_alcantarillado;
                CMD.Parameters.Add(new OracleParameter("P_ID_GAS", OracleDbType.Int32)).Value = inf.id_gas;
                CMD.Parameters.Add(new OracleParameter("P_ID_ELECTRICA", OracleDbType.Int32)).Value = inf.id_electrica;
                CMD.Parameters.Add(new OracleParameter("P_ID_AGUA", OracleDbType.Int32)).Value = inf.id_agua;
                CMD.Parameters.Add(new OracleParameter("P_ID_AGUA_POTABLE", OracleDbType.Int32)).Value = inf.id_agua_potable;

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
        //-----------Botón Modificar-------------------------------------
        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long Numero = long.Parse(lblNumForm.Content.ToString());
                string Estado = "";
                if (rbPrimera.IsChecked == true)
                {
                    Estado = "Primera Revisión";
                }
                if (rbSegunda.IsChecked == true)
                {
                    Estado = "Segunda Revisión";
                }
                if (rbCierre.IsChecked == true)
                {
                    Estado = "Cierre";
                }
                DateTime Fecha = dtfechaIns.SelectedDate.Value.Date;
                string Result = "";
                if (rbBueno.IsChecked == true)
                {
                    Result = "Bueno";
                }
                if (rbDeficiente.IsChecked == true)
                {
                    Result = "Deficiente";
                }
                if (rbRegular.IsChecked == true)
                {
                    Result = "Regular";
                }
                if (rbMalo.IsChecked == true)
                {
                    Result = "Malo";
                }
                int Habitaciones = int.Parse(txtHabitac.Text);
                int Pisos = int.Parse(txtPisos.Text);
                string Observ = txtObserv.Text;
                string Habitabilidad = "";
                if (RbSiHab.IsChecked == true)
                {
                    Habitabilidad = "Si";
                }
                else
                {
                    Habitabilidad = "No";
                }
                string Termica = "";
                if (RbSiTerm.IsChecked == true)
                {
                    Termica = "Si";
                }
                else
                {
                    Termica = "No";
                }
                string Fuego = "";
                if (RbSiFuego.IsChecked == true)
                {
                    Fuego = "Si";
                }
                else
                {
                    Fuego = "No";
                }
                int AreaRegist = int.Parse(txtTotalReg.Text);
                int AreaReal = int.Parse(txtTotalReal.Text);
                int SupReg = int.Parse(txtConsReg.Text);
                int SupReal = int.Parse(txtIConstReal.Text);
                int Emisividad = int.Parse(txtEmisividad.Text);
                int TempReflejada = int.Parse(txtTempRefle.Text);
                int Distancia = int.Parse(txtDistancia.Text);
                int Humedad = int.Parse(txtHumedad.Text);
                int TempAtm = int.Parse(txtTempAtmo.Text);
                string RutCliente = txtRutCliente.Text;
                string RutTecnico = txtRutTecnico.Text;
                int Tipo = ((comboBoxItem1)cbTipoV.SelectedItem).id;//Guardo el id
                int Agrup = ((comboBoxItem1)cbTipoAg.SelectedItem).id;//Guardo el id
                int Solicitud = int.Parse(lblIdSolicitud.Content.ToString());
                int Alc = ((comboBoxItem1)cbAlcanta.SelectedItem).id;//Guardo el id
                int Gas = ((comboBoxItem1)cbGas.SelectedItem).id;//Guardo el id
                int Electric = ((comboBoxItem1)cbElectrica.SelectedItem).id;//Guardo el id
                int Agua = ((comboBoxItem1)cbRedAgua.SelectedItem).id;//Guardo el id
                int Potable = ((comboBoxItem1)cbInstAgua.SelectedItem).id;//Guardo el id

                BibliotecaNegocio.Informe c = new BibliotecaNegocio.Informe()
                {
                    num_formulario = Numero,
                    estado_servicio = Estado,
                    fecha_insp = Fecha,
                    resultado = Result,
                    num_habitaciones = Habitaciones,
                    num_pisos = Pisos,
                    observacion = Observ,
                    habitabilidad = Habitabilidad,
                    termica = Termica,
                    fuego = Fuego,
                    area_regis = AreaRegist,
                    area_real = AreaReal,
                    sup_constr_regis = SupReg,
                    sup_constr_real = SupReal,
                    emisividad = Emisividad,
                    temp_reflejada = TempReflejada,
                    distancia = Distancia,
                    humedad_rel = Humedad,
                    temp_atmosferica = TempAtm,
                    rut_cliente = RutCliente,
                    rut_tecnico = RutTecnico,
                    id_tipo = Tipo,
                    id_agrup = Agrup,
                    id_solicitud = Solicitud,
                    id_alcantarillado = Alc,
                    id_gas = Gas,
                    id_electrica = Electric,
                    id_agua = Agua,
                    id_agua_potable = Potable

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
                //-----------------------------------------------------------------------------------------------
                if (resp == true)
                {
                    lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");
                    dtfechaIns.SelectedDate = DateTime.Now;
                    dtfechaSol.SelectedDate = DateTime.Now;
                    txtRutCliente.Focus();
                    rbPrimera.IsChecked = true;
                    rbSegunda.IsChecked = false;
                    rbCierre.IsChecked = false;

                    txtNFormBuscar.Clear();
                    txtRutCliente.Clear();
                    txtRutTecnico.Clear();
                    txtPisos.Text = "0";
                    txtObserv.Clear();
                    txtNombreCliente.Clear();
                    txtHabitac.Text = "0";
                    txtEquipo.Clear();
                    txtDireccion.Clear();
                    txtConstr.Clear();
                    txtNombreTec.Clear();
                    cbAlcanta.SelectedIndex = 0;
                    cbElectrica.SelectedIndex = 0;
                    cbGas.SelectedIndex = 0;
                    cbInstAgua.SelectedIndex = 0;
                    cbRedAgua.SelectedIndex = 0;
                    cbTipoAg.SelectedIndex = 0;
                    cbTipoV.SelectedIndex = 0;

                    RbNoFuego.IsChecked = true;
                    RbSiFuego.IsChecked = false;
                    RbNoHab.IsChecked = true;
                    RbSiHab.IsChecked = false;
                    RbNoTerm.IsChecked = true;
                    RbSiTerm.IsChecked = false;

                    txtTotalReal.Text = "0";
                    txtTotalReg.Text = "0";
                    txtIConstReal.Text = "0";
                    txtConsReg.Text = "0";
                    txtDistancia.Text = "0";
                    txtEmisividad.Text = "0";
                    txtTempRefle.Text = "0";
                    txtTempAtmo.Text = "0";
                    txtHumedad.Text = "0";


                    rbBueno.IsChecked = true;
                    rbDeficiente.IsChecked = false;
                    rbMalo.IsChecked = false;
                    rbRegular.IsChecked = false;

                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    btnActualizar.Visibility = Visibility.Hidden;
                    btnGuardar.Visibility = Visibility.Visible;
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
        //---------- Buscar Informe por Número------------------------------------------
        private async void btnBuscarForm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string numero = txtNFormBuscar.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Informe.ListaInforme> clie = new List<BibliotecaNegocio.Informe.ListaInforme>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_INFORME_NUM";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_NUM_FORMULARIO", OracleDbType.Int32)).Value = numero;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Informe.ListaInforme c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Informe.ListaInforme();

                    c.Numero = int.Parse(reader[0].ToString());
                    c.Estado = reader[1].ToString();
                    c.Fecha = DateTime.Parse(reader[2].ToString());
                    c.Resultado = reader[3].ToString();
                    c.Habitaciones = int.Parse(reader[4].ToString());
                    c.Pisos = int.Parse(reader[5].ToString());
                    c.Observacion = reader[6].ToString();
                    c.habitabilidad = reader[7].ToString();
                    c.termica = reader[8].ToString();
                    c.fuego = reader[9].ToString();
                    c.area_regis = int.Parse(reader[10].ToString());
                    c.area_real = int.Parse(reader[11].ToString());
                    c.sup_constr_regis = int.Parse(reader[12].ToString());
                    c.sup_constr_real = int.Parse(reader[13].ToString());
                    c.emisividad = int.Parse(reader[14].ToString());
                    c.temp_reflejada = int.Parse(reader[15].ToString());
                    c.distancia = int.Parse(reader[16].ToString());
                    c.humedad_rel = int.Parse(reader[17].ToString());
                    c.temp_atmosferica = int.Parse(reader[18].ToString());
                    c.RutCliente = reader[19].ToString();
                    c.RutTecnico = reader[20].ToString();
                    c.TipoVivienda = reader[21].ToString();
                    c.Agrupamiento = reader[22].ToString();
                    c.Solicitud = int.Parse(reader[23].ToString());
                    c.Alcantarillado = reader[24].ToString();
                    c.Gas = reader[25].ToString();
                    c.Electrica = reader[26].ToString();
                    c.Agua = reader[27].ToString();
                    c.AguaPotable = reader[28].ToString();
                    c.Dirección = reader[29].ToString();
                    c.Constructora = reader[30].ToString();
                    c.Fecha_solicitud = DateTime.Parse(reader[31].ToString());
                    c.Cliente = reader[32].ToString();
                    c.Técnico = reader[33].ToString();
                    c.Equipo = reader[34].ToString();

                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    lblNumForm.Content = c.Numero;
                    if (c.Estado == "Primera Revisión")
                    {
                        rbPrimera.IsChecked = true;
                    }
                    else
                    {
                        rbPrimera.IsChecked = false;
                    }
                    if (c.Estado == "Segunda Revisión")
                    {
                        rbSegunda.IsChecked = true;
                    }
                    else
                    {
                        rbSegunda.IsChecked = false;
                    }
                    if (c.Estado == "Cierre")
                    {
                        rbCierre.IsChecked = true;
                    }
                    else
                    {
                        rbCierre.IsChecked = false;
                    }
                    dtfechaIns.Text = c.Fecha.ToString();
                    if (c.Resultado == "Bueno")
                    {
                        rbBueno.IsChecked = true;
                    }
                    else
                    {
                        rbBueno.IsChecked = false;
                    }
                    if (c.Resultado == "Regular")
                    {
                        rbRegular.IsChecked = true;
                    }
                    else
                    {
                        rbRegular.IsChecked = false;
                    }
                    if (c.Resultado == "Malo")
                    {
                        rbMalo.IsChecked = true;
                    }
                    else
                    {
                        rbMalo.IsChecked = false;
                    }
                    if (c.Resultado == "Deficiente")
                    {
                        rbDeficiente.IsChecked = true;
                    }
                    else
                    {
                        rbDeficiente.IsChecked = false;
                    }                

                    txtHabitac.Text = c.Habitaciones.ToString();
                    txtPisos.Text = c.Pisos.ToString();
                    txtObserv.Text = c.Observacion;
                    if (c.habitabilidad == "Si")
                    {
                        RbSiHab.IsChecked = true;
                        RbNoHab.IsChecked = false;
                    }
                    else
                    {
                            RbNoHab.IsChecked = true;
                            RbSiHab.IsChecked = false;                     
                    }
                    if (c.termica == "Si")
                    {
                        RbSiTerm.IsChecked = true;
                        RbNoTerm.IsChecked = false;
                    }
                    else
                    {
                            RbNoTerm.IsChecked = true;
                            RbSiTerm.IsChecked = false;

                    }
                    if (c.fuego == "Si")
                    {
                        RbSiFuego.IsChecked = true;
                        RbNoFuego.IsChecked = false;
                    }
                    else
                    {
                            RbNoFuego.IsChecked = true;
                            RbSiFuego.IsChecked = false;
                        
                    }
                   
                    txtTotalReg.Text = c.area_regis.ToString();
                    txtTotalReal.Text = c.area_real.ToString();
                    txtConsReg.Text = c.sup_constr_regis.ToString();
                    txtIConstReal.Text = c.sup_constr_real.ToString();
                    txtEmisividad.Text = c.emisividad.ToString();
                    txtTempRefle.Text = c.temp_reflejada.ToString();
                    txtDistancia.Text = c.distancia.ToString();
                    txtHumedad.Text = c.humedad_rel.ToString();
                    txtTempAtmo.Text = c.temp_atmosferica.ToString();
                    txtRutCliente.Text = c.RutCliente.ToString();
                    txtRutTecnico.Text = c.RutTecnico.ToString();
                    cbTipoV.Text = c.TipoVivienda;
                    cbTipoAg.Text = c.Agrupamiento;
                    lblIdSolicitud.Content = c.Solicitud;
                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    cbAlcanta.Text = c.Alcantarillado;
                    cbGas.Text = c.Gas;
                    cbElectrica.Text = c.Electrica;
                    cbRedAgua.Text = c.Agua;
                    cbInstAgua.Text = c.AguaPotable;
                    txtDireccion.Text = c.Dirección;
                    txtConstr.Text = c.Constructora;
                    dtfechaSol.Text = c.Fecha_solicitud.ToString();
                    txtNombreCliente.Text = c.Cliente;
                    txtNombreTec.Text = c.Técnico;
                    txtEquipo.Text = c.Equipo;


                    btnActualizar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;

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
        
        //--------- Buscar Cliente y solicitud------------------------------------------------------
        private async void btnBuscarRutC1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rut = txtRutCliente.Text;
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
                CMD.CommandText = "SP_BUSCAR_CLI";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2,20)).Value = rut;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Solicitud.ListaSolicitud c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Solicitud.ListaSolicitud();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Fecha = DateTime.Parse(reader[2].ToString());
                    c.id_solicitud = int.Parse(reader[3].ToString());
                    c.Direccion = reader[4].ToString();
                    c.Constructora = reader[5].ToString();

                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    txtRutCliente.Text = c.Rut;
                    txtNombreCliente.Text = c.Nombre;
                    dtfechaSol.Text = c.Fecha.ToString();
                    lblIdSolicitud.Content = c.id_solicitud;
                    txtDireccion.Text = c.Direccion;
                    txtConstr.Text = c.Constructora;
                    
                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    txtRutCliente.IsEnabled = false;
                    
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

        //-----------Buscar Técnico-----------------------------------------------------
        private async void btnBuscarRutT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rut = txtRutTecnico.Text;
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");
                //nucna una instruccion sql en el sistema solo en base de datos
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Informe.ListaInforme> clie = new List<BibliotecaNegocio.Informe.ListaInforme>();
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
                BibliotecaNegocio.Informe.ListaInforme c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Informe.ListaInforme();

                    c.RutTecnico = reader[0].ToString();
                    c.Técnico = reader[1].ToString();
                    c.Equipo = reader[2].ToString();
                    
                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    txtRutTecnico.Text = c.RutTecnico;
                    txtNombreTec.Text = c.Técnico;
                    txtEquipo.Text = c.Equipo;
                    

                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    txtRutTecnico.IsEnabled =false;

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
        //----------Buscar para el traspasar (Listado de Informes)------------------------------

        //----------Buscar para el traspasar (Listado de Clientes)------------------------------
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
        //----------Buscar para el traspasar (Listado de Técnico)------------------------------






    }
}
