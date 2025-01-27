using app.View.Usuarios.Login;
using IntermodularWPF;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace app.View.Habitaciones
{
    /// <summary>
    /// Interaction logic for BuscadorHabitaciones.xaml
    /// </summary>
    public partial class BuscadorHabitaciones : Window
    {
        private static readonly HttpClient httpClient=new HttpClient();


        public BuscadorHabitaciones()
        {
            InitializeComponent();
           httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        // Modelo de Habitaciones en MongoDB
        public class Habitacion
        {
            public string _id { get; set; }
            public int num_planta {  get; set; }
            public string tipo { get; set; }
            public string capacidad { get; set; }
            public string descripcion { get; set; }
            public Opciones opciones { get; set; }    
            public decimal precio_noche { get; set; }
            public decimal? precio_noche_original { get; set; }
            public bool tieneOferta { get; set; }
            public bool estado { get; set; }
        }

        public class Opciones
        {
            public bool CamaExtra { get; set; }
            public bool Cuna { get; set; }
        }

        public class Reserva
        {
            public string _id { get; set; }
            public string id_hab { get; set; }
            public string fecha_check_in { get; set; }
            public string fecha_check_out { get; set; }
        }

        public class ApiResponse<T>
        {
            public string Status { get; set; }
            public T Data { get; set; }
        }


        
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
                var reservasResponseObj = JsonConvert.DeserializeObject<ApiResponse<List<Reserva>>>(reservasJson);
                var reservas = reservasResponseObj?.Data ?? new List<Reserva>();

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
                    .Where(h => int.Parse(h.capacidad) >= numeroHuespedes) // Filtra por capacidad
                    .Where(h => h.opciones.CamaExtra == camaExtra) // Filtra por opción de cama extra
                    .Where(h => h.opciones.Cuna == cuna) // Filtra por opción de cuna
                    .Where(h => h.precio_noche <= precioMaximo) // Filtra por precio
                    .Where(h => h.tieneOferta== soloOfertas) // Filtra por ofertas si 'soloOfertas' es true
                    .ToList();

                // Filtrar habitaciones ocupadas según las reservas
                var habitacionesDisponibles = habitacionesFiltradas
                    .Where(h => !reservas.Any(r =>
                    {
                        // Intentar convertir las fechas de string a DateTime
                        if (DateTime.TryParse(r.fecha_check_in, out var fechaCheckIn) &&
                            DateTime.TryParse(r.fecha_check_out, out var fechaCheckOut))
                        {
                            // Comparar las fechas con las proporcionadas por el usuario
                            return r.id_hab == h._id &&
                                   fechaCheckIn < fechaSalida && // Fecha de entrada de la reserva es antes de la salida del usuario
                                   fechaCheckOut > fechaEntrada; // Fecha de salida de la reserva es después de la entrada del usuario
                        }
                        return false; // Si no se pueden convertir las fechas, asumir que no hay conflicto
                    }))
                    .ToList();

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


        // Evento del botón "Volver"
        private void Click_btnVolver(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            Close();
        }


        // Evento del botón "Añadir habitacion"
        private void btn_AddHabitacion_Click(object sender, RoutedEventArgs e)
        {
            AñadirHabitacion employeeWindow = new AñadirHabitacion();
            employeeWindow.Show();
            Close();
        }


        // Evento del botón "Buscar"
        private async void btn_Buscar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si la fecha de entrada y salida son válidas
            if (fecha_in.SelectedDate == null || fecha_out.SelectedDate == null)
            {
                MessageBox.Show("Por favor, selecciona fechas válidas.");
                return;
            }

            // Recoger los valores del formulario
            int numeroHuespedes = int.Parse(((ComboBoxItem)comboBoxHuespedes.SelectedItem).Content.ToString());
            DateTime fechaEntrada = fecha_in.SelectedDate.Value;
            DateTime fechaSalida = fecha_out.SelectedDate.Value;
            decimal precioMaximo = Convert.ToDecimal(sliderPreu.Value);

            // Verificar si los valores son correctos
            if (numeroHuespedes == 0)
            {
                MessageBox.Show("Número de huéspedes no válido.");
                return;
            }

            // Recoger los valores de los CheckBox (CamaExtra y Cuna)
            bool camaExtra = txtOp.IsChecked == true;
            bool cuna = txtOp2.IsChecked == true;


            // Buscar habitaciones usando la API
            var habitacionesEncontradas = await BuscarHabitaciones(numeroHuespedes, camaExtra, cuna, fechaEntrada, fechaSalida, precioMaximo, false);

            // Mostrar resultados
            MostrarResultados(habitacionesEncontradas);
        }



        // Método para mostrar los resultados encontrados
        private void MostrarResultados(List<Habitacion> habitaciones)
        {
            stackPanelResultados.Children.Clear();

            if (habitaciones.Count == 0)
            {
                var noResultados = new Label
                {
                    Content = "No se encontraron habitaciones disponibles.",
                    FontSize = 25,
                    Foreground = Brushes.Red,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 450, 0, 0)
                };
                stackPanelResultados.Children.Add(noResultados);
                return;
            }

            foreach (var habitacion in habitaciones)
            {
                var stackPanelHabitacion = new StackPanel { VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(10) };

                var imagen = new Image { Width = 220, Height = 220, Source = new BitmapImage(new Uri("/Image/habitacion.png", UriKind.Relative)) };
                var nombre = new Label { HorizontalAlignment = HorizontalAlignment.Center, FontSize = 20, Margin = new Thickness(0, 10, 0, 5), Content = habitacion.tipo };
                
                var precio = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center };
                if (habitacion.tieneOferta&& habitacion.precio_noche_original.HasValue)
                {
                    var precioTachado = new TextBlock
                    {
                        Text = $"{habitacion.precio_noche_original}€",
                        TextDecorations = TextDecorations.Strikethrough,
                        Foreground = Brushes.Gray,
                        Margin = new Thickness(0, 0, 10, 0)
                    };
                    precio.Children.Add(precioTachado);
                }
                var precioActual = new Label { FontSize = 16, Foreground = Brushes.DarkGreen, Content = $"{habitacion.precio_noche}€ / noche" };
                precio.Children.Add(precioActual);

                // Estado de la habitación
                var estadoLabel = new Label
                {
                    Content = habitacion.estado ? "Estado: Activa" : "Estado: Baja",
                    FontSize = 14,
                    Foreground = habitacion.estado ? Brushes.Green : Brushes.Red,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                // Botón para editar
                var botonEditar = new Button
                {
                    Content = "Editar",
                    Height = 30,
                    Width = 120,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(150, 200, 250)),
                    BorderBrush = Brushes.DarkBlue
                };
                botonEditar.Click += (sender, e) => EditarHabitacion(habitacion);

                // Botón para eliminar
                var botonEliminar = new Button
                {
                    Content = "Eliminar",
                    Height = 30,
                    Width = 120,
                    Margin = new Thickness(5),
                    Background = Brushes.Red,
                    Foreground = Brushes.White
                };
                botonEliminar.Click += (sender, e) => EliminarHabitacion(habitacion);

                // Añadir controles a la UI
                stackPanelHabitacion.Children.Add(imagen);
                stackPanelHabitacion.Children.Add(nombre);
                stackPanelHabitacion.Children.Add(precio);
                stackPanelHabitacion.Children.Add(estadoLabel);
                stackPanelHabitacion.Children.Add(botonEditar);
                stackPanelHabitacion.Children.Add(botonEliminar);

                stackPanelResultados.Children.Add(stackPanelHabitacion);
            }
        }

        /*// Método para editar una habitación
        private async void EditarHabitacion(Habitacion habitacion)
          {
              double precioHabitacion = (double)habitacion.precio_noche;
              double precioOriginal = (double)habitacion.precio_noche_original; // Asumiendo que tienes un campo para el precio original
              bool estado = habitacion.estado;

              // Crear y mostrar la ventana de edición
              var editarWindow = new EditarHabitacion(
                  habitacion.tipo,
                  habitacion.capacidad,
                  precioHabitacion,
                  habitacion.descripcion,
                  habitacion.opciones.CamaExtra, // Asumimos que opciones es un string y contiene la palabra "CamaExtra" si es verdadero
                  habitacion.opciones.Cuna,      // Igualmente, para "Cuna"
                  precioOriginal,
                  estado
              );

              if (editarWindow.ShowDialog() == true)
              {
                  // Si el usuario aceptó los cambios
                  var nuevoTipo = editarWindow.NuevoTipo;
                  var nuevaCapacidad = editarWindow.NuevaCapacidad;
                  var nuevaDescripcion = editarWindow.NuevaDescripcion;
                  var nuevoPrecio = editarWindow.NuevoPrecio;
                  var nuevaOpcion = new Opciones
                  {
                      CamaExtra = editarWindow.CamaExtra,
                      Cuna = editarWindow.Cuna
                  };
                  bool nuevoEstado = editarWindow.Estado;

                  // Crear objeto con los datos actualizados
                  habitacion.tipo = nuevoTipo;
                  habitacion.capacidad = nuevaCapacidad;
                  habitacion.descripcion = nuevaDescripcion;
                  precioHabitacion = nuevoPrecio;
                  habitacion.opciones = nuevaOpcion;
                  estado = nuevoEstado;

                  if (string.IsNullOrEmpty(habitacion._id))
                  {
                      MessageBox.Show("El _id de la habitación no es válido o está vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                      return; // Salir del método si el _id no es válido
                  }
                  else
                  {
                      MessageBox.Show($"El _id de la habitación es: {habitacion._id}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                  }


                  // Realizar la actualización en la API
                  var jsonContent = new StringContent(JsonConvert.SerializeObject(habitacion), Encoding.UTF8, "application/json");
                  var response = await httpClient.PutAsync($"http://127.0.0.1:3505/Habitacion/Actualizar/{habitacion._id}", jsonContent);


                  if (response.IsSuccessStatusCode)
                  {
                      MessageBox.Show("Habitación actualizada correctamente.");
                      // Aquí puedes recargar la lista de habitaciones si es necesario
                       btn_Buscar_Click(null, null);
                  }
                  else
                  {
                      var errorContent = await response.Content.ReadAsStringAsync();
                      MessageBox.Show($"Error al actualizar la habitación: {errorContent}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                  }
              }
          }*/

        // Método para editar una habitación
        private async void EditarHabitacion(Habitacion habitacion)
        {
            double precioHabitacion = (double)habitacion.precio_noche;
            double precioOriginal = (double)habitacion.precio_noche_original; // Asumiendo que tienes un campo para el precio original
            bool estado = habitacion.estado;

            // Crear y mostrar la ventana de edición
            var editarWindow = new EditarHabitacion(
                habitacion.tipo,
                habitacion.capacidad,
                precioHabitacion,
                habitacion.descripcion,
                habitacion.opciones.CamaExtra, // Asumimos que opciones es un string y contiene la palabra "CamaExtra" si es verdadero
                habitacion.opciones.Cuna,      // Igualmente, para "Cuna"
                precioOriginal,
                estado
            );

            if (editarWindow.ShowDialog() == true)
            {
                // Si el usuario aceptó los cambios
                var nuevoTipo = editarWindow.NuevoTipo;
                var nuevaCapacidad = editarWindow.NuevaCapacidad;
                var nuevaDescripcion = editarWindow.NuevaDescripcion;
                var nuevoPrecio = editarWindow.NuevoPrecio;
                var nuevaOpcion = new Opciones
                {
                    CamaExtra = editarWindow.CamaExtra,
                    Cuna = editarWindow.Cuna
                };
                bool nuevoEstado = editarWindow.Estado;

                // Crear objeto con los datos actualizados
                habitacion.tipo = nuevoTipo;
                habitacion.capacidad = nuevaCapacidad;
                habitacion.descripcion = nuevaDescripcion;
                habitacion.opciones = nuevaOpcion;

                if (string.IsNullOrEmpty(habitacion._id))
                {
                    MessageBox.Show("El _id de la habitación no es válido o está vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Salir del método si el _id no es válido
                }
                else
                {
                    MessageBox.Show($"El _id de la habitación es: {habitacion._id}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Actualizar precio y estado en MongoDB
                var connectionString = "mongodb+srv://admin:clue@receptaculum.y9i4n.mongodb.net/HotelIES"; // Cambia la URL si es necesario
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("HotelIES"); // Cambia el nombre de la base de datos
                var collection = database.GetCollection<BsonDocument>("habitacions");

                var filtro = Builders<BsonDocument>.Filter.Eq("_id", habitacion._id);
                var actualizacionMongo = Builders<BsonDocument>.Update
                    .Set("precio_noche", nuevoPrecio)
                    .Set("estado", nuevoEstado);

                try
                {
                    var resultadoMongo = await collection.UpdateOneAsync(filtro, actualizacionMongo);
                    if (resultadoMongo.ModifiedCount > 0)
                    {
                        MessageBox.Show("Precio y estado actualizados correctamente en MongoDB.");
                        btn_Buscar_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el precio y estado en MongoDB.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar en MongoDB: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Realizar la actualización del resto de los datos en la API
                var jsonContent = new StringContent(JsonConvert.SerializeObject(habitacion), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"http://127.0.0.1:3505/Habitacion/Actualizar/{habitacion._id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("El resto de los datos de la habitación se actualizaron correctamente en la API.");
                    // Aquí puedes recargar la lista de habitaciones si es necesario
                    btn_Buscar_Click(null, null);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al actualizar los datos en la API: {errorContent}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private async void EliminarHabitacion(Habitacion habitacion)
        {
            if (habitacion == null)
            {
                MessageBox.Show("No se ha seleccionado ninguna habitación para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Confirmar la eliminación
                var confirmacion = MessageBox.Show($"¿Estás seguro de que deseas eliminar la habitación con ID: {habitacion._id}?",
                                                   "Eliminar Habitación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirmacion != MessageBoxResult.Yes) return;

                // Enviar la solicitud DELETE a la API para eliminar la habitación
                var response = await httpClient.DeleteAsync($"http://127.0.0.1:3505/Habitacion/Eliminar/{habitacion._id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Habitación con ID: {habitacion._id} eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Actualizar la lista de habitaciones
                    btn_Buscar_Click(null, null); // Asegúrate de que este método recargue la lista correctamente
                       // Asegúrate de que este método también tenga un propósito claro
                }
                else
                {
                    MessageBox.Show($"Error al eliminar la habitación con ID: {habitacion._id}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la habitación: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Evento del botón "Ofertas"
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (fecha_in.SelectedDate == null || fecha_out.SelectedDate == null)
            {
                MessageBox.Show("Por favor, selecciona fechas válidas.");
                return;
            }

            int numeroHuespedes = int.Parse(((ComboBoxItem)comboBoxHuespedes.SelectedItem).Content.ToString());
            DateTime fechaEntrada = fecha_in.SelectedDate.Value;
            DateTime fechaSalida = fecha_out.SelectedDate.Value;
            decimal precioMaximo = Convert.ToDecimal(sliderPreu.Value);

            if (numeroHuespedes == 0)
            {
                MessageBox.Show("Número de huéspedes no válido.");
                return;
            }

            // Establecemos soloOfertas en true para filtrar solo habitaciones con ofertas
            bool camaExtra = txtOp.IsChecked == true;
            bool cuna = txtOp2.IsChecked == true;


            // Buscar habitaciones solo con ofertas
            var habitacionesEncontradas = await BuscarHabitaciones(numeroHuespedes, camaExtra, cuna, fechaEntrada, fechaSalida, precioMaximo, true);

            // Mostrar resultados
            MostrarResultados(habitacionesEncontradas);
        }

        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            Usuarios.MainUsuarios.MainUsuarios user = new Usuarios.MainUsuarios.MainUsuarios();
            user.Show();
            this.Close();
        }
    }
}
