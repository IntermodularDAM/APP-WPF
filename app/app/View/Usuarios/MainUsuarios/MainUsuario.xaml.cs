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
using app.Models.Usuarios;
using app.View.Usuarios.CambiarContraseña;
using app.View.Usuarios.EditarUsuarios;
using app.View.Usuarios.InformacionUsuarios;
using app.View.Usuarios.Notificaciones;
using app.View.Usuarios.RegistroUsuarios;
using app.ViewModel.Usuarios;
using Newtonsoft.Json;

namespace app.View.Usuarios.MainUsuarios
{
    /// <summary>
    /// Lógica de interacción para MainUsuario.xaml
    /// </summary>
    public partial class MainUsuario : Window
    {
        //Instacia que guarda las variables que se usaran durante la ejecucion del programa
        private readonly UsuarioViewModel _viewModel;
        public MainUsuario()
        {
            InitializeComponent();
            
            _viewModel = new UsuarioViewModel();
            this.DataContext =  _viewModel;
        }

        private void btnRegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuarios.RegistroUsuario.RegistroUsuario registro = new Usuarios.RegistroUsuario.RegistroUsuario(_viewModel);
            registro.Owner = this;
            registro.ShowDialog();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try { 
                _viewModel.CargarTodosLosUsuarios();


            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToggleButtonCambiarLista_Checked(object sender, RoutedEventArgs e)
        {
            DataGridPerfilUsuarios.Visibility = Visibility.Collapsed;
            ListViewPerfilUsuarios.Visibility = Visibility.Visible;
            btnToggleButtonCambiarLista.Content = "ListView";
            imgToggleButton.Source = new BitmapImage(new Uri("/View/Usuarios/MainUsuarios/imgListView.png", UriKind.RelativeOrAbsolute)); ;

            _viewModel.CargarTodosLosUsuarios();

        }

        private void ToggleButtonCambiarLista_Unchecked(object sender, RoutedEventArgs e)
        {
            ListViewPerfilUsuarios.Visibility = Visibility.Collapsed;
            DataGridPerfilUsuarios.Visibility = Visibility.Visible;
            btnToggleButtonCambiarLista.Content = "DataGrid";
            imgToggleButton.Source = new BitmapImage(new Uri("/View/Usuarios/MainUsuarios/imgDataGrid.png", UriKind.RelativeOrAbsolute)); ;
            _viewModel.CargarTodosLosUsuarios();
        }

        private  void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBase usuarioSeleccionado = null;
            if (DataGridPerfilUsuarios.SelectedItem != null) { usuarioSeleccionado = (UsuarioBase)DataGridPerfilUsuarios.SelectedItem; }
            else if (ListViewPerfilUsuarios.SelectedItem != null) { usuarioSeleccionado = (UsuarioBase)ListViewPerfilUsuarios.SelectedItem; }
            else { MessageBox.Show("Por favor, selecciona una usuario para editar.", "Error.",MessageBoxButton.OK,MessageBoxImage.Error); return; }


            EditarUsuario edit = new EditarUsuario(usuarioSeleccionado._id,  _viewModel);
            edit.Owner = this;
            edit.ShowDialog();

            _viewModel.CargarTodosLosUsuarios();

        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            InformacionUsuario info = new InformacionUsuario(); info.Owner = this;
            info.ShowDialog();

        }
        private async void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBase usuarioSeleccionado = null;

            if (DataGridPerfilUsuarios.SelectedItem != null) { usuarioSeleccionado = (UsuarioBase)DataGridPerfilUsuarios.SelectedItem; }
            else if (ListViewPerfilUsuarios.SelectedItem != null) { usuarioSeleccionado = (UsuarioBase)ListViewPerfilUsuarios.SelectedItem; }
            if (usuarioSeleccionado == null)
            {
                MessageBox.Show("Por favor, selecciona una usuario para borrar.", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var result = MessageBox.Show($"¿Estás seguro de que quieres eliminar la cuenta seleccionado ?", "Confirmar eliminación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

                var response = await _viewModel.EliminarPerfil(usuarioSeleccionado._id.ToString(),usuarioSeleccionado.rol.ToString());
                var resultEliminar = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var status = JsonConvert.DeserializeObject(resultEliminar);
                    MessageBox.Show("Correctamente...", "Usuario Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Hubo un error al eliminar el usuario . Por favor, intenta de nuevo.");
                }
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Notificacion noti = new Notificacion();
            noti.Show();
   
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Usuarios.CambiarContraseña.CambiarContraseña cambiar = new Usuarios.CambiarContraseña.CambiarContraseña();
            cambiar.Show();
        }
    }
}
