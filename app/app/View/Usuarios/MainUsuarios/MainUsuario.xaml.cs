using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private MultipartFormDataContent _multipartFormDataContent;
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


            EditarUsuario edit = new EditarUsuario(usuarioSeleccionado._id,usuarioSeleccionado.rol,  _viewModel);
            edit.Owner = this;
            edit.ShowDialog();

            _viewModel.CargarTodosLosUsuarios();

        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            UsuarioBase usuarioSeleccionado = null;
            if (DataGridPerfilUsuarios.SelectedItem != null) { usuarioSeleccionado = (UsuarioBase)DataGridPerfilUsuarios.SelectedItem; }
            else if (ListViewPerfilUsuarios.SelectedItem != null) { usuarioSeleccionado = (UsuarioBase)ListViewPerfilUsuarios.SelectedItem; }
            else { MessageBox.Show("Por favor, selecciona una usuario para editar.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            InformacionUsuario info = new InformacionUsuario(usuarioSeleccionado._id,_viewModel); 
            
            info.Owner = this;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _multipartFormDataContent = new MultipartFormDataContent(); 

            ComboBoxItem itemComboBox = (ComboBoxItem)txtRol.SelectedItem;
            
            if (itemComboBox != null) { 
                string contenidoSeleccionado = itemComboBox.Content.ToString();
                _multipartFormDataContent.Add(new StringContent(contenidoSeleccionado),"rol");
            }

            if (!string.IsNullOrEmpty(txtNombre.Text))
                _multipartFormDataContent.Add(new StringContent(txtNombre.Text.Trim()),"nombre");

            if (!string.IsNullOrEmpty(txtDni.Text))
                _multipartFormDataContent.Add(new StringContent(txtDni.Text.Trim()), "dni");

            if (!string.IsNullOrEmpty(txtCiudad.Text))
                _multipartFormDataContent.Add(new StringContent(txtCiudad.Text.Trim()), "ciudad");

            if (!string.IsNullOrEmpty(txtDate.Text))
                _multipartFormDataContent.Add(new StringContent(txtDate.Text.Trim()), "date");

            if (!_multipartFormDataContent.Any(c => c.Headers.ContentDisposition.Name.Trim('"') == "rol"))
            {
                MessageBox.Show("Se requiere un rol...", "Ojo...", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else {
                var rolContent = _multipartFormDataContent.FirstOrDefault(c => c.Headers.ContentDisposition.Name.Trim('"') == "rol");
                string rolValue = await rolContent.ReadAsStringAsync();

                _viewModel.Buscar(rolValue, _multipartFormDataContent);
                
            }

            ResetFields();

        }
        // Método para restablecer los campos
        private void ResetFields()
        {
            txtNombre.Text = string.Empty; // Vaciar el TextBox
            txtDni.Text = string.Empty; // Vaciar el TextBox
            txtCiudad.Text = string.Empty; // Vaciar el TextBox
            txtDate.SelectedDate = null; // Vaciar el DatePicker
            txtRol.SelectedIndex = -1; // Restablecer el ComboBox
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CargarTodosLosUsuarios();
        }
    }
}
