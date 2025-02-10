using app.Models.Habitaciones;
using app.Models.Reservas;
using app.View.Home;
using app.View.Usuarios.InicioDeSesion;
using app.View.Usuarios.MainUsuarios;
using app.ViewModel.Habitaciones;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace app.View.Habitaciones
{
    /// <summary>
    /// Interaction logic for BuscadorHabitaciones.xaml
    /// </summary>
    public partial class BuscadorHabitaciones : Window
    {
        private static readonly HttpClient httpClient = new HttpClient();


        public BuscadorHabitaciones()
        {
            InitializeComponent();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
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
            double precioMaximo = Convert.ToDouble(sliderPreu.Value);

            // Verificar si los valores son correctos
            if (numeroHuespedes == 0)
            {
                MessageBox.Show("Número de huéspedes no válido.");
                return;
            }

            // Recoger los valores de los CheckBox (CamaExtra y Cuna)
            bool camaExtra = txtOp.IsChecked == true;
            bool cuna = txtOp2.IsChecked == true;

            var viewModel = new HabitacionesViewModel();


            // Buscar habitaciones usando la API
            var habitacionesEncontradas = await viewModel.BuscarHabitaciones(numeroHuespedes, camaExtra, cuna, fechaEntrada, fechaSalida, precioMaximo, false);

            // Mostrar resultados
            MostrarResultados(habitacionesEncontradas);
        }


        private void MostrarResultados(List<Habitacion> habitaciones)
        {
            // Cambiamos StackPanel por WrapPanel
            var wrapPanelResultados = new WrapPanel
            {
                Orientation = Orientation.Horizontal, // Los elementos fluyen horizontalmente
                HorizontalAlignment = HorizontalAlignment.Center
            };

            stackPanelResultados.Children.Clear();
            stackPanelResultados.Children.Add(wrapPanelResultados);

            if (habitaciones.Count == 0)
            {
                var noResultados = new Label
                {
                    Content = "No se encontraron habitaciones disponibles.",
                    FontSize = 25,
                    Foreground = Brushes.Red,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 100, 0, 0)
                };
                wrapPanelResultados.Children.Add(noResultados);
                return;
            }

            foreach (var habitacion in habitaciones)
            {
                // Usamos un Border para permitir Padding
                var borderHabitacion = new Border
                {
                    Width = 200, // Definimos un ancho para que se ajusten mejor en el WrapPanel
                    Margin = new Thickness(10),
                    Background = Brushes.LightGray,
                    Padding = new Thickness(10), // Aquí sí es válido el Padding
                    CornerRadius = new CornerRadius(8), // Opcional: esquinas redondeadas
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1)
                };

                var stackPanelHabitacion = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                // Cargar la imagen de la habitación desde Base64 (si existe)
                Image imagen = new Image { Width = 150, Height = 150 };  // Aumenté un poco el tamaño para mejor visualización

                if (!string.IsNullOrEmpty(habitacion.ImagenBase64))
                {
                    imagen.Source = ConvertBase64ToImage(habitacion.ImagenBase64);
                }
                else
                {
                    imagen.Source = new BitmapImage(new Uri("/Image/habitacion.png", UriKind.Relative)); // Imagen predeterminada
                }

                var nombre = new Label
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 20,
                    Margin = new Thickness(0, 10, 0, 5),
                    Content = habitacion.nombre
                };

                var precio = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center };

                if (habitacion.tieneOferta && habitacion.precio_noche_original.HasValue)
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

                var precioActual = new Label
                {
                    FontSize = 16,
                    Foreground = Brushes.DarkGreen,
                    Content = $"{habitacion.precio_noche}€ / noche"
                };
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

                // Añadir controles al StackPanel
                stackPanelHabitacion.Children.Add(imagen);
                stackPanelHabitacion.Children.Add(nombre);
                stackPanelHabitacion.Children.Add(precio);
                stackPanelHabitacion.Children.Add(estadoLabel);
                stackPanelHabitacion.Children.Add(botonEditar);
                stackPanelHabitacion.Children.Add(botonEliminar);

                // Añadir el StackPanel al Border
                borderHabitacion.Child = stackPanelHabitacion;

                // Añadir el Border al WrapPanel
                wrapPanelResultados.Children.Add(borderHabitacion);
            }
        }




        private BitmapImage ConvertBase64ToImage(string base64String)
        {
            // Convertir Base64 a imagen
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream stream = new MemoryStream(imageBytes);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }



        // Método para editar una habitación
        private async void EditarHabitacion(Habitacion habitacion)
        {
            double precioHabitacion = (double)habitacion.precio_noche;
            double precioOriginal = (double)habitacion.precio_noche_original; // Asumiendo que tienes un campo para el precio original
            bool estado = habitacion.estado;

            // Crear y mostrar la ventana de edición
            var editarWindow = new EditarHabitacion(
                habitacion.nombre,
                habitacion.tipo,
                habitacion.capacidad,
                precioHabitacion,
                habitacion.descripcion,
                habitacion.opciones.CamaExtra, // Asumimos que opciones es un string y contiene la palabra "CamaExtra" si es verdadero
                habitacion.opciones.Cuna,      // Igualmente, para "Cuna"
                precioOriginal,
                estado,
                habitacion.ImagenBase64
            );

            if (editarWindow.ShowDialog() == true)
            {
                // Si el usuario aceptó los cambios
                var nuevoNombre = editarWindow.NuevoNombre;
                var nuevoTipo = editarWindow.NuevoTipo;
                var nuevaCapacidad = int.Parse(editarWindow.NuevaCapacidad);
                var nuevaDescripcion = editarWindow.NuevaDescripcion;
                var nuevoPrecio = editarWindow.NuevoPrecio;
                var nuevoPrecioOriginal = editarWindow.NuevoPrecioOriginal;
                var nuevaOpcion = new Opciones
                {
                    CamaExtra = editarWindow.CamaExtra,
                    Cuna = editarWindow.Cuna
                };
                bool nuevoEstado = editarWindow.Estado;
                var nuevaImagen = editarWindow.ImagenBase64;

                bool tieneOferta = editarWindow.tieneOferta;

                if (string.IsNullOrEmpty(habitacion._id))
                {
                    MessageBox.Show("El _id de la habitación no es válido o está vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Salir del método si el _id no es válido
                }
                else
                {
                    MessageBox.Show($"El _id de la habitación es: {habitacion._id}", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Crear objeto con los datos actualizados
                habitacion.nombre = nuevoNombre;
                habitacion.tipo = nuevoTipo;
                habitacion.capacidad = nuevaCapacidad;
                habitacion.descripcion = nuevaDescripcion;
                habitacion.opciones = nuevaOpcion;
                habitacion.ImagenBase64 = nuevaImagen;
                habitacion.tieneOferta = tieneOferta;

                // Actualizar precio y estado en MongoDB
                var connectionString = "mongodb+srv://admin:clue@receptaculum.y9i4n.mongodb.net/HotelIES"; // Cambia la URL si es necesario
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("HotelIES"); // Cambia el nombre de la base de datos
                var collection = database.GetCollection<BsonDocument>("habitacions");

                var filtro = Builders<BsonDocument>.Filter.Eq("_id", habitacion._id);
                var actualizacionMongo = Builders<BsonDocument>.Update
                    .Set("precio_noche", nuevoPrecio)
                    .Set("precio_noche_original", nuevoPrecioOriginal)
                    //.Set("tieneOferta", tieneOferta)
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
            var reservasResponse = await httpClient.GetAsync("http://127.0.0.1:3505/Reserva/getAll");
            reservasResponse.EnsureSuccessStatusCode();
            var reservasJson = await reservasResponse.Content.ReadAsStringAsync();

            // Deserializar las reservas
            var reservas = JsonConvert.DeserializeObject<List<ReservaBase>>(reservasJson);

            if (habitacion == null)
            {

                MessageBox.Show("No se ha seleccionado ninguna habitación para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }
            else if ((habitacion.estado == true) || (reservas.Any(r => r.id_hab == habitacion._id)))
            {

                MessageBox.Show("No puedes eliminar la habitación del sistema", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            try
            {
                // Confirmar la eliminación
                var confirmacion = MessageBox.Show($"¿Estás seguro de que deseas eliminar la habitación con ID: {habitacion._id}?", "Eliminar Habitación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
            double precioMaximo = Convert.ToDouble(sliderPreu.Value);

            if (numeroHuespedes == 0)
            {
                MessageBox.Show("Número de huéspedes no válido.");
                return;
            }

            // Establecemos soloOfertas en true para filtrar solo habitaciones con ofertas
            bool camaExtra = txtOp.IsChecked == true;
            bool cuna = txtOp2.IsChecked == true;

            var viewModel = new HabitacionesViewModel();

            // Buscar habitaciones solo con ofertas
            var habitacionesEncontradas = await viewModel.BuscarHabitaciones(numeroHuespedes, camaExtra, cuna, fechaEntrada, fechaSalida, precioMaximo, true);

            // Mostrar resultados
            MostrarResultados(habitacionesEncontradas);
        }


        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            Inicio user = new Inicio();
            user.Show();
            this.Close();
        }
    }
}
