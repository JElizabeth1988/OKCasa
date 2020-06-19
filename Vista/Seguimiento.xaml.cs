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
    /// Lógica de interacción para Seguimiento.xaml
    /// </summary>
    public partial class Seguimiento : MetroWindow
    {
        public Seguimiento()
        {
            InitializeComponent();
            txtFiltroNumero.Focus();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnListCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente lc = new ListadoCliente();
            lc.ShowDialog();
        }
    }
}
