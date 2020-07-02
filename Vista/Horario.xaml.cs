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
using BibliotecaDALC;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using Oracle.ManagedDataAccess.Client;

using System.Data;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para Horario.xaml
    /// </summary>
    public partial class Horario : MetroWindow
    {
        OracleConnection conn = null;
        int hora = 0, minutos=0;
        public Horario()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();
            cbEquipo.Focus();
            cbEquipo.SelectedIndex = 0;
            txtHora.Text = DateTime.Now.Hour.ToString();
            txtMinuto.Text = DateTime.Now.Minute.ToString();
            txtHoraHasta.Text = DateTime.Now.Hour.ToString();
            txtMinHasta.Text = DateTime.Now.Minute.ToString();

            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }

        }
        
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private bool Agregar(BibliotecaNegocio.Agenda ag)
        {
            try
            {
                
                OracleCommand CMD = new OracleCommand();
                //que tipo de tipo voy a ejecutar
                CMD.CommandType = System.Data.CommandType.StoredProcedure;
                //nombre de la conexion
                CMD.Connection = conn;
                //nombre del procedimeinto almacenado
                CMD.CommandText = "SP_AGREGAR_AGENDA";
                //////////se crea un nuevo de tipo parametro//nombre parámetro//el tipo//el largo// y el valor es igual al de la clase
                CMD.Parameters.Add(new OracleParameter("P_DIA", OracleDbType.Date)).Value = ag.dia;
                CMD.Parameters.Add(new OracleParameter("P_HORA", OracleDbType.Varchar2, 100)).Value = ag.hora;
                CMD.Parameters.Add(new OracleParameter("P_ID_EQUIPO", OracleDbType.Int32)).Value = ag.id_equipo;
                //se abre la conexion
                conn.Open();
                //se ejecuta la query CON  VARIABLE DE SALIDA en caso de tener
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();
                return true;
            }

            catch (Exception ex)
            {

                return false;
                Logger.Mensaje(ex.Message);

            }
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime Fecha = ClFecha.SelectedDate.Value.Date;
                string Hora = txtHora.Text + ":" + txtMinuto.Text + " - " + txtHoraHasta.Text + ":" + txtMinHasta.Text;
                int equipo = ((comboBoxItem1)cbEquipo.SelectedItem).id;//Guardo el id

                BibliotecaNegocio.Agenda c = new BibliotecaNegocio.Agenda()
                {
                    dia = Fecha,
                    hora= Hora,
                    id_equipo = equipo
                };
                bool resp = Agregar(c);
                await this.ShowMessageAsync("Mensaje:",
                      string.Format(resp ? "Guardado" : "No Guardado"));
                /*MessageBox.Show(resp ? "Guardado" : "No Guardado");*/

                //-----------------------------------------------------------------------------------------------
                //MOSTRAR LISTA DE ERRORES (validación de la clase)
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


        private void btnMasHora_Click_1(object sender, RoutedEventArgs e)
        {
            hora++;
            if (hora > 24)
            {
                hora = 0;
            }
            txtHora.Text = hora.ToString();
        }

        private void btnMenosHora_Copy_Click(object sender, RoutedEventArgs e)
        {
            hora--;
            if (hora < 0)
            {
                hora = 24;
            }
            txtHora.Text = hora.ToString();
        }

        private void btnMasMin_Click(object sender, RoutedEventArgs e)
        {
            minutos++;
            if (minutos > 59)
            {
                minutos = 0;
            }
            txtMinuto.Text = minutos.ToString();
        }

        private void btnMasHoraHasta_Click(object sender, RoutedEventArgs e)
        {
            hora++;
            if (hora > 24)
            {
                hora = 0;
            }
            txtHora.Text = hora.ToString();
        }

        private void btnMenosHoraHasta_Click(object sender, RoutedEventArgs e)
        {
            hora--;
            if (hora < 0)
            {
                hora = 24;
            }
            txtHora.Text = hora.ToString();
        }

        private void btnMasMinHasta_Click(object sender, RoutedEventArgs e)
        {
            minutos++;
            if (minutos > 59)
            {
                minutos = 0;
            }
            txtMinuto.Text = minutos.ToString();
        }

        private void btnMenosMinHasta_Click(object sender, RoutedEventArgs e)
        {
            minutos--;
            if (minutos < 0)
            {
                minutos = 59;
            }
            txtMinuto.Text = minutos.ToString();
        }

        private void btnMenosMin_Click(object sender, RoutedEventArgs e)
        {
            minutos--;
            if (minutos < 0)
            {
                minutos = 59;
            }
            txtMinuto.Text = minutos.ToString();
        }      


    }
}
