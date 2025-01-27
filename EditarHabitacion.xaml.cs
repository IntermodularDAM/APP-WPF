using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace app.View.Habitaciones
{
    public partial class EditarHabitacion : Window
    {
        // Propiedades públicas para devolver los valores editados
        public string NuevaDescripcion { get; private set; }
        public string NuevoTipo { get; private set; }
        public string NuevaCapacidad { get; private set; }
        public double NuevoPrecio { get; private set; }
        public bool CamaExtra { get; private set; }
        public bool Cuna { get; private set; }
        public double PrecioOriginal { get; private set; } // Precio original de la habitación
        public bool Estado { get; private set; }

        // Constructor actualizado para recibir las opciones como parámetros
        public EditarHabitacion(string tipo, string capacidad, double precio, string descripcion, bool camaExtra, bool cuna, double precioOriginal, bool estado)
        {
            InitializeComponent();

            // Rellenar los controles con los valores actuales
            TipoTextBox.Text = tipo;
            PrecioTextBox.Text = precio.ToString("F2"); // Aseguramos formato consistente
            txtDescripcion.Text = descripcion;
            PrecioOriginal = precioOriginal; // Guardamos el precio original
            EstadoCheckBox.IsChecked = estado; // Mostrar el estado actual como texto

            // Establecer la capacidad correctamente
            CapacidadComboBox.SelectedItem = CapacidadComboBox.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString().Equals(capacidad, StringComparison.OrdinalIgnoreCase));

            // Configurar las opciones de cama extra y cuna
            PrimeraOpcion.IsChecked = camaExtra;
            SegundaOpcion.IsChecked = cuna;
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener los valores editados
                NuevoTipo = TipoTextBox.Text;
                NuevaDescripcion = txtDescripcion.Text;
                NuevaCapacidad = (CapacidadComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                // Validar y convertir el precio
                if (!double.TryParse(PrecioTextBox.Text, out double nuevoPrecio))
                {
                    MessageBox.Show("El precio ingresado no es válido. Por favor, ingresa un número.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                NuevoPrecio = nuevoPrecio;

                // Validar y convertir el estado de forma más robusta
                Estado = EstadoCheckBox.IsChecked ?? false;

                // Asignar las opciones de cama extra y cuna
                CamaExtra = PrimeraOpcion.IsChecked == true;
                Cuna = SegundaOpcion.IsChecked == true;

                // Cerrar la ventana con éxito
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
