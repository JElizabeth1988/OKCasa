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
    /// Lógica de interacción para ListadoFormulario.xaml
    /// </summary>
    public partial class ListadoFormulario : MetroWindow
    {
        FormularioInspeccion formu;
        FormularioMedicion forMe;
        FormularioTermografia forTer;
        FormularioVerificacion formVe;


        public ListadoFormulario()
        {
            InitializeComponent();

            btnPasar.Visibility = Visibility.Hidden;

            try
            {
                BibliotecaNegocio.InformeInspeccion ii = new BibliotecaNegocio.InformeInspeccion();
                dgLista.ItemsSource = ii.ReadAll2();
                dgLista.Items.Refresh();

                BibliotecaNegocio.InformeMedicion im = new BibliotecaNegocio.InformeMedicion();
                dgLista.ItemsSource = im.ReadAll2();
                dgLista.Items.Refresh();

                BibliotecaNegocio.InformeTermografia it = new BibliotecaNegocio.InformeTermografia();
                dgLista.ItemsSource = it.ReadAll2();
                dgLista.Items.Refresh();

                BibliotecaNegocio.InformeVerificacion iv = new BibliotecaNegocio.InformeVerificacion();
                dgLista.ItemsSource = iv.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }
        }
        //Llamado desde Formulario
        public ListadoFormulario(FormularioInspeccion origen)
        {
            InitializeComponent();
            formu = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnCrear.Visibility = Visibility.Hidden;

        }

        //Llamado desde FormularioMedicion
        public ListadoFormulario(FormularioMedicion origen)
        {
            InitializeComponent();
            forMe = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnCrear.Visibility = Visibility.Hidden;

        }

        //Llamado desde FormularioTermografia
        public ListadoFormulario(FormularioTermografia origen)
        {
            InitializeComponent();
            forTer = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnCrear.Visibility = Visibility.Hidden;

        }

        

        //Llamado desde FormularioVerificacion
        public ListadoFormulario(FormularioVerificacion origen)
        {
            InitializeComponent();
            formVe = origen;

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
