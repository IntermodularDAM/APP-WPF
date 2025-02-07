using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Interaction logic for EditarHabitacion.xaml
    /// </summary>
    public partial class EditarHabitacion : Window
    {
        // Propiedades públicas para devolver los valores editados
        public string NuevoNombre { get; private set; }
        public string NuevoTipo { get; private set; }
        public string NuevaCapacidad { get; private set; }
        public string NuevaDescripcion { get; private set; }
        public double NuevoPrecio { get; private set; }
        public bool CamaExtra { get; private set; }
        public bool Cuna { get; private set; }
        public double NuevoPrecioOriginal { get; private set; }
        public bool Estado { get; private set; }
        public bool tieneOferta { get; private set; }
        public string ImagenBase64 { get; private set; }

        // Constructor actualizado para recibir las opciones como parámetros
        public EditarHabitacion(string nombre, string tipo, int capacidad, double precio, string descripcion, bool camaExtra, bool cuna, double precioOriginal, bool estado, string imagenBase64 = null)
        {
            InitializeComponent();

            // Rellenar los controles con los valores actuales
            NomTextBox.Text = nombre;
            TipoTextBox.Text = tipo;
            PrecioTextBox.Text = precio.ToString("F2"); // Aseguramos formato consistente
            txtDescripcion.Text = descripcion;
            PrecioOriginalTextBox.Text = precioOriginal.ToString("F2"); // Guardamos el precio original
            EstadoCheckBox.IsChecked = estado; // Mostrar el estado actual como texto

            // Establecer la capacidad correctamente
            // Establecer la capacidad correctamente
            CapacidadComboBox.SelectedItem = CapacidadComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item =>
                    int.TryParse(item.Content.ToString(), out int itemCapacidad) && itemCapacidad == capacidad
                );


            // Configurar las opciones de cama extra y cuna
            PrimeraOpcion.IsChecked = camaExtra;
            SegundaOpcion.IsChecked = cuna;

            // Si existe una imagen, establecerla
            if (!string.IsNullOrEmpty(imagenBase64))
            {
                ImagenBase64 = imagenBase64;
            }
        }



        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NomTextBox.Text) || !Regex.IsMatch(NomTextBox.Text, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$"))
                {
                    MessageBox.Show("Fallo inminente, por favor corrígelo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Obtener los valores editados
                NuevoNombre = NomTextBox.Text;
                NuevoTipo = TipoTextBox.Text;
                NuevaDescripcion = txtDescripcion.Text;
                NuevaCapacidad = (CapacidadComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                // Validar y convertir el precio
                if (!double.TryParse(PrecioTextBox.Text, out double nuevoPrecio) || !double.TryParse(PrecioOriginalTextBox.Text, out double nuevoPrecioOriginal))
                {
                    MessageBox.Show("El precio ingresado no es válido. Por favor, ingresa un número.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                NuevoPrecio = nuevoPrecio;
                NuevoPrecioOriginal = nuevoPrecioOriginal;

                if (nuevoPrecio >= nuevoPrecioOriginal)
                {
                    tieneOferta = false;
                }
                else
                {
                    tieneOferta = true;
                }

                // Validar y convertir el estado de forma más robusta
                Estado = EstadoCheckBox.IsChecked ?? false;

                // Asignar las opciones de cama extra y cuna
                CamaExtra = PrimeraOpcion.IsChecked == true;
                Cuna = SegundaOpcion.IsChecked == true;

                // Obtener la imagen seleccionada y convertirla a Base64
                ImagenBase64 = ObtenerImagenBase64();

                // Cerrar la ventana con éxito
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private string ObtenerImagenBase64()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == true)
            {
                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                FilePathTextBox.Text = openFileDialog.FileName; // Mostrar la ruta del archivo seleccionado
                return Convert.ToBase64String(imageBytes);
            }
            return string.Empty; // Retorna vacío si no se seleccionó imagen
        }



        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Llamamos a la función que selecciona y convierte la imagen
            string base64Imagen = ObtenerImagenBase64();

            if (!string.IsNullOrEmpty(base64Imagen))
            {
                ImagenBase64 = base64Imagen;
            }
        }



        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana sin hacer cambios
            DialogResult = false;
            Close();
        }


    }
}
