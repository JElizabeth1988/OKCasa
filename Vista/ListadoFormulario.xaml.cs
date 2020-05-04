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
    /// Lógica de interacción para ListadoFormulario.xaml
    /// </summary>
    public partial class ListadoFormulario : MetroWindow
    {
        FormularioInspeccion formu;

        public ListadoFormulario()
        {
            InitializeComponent();

            btnPasar.Visibility = Visibility.Hidden;
        }
        //Llamado desde Formulario
        public ListadoFormulario(FormularioInspeccion origen)
        {
            InitializeComponent();
            formu = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnCrear.Visibility = Visibility.Hidden;

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
            FormularioInspeccion form = new FormularioInspeccion();
            form.ShowDialog();
        }

        private void btnListCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente listCli = new ListadoCliente();
            listCli.ShowDialog();
        }
    }
}
