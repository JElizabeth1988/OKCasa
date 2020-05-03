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
    /// Lógica de interacción para AdministradorTecnico.xaml
    /// </summary>
    public partial class AdministradorTecnico : MetroWindow
    {
        public AdministradorTecnico()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            EquiposInspeccion equi = new EquiposInspeccion();
            equi.ShowDialog();
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            Tecnico tec = new Tecnico();
            tec.ShowDialog();
        }

        private void Tile_Click_2(object sender, RoutedEventArgs e)
        {
            ListadoInspectores insp = new ListadoInspectores();
            insp.ShowDialog();
        }
    }
}
