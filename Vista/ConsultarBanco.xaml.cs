﻿using System;
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
    /// Lógica de interacción para ConsultarBanco.xaml
    /// </summary>
    public partial class ConsultarBanco : MetroWindow
    {
        public ConsultarBanco()
        {
            InitializeComponent();
            txtRut.Focus();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInvitación_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
