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

using BibliotecaNegocio;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ListadoCliente.xaml
    /// </summary>
    public partial class ListadoCliente : MetroWindow
    {
        //This
        Cliente cli;
        FormularioInspeccion form;
        ListadoFormulario liForm;
        FormularioVerificacion forVe;
        FormularioMedicion forMe;
        FormularioTermografia forTer;
       

        public ListadoCliente()
        {
            InitializeComponent();

            btnPasar.Visibility = Visibility.Hidden;//el botón traspasar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;//no se ve

            btnCrear.Visibility = Visibility.Hidden; //No se ve

            try
            {
                BibliotecaNegocio.Cliente cl = new BibliotecaNegocio.Cliente();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }


        }

        public ListadoCliente(FormularioInspeccion origen)
        {
            InitializeComponent();
            form = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasar.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;

        }
        //Llamado desde medición
        public ListadoCliente(FormularioMedicion origen)
        {
            InitializeComponent();
            forMe = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasar.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;

        }
        //Llamado desde Termografía
        public ListadoCliente(FormularioTermografia origen)
        {
            InitializeComponent();
            forTer = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasar.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;

        }

       
        //Llamado desde verificación
        public ListadoCliente(FormularioVerificacion origen)
        {
            InitializeComponent();
            forVe = origen;

            btnPasarAForm.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasar.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;

        }


        //Llamado desde cliente
        public ListadoCliente(Cliente origen)
        {
            InitializeComponent();
            cli = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;
        }

        public ListadoCliente(ListadoFormulario origen)
        {
            InitializeComponent();
            liForm = origen;

            btnPasar.Visibility = Visibility.Visible;//el botón traspasar se ve
            btnEliminar.Visibility = Visibility.Hidden;//Botón eliminar no se ve
            btnPasarAForm.Visibility = Visibility.Hidden;
            btnCrear.Visibility = Visibility.Hidden;
        }



        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();
            cli.ShowDialog();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            dgLista.Items.Refresh();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPasar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPasarAForm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
