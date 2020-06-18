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

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para FormularioTermografia.xaml
    /// </summary>
    public partial class FormularioTermografia : MetroWindow
    {
        public FormularioTermografia()
        {
            InitializeComponent();
            this.DataContext = this;

            btnActualizar.Visibility = Visibility.Hidden;

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Validación campo solo numerico
        private void txtNCasa_KeyDown(object sender, KeyEventArgs e)
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

        private void btnListarCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }

        private void btnListarCli_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }

        private void btnListarForm_Click(object sender, RoutedEventArgs e)
        {
            ListadoFormulario liForm = new ListadoFormulario(this);
            liForm.ShowDialog();
        }


        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {

        }

       
       
    }
}

