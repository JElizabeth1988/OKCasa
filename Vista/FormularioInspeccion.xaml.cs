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
using BibliotecaClases;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para FormularioInspeccion.xaml
    /// </summary>
    public partial class FormularioInspeccion : MetroWindow
    {
        public FormularioInspeccion()
        {
            InitializeComponent();
            this.DataContext = this;

            cbComuna.ItemsSource = Enum.GetValues(typeof
                (Comuna));
            cbComuna.SelectedIndex = 0;

            cbTipoV.ItemsSource = Enum.GetValues(typeof
                (TipoVivienda));
            cbTipoV.SelectedIndex = 0;

            cbTipoAg.ItemsSource = Enum.GetValues(typeof
                (TipoAgrupamiento));
            cbTipoAg.SelectedIndex = 0;

            cbHabita.ItemsSource = Enum.GetValues(typeof
                (Habitaciones));
            cbHabita .SelectedIndex = 0;

            cbPisos.ItemsSource = Enum.GetValues(typeof
               (Pisos));
            cbPisos.SelectedIndex = 0;

            cbHerramientas.ItemsSource = Enum.GetValues(typeof
               (Herramientas));
            cbHerramientas.SelectedIndex = 0;

            cbAlcanta.ItemsSource = Enum.GetValues(typeof
               (Alcantarillado));
            cbAlcanta.SelectedIndex = 0;

            cbElectrica.ItemsSource = Enum.GetValues(typeof
               (Electrica));
            cbElectrica.SelectedIndex = 0;

            cbGas.ItemsSource = Enum.GetValues(typeof
               (Gas));
            cbGas.SelectedIndex = 0;

            cbInstAgua.ItemsSource = Enum.GetValues(typeof
               (InstAgua));
            cbInstAgua.SelectedIndex = 0;

            cbRedAgua.ItemsSource = Enum.GetValues(typeof
               (RedAgua));
            cbRedAgua.SelectedIndex = 0;

            cbSanitario.ItemsSource = Enum.GetValues(typeof
               (Sanitario));
            cbSanitario.SelectedIndex = 0;


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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

        /*private void txtDepto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }

        }*/
        
    }
}
