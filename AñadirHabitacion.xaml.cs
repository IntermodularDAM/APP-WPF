using IntermodularWPF;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace app.View.Habitaciones
{
    /// <summary>
    /// Interaction logic for AñadirHabitacion.xaml
    /// </summary>
    public partial class AñadirHabitacion : Window
    {
        public AñadirHabitacion()
        {
            InitializeComponent();
            Opcion.IsChecked = false; // Cama extra
            Opcion1.IsChecked = false;
        }

        private async void btnAgregarHabitacion_Click(object sender, RoutedEventArgs e)
        {
            // Recopilar datos del formulario
            var num_planta = int.Parse(txtNumPlanta.Text);
            var tipo = txtTipo.Text;
            var capacidad = txtCapacidad.SelectedItem != null ? (txtCapacidad.SelectedItem as ComboBoxItem)?.Content.ToString() : string.Empty;
            var descripcion = txtDescripcion.Text;
            var precio_noche = int.Parse(txtPrecio.Text);
            var estado = true; // Valor por defecto
            var precio_noche_original = int.Parse(txtPrecioOriginal.Text);

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
                estado
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
    }
}
