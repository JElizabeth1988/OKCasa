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
    /// Lógica de interacción para FormularioInspeccion.xaml
    /// </summary>
    public partial class FormularioInspeccion : MetroWindow
    {
        OracleConnection conn = null;
        //---------Main Window------------------------------------------
        public FormularioInspeccion()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
            this.DataContext = this;
            txtRutCliente.Focus();

            lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");//Fecha y hora actual

            btnActualizar.Visibility = Visibility.Hidden;
            btnEliminar.Visibility = Visibility.Hidden;

            //---------llenar el combo box ---------------------------------------
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

            foreach (Comuna item in new Comuna().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_comuna;
                cb.nombre = item.nombre;
                cbComuna.Items.Add(cb);
            }
            //_------------------------------------------------

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
            cbComuna.SelectedIndex = 0;

            lblIdSolicitud.Visibility = Visibility.Hidden;
        }
        //--------BOTÓN CANCELAR-----------------------------------------------
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
            ListadoInspectores lii = new ListadoInspectores(this);
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
        //------------Método lompiar
        private void Limpiar()
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

            RbNoFuego.IsChecked = false;
            RbSiFuego.IsChecked = true;
            RbNoHab.IsChecked = false;
            RbSiHab.IsChecked = true;
            RbNoTerm.IsChecked = false;
            RbSiTerm.IsChecked = true;

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
            btnEliminar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;
            txtRutTecnico.IsEnabled = true;
            txtRutCliente.IsEnabled = true;
        }
        //-------------BOTÓN LIMPIAR----------------------------
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //---------------------CRUD-------------------------------------------------
        //--------------------------------------------------------------------------
        //----------------Método agregar----------------------
        public bool Agregar(BibliotecaNegocio.Informe inf)
        {
            try
            {
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
                CMD.Parameters.Add(new OracleParameter("P_DIRECCION_VIVIENDA", OracleDbType.Varchar2, 100)).Value = inf.direccion_vivienda;
                CMD.Parameters.Add(new OracleParameter("P_CONSTRUCTORA", OracleDbType.Varchar2, 100)).Value = inf.constructora;
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
                CMD.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleDbType.Int32)).Value = inf.id_comuna;
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

        //--------------------------BOTÓN GUARDAR----------------------------
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
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
                string Direccion = txtDireccion.Text;
                string Constr = txtConstr.Text;
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
                int Comuna = ((comboBoxItem1)cbComuna.SelectedItem).id;//Guardo el id
                BibliotecaNegocio.Informe c = new BibliotecaNegocio.Informe()
                {
                    num_formulario = Numero,
                    estado_servicio = Estado,
                    fecha_insp = Fecha,
                    resultado = Result,
                    num_habitaciones = Habitaciones,
                    num_pisos = Pisos,
                    direccion_vivienda = Direccion,
                    constructora = Constr,
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
                    id_agua_potable = Potable,
                    id_comuna = Comuna

                };
                bool resp = Agregar(c);
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));
                
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
                if (resp == true)
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
        public bool Actualizar(BibliotecaNegocio.Informe inf)
        {
            try
            {
                string numero = lblNumForm.Content.ToString();
                
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
                CMD.Parameters.Add(new OracleParameter("P_DIRECCION_VIVIENDA", OracleDbType.Varchar2, 100)).Value = inf.direccion_vivienda;
                CMD.Parameters.Add(new OracleParameter("P_CONSTRUCTORA", OracleDbType.Varchar2,100)).Value = inf.constructora;
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
                CMD.Parameters.Add(new OracleParameter("P_ID_COMUNA", OracleDbType.Int32)).Value = inf.id_comuna;

                
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
                string Direccion = txtDireccion.Text;
                string Constructora = txtConstr.Text;
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
                int Comuna = ((comboBoxItem1)cbComuna.SelectedItem).id;//Guardo el id

                BibliotecaNegocio.Informe c = new BibliotecaNegocio.Informe()
                {
                    num_formulario = Numero,
                    estado_servicio = Estado,
                    fecha_insp = Fecha,
                    resultado = Result,
                    num_habitaciones = Habitaciones,
                    num_pisos = Pisos,
                    direccion_vivienda = Direccion,
                    constructora= Constructora,
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
                    id_agua_potable = Potable,
                    id_comuna = Comuna

                };
                bool resp = Actualizar(c);
                await this.ShowMessageAsync("Mensaje:",
                     string.Format(resp ? "Actualizado" : "No Actualizado"));
               
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
                if (resp == true)
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
        //---------- Buscar Informe por Número------------------------------------------
        private async void btnBuscarForm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long numero = long.Parse(txtNFormBuscar.Text);
               
                
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Informe.ListaInforme> clie = new List<BibliotecaNegocio.Informe.ListaInforme>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_INFORME_NUM";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_NUM_FORMULARIO", OracleDbType.Int64)).Value = numero;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Informe.ListaInforme c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Informe.ListaInforme();

                    c.Numero = long.Parse(reader[0].ToString());
                    c.Estado_Servicio = reader[1].ToString();
                    c.Fecha_Inspeccion = DateTime.Parse(reader[2].ToString());
                    c.Rut_Cliente = reader[3].ToString();
                    c.Cliente = reader[4].ToString();
                    c.Dirección = reader[5].ToString();
                    c.Comuna = reader[6].ToString();
                    c.Constructora = reader[7].ToString();
                    c.N_Habitaciones = int.Parse(reader[8].ToString());
                    c.N_Pisos = int.Parse(reader[9].ToString());
                    c.Tipo_Agrupamiento = reader[10].ToString();
                    c.Tipo_Vivienda = reader[11].ToString();
                    c.Rut_Tecnico = reader[12].ToString();
                    c.Técnico = reader[13].ToString();
                    c.Equipo = reader[14].ToString();
                    c.Resultado = reader[15].ToString();
                    c.Observacion = reader[16].ToString();
                    c.habitabilidad = reader[17].ToString();
                    c.Resistencia_Térmica = reader[18].ToString();
                    c.Resistencia_Fuego = reader[19].ToString();
                    c.Area_regis = int.Parse(reader[20].ToString());
                    c.Area_real = int.Parse(reader[21].ToString());
                    c.sup_construida_reg = int.Parse(reader[22].ToString());
                    c.Sup_construida_real = int.Parse(reader[23].ToString());
                    c.Emisividad = int.Parse(reader[24].ToString());
                    c.Temp_reflejada = int.Parse(reader[25].ToString());
                    c.Distancia = int.Parse(reader[26].ToString());
                    c.Humedad_relativa = int.Parse(reader[27].ToString());
                    c.temp_atmosferica = int.Parse(reader[28].ToString());
                    c.Inst_Agua_Potable = reader[29].ToString();
                    c.Inst_Alcantarillado = reader[30].ToString();
                    c.Inst_Gas = reader[31].ToString();
                    c.Inst_Electrica = reader[32].ToString();
                    c.Red_Agua = reader[33].ToString();
                    c.Solicitud = int.Parse(reader[34].ToString());
                    c.Fecha_solicitud = DateTime.Parse(reader[35].ToString());

                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    lblNumForm.Content = c.Numero;
                    if (c.Estado_Servicio == "Primera Revisión")
                    {
                        rbPrimera.IsChecked = true;
                    }
                    else
                    {
                        rbPrimera.IsChecked = false;
                    }
                    if (c.Estado_Servicio == "Segunda Revisión")
                    {
                        rbSegunda.IsChecked = true;
                    }
                    else
                    {
                        rbSegunda.IsChecked = false;
                    }
                    if (c.Estado_Servicio == "Cierre")
                    {
                        rbCierre.IsChecked = true;
                    }
                    else
                    {
                        rbCierre.IsChecked = false;
                    }
                    dtfechaIns.Text = c.Fecha_Inspeccion.ToString();
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

                    txtHabitac.Text = c.N_Habitaciones.ToString();
                    txtPisos.Text = c.N_Pisos.ToString();
                    txtDireccion.Text = c.Dirección;
                    txtConstr.Text = c.Constructora;
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
                    if (c.Resistencia_Térmica == "Si")
                    {
                        RbSiTerm.IsChecked = true;
                        RbNoTerm.IsChecked = false;
                    }
                    else
                    {
                            RbNoTerm.IsChecked = true;
                            RbSiTerm.IsChecked = false;

                    }
                    if (c.Resistencia_Fuego == "Si")
                    {
                        RbSiFuego.IsChecked = true;
                        RbNoFuego.IsChecked = false;
                    }
                    else
                    {
                            RbNoFuego.IsChecked = true;
                            RbSiFuego.IsChecked = false;
                        
                    }
                   
                    txtTotalReg.Text = c.Area_regis.ToString();
                    txtTotalReal.Text = c.Area_real.ToString();
                    txtConsReg.Text = c.sup_construida_reg.ToString();
                    txtIConstReal.Text = c.Sup_construida_real.ToString();
                    txtEmisividad.Text = c.Emisividad.ToString();
                    txtTempRefle.Text = c.Temp_reflejada.ToString();
                    txtDistancia.Text = c.Distancia.ToString();
                    txtHumedad.Text = c.Humedad_relativa.ToString();
                    txtTempAtmo.Text = c.temp_atmosferica.ToString();
                    txtRutCliente.Text = c.Rut_Cliente.ToString();
                    txtRutTecnico.Text = c.Rut_Tecnico.ToString();
                    cbTipoV.Text = c.Tipo_Vivienda;
                    cbTipoAg.Text = c.Tipo_Agrupamiento;
                    lblIdSolicitud.Content = c.Solicitud;
                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    cbAlcanta.Text = c.Inst_Alcantarillado;
                    cbGas.Text = c.Inst_Gas;
                    cbElectrica.Text = c.Inst_Electrica;
                    cbRedAgua.Text = c.Red_Agua;
                    cbInstAgua.Text = c.Inst_Agua_Potable;
                    cbComuna.Text = c.Comuna;
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
                    c.Comuna = reader[6].ToString();

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
                    cbComuna.Text = c.Comuna;
                    
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

                    c.Rut_Tecnico = reader[0].ToString();
                    c.Técnico = reader[1].ToString();
                    c.Equipo = reader[2].ToString();
                    
                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    txtRutTecnico.Text = c.Rut_Tecnico;
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
        public async void BuscarInf()
        {
            try
            {
                long numero = long.Parse(txtNFormBuscar.Text);
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Informe.ListaInforme> clie = new List<BibliotecaNegocio.Informe.ListaInforme>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_INFORME_NUM";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_NUM_FORMULARIO", OracleDbType.Int64)).Value = numero;
                CMD.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;

                //se abre la conexion
                conn.Open();
                OracleDataReader reader = CMD.ExecuteReader();
                BibliotecaNegocio.Informe.ListaInforme c = null;
                while (reader.Read())
                {
                    c = new BibliotecaNegocio.Informe.ListaInforme();

                    c.Numero = long.Parse(reader[0].ToString());
                    c.Estado_Servicio = reader[1].ToString();
                    c.Fecha_Inspeccion = DateTime.Parse(reader[2].ToString());
                    c.Rut_Cliente = reader[3].ToString();
                    c.Cliente = reader[4].ToString();
                    c.Dirección = reader[5].ToString();
                    c.Comuna = reader[6].ToString();
                    c.Constructora = reader[7].ToString();
                    c.N_Habitaciones = int.Parse(reader[8].ToString());
                    c.N_Pisos = int.Parse(reader[9].ToString());
                    c.Tipo_Agrupamiento = reader[10].ToString();
                    c.Tipo_Vivienda = reader[11].ToString();
                    c.Rut_Tecnico = reader[12].ToString();
                    c.Técnico = reader[13].ToString();
                    c.Equipo = reader[14].ToString();
                    c.Resultado = reader[15].ToString();
                    c.Observacion = reader[16].ToString();
                    c.habitabilidad = reader[17].ToString();
                    c.Resistencia_Térmica = reader[18].ToString();
                    c.Resistencia_Fuego = reader[19].ToString();
                    c.Area_regis = int.Parse(reader[20].ToString());
                    c.Area_real = int.Parse(reader[21].ToString());
                    c.sup_construida_reg = int.Parse(reader[22].ToString());
                    c.Sup_construida_real = int.Parse(reader[23].ToString());
                    c.Emisividad = int.Parse(reader[24].ToString());
                    c.Temp_reflejada = int.Parse(reader[25].ToString());
                    c.Distancia = int.Parse(reader[26].ToString());
                    c.Humedad_relativa = int.Parse(reader[27].ToString());
                    c.temp_atmosferica = int.Parse(reader[28].ToString());
                    c.Inst_Agua_Potable = reader[29].ToString();
                    c.Inst_Alcantarillado = reader[30].ToString();
                    c.Inst_Gas = reader[31].ToString();
                    c.Inst_Electrica = reader[32].ToString();
                    c.Red_Agua = reader[33].ToString();
                    c.Solicitud = int.Parse(reader[34].ToString());
                    c.Fecha_solicitud = DateTime.Parse(reader[35].ToString());

                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    lblNumForm.Content = c.Numero;
                    if (c.Estado_Servicio == "Primera Revisión")
                    {
                        rbPrimera.IsChecked = true;
                    }
                    else
                    {
                        rbPrimera.IsChecked = false;
                    }
                    if (c.Estado_Servicio == "Segunda Revisión")
                    {
                        rbSegunda.IsChecked = true;
                    }
                    else
                    {
                        rbSegunda.IsChecked = false;
                    }
                    if (c.Estado_Servicio == "Cierre")
                    {
                        rbCierre.IsChecked = true;
                    }
                    else
                    {
                        rbCierre.IsChecked = false;
                    }
                    dtfechaIns.Text = c.Fecha_Inspeccion.ToString();
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

                    txtHabitac.Text = c.N_Habitaciones.ToString();
                    txtPisos.Text = c.N_Pisos.ToString();
                    txtDireccion.Text = c.Dirección;
                    txtConstr.Text = c.Constructora;
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
                    if (c.Resistencia_Térmica == "Si")
                    {
                        RbSiTerm.IsChecked = true;
                        RbNoTerm.IsChecked = false;
                    }
                    else
                    {
                        RbNoTerm.IsChecked = true;
                        RbSiTerm.IsChecked = false;

                    }
                    if (c.Resistencia_Fuego == "Si")
                    {
                        RbSiFuego.IsChecked = true;
                        RbNoFuego.IsChecked = false;
                    }
                    else
                    {
                        RbNoFuego.IsChecked = true;
                        RbSiFuego.IsChecked = false;

                    }

                    txtTotalReg.Text = c.Area_regis.ToString();
                    txtTotalReal.Text = c.Area_real.ToString();
                    txtConsReg.Text = c.sup_construida_reg.ToString();
                    txtIConstReal.Text = c.Sup_construida_real.ToString();
                    txtEmisividad.Text = c.Emisividad.ToString();
                    txtTempRefle.Text = c.Temp_reflejada.ToString();
                    txtDistancia.Text = c.Distancia.ToString();
                    txtHumedad.Text = c.Humedad_relativa.ToString();
                    txtTempAtmo.Text = c.temp_atmosferica.ToString();
                    txtRutCliente.Text = c.Rut_Cliente.ToString();
                    txtRutTecnico.Text = c.Rut_Tecnico.ToString();
                    cbTipoV.Text = c.Tipo_Vivienda;
                    cbTipoAg.Text = c.Tipo_Agrupamiento;
                    lblIdSolicitud.Content = c.Solicitud;
                    lblIdSolicitud.Visibility = Visibility.Hidden;
                    cbAlcanta.Text = c.Inst_Alcantarillado;
                    cbGas.Text = c.Inst_Gas;
                    cbElectrica.Text = c.Inst_Electrica;
                    cbRedAgua.Text = c.Red_Agua;
                    cbInstAgua.Text = c.Inst_Agua_Potable;
                    cbComuna.Text = c.Comuna;
                    dtfechaSol.Text = c.Fecha_solicitud.ToString();
                    txtNombreCliente.Text = c.Cliente;
                    txtNombreTec.Text = c.Técnico;
                    txtEquipo.Text = c.Equipo;


                    btnActualizar.Visibility = Visibility.Visible;
                    btnEliminar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;
                    txtRutCliente.IsEnabled = false;
                    txtRutTecnico.IsEnabled = false;

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

        //----------Buscar para el traspasar (Listado de Clientes)------------------------------
        public async void Buscar()
        {
            try
            {
                string rut = txtRutCliente.Text;
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Solicitud.ListaSolicitud> clie = new List<BibliotecaNegocio.Solicitud.ListaSolicitud>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_CLI";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2, 20)).Value = rut;
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
                    c.Comuna = reader[6].ToString();

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
                    cbComuna.Text = c.Comuna;

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
        //----------Buscar para el traspasar (Listado de Técnico)------------------------------
        public async void BuscarTec()
        {
            try
            {
                string rut = txtRutTecnico.Text;

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
                    c = new BibliotecaNegocio.Tecnico.ListaTecnico2 ();

                    c.Rut = reader[0].ToString();
                    c.Nombre = reader[1].ToString();
                    c.Equipo = reader[2].ToString();

                    clie.Add(c);

                }
                conn.Close();


                if (c != null)
                {
                    txtRutTecnico.Text = c.Rut;
                    txtNombreTec.Text = c.Nombre;
                    txtEquipo.Text = c.Equipo;

                    txtRutTecnico.IsEnabled = false;

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
        //-----------.--Método Eliminar-----------------------------------
        public bool Eliminar(BibliotecaNegocio.Informe client)
        {
            try
            {
                long numero = long.Parse(lblNumForm.Content.ToString());
                //long numero = client.num_formulario;
                OracleCommand CMD = new OracleCommand();
                //que tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_ELIMINAR_INFORME";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_NUM_FORMULARIO", OracleDbType.Int64)).Value = numero;
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
                Logger.Mensaje(ex.Message);

            }
        }

        //--------------Botón Eliminar------------------------------------
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BibliotecaNegocio.Informe cli = new BibliotecaNegocio.Informe();
                long numero = long.Parse(lblNumForm.Content.ToString());
                var x = await this.ShowMessageAsync("Eliminar Datos: ",
                         "¿Está Seguro de eliminar el informe n° " + numero + "?",
                        MessageDialogStyle.AffirmativeAndNegative);
                if (x == MessageDialogResult.Affirmative)
                {
                    bool resp = Eliminar(cli);
                    if (resp == true)
                    {
                        await this.ShowMessageAsync("Éxito:",
                          string.Format("Informe Eliminado"));
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
    }
}
