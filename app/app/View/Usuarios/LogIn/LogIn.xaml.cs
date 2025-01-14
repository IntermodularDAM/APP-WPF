using System;
using System.Collections.Generic;
using System.Linq;
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
using app.Models.Usuarios;
using app.View.Usuarios.MainUsuarios;
using app.View.Usuarios.RegistroUsuarios;
using app.ViewModel.Usuarios.LogIn;
using IntermodularWPF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace app.View.Usuarios.Login
{
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateLogin();
        }
        private void PasswordBoxEmail_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidateLogin();
        }

        private void ValidateLogin()
        {
            // Obtengo los valores ingresados en los campos de texto para email y contraseña
            string email = TextBoxEmail.Text;
            string password = PasswordBoxEmail.Password;

            // Valido el email y la contraseña utilizando los métodos correspondientes
            bool isEmailValid = IsValidEmail(email);
            bool isPasswordValid = IsValidPassword(password);

            // Muestro u oculto el mensaje de error para el campos dependiendo de su validez
            ErrorTextEmail.Visibility = isEmailValid ? Visibility.Collapsed : Visibility.Visible;
            ErrorTextPassword.Visibility = isPasswordValid ? Visibility.Collapsed : Visibility.Visible;

            // Habilito el botón de inicio de sesión solo si ambos campos son válidos
            BtnLogin.IsEnabled = isEmailValid && isPasswordValid;
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

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var log = new LogInViewModel();

            // Configurar los datos que se enviarán al servidor en el cuerpo de la solicitud
            Usuario data = new Usuario { email = TextBoxEmail.Text, password = PasswordBoxEmail.Password };

            var response = await log.LogIn(data);
             var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = JsonConvert.DeserializeObject<dynamic>(result);

                if (responseData != null)
                {
                    // Obtener los datos del usuario y mostrarlos en la pantalla principal
                    UserSession.Instance.Token = responseData.data.token;
                    UserSession.Instance.Data = JObject.FromObject(responseData.data.user);

                    MainUsuario user = new MainUsuario();
                    user.Show();
                    this.Close();
                    

                }
                else
                {
                    var error = JsonConvert.DeserializeObject<Exception>(result);
                    MessageBox.Show(error + " : WPF = ERROR 500");
                }
            }
            else
            {
                var error = JsonConvert.DeserializeObject<dynamic>(result);
                MessageBox.Show(error + " : WPF : error 404");
            }


            
        }
    }
}
