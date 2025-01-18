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

namespace app.View.Usuarios.RecordarContraseñas
{
    /// <summary>
    /// Lógica de interacción para RecordarContraseña.xaml
    /// </summary>
    public partial class RecordarContraseña : Window
    {
        public RecordarContraseña()
        {
            InitializeComponent();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            this.Close();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            this.Close();
        }
    }
}
