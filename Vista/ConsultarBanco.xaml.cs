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
using Oracle.ManagedDataAccess.Types;

using System.Configuration;
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
            //Crear Cliente del WS
            WSBancoEstado.WS_BANCOClient cliente = new WSBancoEstado.WS_BANCOClient();

            //Capturar Dato
            string rut = txtRut.Text;

            //Validar Datos en el WS
            if (cliente.Hipotecario(rut) == rut)
            {
                OracleConnection conn = null;
                DataTable dt = new DataTable();
                string connectionString = ConfigurationManager.ConnectionStrings["OkCasa_Entities"].ConnectionString;
                conn = new OracleConnection("Data Source=localhost:1521/XE;User Id=OKCasa;Password=OKCasa");

                conn.Open();

                OracleCommand myCmd = new OracleCommand("p_banco_estado", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                OracleDataAdapter da = new OracleDataAdapter(myCmd);

                myCmd.Parameters.Add("rut", "varchar2").Value = rut;//entregar parámetro rut
                da.Fill(dt);
                dt.Columns.Add("Resultado");
                dt.Rows.Add(da);
                dgLista.ItemsSource = dt.DefaultView;

                btnInvitación.Visibility = Visibility.Visible;//Ahora Botón Se vé
            }
            else
            {
                //Mostrar texto en la Grilla
                dgLista.ItemsSource = null;
                DataTable dt = new DataTable();
                dt.Columns.Add("Resultado");
                dt.Rows.Add("El rut " + rut + " No corresponde a un cliente hipotecario de Banco Estado");
                dgLista.ItemsSource = dt.DefaultView;

                btnInvitación.Visibility = Visibility.Hidden;//Botón invitación no aparece para clientes no hipotecarios

            }
        }

        private async void btnInvitación_Click(object sender, RoutedEventArgs e)
        {
            //Mostrar mensajes de envío(Solo visual)
            var x = await this.ShowProgressAsync("Por Favor Espere... ", "Enviando Invitación...");

            await Task.Delay(3000);

            x.SetCancelable(false);

            await this.ShowMessageAsync("Mensaje:", "¡Invitación Enviada!");
            await x.CloseAsync().ConfigureAwait(false);
        }
    }
}
