using System;
using System.Collections.Generic;
using System.Data;
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
using app.Models.Usuarios;
using app.ViewModel.Usuarios;

namespace app.View.Usuarios.EditarUsuarios
{
    /// <summary>
    /// Lógica de interacción para EditarUsuario.xaml
    /// </summary>
    public partial class EditarUsuario : Window
    {
        private readonly UsuarioViewModel _viewModel;
        private string ID;
        private byte[] imagenCargadaBytes;
        public EditarUsuario(string id,UsuarioViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            ID = id;
        }


        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UsuarioBase usuarioEdita = _viewModel.AllPerfiles.FirstOrDefault(item => item._id == ID);

            txtNombre.Text = usuarioEdita.nombre;
            txtApellidos.Text = usuarioEdita.apellido;
            txtRol.SelectedIndex = usuarioEdita.rol == "Administrador" ? 0 : usuarioEdita.rol == "Empleado" ? 1 : usuarioEdita.rol == "Cliente" ? 2: -1;    
            txtDni.Text = usuarioEdita.dni;
            if (DateTime.TryParseExact(usuarioEdita.date, "dd/MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, 
                out DateTime parsedDate))
            {
                txtDate.SelectedDate = parsedDate;
            }
            txtCiudad.Text = usuarioEdita.rutaFoto;
            rbH.IsChecked = usuarioEdita.sexo == "Hombre" ? true : false;
            rbM.IsChecked = usuarioEdita.sexo == "Mujer" ? true : false;
            rb49.IsChecked = usuarioEdita.sexo == "Indeterminado" ? true : false;
            string imageUrl = usuarioEdita.rutaFoto; // Ruta de la API
            BitmapImage bitmap = new BitmapImage();

            try
            {
                // Cargar la imagen desde la URL
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imageUrl, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Asegura que la imagen se carga completamente
                bitmap.EndInit();

                // Crear un ImageBrush y asignarlo al Ellipse
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmap;
                imageBrush.Stretch = Stretch.UniformToFill; // Asegura que la imagen llena el Ellipse correctamente
                miEllipse.Fill = imageBrush;
                //Guardo la imagen en una variable para despues usarla
                // Guardar la imagen en la variable imagenCargadaBytes
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    imagenCargadaBytes = stream.ToArray(); // Convertir el stream en un arreglo de bytes
                }
                ValidateForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        private void txtApellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        private void txtRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateForm();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        private void txtDni_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        private void txtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateForm();
        }

        private void txtCiudad_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateForm();
        }

        private void btnCargarImg_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png"; // Filtro de extensiones para el cuadro de diálogo
            dlg.Filter = "Archivos de imagen (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    // Obtener la ruta del archivo seleccionado por el usuario
                    string filePath = dlg.FileName;

                    // Leer los datos del archivo
                    imagenCargadaBytes = File.ReadAllBytes(filePath);
                    




                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(dlg.FileName);
                    bitmapImage.EndInit();
                    miEllipse.Fill = new ImageBrush(bitmapImage);
                    ValidateForm();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al cargar la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private void ValidateForm()
        {
            // Obtengo los valores ingresados en los campos
            string nombre = txtNombre.Text;
            string apellido = txtApellidos.Text;
            string email = txtEmail.Text;
            string ciudad = txtCiudad.Text;
            string dni = txtDni.Text;
            DateTime? fechaSeleccionada = txtDate.SelectedDate;
            bool isRolSelected = txtRol.SelectedItem != null;
            bool isFotoCargada = imagenCargadaBytes != null;

            // Valido
            bool isNombreValid = IsValidField(nombre);
            bool isApellidoValid = IsValidField(apellido);
            bool isEmailValid = IsValidEmail(email);
            bool isCiudadValid = IsValidField(ciudad);
            bool isDniValid = IsValidDniNie(dni);
            bool isFechaValid = fechaSeleccionada.HasValue;
            bool isRolValid = isRolSelected;

            // Muestro u oculto el mensaje de error para el campos dependiendo de su validez
            ErrorTextNombre.Visibility = isNombreValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextApellidos.Visibility = isApellidoValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextEmail.Visibility = isEmailValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextCiudad.Visibility = isCiudadValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextDate.Visibility = isFechaValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextRol.Visibility = isRolValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextDni.Visibility = isDniValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextFoto.Visibility = isFotoCargada ? Visibility.Collapsed : Visibility.Visible;

            // Habilito el botón de inicio de sesión solo si ambos campos son válidos
            btnEditarPerfil.IsEnabled = isNombreValid && isApellidoValid && isEmailValid && isCiudadValid && isDniValid && isFechaValid && isRolValid && isFotoCargada;

        }

        public bool IsValidField(string field)
        {

            //Si no es nula regreso false
            if (string.IsNullOrWhiteSpace(field))
                return false;

            //Si al menos tiene 3 letras el campo es valido
            return field.Length > 2 ? true : false;
        }

        public bool IsValidEmail(string email)
        {

            //Si no es nula regreso false
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Expresión regular para validar un email, se agrega using System.Text.RegularExpressions;
            //Explicación del patter al final del documento.
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        public static bool IsValidDniNie(string input)
        {
            // Patrón para DNI: 8 dígitos seguidos de una letra
            string patronDni = @"^\d{8}[A-Za-z]$";

            // Patrón para NIE: Letra inicial (X, Y, Z), 7 dígitos y una letra
            string patronNie = @"^[XYZ]\d{7}[A-Za-z]$";

            // Verificar si cumple con el formato de DNI o NIE
            if (Regex.IsMatch(input, patronDni) || Regex.IsMatch(input, patronNie))
            {
                // Validar la letra del DNI o NIE
                return ValidarLetraDniNie(input);
            }
            return false;
        }

        private static bool ValidarLetraDniNie(string input)
        {
            // Extraer números del DNI o NIE
            string numeros;
            if (input[0] == 'X')
                numeros = "0" + input.Substring(1, 7); // Reemplazar X por 0
            else if (input[0] == 'Y')
                numeros = "1" + input.Substring(1, 7); // Reemplazar Y por 1
            else if (input[0] == 'Z')
                numeros = "2" + input.Substring(1, 7); // Reemplazar Z por 2
            else
                numeros = input.Substring(0, 8); // Es un DNI

            // Calcular letra esperada
            string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            int indice = int.Parse(numeros) % 23;
            char letraEsperada = letras[indice];

            // Comparar con la letra del DNI o NIE
            return char.ToUpper(input.Substring(input.Length - 1, 1)[0]) == letraEsperada;

        }

        private void btnEditarPerfil_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
