﻿using app.Models.Reservas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app.ViewModel.Reservas
{
    public class ReservaViewModel : INotifyPropertyChanged
    {
        private const string ApiUrlAllReservas = "http://127.0.0.1:3505/Reserva/getAll";
        private const string ApiUrlEliminarReservas = "http://127.0.0.1:3505/Reserva/eliminarReserva";
        private const string ApiUrlModificarReservas = "http://127.0.0.1:3505/Reserva/modificarReserva";

        private ObservableCollection<ReservaBase> allReservas;
        public ObservableCollection<ReservaBase> AllReservas
        {
            get => allReservas;
            set { allReservas = value; OnPropertyChanged("AllReservas"); }
        }

        public ReservaViewModel()
        {
            AllReservas = new ObservableCollection<ReservaBase>();
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

                        if (reservasResponse.reservas == null || reservasResponse.reservas.Count == 0)
                        {
                            MessageBox.Show("No se encontraron reservas.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

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
                    else if (responseReservas.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("No se encontraron reservas en la base de datos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Error al obtener reservas: {responseReservas.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Error inesperado: {e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
