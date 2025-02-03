using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using app.Models.Habitaciones;
using app.Models.Reservas;


namespace app.ViewModel.Habitaciones
{
    public class HabitacionViewModel
    {
        private readonly HttpClient httpClient = new HttpClient();
        // Método para buscar habitaciones según los criterios
        public async Task<List<Habitacion>> BuscarHabitaciones(int numeroHuespedes, bool camaExtra, bool cuna, DateTime fechaEntrada, DateTime fechaSalida, decimal precioMaximo, bool soloOfertas)
        {
            try
            {
                // Solicitud GET para obtener todas las reservas
                var reservasResponse = await httpClient.GetAsync("http://127.0.0.1:3505/Reserva/getAll");
                reservasResponse.EnsureSuccessStatusCode();
                var reservasJson = await reservasResponse.Content.ReadAsStringAsync();

                // Deserializar las reservas
                var reservas = JsonConvert.DeserializeObject<List<Reserva>>(reservasJson);

                // Obtener todas las habitaciones
                var habitacionesResponse = await httpClient.GetAsync("http://127.0.0.1:3505/Habitacion/habitaciones");
                habitacionesResponse.EnsureSuccessStatusCode();
                var habitacionesJson = await habitacionesResponse.Content.ReadAsStringAsync();

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
                MessageBox.Show($"📌 Total reservas obtenidas: {reservas.Count}", "Depuración", MessageBoxButton.OK, MessageBoxImage.Information);

                var habitacionesDisponibles = habitacionesFiltradas
                    .Where(h => !reservas.Any(r =>
                    {
                        try
                        {
                            // 💡 Mostrar cada reserva procesada
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
                    }))
                    .ToList();

                // Mostrar cuántas habitaciones quedan disponibles
                MessageBox.Show($"Habitaciones disponibles tras filtro: {habitacionesDisponibles.Count}", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);

                // Retornar habitaciones disponibles
                return habitacionesDisponibles;
            }
            catch (Exception ex)
            {
                // Manejar excepciones y mostrar mensajes de error
                MessageBox.Show($"Error al buscar habitaciones: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Habitacion>(); // Retornar una lista vacía en caso de error
            }
        }
    }
}
