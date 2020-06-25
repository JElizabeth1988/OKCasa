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
    /// Lógica de interacción para FormularioInspeccion.xaml
    /// </summary>
    public partial class FormularioInspeccion : MetroWindow
    {
        public FormularioInspeccion()
        {
            InitializeComponent();
            this.DataContext = this;
            dtfechaIns.Focus();

            lblNumForm.Content = DateTime.Now.ToString("yyMMddHHmmss");

            btnActualizar.Visibility = Visibility.Hidden;

            //llenar el combo box 
            foreach (InstAlcantarillado item in new InstAlcantarillado().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_alcantarillado;
                cb.nombre = item.nombre;
                cbAlcanta.Items.Add(cb);
            }

            foreach (InstGas item in new InstGas().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_gas;
                cb.nombre = item.nombre;
                cbGas.Items.Add(cb);
            }

            foreach (InstElectrica item in new InstElectrica().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_electrica;
                cb.nombre = item.nombre;
                cbElectrica.Items.Add(cb);
            }

            foreach (RedAgua item in new RedAgua().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_agua;
                cb.nombre = item.nombre;
                cbRedAgua.Items.Add(cb);
            }

            foreach (InstAguaPotable item in new InstAguaPotable().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_agua_potable;
                cb.nombre = item.nombre;
                cbInstAgua.Items.Add(cb);
            }


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

            cbAlcanta.SelectedIndex = 0;
            cbGas.SelectedIndex = 0;
            cbElectrica.SelectedIndex = 0;
            cbRedAgua.SelectedIndex = 0;
            cbInstAgua.SelectedIndex = 0;
            cbTipoV.SelectedIndex = 0;
            cbTipoAg.SelectedIndex = 0;

            txtPisos.Text = "0";
            txtHabitac.Text = "0";

            txtTotalReal.Text = "0";
            txtTotalReg.Text = "0";
            txtIConstReal.Text = "0";
            txtConsReg.Text = "0";
            txtDistancia.Text = "0";
            txtEmisividad.Text = "0";
            txtTempRefle.Text = "0";
            txtTempAtmo.Text = "0";
            txtHumedad.Text = "0";




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
            cbAlcanta.SelectedIndex = 0;
            cbElectrica.SelectedIndex = 0;
            cbGas.SelectedIndex = 0;
            cbInstAgua.SelectedIndex = 0;
            cbRedAgua.SelectedIndex = 0;
            cbTipoAg.SelectedIndex = 0;
            cbTipoV.SelectedIndex = 0;

            RbNoFuego.IsChecked = true;
            RbSiFuego.IsChecked = false;
            RbNoHab.IsChecked = true;
            RbSiHab.IsChecked = false;
            RbNoTerm.IsChecked = false;

            txtTotalReal.Text="0";
            txtTotalReg.Text="0";
            txtIConstReal.Text="0";
            txtConsReg.Text="0";
            txtDistancia.Text = "0";
            txtEmisividad.Text = "0";
            txtTempRefle.Text = "0";
            txtTempAtmo.Text = "0";
            txtHumedad.Text = "0";


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
