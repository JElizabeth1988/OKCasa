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
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : MetroWindow
    {
        public Cliente()
        {
            InitializeComponent();

            btnModificar.Visibility = Visibility.Hidden;//Botón Modificar no se ve
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPregunta_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }

        private void btnBanco_Click(object sender, RoutedEventArgs e)
        {
            ConsultarBanco be = new ConsultarBanco();
            be.ShowDialog();
        }
    }
}
