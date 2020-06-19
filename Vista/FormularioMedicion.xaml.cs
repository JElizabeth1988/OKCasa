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
    /// Lógica de interacción para FormularioMedicion.xaml
    /// </summary>
    public partial class FormularioMedicion : MetroWindow
    {
        public FormularioMedicion()
        {
            InitializeComponent();
            this.DataContext = this;

            dtfechaIns.Focus();

            lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");

            btnActualizar.Visibility = Visibility.Hidden;

            foreach (TipoVivienda item in new TipoVivienda().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_tipo;
                cb.nombre = item.nombre_tipo;
                cbTipoV.Items.Add(cb);
            }

            foreach (Agrupamiento item in new Agrupamiento().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_agrup;
                cb.nombre = item.nombre_agr;
                cbTipoAg.Items.Add(cb);
            }

            cbTipoV.SelectedIndex = 0;
            cbTipoAg.SelectedIndex = 0;

            txtPisos.Text = "0";
            txtHabitac.Text = "0";

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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



        //Validación campo solo numerico
        private void txtPisos_KeyDown(object sender, KeyEventArgs e)
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

        private void txtHabitac_KeyDown(object sender, KeyEventArgs e)
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

        private void btnBuscarForm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscarCli_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscarRutC1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");
            //dtfechaIns
            rbPrimera.IsChecked = true;
            rbSegunda.IsChecked = false;
            rbCierre.IsChecked = false;

            txtNFormBuscar.Clear();
            txtRutCliBuscar.Clear();
            txtRutCliente.Clear();
            txtRutCliente1.Clear();
            txtRutTecnico.Clear();
            txtPisos.Text = "0";
            txtObserv.Clear();
            txtNombreCliente1.Clear();
            txtNombreCliente.Clear();
            txtHabitac.Text = "0";
            txtEquipo.Clear();
            txtDireccion.Clear();
            txtConstr.Clear();
            txtNombreTec.Clear();

            txtComunReal.Clear();
            txtComunReg.Clear();
            txtConsReg.Clear();
            txtIConstReal.Clear();
            txtTotalReal.Clear();
            txtTotalReg.Clear();
            txtUtilReal.Clear();
            txtUtilReg.Clear();
            
            cbTipoAg.SelectedIndex = 0;
            cbTipoV.SelectedIndex = 0;

            rbBueno.IsChecked = true;
            rbDeficiente.IsChecked = false;
            rbMalo.IsChecked = false;
            rbRegular.IsChecked = false;


        }

        private void btnBuscarRutC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLsitarInsp_Click(object sender, RoutedEventArgs e)
        {
            ListadoInspectores lii = new ListadoInspectores();
            lii.ShowDialog();
        }

        private void btnBuscarRutT_Click(object sender, RoutedEventArgs e)
        {

        }
        //Solo números
        private void txtNFormBuscar_KeyDown(object sender, KeyEventArgs e)
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
    }
}
