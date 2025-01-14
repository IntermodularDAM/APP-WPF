﻿using System;
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
using app.View.Usuarios.RegistroUsuarios;
using app.ViewModel.Usuarios.RegistroUsuarios;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            btnEnviar.IsEnabled = false;
            var registroUsuarioViewModel = new RegistroUsuarioViewModel();

            // Obtengo los valores ingresados en los campos de texto para email y contraseña
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            try
            {
                Usuario registrarUsuario = new Usuario
                {
                    email = email,
                    password = password
                };

                var response = await registroUsuarioViewModel.RegistrarUsuario(registrarUsuario);
                 var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    // Convertir la respuesta JSON a un objeto dynamic
                    dynamic responseData = JsonConvert.DeserializeObject<dynamic>(result);


                    // Guardar el token y los datos del usuario en la sesión de usuario

                    if (responseData != null)
                    {
                        CodigoDeVerificacion codigo = new CodigoDeVerificacion
                        {
                            Email = responseData.data.email
                        };
                        codigo.Show();
                       

                        // Cerrar la pantalla de inicio de sesión
                        this.Close();
                    }
                        // Obtener los datos del usuario y enviarlos a verificación
                        //UserSession.Instance.Token = responseData.data.token;
                        //UserSession.Instance.Data = JObject.FromObject(responseData.data.user);
                    else
                    {
                        // Manejar el caso en que responseData es nulo
                        var error = JsonConvert.DeserializeObject<dynamic>(result);
                        MessageBox.Show(error + " : WPF = ERROR 500");
                        btnEnviar.IsEnabled = true;

                    }
                }
                else
                {
                    // La solicitud no fue exitosa, manejar el error
                    var error = JsonConvert.DeserializeObject<dynamic>(result);
                    MessageBox.Show(error + " : WPF  = ERROR 4047");
                    btnEnviar.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WPF : Ocurrió un error al cargar los datos : {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                btnEnviar.IsEnabled = true;
            }
            finally
            {
                btnEnviar.IsEnabled = true;
            }   


        }
    }
}
