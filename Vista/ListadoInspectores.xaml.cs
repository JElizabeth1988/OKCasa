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
    /// Lógica de interacción para ListadoInspectores.xaml
    /// </summary>
    public partial class ListadoInspectores : MetroWindow
    {
        public ListadoInspectores()
        {
            InitializeComponent();
        }

        private void btnListCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente();
            liCli.ShowDialog();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            dgLista.Items.Refresh();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            Tecnico tec = new Tecnico();
            tec.ShowDialog();
        }
    }
}
