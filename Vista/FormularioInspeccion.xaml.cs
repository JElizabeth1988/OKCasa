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
        }


    }
}
