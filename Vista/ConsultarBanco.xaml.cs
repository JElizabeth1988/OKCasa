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
    /// Lógica de interacción para ConsultarBanco.xaml
    /// </summary>
    public partial class ConsultarBanco : MetroWindow
    {
        OracleConnection conn = null;
        public ConsultarBanco()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
            txtRut.Focus();
            btnInvitación.Visibility = Visibility.Hidden;//Esconder botón hasta que el resultado de la busqueda sea positivo

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            dgLista.ItemsSource = null;
            btnInvitación.Visibility = Visibility.Hidden;
            txtRut.Focus();


        }

        private async void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            //Se instancia al cliente
            WSBancoEstado.WS_BANCOClient cliente = new WSBancoEstado.WS_BANCOClient();

            //Capturar rut
            string rut = txtRut.Text;
            if (rut != "")
            {
                if (rut.Length ==10)
                {
                    //Validar Datos en el WS
                    if (cliente.TipoCliente(rut) == 1)
                    {
                        try
                        {
                            //se crea una lista de tipo cine
                            List<BancoEstado> lista_tipos = new List<BancoEstado>();
                            //se crea un comando de oracle
                            OracleCommand cmd = new OracleCommand();
                            //se ejecutan los comandos de procedimeintos
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            //conexion
                            cmd.Connection = conn;
                            //procedimiento
                            cmd.CommandText = "SP_BANCO_ESTADO";

                            cmd.Parameters.Add(new OracleParameter("RUT", OracleDbType.Varchar2)).Value = rut;
                            //Se agrega el parametro de salida
                            cmd.Parameters.Add(new OracleParameter("HIPOTECARIOS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                            //se abre la conexion
                            conn.Open();
                            //se crea un reader
                            OracleDataReader dr = cmd.ExecuteReader();
                            //mientras lea
                            while (dr.Read())
                            {
                                BancoEstado be = new BancoEstado();

                                //se obtiene el valor con getvalue es lo mismo pero con get
                                be.rut = dr.GetValue(0).ToString();
                                be.nombre = dr.GetValue(1).ToString();
                                be.Descripción = dr.GetValue(2).ToString();

                                lista_tipos.Add(be);
                            }
                            conn.Close();
                            dgLista.ItemsSource = lista_tipos;
                            btnInvitación.Visibility = Visibility.Visible;//Botón se ve


                        }
                        catch (Exception ex)
                        {
                            await this.ShowMessageAsync("Error:",
                                         string.Format("Excepcion Base de Datos Oracle: {0}", ex.ToString()));
                            Logger.Mensaje(ex.Message);
                        }
                    }
                    else
                    {
                        //Mostrar texto en la Grilla
                        dgLista.ItemsSource = null;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Rut");
                        dt.Columns.Add("Descripción");
                        dt.Rows.Add(rut, " No corresponde a un cliente hipotecario de Banco Estado");
                        dgLista.ItemsSource = dt.DefaultView;

                        btnInvitación.Visibility = Visibility.Hidden;//Botón invitación no aparece para clientes no hipotecarios

                    }
                }
                else
                {
                    await this.ShowMessageAsync("Error:",
                                     string.Format("Ingrese un Rut válido"));
                }
            }
            else
            {
                await this.ShowMessageAsync("Error:",
                                     string.Format("Ingrese Rut de cliente a buscar"));
            }

        }

        private async void btnInvitación_Click(object sender, RoutedEventArgs e)
        {
            //Proximamente enviará correos, por tiempo no se pudo :(
            //Mostrar mensajes de envío(Solo visual)
            var x = await this.ShowProgressAsync("Por Favor Espere... ", "Enviando Invitación...");

            await Task.Delay(3000);

            x.SetCancelable(false);

            await this.ShowMessageAsync("Mensaje:", "¡Invitación Enviada!");
            await x.CloseAsync().ConfigureAwait(false);
        }
    }
}
