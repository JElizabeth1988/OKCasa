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
    public partial class Horario : MetroWindow
    {
        OracleConnection conn = null;
        int horaDesde = int.Parse(DateTime.Now.Hour.ToString()), 
            minDesde= int.Parse(DateTime.Now.Minute.ToString()), 
            horaHasta = int.Parse(DateTime.Now.Hour.ToString()), 
            minHasta = int.Parse(DateTime.Now.Minute.ToString());
        public Horario()
        {
            InitializeComponent();
            conn = new Conexion().Getcone();//Instanciar conexión
            cbEquipo.Focus();
            cbEquipo.SelectedIndex = 0;
            txtHora.Text = DateTime.Now.Hour.ToString();//Hora
            txtMinuto.Text = DateTime.Now.Minute.ToString();//Minuto
            txtHoraHasta.Text = DateTime.Now.Hour.ToString();//Hora
            txtMinHasta.Text = DateTime.Now.Minute.ToString();//Minuto
            ClFecha.SelectedDate = DateTime.Now;//Día actual
            //ComboBox
            foreach (EquipoTecnico item in new EquipoTecnico().ReadAll())
            {
                comboBoxItem1 cb = new comboBoxItem1();
                cb.id = item.id_equipo;
                cb.nombre = item.nombre;
                cbEquipo.Items.Add(cb);
            }

        }
        //---------Botón Salir-----------------
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //---------Método Agregar------------------
        private bool Agregar(BibliotecaNegocio.Agenda ag)
        {
            try
            {
                
                OracleCommand CMD = new OracleCommand();
                //que tipo de comando voy a ejecutar
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
                //se ejecuta la query 
                CMD.ExecuteNonQuery();
                //se cierra la conexioin
                conn.Close();
                //Retorno
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

                string horaDesde = txtHora.Text;
                if (horaDesde.Length < 2)
                {
                    horaDesde = "0" + txtHora.Text;//agrego un cero antes si es de 1 dígito
                }

                string MinDesde = txtMinuto.Text;
                if (MinDesde.Length < 2)
                {
                    MinDesde = "0" + txtMinuto.Text;
                }

                string horaHasta = txtHoraHasta.Text;
                if (horaHasta.Length < 2)
                {
                    horaHasta = "0" + txtHoraHasta.Text;
                }

                string minHasta = txtMinHasta.Text;
                if (minHasta.Length < 2)
                {
                    minHasta = "0" + txtMinHasta.Text;
                }
                string Hora = horaDesde + ":" + MinDesde + " - " + horaHasta + ":" + minHasta;
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

                if (resp == true)
                {
                    cbEquipo.Focus();
                    cbEquipo.SelectedIndex = 0;
                    txtHora.Text = DateTime.Now.Hour.ToString();//Hora
                    txtMinuto.Text = DateTime.Now.Minute.ToString();//Minuto
                    txtHoraHasta.Text = DateTime.Now.Hour.ToString();//Hora
                    txtMinHasta.Text = DateTime.Now.Minute.ToString();//Minuto
                    ClFecha.SelectedDate = DateTime.Now;//Día actual

                }
                
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("Mensaje:",
                      string.Format("Error de ingreso de datos"));
                Logger.Mensaje(ex.Message);
            }
        }
        //Cargar Data Grid
        private void CargarGrilla()
        {
            try
            {
                List<BibliotecaNegocio.Agenda.ListaAgenda> lista = new List<BibliotecaNegocio.Agenda.ListaAgenda>();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = "SP_LISTAR_AGENDA2";
                cmd.Parameters.Add(new OracleParameter("AGENDAS", OracleDbType.RefCursor)).Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BibliotecaNegocio.Agenda.ListaAgenda s = new BibliotecaNegocio.Agenda.ListaAgenda();

                    //se obtiene el valor con getvalue es lo mismo pero con get
                    s.Id = int.Parse(dr.GetValue(0).ToString());
                    s.Fecha = dr.GetValue(1).ToString();
                    s.Hora = dr.GetValue(2).ToString();
                    s.Disponibilidad = dr.GetValue(3).ToString();
                    s.Equipo = dr.GetValue(4).ToString();

                    lista.Add(s);
                }
                conn.Close();

                dgLista.ItemsSource = lista;
                dgLista.Columns[0].Visibility = Visibility.Collapsed;//Ocullto la columna id                
            }
            catch (Exception ex)
            {
                Logger.Mensaje(ex.Message);
            }
        }
        //-------Listado----------------
        private async void btnLista_Click(object sender, RoutedEventArgs e)
        {
            CargarGrilla();
        }
        private void btnMasHora_Click_1(object sender, RoutedEventArgs e)
        {
            horaDesde++;
            if (horaDesde == 24)
            {
                horaDesde = 0;
            }
            txtHora.Text = horaDesde.ToString();
        }

        private void btnMenosHora_Copy_Click(object sender, RoutedEventArgs e)
        {
            horaDesde--;
            if (horaDesde <0)
            {
                horaDesde = 23;
            }
            txtHora.Text = horaDesde.ToString();
        }

        private void btnMasMin_Click(object sender, RoutedEventArgs e)
        {
            minDesde++;
            if (minDesde == 60)
            {
                minDesde = 0;
            }
 
            txtMinuto.Text = minDesde.ToString();
        }

        private void btnMasHoraHasta_Click(object sender, RoutedEventArgs e)
        {
            horaHasta++;
            if (horaHasta == 24)
            {
                horaHasta = 0;
            }
            txtHoraHasta.Text = horaHasta.ToString();
        }

        private void btnMenosHoraHasta_Click(object sender, RoutedEventArgs e)
        {
            horaHasta--;
            if (horaHasta <0)
            {
                horaHasta = 23;
            }
            txtHoraHasta.Text = horaHasta.ToString();
        }

        private void btnMasMinHasta_Click(object sender, RoutedEventArgs e)
        {
            minHasta++;
            if (minHasta ==60)
            {
                minHasta = 0;
            }
            txtMinHasta.Text = minHasta.ToString();
        }

        private void btnMenosMinHasta_Click(object sender, RoutedEventArgs e)
        {
            minHasta--;
            if (minHasta < 0)
            {
                minHasta = 59;
            }
            txtMinHasta.Text = minHasta.ToString();
        }

        private void btnMenosMin_Click(object sender, RoutedEventArgs e)
        {
            minDesde--;
            if (minDesde < 0)
            {
                minDesde = 59;
            }
            txtMinuto.Text = minDesde.ToString();
        }      


    }
}
