using IntermodularWPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AñadirHabitacion.xaml
    /// </summary>
    public partial class AñadirHabitacion : Window
    {
        private string ImagenBase64 = string.Empty;
        public AñadirHabitacion()
        {
            InitializeComponent();
            Opcion.IsChecked = false; // Cama extra
            Opcion1.IsChecked = false;
        }


        private async void btnAgregarHabitacion_Click(object sender, RoutedEventArgs e)
        {

            // Validar que los campos de precio no estén vacíos
            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || string.IsNullOrWhiteSpace(txtPrecioOriginal.Text))
            {
                MessageBox.Show("Valor de precio o precio original está vacío. Por favor corrígelo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detener la ejecución si los campos están vacíos
            }

            // Validar que los campos numéricos tengan un valor válido
            int num_planta;
            if (!int.TryParse(txtNumPlanta.Text, out num_planta))
            {
                MessageBox.Show("El número de planta no es válido. Por favor ingresa un número entero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detener la ejecución si el valor no es válido
            }

            int precio_noche;
            if (!int.TryParse(txtPrecio.Text, out precio_noche))
            {
                MessageBox.Show("El precio de la noche no es válido. Por favor ingresa un valor numérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detener la ejecución si el precio no es válido
            }

            int precio_noche_original;
            if (!int.TryParse(txtPrecioOriginal.Text, out precio_noche_original))
            {
                MessageBox.Show("El precio original no es válido. Por favor ingresa un valor numérico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Detener la ejecución si el precio original no es válido
            }

            var nombre = txtNom.Text;

            try
            {
                ValidarNombre(nombre);
                MessageBox.Show("Nombre validado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Recopilar datos del formulario

            var tipo = txtTipo.Text;
            var capacidad = txtCapacidad.SelectedItem != null ? (txtCapacidad.SelectedItem as ComboBoxItem)?.Content.ToString() : string.Empty;
            var descripcion = txtDescripcion.Text;

            var estado = true; // Valor por defecto


            // Opciones adicionales
            bool isCamaExtra = Opcion.IsChecked == true;
            bool isCuna = Opcion1.IsChecked == true;

            try
            {
                // Validación de capacidad según tipo de habitación
                if (tipo == "individual" && capacidad != "1")
                {
                    MessageBox.Show("Habitaciones individuales solo tienen capacidad para una persona", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (tipo == "doble" && capacidad != "2")
                {
                    MessageBox.Show("Habitaciones dobles solo tienen capacidad para dos personas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (tipo == "triple" && capacidad != "3")
                {
                    MessageBox.Show("Habitaciones triples solo tienen capacidad para tres personas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (tipo == "suite" && capacidad != "4" && capacidad != "5" && capacidad != "8")
                {
                    MessageBox.Show("Suites pueden tener capacidad para 4, 5 o 8 personas", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ha ocurrido un error durante la validación: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || string.IsNullOrEmpty(txtPrecioOriginal.Text))
            {
                MessageBox.Show("Valor de precio o precio original esta vacio. Por favor corrigelo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Validar el estado
            try
            {
                if (!string.IsNullOrWhiteSpace(txtEstado.Text))
                {
                    if (txtEstado.Text.ToLower() == "true")
                        estado = true;
                    else if (txtEstado.Text.ToLower() == "false")
                        estado = false;
                    else
                    {
                        MessageBox.Show("Valor de estado no válido. Se usará 'true' por defecto.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        estado = true;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error procesando el estado: {err.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool Oferta = false; // Definir si la habitación tiene una oferta
            if (precio_noche < precio_noche_original) // Esto es solo un ejemplo de cómo identificar si hay una oferta
            {
                Oferta = true;
            }

            // Crear el objeto que será enviado como JSON
            var nuevaHabitacion = new
            {
                num_planta,
                nombre,
                tipo,
                capacidad,
                descripcion,
                opciones = new
                {
                    CamaExtra = isCamaExtra,
                    Cuna = isCuna
                },
                precio_noche,
                precio_noche_original,
                tieneOferta = Oferta,
                estado,
                imagenBase64 = ImagenBase64
            };

            // Convertir el objeto a JSON
            var json = JsonConvert.SerializeObject(nuevaHabitacion);

            MessageBox.Show($"JSON generado:\n{json}", "Debug", MessageBoxButton.OK, MessageBoxImage.Information);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviar la solicitud HTTP POST
            using (var client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserSession.Instance.Token);
                    var response = await client.PostAsync("http://127.0.0.1:3505/Habitacion/habitaciones", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Habitación agregada con éxito", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Limpiar campos del formulario
                        txtNumPlanta.Text = "";
                        txtNom.Text = "";
                        txtTipo.Text = "";
                        txtCapacidad.Text = "";
                        txtPrecio.Text = "";
                        txtDescripcion.Text = "";
                        txtPrecioOriginal.Text = "";
                        Opcion.IsChecked = false;
                        Opcion1.IsChecked = false;
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error en la agregación de la habitación. Código de estado: {response.StatusCode}\nMensaje: {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al enviar la solicitud: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        private void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.");
            }

            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$"))
            {
                throw new ArgumentException("El nombre solo puede contener letras y espacios.");
            }
        }



        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            BuscadorHabitaciones bHabitacion = new BuscadorHabitaciones();
            bHabitacion.Show();
            this.Close();
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Si se marca un checkbox, desmarcar el otro
            if (sender == Opcion) // "Cama extra" seleccionado
            {
                Opcion1.IsChecked = false; // Desmarcar "Cuna"
            }
            else if (sender == Opcion1) // "Cuna" seleccionado
            {
                Opcion.IsChecked = false; // Desmarcar "Cama extra"
            }
        }


        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // No es estrictamente necesario, pero puedes manejar el desmarcado si es necesario
        }


        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar el archivo de la imagen
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Imagenes (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"; // Filtrar solo imágenes

            // Mostrar el cuadro de diálogo
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                // Obtener la ruta de la imagen seleccionada
                string imagePath = openFileDialog.FileName;

                try
                {
                    // Leer la imagen como un array de bytes
                    byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                    // Convertir los bytes a una cadena Base64
                    ImagenBase64 = Convert.ToBase64String(imageBytes);

                    // Aquí puedes mostrar un mensaje de confirmación
                    MessageBox.Show("Imagen seleccionada y convertida a Base64.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
