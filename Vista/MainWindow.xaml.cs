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
            FormularioInspeccion form = new FormularioInspeccion();
            form.ShowDialog();
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Tile_Click_4(object sender, RoutedEventArgs e)
        {

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
    }
}
