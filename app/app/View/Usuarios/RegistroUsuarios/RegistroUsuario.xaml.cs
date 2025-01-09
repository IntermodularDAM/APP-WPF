using System;
using System.Collections.Generic;
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
using app.View.Usuarios.RegistroUsuarios;

namespace app.View.Usuarios.RegistroUsuario
{
    /// <summary>
    /// Lógica de interacción para RegistroUsuario.xaml
    /// </summary>
    public partial class RegistroUsuario : Window
    {
        public RegistroUsuario()
        {
            InitializeComponent();
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateLogin();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidateLogin();
        }
        private void ValidateLogin()
        {
            // Obtengo los valores ingresados en los campos de texto para email y contraseña
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            // Valido el email y la contraseña utilizando los métodos correspondientes
            bool isEmailValid = IsValidEmail(email);
            bool isPasswordValid = IsValidPassword(password);

            // Muestro u oculto el mensaje de error para el campos dependiendo de su validez
            ErrorTextEmail.Visibility = isEmailValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextPassword.Visibility = isPasswordValid ? Visibility.Collapsed : Visibility.Visible;

            // Habilito el botón de inicio de sesión solo si ambos campos son válidos
            btnEnviar.IsEnabled = isEmailValid && isPasswordValid;
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

        public bool IsValidPassword(string password)
        {
            //Si no es nula regreso false
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Verificar que la contraseña tenga al menos una letra mayúscula, una minúscula y un número
            //Explicación del patter al final del documento.
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$";
            return Regex.IsMatch(password, pattern);
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            CodigoDeVerificacion codigo = new CodigoDeVerificacion();
            codigo.Show();
            this.Close();
        }
    }
}
