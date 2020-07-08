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
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
            txtUsuario.Focus();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            //Crear Cliente del WS
            WSLOGIN.WSLOGINClient cliente = new WSLOGIN.WSLOGINClient();

            //Capturar las Credenciales
            string user = txtUsuario.Text;
            string pass = TxtContrasenia.Password.ToString();

            //Validar Credenciales en el WS
            if (cliente.Login(user, pass) == 1)
            {
                
                await this.ShowMessageAsync("Mensaje:",
                    //----------------------Nombre del user Con primera letra mayúscula
                string.Format("Bienvenido "+ user.Substring(0,1).ToUpper())+user.Substring(1).ToLower());
                MainWindow main = new MainWindow();
                this.Close();
                main.ShowDialog();
            }
            else
            {
                await this.ShowMessageAsync("Mensaje:",
                                     string.Format("¡Error de Credenciales!"));
                txtUsuario.Clear();
                TxtContrasenia.Clear();
                txtUsuario.Focus();
            }
        }
    }
}
