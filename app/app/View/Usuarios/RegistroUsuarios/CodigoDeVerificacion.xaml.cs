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

namespace app.View.Usuarios.RegistroUsuarios
{
    /// <summary>
    /// Lógica de interacción para CodigoDeVerificacion.xaml
    /// </summary>
    public partial class CodigoDeVerificacion : Window
    {
        public CodigoDeVerificacion()
        {
            InitializeComponent();
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

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            RegistroPerfil registro = new RegistroPerfil();
            registro.Show();
            this.Close();   
        }
    }
}
