using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reservas_Final.Models.Reservas;
using Reservas_Final.Models.Habitaciones;
using System.Windows;
using System.Linq;

namespace Reservas_Final.ViewModel.Reservas
{
    public class ReservaViewModel : INotifyPropertyChanged
    {
        private const string ApiUrlAllReservas = "http://127.0.0.1:3505/Reserva/getAll";
        private const string ApiUrlEliminarReservas = "http://127.0.0.1:3505/Reserva/eliminarReserva";
        private const string ApiUrlModificarReservas = "http://127.0.0.1:3505/Reserva/modificarReserva";
        private const string ApiUrlHabitaciones = "http://127.0.0.1:3505/Habitacion/getAll";

        private ObservableCollection<ReservaBase> allReservas;
        public ObservableCollection<ReservaBase> AllReservas
        {
            get => allReservas;
            set { allReservas = value; OnPropertyChanged("AllReservas"); }
        }

        private ObservableCollection<Habitacion> allHabitaciones;
        public ObservableCollection<Habitacion> AllHabitaciones
        {
            get => allHabitaciones;
            set { allHabitaciones = value; OnPropertyChanged("AllHabitaciones"); }
        }

        public ReservaViewModel()
        {
            AllHabitaciones = new ObservableCollection<Habitacion>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void CargarTodasLasReservas()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage responseReservas = await client.GetAsync(ApiUrlAllReservas);

                    if (responseReservas.IsSuccessStatusCode)
                    {
                        var jsonReservas = await responseReservas.Content.ReadAsStringAsync();
                        var reservasResponse = JsonConvert.DeserializeObject<ApiResponse<List<ReservaBase>>>(jsonReservas);

                        AllReservas = new ObservableCollection<ReservaBase>();

                        foreach (var reserva in reservasResponse.reservas)
                        {
                            if (!string.IsNullOrEmpty(reserva._id))
                                AllReservas.Add(new ReservaBase
                                {
                                    _id = reserva._id,
                                    id_usu = reserva.id_usu,
                                    id_hab = reserva.id_hab,
                                    fecha_check_in = reserva.fecha_check_in,
                                    fecha_check_out = reserva.fecha_check_out,
                                    estado_reserva = reserva.estado_reserva,
                                });
                        }

                        OnPropertyChanged("AllReservas");
                    }
                    else
                    {
                        throw new Exception($"Error al obtener reservas: {responseReservas.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error: {e.Message}");
                }
            }
        }

        public async Task CargarTodasHabitaciones()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync("http://localhost:3505/Habitacion/getAll");
                    var habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(response);

                    AllHabitaciones = new ObservableCollection<Habitacion>(habitaciones); // Actualiza la colección
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las habitaciones: {ex.Message}");
            }

            //using (var client = new HttpClient())
            //{
            //    //try
            //    //{
            //    //    var response = await client.GetStringAsync(ApiUrlHabitaciones);

            //    //    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Habitacion>>>(response);

            //    //    var habitaciones = apiResponse.habitaciones;

            //    //    //if (responseHabitaciones.IsSuccessStatusCode)
            //    //    //{
            //    //    //    var jsonHabitaciones = await responseHabitaciones.Content.ReadAsStringAsync();
            //    //    //    var habitacionResponse = JsonConvert.DeserializeObject<ApiResponse<List<Habitacion>>>(jsonHabitaciones);

            //    //    //    AllHabitaciones = new ObservableCollection<Habitacion>();

            //    //    //    foreach (var habitacion in habitacionResponse.habitaciones)
            //    //    //    {
            //    //    //        AllHabitaciones.Add(new Habitacion
            //    //    //        {
            //    //    //            _id = habitacion._id,
            //    //    //            num_planta = habitacion.num_planta,
            //    //    //            tipo = habitacion.tipo,
            //    //    //            capacidad = habitacion.capacidad,
            //    //    //            descripcion = habitacion.descripcion,
            //    //    //            opciones = new Opciones
            //    //    //            {
            //    //    //                CamaExtra = habitacion.opciones.CamaExtra,
            //    //    //                Cuna = habitacion.opciones.Cuna
            //    //    //            },
            //    //    //            precio_noche = habitacion.precio_noche,
            //    //    //            precio_noche_original = habitacion.precio_noche_original,
            //    //    //            tieneOferta = habitacion.tieneOferta,
            //    //    //            estado = habitacion.estado
            //    //    //        });
            //    //    //    }

            //    //    //    OnPropertyChanged("AllHabitaciones");
            //    //    //}
            //    //    //else
            //    //    //{
            //    //    //    throw new Exception($"Error al obtener habitaciones: {responseHabitaciones.StatusCode}");
            //    //    //}
            //    //}
            //    //catch (Exception e)
            //    //{
            //    //    MessageBox.Show($"Error: {e.Message}");
            //    //}
            //}
        }

        public async Task<HttpResponseMessage> EditarReserva(string id, ReservaBase reserva)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = JsonConvert.SerializeObject(reserva);
                    var json = new StringContent(content, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync($"{ApiUrlModificarReservas}/{id}", json);
                    return response;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error: {e.Message}");
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError) { ReasonPhrase = e.Message };
                }
            }
        }

        public async Task<HttpResponseMessage> EliminarReserva(string id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.DeleteAsync($"{ApiUrlEliminarReservas}/{id}");
                    return response;
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error: {e.Message}");
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError) { ReasonPhrase = e.Message };
                }
            }
        }
    }
}