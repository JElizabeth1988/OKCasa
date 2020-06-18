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
    /// Lógica de interacción para Cliente.xaml
    /// </summary>
    public partial class Cliente : MetroWindow
    {
        public Cliente()
        {
            InitializeComponent();

            txtDV.IsEnabled = false;
            btnModificar.Visibility = Visibility.Hidden;//el botón Modificar no se ve

            //llenar el combo box 
            foreach (Comuna item in new Comuna().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_comuna;
                cb.nombre = item.nombre;
                cboComuna.Items.Add(cb);
            }
            
            cboComuna.SelectedIndex = 0;
            txtTelefono.Text = "0";

            
        }
        //Solo acepta valores numéricos
        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPregunta_Click(object sender, RoutedEventArgs e)
        {
            ListadoCliente liCli = new ListadoCliente(this);
            liCli.ShowDialog();
        }

        private void btnBanco_Click(object sender, RoutedEventArgs e)
        {
            ConsultarBanco be = new ConsultarBanco();
            be.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtRut.Clear();
            txtDV.Clear();
            txtNombre.Clear();
            txtSegNombre.Clear();
            txtApPaterno.Clear();
            txtApeMaterno.Clear();
            txtDireccion.Clear();
            txtTelefono.Text="0";
            txtEmail.Clear();
            cboComuna.SelectedIndex = 0;
            rbSi.IsChecked = false;
            rbNo.IsChecked = true;

            btnModificar.Visibility = Visibility.Hidden;
            btnGuardar.Visibility = Visibility.Visible;//botón guardar aparece
            txtRut.IsEnabled = true;

            txtRut.Focus();//Mover el cursor a la poscición Rut



        }

        //Botón Guardar
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                if (rut.Length == 9)
                {
                    rut = "0" + txtRut.Text + "-" + txtDV.Text;
                }
                String Nombre = txtNombre.Text;
                String segNombre = txtSegNombre.Text;
                String apPaterno = txtApPaterno.Text;
                String apMaterno = txtApeMaterno.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;
                String Hipotecario;

                if (rbSi.IsChecked == true)
                {
                    Hipotecario = "Si";
                    
                }
                else
                {
                    Hipotecario = "No";
                }
                int telefono = int.Parse(txtTelefono.Text);
                /*int telefono = 0;
                if (int.TryParse(txtTelefono.Text, out telefono))
                {
                    
                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                     string.Format("Ingrese un número de 9 dígitos"));
                   txtTelefono.Focus();
                    return;
                }*/
                int Comuna = ((comboBoxItem1)cboComuna.SelectedItem).id;//Guardo el id
                BibliotecaNegocio.Cliente c = new BibliotecaNegocio.Cliente()
                {
                    rut_cliente = rut,
                    primer_nombre = Nombre,
                    segundo_nombre = segNombre,
                    ap_paterno = apPaterno,
                    ap_materno = apMaterno,
                    direccion = direccion,
                    telefono = telefono,
                    email = mail,
                    hipotecario = Hipotecario,
                    id_comuna = Comuna
                    
                };
                bool resp = c.Guardar();
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));
                /*MessageBox.Show(resp ? "Guardado" : "No Guardado");*/


                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {
                    DaoErrores de = c.retornar();
                    string li = "";
                    foreach (string item in de.ListarErrores())
                    {
                        li += item + " \n";
                    }
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format(li));
                }


                //-----------------------------------------------------------------------------------------------


            }
            catch (ArgumentException exa)//mensajes de reglas de negocios
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error de ingreso de datos"));
                /*MessageBox.Show("Error de ingreso de datos");*/
                Logger.Mensaje(ex.Message);

            }
        }

        //Botón modificar
        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                String rut = txtRut.Text + "-" + txtDV.Text;
                String nombre = txtNombre.Text;
                String segNombre = txtSegNombre.Text;
                String apPaterno = txtApPaterno.Text;
                String apMaterno = txtApeMaterno.Text;
                String mail = txtEmail.Text;
                String direccion = txtDireccion.Text;
                String Hipotecario;

                if (rbSi.IsChecked == true)
                {
                    Hipotecario = "Si";

                }
                else
                {
                    Hipotecario = "No";
                }
                int telefono = 0;
                if (int.TryParse(txtTelefono.Text, out telefono))
                {

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("Ingrese un número de 9 dígitos"));
                        txtTelefono.Focus();
                    return;
                }

                int Comuna = ((comboBoxItem1)cboComuna.SelectedItem).id;
                
                BibliotecaNegocio.Cliente c = new BibliotecaNegocio.Cliente()
                {
                    rut_cliente = rut,
                    primer_nombre = nombre,
                    segundo_nombre = segNombre,
                    ap_paterno = apPaterno,
                    ap_materno = apMaterno,
                    direccion = direccion,
                    telefono = telefono,
                    email = mail,               
                    hipotecario = Hipotecario,
                    id_comuna = Comuna

                };
                bool resp = c.Modificar();
                await this.ShowMessageAsync("Mensaje:",
                     string.Format(resp ? "Actualizado" : "No Actualizado"));
                /*MessageBox.Show(resp ? "Actualizado" : "No Actualizado, (El rut no se debe modificar)");*/

                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES
                if (resp == false)//If para que no muestre mensaje en blanco en caso de éxito
                {

                    DaoErrores de = c.retornar();
                    string li = "";
                    foreach (string item in de.ListarErrores())
                    {
                        li += item + " \n";
                    }
                    await this.ShowMessageAsync("Mensaje:",
                        string.Format(li));
                }


                //-----------------------------------------------------------------------------------------------

            }
            catch (ArgumentException exa)//mensajes de reglas de negocios
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format((exa.Message)));
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Actualizar Datos"));
                /*MessageBox.Show("Error al Actualizar");*/
                Logger.Mensaje(ex.Message);

            }
        }

        //Llamado del botón traspasar
        public async void Buscar()
        {
            try
            {
                BibliotecaNegocio.Cliente c = new BibliotecaNegocio.Cliente();
                c.rut_cliente = txtRut.Text;
                bool buscar = c.Buscar();

                if (buscar)
                {
                    txtRut.Text = c.rut_cliente.Substring(0, 10);
                    txtDV.Text = c.rut_cliente.Substring(11, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;
                    txtNombre.Text = c.primer_nombre;
                    txtSegNombre.Text = c.segundo_nombre;
                    txtApeMaterno.Text = c.ap_paterno;
                    txtApPaterno.Text = c.ap_materno;
                    txtEmail.Text = c.email;
                    txtDireccion.Text = c.direccion;
                    txtTelefono.Text = c.telefono.ToString();

                    if (c.hipotecario == "Si")
                    {
                        rbSi.IsChecked = true;
                        rbNo.IsChecked = false;
                    }
                    else
                    {
                        rbSi.IsChecked = false;
                        rbNo.IsChecked = true;
                    }



                        Comuna co = new Comuna();
                    co.id_comuna = c.id_comuna;
                    co.Read();
                    cboComuna.Text = co.nombre;//Cambiar a nombre
                    

                    btnModificar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;

                    txtRut.IsEnabled = false;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                      string.Format("No se encontraron resultados!"));
                    /*MessageBox.Show("No se encontraron resultados!");*/
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Buscar Información"));
                /*MessageBox.Show("error al buscar");*/
                Logger.Mensaje(ex.Message);


            }
        }

        //Botón Buscar (de administrar cliente)
        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BibliotecaNegocio.Cliente c = new BibliotecaNegocio.Cliente();
                c.rut_cliente = txtRut.Text +"-"+ txtDV.Text;
                bool buscar = c.Buscar();
                if (buscar)
                {
                    txtRut.Text = c.rut_cliente.Substring(0, 10);
                    txtDV.Text = c.rut_cliente.Substring(11, 1);
                    txtRut.IsEnabled = false;
                    txtDV.IsEnabled = false;
                    txtNombre.Text = c.primer_nombre;
                    txtSegNombre.Text = c.segundo_nombre;
                    txtApeMaterno.Text = c.ap_paterno;
                    txtApPaterno.Text = c.ap_materno;
                    txtEmail.Text = c.email;
                    txtDireccion.Text = c.direccion;
                    txtTelefono.Text = c.telefono.ToString();

                    if (c.hipotecario == "Si")
                    {
                        rbSi.IsChecked = true;
                        rbNo.IsChecked = false;
                    }
                    else
                    {
                        rbSi.IsChecked = false;
                        rbNo.IsChecked = true;
                    }

                    Comuna co = new Comuna();
                    co.id_comuna = c.id_comuna;
                    co.Read();
                    cboComuna.Text = co.nombre;//Cambiar a nombre

                    btnModificar.Visibility = Visibility.Visible;
                    btnGuardar.Visibility = Visibility.Hidden;

                }
                else
                {
                    await this.ShowMessageAsync("Mensaje:",
                     string.Format("No se encontraron resultados!"));
                    /*MessageBox.Show("No se encontraron resultados!");*/
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                     string.Format("Error al Buscar Información! "));
                /*MessageBox.Show("error al buscar");*/
                Logger.Mensaje(ex.Message);

            }
        }

        private void txtRut_KeyDown(object sender, KeyEventArgs e)
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

        //añadir formato al rut

        private void txtRut_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text.Length >= 7 && txtRut.Text.Length <= 8)
            {
                string v = new Verificar().ValidarRut(txtRut.Text);
                txtDV.Text = v;
                try
                {
                    string rutSinFormato = txtRut.Text;

                    //si el rut ingresado tiene "." o "," o "-" son ratirados para realizar la formula 
                    rutSinFormato = rutSinFormato.Replace(",", "");
                    rutSinFormato = rutSinFormato.Replace(".", "");
                    rutSinFormato = rutSinFormato.Replace("-", "");
                    string rutFormateado = String.Empty;

                    //obtengo la parte numerica del RUT
                    //string rutTemporal = rutSinFormato.Substring(0, rutSinFormato.Length - 1);
                    string rutTemporal = rutSinFormato;
                    //obtengo el Digito Verificador del RUT
                    //string dv = rutSinFormato.Substring(rutSinFormato.Length - 1, 1);

                    Int64 rut;

                    //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                    if (!Int64.TryParse(rutTemporal, out rut))
                    {
                        rut = 0;
                    }

                    //este comando es el que formatea con los separadores de miles
                    //rutFormateado = rut.ToString("N0"); (11.111.111-1)
                    rutFormateado = rut.ToString();

                    if (rutFormateado.Equals("0"))
                    {
                        rutFormateado = string.Empty;
                    }
                    else
                    {
                        //si no hubo problemas con el formateo agrego el DV a la salida
                        // rutFormateado += "-" + dv;

                        //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                        rutFormateado = rutFormateado.Replace(",", ".");
                    }

                    //se pasa a mayuscula si tiene letra k
                    rutFormateado = rutFormateado.ToUpper();

                    //Si se uso rutFormateado = rut.ToString("N0"); la salida esperada para el ejemplo es 99.999.999-K
                    txtRut.Text = rutFormateado;
                }
                catch (Exception)
                {

                }
            }
            else
            {
                txtRut.Text = "";
            }
        }

       
    }
}
