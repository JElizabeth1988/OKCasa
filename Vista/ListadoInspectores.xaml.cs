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
    /// Lógica de interacción para ListadoInspectores.xaml
    /// </summary>
    public partial class ListadoInspectores : MetroWindow
    {
        public ListadoInspectores()
        {
            InitializeComponent();
            txtFiltroRut.Focus();

            btnPasar.Visibility = Visibility.Hidden;//Btn no se ve

            //llenar CB
            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }

            cbEquipo.SelectedIndex = 0;

            try
            {
                BibliotecaNegocio.Tecnico cl = new BibliotecaNegocio.Tecnico();
                dgLista.ItemsSource = cl.ReadAll2();
                dgLista.Items.Refresh();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!" + ex.Message);
                Logger.Mensaje(ex.Message);
            }
        }

        private void btnListCliente_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente();
            liCli.ShowDialog();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
            BibliotecaNegocio.Tecnico cl = new BibliotecaNegocio.Tecnico();
            dgLista.ItemsSource = cl.ReadAll2();
            dgLista.Items.Refresh();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            Tecnico tec = new Tecnico();
            tec.ShowDialog();
        }

        private async void btnFiltrarEquipo_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                comboBoxItem1 eq = (comboBoxItem1)cbEquipo.SelectedItem;
                List<BibliotecaNegocio.Tecnico.ListaTecnico> lf = new BibliotecaNegocio.Tecnico().FiltroEquipo(eq.nombre);
                dgLista.ItemsSource = lf;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al filtrar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);
                dgLista.Items.Refresh();
            }
        }

        private async void btnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string rut = txtFiltroRut.Text;

                List<BibliotecaNegocio.Tecnico.ListaTecnico> lc = new BibliotecaNegocio.Tecnico().FiltroRut(rut);
                dgLista.ItemsSource = lc;
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error al filtrar la Información"));
                /*MessageBox.Show("error al Filtrar Información");*/
                Logger.Mensaje(ex.Message);

                dgLista.Items.Refresh();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPasar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
