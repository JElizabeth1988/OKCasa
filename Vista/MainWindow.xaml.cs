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
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;


namespace Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }
              
        private async void Tile_Click(object sender, RoutedEventArgs e)
        {
            var x =
           await this.ShowMessageAsync("Advertencia", "¿Desea cerrar sesión?",
                   MessageDialogStyle.AffirmativeAndNegative);
            if (x == MessageDialogResult.Affirmative)
            {
                Login log = new Login();
                this.Close();
                log.ShowDialog();
            }
            else
            {

            }
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            FormularioInspeccion formu = new FormularioInspeccion();
            formu.ShowDialog();
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            Agenda age = new Agenda();
            age.ShowDialog();
        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {
            Tecnico Tec = new Tecnico();
            Tec.ShowDialog();
        }

        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();
            cli.ShowDialog();
        }

        private void Tile_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_6(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_8(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_9(object sender, RoutedEventArgs e)
        {
            ConsultarBanco be = new ConsultarBanco();
            be.ShowDialog();
        }

        private void Tile_Click_10(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente();
            liCli.ShowDialog();
        }

        private void Tile_Click_11(object sender, RoutedEventArgs e)
        {
            ListadoInspectores liIns = new ListadoInspectores();
            liIns.ShowDialog();
        }

        private void Tile_Click_12(object sender, RoutedEventArgs e)
        {
            EquiposInspeccion equi = new EquiposInspeccion();
            equi.ShowDialog();
        }
        //Click Servicios
        private void Tile_Click_13(object sender, RoutedEventArgs e)
        {

        }
        //Click Consultar Agenda
        private void Tile_Click_14(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_15(object sender, RoutedEventArgs e)
        {
            Agenda ag = new Agenda();
            ag.ShowDialog();
        }

        private void Tile_Click_16(object sender, RoutedEventArgs e)
        {
            Insumos ins = new Insumos();
            ins.ShowDialog();
        }

        //Seguimiento
        private void Tile_Click_17(object sender, RoutedEventArgs e)
        {

        }
        //Historial 
        private void Tile_Click_18(object sender, RoutedEventArgs e)
        {
            ListadoFormulario lf = new ListadoFormulario();
            lf.ShowDialog();
        }
    }
}
