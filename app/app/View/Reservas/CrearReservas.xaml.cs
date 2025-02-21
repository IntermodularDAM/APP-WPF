using app.Models.Habitaciones;
using app.Models.Reservas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace app.View.Reservas
{
    /// <summary>
    /// Interaction logic for CrearReservas.xaml
    /// </summary>
    public partial class CrearReservas : Window
    {
        private Habitacion _habitacion;
        public int precioExtra = 0;
        private bool camaExtra = false;
        private bool cunaExtra = false;

        public CrearReservas(Habitacion habitacion)
        {
            InitializeComponent();
            _habitacion = habitacion;

            // Prellenar los campos con los datos de la habitación
            //txtNombreHabitacion.Text = _habitacion.nombre;
            //txtPrecio.Text = $"${_habitacion.precio}";

            txtNombreHabitacion.Text = _habitacion._id;
            txtPrecio.Text = "" + _habitacion.precio_noche + "€";
        }

        private async void btnGuardarReserva_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validar los campos
                if (dpFechaEntrada.SelectedDate == null || dpFechaSalida.SelectedDate == null || string.IsNullOrEmpty(txtCantidadPersonas.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Crear la reserva
                var reserva = new ReservaBase
                {
                    id_hab = _habitacion._id,
                    fecha_check_in = dpFechaEntrada.SelectedDate?.ToString("yyyy-MM-dd"),
                    fecha_check_out = dpFechaSalida.SelectedDate?.ToString("yyyy-MM-dd"),
                    id_usu = SettingsData.Default.idPerfil,
                    estado_reserva = "Confirmada"
                };

                // Enviar a la API
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://localhost:3505/Reserva/crearReserva", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Reserva creada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al crear la reserva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkCamaExtra_Checked(object sender, RoutedEventArgs e)
        {
            if (!camaExtra)
            {
                camaExtra = !camaExtra;
                precioExtra = precioExtra + 10;
                txtExtPrecio.Text = "+" + precioExtra + "€";
            }
            else
            {
                camaExtra = !camaExtra;
                precioExtra = precioExtra - 10;
                txtExtPrecio.Text = "+" + precioExtra + "€";
            }
        }

        private void chkCuna_Checked(object sender, RoutedEventArgs e)
        {
            if (!cunaExtra)
            {
                cunaExtra = !cunaExtra;
                precioExtra = precioExtra + 5;
                txtExtPrecio.Text = "+" + precioExtra + "€";
            }
            else
            {
                cunaExtra = !cunaExtra;
                precioExtra = precioExtra - 5;
                txtExtPrecio.Text = "+" + precioExtra + "€";
            }
        }
    }
}
