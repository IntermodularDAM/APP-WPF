﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Newtonsoft.Json;
using Reservas_Final.Models.Reservas;
using Reservas_Final.ViewModel.Reservas;

namespace Reservas_Final.View.Reservas
{
    /// <summary>
    /// Interaction logic for MainReservas.xaml
    /// </summary>
    public partial class MainReservas : Window
    {
        public ObservableCollection<ReservaBase> Reservas { get; set; }
        private readonly ReservaViewModel viewModel;
        public MainReservas()
        {
            InitializeComponent();

            viewModel = new ReservaViewModel();
            this.DataContext = viewModel;

            //Reservas = new ObservableCollection<ReservaBase>();
            //DataGridPerfilUsuarios.ItemsSource = Reservas;
        }

        /// <summary>
        /// Carga las reservas desde la API.
        /// </summary>
        private async Task LoadReservationsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://localhost:3505/Reserva/getAll");
                    var reservations = JsonConvert.DeserializeObject<List<ReservaBase>>(response);

                    Reservas = new ObservableCollection<ReservaBase>(reservations);
                    DataGridReservas.ItemsSource = Reservas;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las reservas: {ex.Message}");
            }
        }

        private void btnBuscadorReserva_Click(object sender, RoutedEventArgs e)
        {
            BuscadorReservas ventanaBuscador = new BuscadorReservas();
            ventanaBuscador.Show();
            this.Close();
        }

        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = DataGridReservas.SelectedItem as ReservaBase;

            if (selectedReservation != null)
            {
                EditarReserva editarReservaWindow = new EditarReserva(selectedReservation, viewModel);

                if (editarReservaWindow.ShowDialog() == true)
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var reservaModificada = new
                            {
                                selectedReservation.fecha_check_in,
                                selectedReservation.fecha_check_out,
                                selectedReservation.estado_reserva
                            };

                            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"http://localhost:3505/Reserva/modificarReserva/{selectedReservation._id}")
                            {
                                Content = new StringContent(JsonConvert.SerializeObject(reservaModificada), Encoding.UTF8, "application/json")
                            };

                            var response = await client.SendAsync(request);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Reserva actualizada con éxito.");
                                viewModel.CargarTodasLasReservas();
                            }
                            else
                            {
                                var errorContent = await response.Content.ReadAsStringAsync();
                                MessageBox.Show($"Error al actualizar la reserva: {errorContent}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}\nDetalles: {ex.InnerException?.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva para editar.");
            }
        }

        private async void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = DataGridReservas.SelectedItem as ReservaBase;

            if (selectedReservation != null)
            {
                var result = MessageBox.Show($"¿Estás seguro de que deseas eliminar la reserva '{selectedReservation._id}'?",
                                             "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            var response = await client.DeleteAsync($"http://localhost:3505/Reserva/eliminarReserva/{selectedReservation._id}");

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Reserva eliminada con éxito.");
                                viewModel.CargarTodasLasReservas();
                            }
                            else
                            {
                                MessageBox.Show("Error al eliminar la reserva.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva para eliminar.");
            }


            //ReservaBase reservaSeleccionado = null;

            //if (DataGridPerfilUsuarios.SelectedItem != null) { reservaSeleccionado = (ReservaBase)DataGridPerfilUsuarios.SelectedItem; }
            //if (reservaSeleccionado == null)
            //{
            //    MessageBox.Show("Por favor, selecciona una usuario para borrar.", "Error !", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //var result = MessageBox.Show($"¿Estás seguro de que quieres eliminar la cuenta seleccionado ?", "Confirmar eliminación", MessageBoxButton.YesNo);
            //if (result == MessageBoxResult.Yes)
            //{

            //    var response = await viewModel.EliminarReserva(reservaSeleccionado._id.ToString());
            //    var resultEliminar = await response.Content.ReadAsStringAsync();

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var status = JsonConvert.DeserializeObject(resultEliminar);
            //        MessageBox.Show("Correctamente...", "Reserva Eliminada", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Hubo un error al eliminar la Reserva. Por favor, intenta de nuevo.");
            //    }
            //}
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = DataGridReservas.SelectedItem as ReservaBase;

            if (selectedReservation != null)
            {
                InformacionReserva info = new InformacionReserva(selectedReservation);
                info.Owner = this;
                info.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.CargarTodasLasReservas();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventanaMain = new MainWindow();
            ventanaMain.Show();
            this.Close();
        }
    }
}
