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
using app.Models.Usuarios;
using app.ViewModel.Usuarios.RegistroUsuarios;
using Newtonsoft.Json;

namespace app.View.Usuarios.RegistroUsuarios
{
    /// <summary>
    /// Lógica de interacción para CodigoDeVerificacion.xaml
    /// </summary>
    public partial class CodigoDeVerificacion : Window
    {
        public string Email { get; set; }
        public CodigoDeVerificacion()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Asignar valores a los campos de texto al cargar el diálogo
            TextEmail.Text = Email;
        }


        private void txtCodigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateCode();
        }
        private void ValidateCode()
        {
            
            string codigo = txtCodigo.Text;
          

            // Valido 
            bool isCodeValid = IsValidCode(codigo);
          

            // Muestro u oculto el mensaje de error para el campos dependiendo de su validez
            ErrorTextCodigo.Visibility = isCodeValid ? Visibility.Collapsed : Visibility.Visible;

            // Habilito el botón 
            btnEnviar.IsEnabled = isCodeValid;
        }

        public bool IsValidCode(string codigo)
        {
            
            //Si no es nula regreso false
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            if (!int.TryParse(codigo, out int val)) 
                return false;

            if (codigo.Length <= 5)
                return false;

            return true;
        }

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            var verificarUsuario = new RegistroUsuarioViewModel();

            Usuario usuario = new Usuario
            {
                email = Email,
                verificationCode = txtCodigo.Text
            };
            var response = await verificarUsuario.ValidarUsuario(usuario);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                dynamic responseData = JsonConvert.DeserializeObject<dynamic>(result);

                if (responseData != null) {
                    
                    RegistroPerfil perfil = new RegistroPerfil{
                        Email = responseData.data.email,
                        IdUsuario = responseData.data.idUsuario
                    };
                   
                    perfil.Show();
                    this.Close();

                }
                else {
                    var error = JsonConvert.DeserializeObject<Exception>(result);
                    MessageBox.Show(error + " : WPF = ERROR 500");
                }
            }
            else 
            {
                var error = JsonConvert.DeserializeObject<dynamic>(result);
                MessageBox.Show(error+" : WPF : error 404");
            }

         
        }


    }
}
