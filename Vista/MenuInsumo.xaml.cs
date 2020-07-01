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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;


namespace Vista
{
    /// <summary>
    /// Lógica de interacción para MenuInsumo.xaml
    /// </summary>
    public partial class MenuInsumo : MetroWindow
    {
        public MenuInsumo()
        {
            InitializeComponent();
        }

        private void Tile_Click_Insumos(object sender, RoutedEventArgs e)
        {
            Insumo i = new Insumo();
            i.ShowDialog();
        }

        private void Tile_Click_Asignar(object sender, RoutedEventArgs e)
        {
            AsignarInsumo asi = new AsignarInsumo();
            asi.ShowDialog();
        }

        private void Tile_Click_Devolver(object sender, RoutedEventArgs e)
        {
            DevolverInsumo de = new DevolverInsumo();
            de.ShowDialog();
        }
    }
}
