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
    
    public partial class ListadoFormulario : MetroWindow
    {
        FormularioInspeccion formu;
        OracleConnection conn = null;
        //--------------MainWindow-----------------------------------------------
        public ListadoFormulario()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();//Conexión
            txtFiltroNumero.Focus();
            btnPasar.Visibility = Visibility.Hidden;
            CargarGrilla();
        }

        //-----------------Cargar Grilla--------------------
        private void CargarGrilla()
        {
            try
            {
                List<BibliotecaNegocio.Informe.ListaInforme> lista = new List<BibliotecaNegocio.Informe.ListaInforme>();
                //se crea un comando de oracle
                OracleCommand cmd = new OracleCommand();
                //se ejecutan los comandos de procedimeintos
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //conexion
                cmd.Connection = conn;
                //procedimiento
                cmd.CommandText = "SP_LISTAR_INFORME";
                //Se agrega el parametro de salida
                cmd.Parameters.Add(new OracleParameter("INFORMES", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                //se abre la conexion
                conn.Open();
                //se crea un reader
                OracleDataReader dr = cmd.ExecuteReader();
                //mientras lea
                while (dr.Read())
                {
                    BibliotecaNegocio.Informe.ListaInforme i = new BibliotecaNegocio.Informe.ListaInforme();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    i.Numero = long.Parse(dr.GetValue(0).ToString());
                    i.Estado_Servicio = dr.GetValue(1).ToString();
                    i.Fecha_Inspeccion = DateTime.Parse(dr.GetValue(2).ToString());
                    i.Rut_Cliente = dr.GetValue(3).ToString();
                    i.Cliente = dr.GetValue(4).ToString();
                    i.Dirección = dr.GetValue(5).ToString();
                    i.Comuna = dr.GetValue(6).ToString();
                    i.Constructora = dr.GetValue(7).ToString();
                    i.N_Habitaciones = int.Parse(dr.GetValue(8).ToString());
                    i.N_Pisos = int.Parse(dr.GetValue(9).ToString());
                    i.Tipo_Agrupamiento = dr.GetValue(10).ToString();
                    i.Tipo_Vivienda = dr.GetValue(11).ToString();
                    i.Rut_Tecnico = dr.GetValue(12).ToString();
                    i.Técnico = dr.GetValue(13).ToString();
                    i.Equipo = dr.GetValue(14).ToString();
                    i.Resultado = dr.GetValue(15).ToString();
                    i.Observacion = dr.GetValue(16).ToString();
                    i.habitabilidad = dr.GetValue(17).ToString();
                    i.Resistencia_Térmica = dr.GetValue(18).ToString();
                    i.Resistencia_Fuego = dr.GetValue(19).ToString();
                    i.Area_regis = int.Parse(dr.GetValue(20).ToString());
                    i.Area_real = int.Parse(dr.GetValue(21).ToString());
                    i.sup_construida_reg = int.Parse(dr.GetValue(22).ToString());
                    i.Sup_construida_real = int.Parse(dr.GetValue(23).ToString());
                    i.Emisividad = int.Parse(dr.GetValue(24).ToString());
                    i.Temp_reflejada = int.Parse(dr.GetValue(25).ToString());
                    i.Distancia = int.Parse(dr.GetValue(26).ToString());
                    i.Humedad_relativa = int.Parse(dr.GetValue(27).ToString());
                    i.temp_atmosferica = int.Parse(dr.GetValue(28).ToString());
                    i.Inst_Agua_Potable = dr.GetValue(29).ToString();
                    i.Inst_Alcantarillado = dr.GetValue(30).ToString();
                    i.Inst_Gas = dr.GetValue(31).ToString();
                    i.Inst_Electrica = dr.GetValue(32).ToString();
                    i.Red_Agua = dr.GetValue(33).ToString();
                    i.Solicitud = int.Parse(dr.GetValue(34).ToString());
                    i.Fecha_solicitud = DateTime.Parse(dr.GetValue(35).ToString());

                    lista.Add(i);
                }
                //Cerrar conexión
                conn.Close();

                dgLista.ItemsSource = lista;
            }
            catch (Exception ex)
            {
                Logger.Mensaje(ex.Message);
            }
        }

        //---------------------Llamado desde Formulario botón ?
        public ListadoFormulario(FormularioInspeccion origen)
        {
            InitializeComponent();
            conn = new Conexion().Getcone();//Conexión
            formu = origen;
            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            CargarGrilla();
        }


        //-----------Botón Refrescar lista--------------------------------------
        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
            
        }
        //----------Botón Salir---------------------------------------------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //-------------Botón traspasar a informe------------------------------------
        private async void btnPasar_Click(object sender, RoutedEventArgs e)
        {
            btnPasar.Visibility = Visibility.Visible;
            try
            {
                BibliotecaNegocio.Informe.ListaInforme con = (BibliotecaNegocio.Informe.ListaInforme)dgLista.SelectedItem;
                formu.txtNFormBuscar.Text = con.Numero.ToString();
                formu.BuscarInf();

                Close();
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al traspasar la Información"));
                Logger.Mensaje(ex.Message);
            }
        }
        //-------------------Filtro por Número-----------------------------------
        private async void btnFiltrarNumero_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long numero = long.Parse(txtFiltroNumero.Text);
                OracleCommand CMD = new OracleCommand();
                //que tipo de comando voy a ejecutar
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
                dgLista.ItemsSource = clie;
                conn.Close();

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));
                Logger.Mensaje(ex.Message);
            }
        }

        //-------------------Filtro por Rut Cliente-------------------------------
        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rut = txtFiltroRut.Text;
                OracleCommand CMD = new OracleCommand();
                //que tipo de comando voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;

                List<BibliotecaNegocio.Informe.ListaInforme> clie = new List<BibliotecaNegocio.Informe.ListaInforme>();
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_BUSCAR_INFORME_CLI";
                //////////se crea un nuevo de tipo parametro//P_Nombre//el tipo//el largo// 
                CMD.Parameters.Add(new OracleParameter("P_RUT", OracleDbType.Varchar2)).Value = rut;
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
                dgLista.ItemsSource = clie;
                //Cerrar conexión
                conn.Close();

            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));
                Logger.Mensaje(ex.Message);

                dgLista.Items.Refresh();
            }
        }
        
    }
}
