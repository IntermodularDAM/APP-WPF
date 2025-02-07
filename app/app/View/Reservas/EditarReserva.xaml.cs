using app.Models.Reservas;
using app.ViewModel.Reservas;
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
    /// Interaction logic for EditarReserva.xaml
    /// </summary>
    public partial class EditarReserva : Window
    {
        private ReservaBase _reserva;
        private readonly ReservaViewModel _modeloVista;
        public EditarReserva(ReservaBase selectedReservation, ReservaViewModel modeloVista)
        {
            InitializeComponent();

            _modeloVista = modeloVista;
            _reserva = selectedReservation;

            usuRes.Text = _reserva.id_usu;
            habRes.Text = _reserva.id_hab;

            // Establecer los valores en los controles
            dpFechaEntrada.SelectedDate = string.IsNullOrWhiteSpace(_reserva.fecha_check_in)
                ? (DateTime?)null
                : DateTime.Parse(_reserva.fecha_check_in);

            dpFechaSalida.SelectedDate = string.IsNullOrWhiteSpace(_reserva.fecha_check_out)
                ? (DateTime?)null
                : DateTime.Parse(_reserva.fecha_check_out);


            // Buscar el estado en el ComboBox y seleccionarlo
            foreach (ComboBoxItem item in cbEstadoReserva.Items)
            {
                if (item.Content.ToString() == _reserva.estado_reserva)
                {
                    cbEstadoReserva.SelectedItem = item;
                    break;
                }
            }
        }

        public EditarReserva()
        {
            InitializeComponent();
        }

        private async void btnGuardarPerfil_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores editados
            var fechaEntrada = dpFechaEntrada.SelectedDate;
            var fechaSalida = dpFechaSalida.SelectedDate;
            var estadoReserva = cbEstadoReserva.SelectedItem as ComboBoxItem;
            var estado = estadoReserva?.Content.ToString();

            // Verificar si las fechas y el estado están completos
            if (fechaEntrada.HasValue && fechaSalida.HasValue && !string.IsNullOrEmpty(estado))
            {
                // Actualizar la reserva con los nuevos valores
                _reserva.fecha_check_in = fechaEntrada.HasValue
                    ? fechaEntrada.Value.ToString("yyyy-MM-dd") // Ajusta el formato según sea necesario
                    : null;

                _reserva.fecha_check_out = fechaSalida.HasValue
                    ? fechaSalida.Value.ToString("yyyy-MM-dd")
                    : null;

                _reserva.estado_reserva = estado;

                HttpResponseMessage response = await _modeloVista.EditarReserva(_reserva._id, _reserva);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reserva editada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Mostrar mensaje de éxito y cerrar la ventana
                _modeloVista.CargarTodasLasReservas();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
