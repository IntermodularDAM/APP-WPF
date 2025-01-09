using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using app.View.Usuarios.Login;
using app.View.Usuarios.RegistroUsuario;

namespace app.View.Usuarios.MainUsuarios
{
    /// <summary>
    /// Lógica de interacción para MainUsuarios.xaml
    /// </summary>
    public partial class MainUsuarios : Window
    {
        public MainUsuarios()
        {
            InitializeComponent();
        }

        private void btnAddWpfClient_Click(object sender, RoutedEventArgs e)
        {
            Usuarios.RegistroUsuario.RegistroUsuario registro = new Usuarios.RegistroUsuario.RegistroUsuario();
            registro.Show();

        }
    }
}
