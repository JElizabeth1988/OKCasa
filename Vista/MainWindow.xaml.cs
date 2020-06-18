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
        //Cliente
        private void Tile_Click_AdmCliente(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();
            cli.ShowDialog();
        }
        //ListadoCleinte
        private void Tile_Click_ListCliente(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente();
            liCli.ShowDialog();
        }
        //Banco Estado
        private void Tile_Click_Banco(object sender, RoutedEventArgs e)
        {
            ConsultarBanco be = new ConsultarBanco();
            be.ShowDialog();
        }
        //Servicio
        private void Tile_Click_Sevicio(object sender, RoutedEventArgs e)
        {
            Servicio ser = new Servicio();
            ser.ShowDialog();
        }
        //Seguimiento
        private void Tile_Click_Seguimiento(object sender, RoutedEventArgs e)
        {
            Seguimiento seg = new Seguimiento();
            seg.ShowDialog();
        }
        //Agenda
        private void Tile_Click_Agenda(object sender, RoutedEventArgs e)
        {
            Horario ag = new Horario();
            ag.ShowDialog();
        }
        //Informe
        private void Tile_Click_Informe(object sender, RoutedEventArgs e)
        {
            MenuInforme mi = new MenuInforme();
            mi.ShowDialog();
        }
        //Historial
        private void Tile_Click_Historial(object sender, RoutedEventArgs e)
        {
            ListadoFormulario lf = new ListadoFormulario();
            lf.ShowDialog();
        }
        //Equipo
        private void Tile_Click_Equipo(object sender, RoutedEventArgs e)
        {
            EquipoInspeccion equi = new EquipoInspeccion();
            equi.ShowDialog();
        }
        //Técnico
        private void Tile_Click_Inspector(object sender, RoutedEventArgs e)
        {
            Tecnico Tec = new Tecnico();
            Tec.ShowDialog();
        }
        //ListadoInspectores
        private void Tile_Click_ListaInsp(object sender, RoutedEventArgs e)
        {
            ListadoInspectores liIns = new ListadoInspectores();
            liIns.ShowDialog();
        }
        //Insumos
        private void Tile_Click_Insumos(object sender, RoutedEventArgs e)
        {
            Insumo ins = new Insumo();
            ins.ShowDialog();
        }
        //Solicitud
        private void Tile_Click_Solicitud(object sender, RoutedEventArgs e)
        {
            Solicitud sol = new Solicitud();
            sol.ShowDialog();
        }
    }
}
