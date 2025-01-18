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

namespace app.View.Usuarios.CambiarContraseña
{
    /// <summary>
    /// Lógica de interacción para CambiarContraseña.xaml
    /// </summary>
    public partial class CambiarContraseña : Window
    {
        public CambiarContraseña()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
