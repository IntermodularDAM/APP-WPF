using app.Models.Habitaciones;
using app.Models.Reservas;
using app.ViewModel.Habitaciones;
using app.ViewModel.Reservas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BuscadorReservas.xaml
    /// </summary>
    public partial class BuscadorReservas : Window
    {
        private readonly ReservaViewModel _viewModelR;
        private readonly HabitacionesViewModel _viewModelH;
        private bool isPrecioOriginalVisible = false;
        public ObservableCollection<Habitacion> AllHabitaciones { get; set; }
        public ObservableCollection<ReservaBase> AllReservas { get; set; }

        public BuscadorReservas()
        {
            InitializeComponent();

            _viewModelR = new ReservaViewModel();
            this.DataContext = _viewModelR;

            _viewModelH = new HabitacionesViewModel();
            this.DataContext = _viewModelH;

            _viewModelH.CargarTodasHabitaciones();
            _viewModelR.CargarTodasLasReservas();
        }

        public void CargarTodasHabitaciones()
        {
            // Cargar los datos desde la API o base de datos y asignarlos a AllHabitaciones
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetStringAsync("http://localhost:3505/Habitacion/getAll").Result;
                var habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(response);
                AllHabitaciones = new ObservableCollection<Habitacion>(habitaciones);
            }
        }

        public void CargarTodasReservas()
        {
            // Cargar las reservas desde la API o base de datos y asignarlas a AllReservas
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetStringAsync("http://localhost:3505/Reserva/getAll").Result;
                var reservas = JsonConvert.DeserializeObject<List<ReservaBase>>(response);
                AllReservas = new ObservableCollection<ReservaBase>(reservas);
            }
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar si las colecciones están inicializadas
                if (_viewModelH.AllHabitaciones == null || !_viewModelH.AllHabitaciones.Any())
                {
                    MessageBox.Show("No hay habitaciones disponibles para buscar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_viewModelR.AllReservas == null)
                {
                    MessageBox.Show("No se han cargado las reservas.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Número de huéspedes
                int numHuespedes = int.Parse(txtHuespedes.Text);

                // Precio máximo
                double precioMax = sliderPrecio.Value;

                // Extras seleccionados
                var extras = new List<string>();
                if (chkCuna.IsChecked == true) extras.Add("Cuna");
                if (chkCamaExtra.IsChecked == true) extras.Add("CamaExtra");

                // Fechas de entrada y salida
                string fechaEntrada = dpFechaEntrada.SelectedDate?.ToString("yyyy-MM-dd");
                string fechaSalida = dpFechaSalida.SelectedDate?.ToString("yyyy-MM-dd");

                // Validaciones iniciales
                if (string.IsNullOrEmpty(fechaEntrada) || string.IsNullOrEmpty(fechaSalida))
                {
                    MessageBox.Show("Seleccione las fechas de entrada y salida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.Compare(fechaEntrada, fechaSalida) >= 0)
                {
                    MessageBox.Show("La fecha de entrada debe ser anterior a la fecha de salida.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Filtrar habitaciones disponibles
                var habitacionesDisponibles = _viewModelH.AllHabitaciones
                .Where(h =>
                    h.capacidad >= numHuespedes && // Filtrar por número de huéspedes
                    h.precio_noche <= precioMax && // Filtrar por precio
                    (extras.Count == 0 || // Si no hay extras seleccionados, no filtra por ellos
                     (extras.Contains("Cuna") && h.opciones?.Cuna == true) || // Verifica si el extra "Cuna" está disponible
                     (extras.Contains("CamaExtra") && h.opciones?.CamaExtra == true)) && // Verifica si el extra "CamaExtra" está disponible
                    !_viewModelR.AllReservas.Any(r => // Filtrar por fechas no disponibles
                        r.id_hab == h._id && (
                            (string.Compare(fechaEntrada, r.fecha_check_in) >= 0 && string.Compare(fechaEntrada, r.fecha_check_out) < 0) || // La fecha de entrada se solapa
                            (string.Compare(fechaSalida, r.fecha_check_in) > 0 && string.Compare(fechaSalida, r.fecha_check_out) <= 0) || // La fecha de salida se solapa
                            (string.Compare(fechaEntrada, r.fecha_check_in) < 0 && string.Compare(fechaSalida, r.fecha_check_out) > 0) // La reserva está completamente dentro del rango solicitado
                        )
                    )
                )
                .ToList();

                // Mostrar resultados en la interfaz
                listResultados.ItemsSource = habitacionesDisponibles;

                // Manejo si no se encontraron resultados
                if (!habitacionesDisponibles.Any())
                {
                    MessageBox.Show("No se encontraron habitaciones que coincidan con los criterios.", "Sin resultados", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar habitaciones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOfertas_Click(object sender, RoutedEventArgs e)
        {
            if (isPrecioOriginalVisible)
            {
                listResultados.Columns.First(c => c.Header.ToString() == "Precio Original").Visibility = Visibility.Collapsed;
                listResultados.Columns.First(c => c.Header.ToString() == "Precio por Noche").Visibility = Visibility.Visible;
            }
            else
            {
                listResultados.Columns.First(c => c.Header.ToString() == "Precio por Noche").Visibility = Visibility.Collapsed;
                listResultados.Columns.First(c => c.Header.ToString() == "Precio Original").Visibility = Visibility.Visible;
            }

            // Alternar la bandera
            isPrecioOriginalVisible = !isPrecioOriginalVisible;
        }

        private void btnIncreaseHuespedes_Click(object sender, RoutedEventArgs e)
        {
            int currentHuespedes = int.Parse(txtHuespedes.Text);
            if (currentHuespedes < 5)
            {
                currentHuespedes++;
                txtHuespedes.Text = currentHuespedes.ToString();
            }
        }

        private void btnDecreaseHuespedes_Click(object sender, RoutedEventArgs e)
        {
            int currentHuespedes = int.Parse(txtHuespedes.Text);
            if (currentHuespedes > 1)
            {
                currentHuespedes--;
                txtHuespedes.Text = currentHuespedes.ToString();
            }
        }

        private void sliderPrecio_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtPrecio.Text = $"Máximo: {sliderPrecio.Value:F0}€";
        }

        private void btnReservar_Click(object sender, RoutedEventArgs e)
        {
            var selectedHab = listResultados.SelectedItem as Habitacion;

            if (selectedHab != null)
            {
                CrearReservas ventanaCrear = new CrearReservas(selectedHab);
                ventanaCrear.Owner = this;
                ventanaCrear.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainReservas ventanaMain = new MainReservas();
            ventanaMain.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModelR.CargarTodasLasReservas();
            _viewModelH.CargarTodasHabitaciones();
        }
    }
}
