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
    /// Lógica de interacción para administradorFormulario.xaml
    /// </summary>
    public partial class AdministradorFormulario : MetroWindow
    {
        public AdministradorFormulario()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Tile_Click_1(object sender, RoutedEventArgs e)
        {
            FormularioInspeccion form = new FormularioInspeccion();
            form.ShowDialog();
            Close();
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            ListadoFormulario liForm = new ListadoFormulario();
            liForm.ShowDialog();
            Close();
        }
    }
}
