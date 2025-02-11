using app.Models.Habitaciones;
using app.Models.Reservas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app.ViewModel.Habitaciones
{
    internal class HabitacionesViewModel : INotifyPropertyChanged
    {
        private const string ApiUrlHabitaciones = "http://127.0.0.1:3505/Habitacion/habitaciones";
        private ObservableCollection<Habitacion> allHabitaciones;
        public ObservableCollection<Habitacion> AllHabitaciones
        {
            get => allHabitaciones;
            set { allHabitaciones = value; OnPropertyChanged("AllHabitaciones"); }
        }

        private ObservableCollection<Habitacion> habitacionesDisponibles;
        public ObservableCollection<Habitacion> HabitacionesDisponibles
        {
            get => habitacionesDisponibles;
            set { habitacionesDisponibles = value; OnPropertyChanged("HabitacionesDisponibles"); }
        }


        private DateTime fechaEntrada = DateTime.Today;
        public DateTime FechaEntrada
        {
            get => fechaEntrada;
            set { fechaEntrada = value; OnPropertyChanged(nameof(FechaEntrada)); }
        }

        private DateTime fechaSalida = DateTime.Today.AddDays(1);
        public DateTime FechaSalida
        {
            get => fechaSalida;
            set { fechaSalida = value; OnPropertyChanged(nameof(FechaSalida)); }
        }


        public HabitacionesViewModel()
        {
            AllHabitaciones = new ObservableCollection<Habitacion>();
        }

        private readonly HttpClient httpClient = new HttpClient();
        // Método para buscar habitaciones según los criterios
        public async Task<List<Habitacion>> BuscarHabitaciones(int numeroHuespedes, bool camaExtra, bool cuna, DateTime fechaEntrada, DateTime fechaSalida, double precioMaximo, bool soloOfertas)
        {
            try
            {
                var habitacionesDisponibless = new List<Habitacion>();
                MessageBox.Show("Entro en el Try", "Info");
                // Solicitud GET para obtener todas las reservas
                var reservasResponse = await httpClient.GetAsync("http://127.0.0.1:3505/Reserva/getAll");

                if (reservasResponse.IsSuccessStatusCode) {
                    var reservasJson = await reservasResponse.Content.ReadAsStringAsync();

                    Debug.WriteLine($"Status: {reservasResponse.StatusCode} Content: {reservasJson}");

                    MessageBox.Show("He recogido las Reservas", "Info");

                    // Deserializar las reservas
                    Debug.WriteLine($"Iniciando");
                    //var reservas = JsonConvert.DeserializeObject<List<ReservaBase>>(reservasJson);
                    Debug.WriteLine($"El reservas no ha fallado.");
                    var reservas = JsonConvert.DeserializeObject<ApiResponse<List<ReservaBase>>>(reservasJson);
                    Debug.WriteLine($"El reservas2 no ha fallado");
                    MessageBox.Show("He deserializado las reservas", "Info");

                    //Debug.WriteLine($"Estado Reserva: {reservas.ToString()}");
                    Debug.WriteLine($"Estado Reserva2: {reservas.reservas.ToString()}");

                    try
                    {
                        MessageBox.Show("Entro en el try de habitaciones", "Info");
                        // Obtener todas las habitaciones
                        var habitacionesResponse = await httpClient.GetAsync("http://127.0.0.1:3505/Habitacion/habitaciones");

                        if (habitacionesResponse.IsSuccessStatusCode)
                        {
                            var habitacionesJson = await habitacionesResponse.Content.ReadAsStringAsync();
                            MessageBox.Show("He recogido las habitaciones", "Info");

                            // Deserializar las habitaciones directamente como una lista
                            var habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(habitacionesJson);

                            // Validar que las habitaciones y reservas se cargaron correctamente
                            if (habitaciones == null || reservas == null)
                            {
                                MessageBox.Show("Error al cargar habitaciones o reservas desde la API.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return new List<Habitacion>(); // Retornar una lista vacía en caso de error
                            }

                            // Filtrar habitaciones según los criterios proporcionados
                            var habitacionesFiltradas = habitaciones
                                .Where(h => h.capacidad == numeroHuespedes) // Filtra por capacidad
                                .Where(h => h.opciones.CamaExtra == camaExtra) // Filtra por opción de cama extra
                                .Where(h => h.opciones.Cuna == cuna) // Filtra por opción de cuna
                                .Where(h => h.precio_noche <= precioMaximo) // Filtra por precio
                                .Where(h => h.tieneOferta == soloOfertas) // Filtra por ofertas si 'soloOfertas' es true
                                .ToList();

                            // 🔍 Comprobar si hay reservas
                            //MessageBox.Show($"📌 Total reservas obtenidas: {reservas.Count}", "Depuración", MessageBoxButton.OK, MessageBoxImage.Information);

                            habitacionesDisponibless = habitacionesFiltradas
                                .Where(h => !reservas.reservas.Any(r =>
                                {
                                    try
                                    {
                                        // Mostrar cada reserva procesada
                                        MessageBox.Show($"Procesando reserva {r._id} para habitación {r.id_hab}", "Depuración", MessageBoxButton.OK, MessageBoxImage.Information);

                                        // Verificar que la reserva corresponde a esta habitación
                                        if (r.id_hab.Trim().ToLower() != h._id.Trim().ToLower())
                                        {
                                            MessageBox.Show($"La reserva {r._id} no coincide con la habitación {h._id}. Se ignora.",
                                                "Filtrado", MessageBoxButton.OK, MessageBoxImage.Warning);
                                            return false;
                                        }

                                        // Convertir fechas
                                        if (!DateTime.TryParseExact(r.fecha_check_in, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaCheckIn) ||
                                            !DateTime.TryParseExact(r.fecha_check_out, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaCheckOut))
                                        {
                                            MessageBox.Show($"Error al convertir fechas para la reserva {r._id}: {r.fecha_check_in} - {r.fecha_check_out}",
                                                "Error de Fecha", MessageBoxButton.OK, MessageBoxImage.Warning);
                                            return false;
                                        }

                                        // Agregar un día a la fecha de salida
                                        fechaCheckOut = fechaCheckOut.AddDays(1);

                                        // Verificar si hay solapamiento
                                        bool solapan = fechaCheckIn < fechaSalida && fechaCheckOut > fechaEntrada;

                                        // Mostrar comparación de fechas
                                        MessageBox.Show($"Habitación: {h._id} - Reserva: {r._id}\n"
                                            + $"Check-In Reserva: {fechaCheckIn:yyyy-MM-dd}\n"
                                            + $"Check-Out Reserva: {fechaCheckOut:yyyy-MM-dd}\n"
                                            + $"Rango usuario: {fechaEntrada:yyyy-MM-dd} ➝ {fechaSalida:yyyy-MM-dd}\n"
                                            + $"¿Se solapan? {solapan}",
                                            "Depuración Fechas", MessageBoxButton.OK, MessageBoxImage.Information);

                                        return solapan;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Error al procesar reserva {r._id}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return false;
                                    }
                                })).ToList();
                        }

                        
                    } catch (Exception e)
                    {
                        MessageBox.Show("Error en la recogida de las habitaciones. " + e.Message);
                    }

                    // Mostrar cuántas habitaciones quedan disponibles
                    MessageBox.Show($"Habitaciones disponibles tras filtro: {habitacionesDisponibless.Count}", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                // Retornar habitaciones disponibles
                return habitacionesDisponibless;
            }
            catch (Exception ex)
            {
                // Manejar excepciones y mostrar mensajes de error
                MessageBox.Show($"Error al buscar habitaciones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Habitacion>(); // Retornar una lista vacía en caso de error
            }
        }

        // Método async Task en lugar de async void
        public async  Task CargarTodasHabitaciones()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(ApiUrlHabitaciones);
                    var habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(response);

                    AllHabitaciones = new ObservableCollection<Habitacion>(); // Actualiza la colección

                    foreach (var habitacion in habitaciones)
                    {
                        if (habitacion.estado == true && !string.IsNullOrEmpty(habitacion._id))
                        {
                            AllHabitaciones.Add(new Habitacion
                            {
                                _id = habitacion._id,
                                num_planta = habitacion.num_planta,
                                nombre = habitacion.nombre,
                                tipo = habitacion.tipo,
                                capacidad = habitacion.capacidad,
                                descripcion = habitacion.descripcion,
                                opciones = new Opciones
                                {
                                    CamaExtra = habitacion.opciones.CamaExtra,
                                    Cuna = habitacion.opciones.Cuna,
                                },
                                precio_noche = habitacion.precio_noche,
                                precio_noche_original = habitacion.precio_noche_original,
                                tieneOferta = habitacion.tieneOferta,
                                estado = habitacion.estado,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las habitaciones: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
