﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using app.Models.Usuarios;
using app.Models.Usuarios.Perfiles;
using IntermodularWPF;
using Newtonsoft.Json;

namespace app.ViewModel.Usuarios
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        private const string ApiUrlAllAdministradores = "http://127.0.0.1:3505/Administrador/getAllAdministradores";
        private const string ApiUrlAllEmpleados = "http://127.0.0.1:3505/Empleado/getAllEmpleados";
        private const string ApiUrlAllClientes= "http://127.0.0.1:3505/Cliente/getAllClientes";

        private const string ApiUrlEliminarAdministrador = "http://127.0.0.1:3505/Administrador/eliminarAdministrador";
        private const string ApiUrlEliminarEmpleado = "http://127.0.0.1:3505/Empleado/eliminarEmpleado";
        private const string ApiUrlEliminarCliente = "http://127.0.0.1:3505/Cliente/eliminarCliente";

        private ObservableCollection<UsuarioBase> allUsers;
        public ObservableCollection<UsuarioBase> AllUsers { get => allUsers; set { allUsers = value; OnPropertyChanged("AllUsers"); } }




        // Evento definido por INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para disparar el evento PropertyChanged
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void CargarTodosLosUsuarios()
        {
            using (var client = new HttpClient()) {
                try
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", UserSession.Instance.Token);
                    client.DefaultRequestHeaders.Add("x-user-role", UserSession.Instance.Data["rol"].ToString()); // Enviar el rol en el encabezado

                    HttpResponseMessage responseAdministradores = await client.GetAsync(ApiUrlAllAdministradores);
                    HttpResponseMessage responseEmpleados = await client.GetAsync(ApiUrlAllEmpleados);
                    HttpResponseMessage responseCliente = await client.GetAsync(ApiUrlAllClientes);

                    if (responseAdministradores.IsSuccessStatusCode && responseCliente.IsSuccessStatusCode && responseEmpleados.IsSuccessStatusCode)
                    {
                        var jsonAdministrador = await responseAdministradores.Content.ReadAsStringAsync();
                        var jsonEmpleado = await responseEmpleados.Content.ReadAsStringAsync();
                        var jsonCliente = await responseCliente.Content.ReadAsStringAsync();

                        // Muestra el JSON crudo para depuración
                        //MessageBox.Show($"JSON Administradores: {jsonAdministrador}, \n JSON Empleados : {jsonEmpleado}, JSON Clientes: {jsonCliente}");

                        var administradoresResponse = JsonConvert.DeserializeObject<ApiResponse<List<Administrador>>>(jsonAdministrador);
                        var empleadosResponse = JsonConvert.DeserializeObject<ApiResponse<List<Empleado>>>(jsonEmpleado);
                        var clientesResponse = JsonConvert.DeserializeObject<ApiResponse<List<Cliente>>>(jsonCliente);

                        //MessageBox.Show($"Adminitradores : {administradoresResponse.data.Count()}, \n Empleado : {empleadosResponse.data.Count()} \n Cliente : {clientesResponse.data.Count()}");

    

                        AllUsers = new ObservableCollection<UsuarioBase>();


                        foreach (var administrador in administradoresResponse.data) 
                        {
                            if (!string.IsNullOrEmpty(administrador._id))
                                AllUsers.Add(new Administrador
                                {
                                    _id = administrador._id,
                                    idUsuario = administrador.idUsuario,
                                    nombre = administrador.nombre,
                                    apellido = administrador.apellido,
                                    dni = administrador.dni,
                                    rol = administrador.rol,
                                    date = administrador.date,
                                    ciudad = administrador.ciudad,
                                    sexo = administrador.sexo,
                                    registro = administrador.registro,
                                    rutaFoto = "http://127.0.0.1:3505/" + administrador.rutaFoto,
                                });
                            
                        }

                        foreach (var empleado in empleadosResponse.data) 
                        {
                            if (!string.IsNullOrEmpty(empleado._id))
                                AllUsers.Add(new Empleado
                                {
                                    _id = empleado._id,
                                    idUsuario = empleado.idUsuario,
                                    nombre = empleado.nombre,
                                    apellido = empleado.apellido,
                                    dni = empleado.dni,
                                    rol = empleado.rol,
                                    date = empleado.date,
                                    ciudad = empleado.ciudad,
                                    sexo = empleado.sexo,
                                    registro = empleado.registro,
                                    rutaFoto = "http://127.0.0.1:3505/" + empleado.rutaFoto,
                                });
                        }

                        foreach (var cliente in clientesResponse.data) 
                        {
                            if (!string.IsNullOrEmpty(cliente._id))
                                AllUsers.Add(new Cliente
                            {
                                _id = cliente._id,
                                idUsuario = cliente.idUsuario,
                                nombre = cliente.nombre,
                                apellido = cliente.apellido,
                                dni = cliente.dni,
                                rol = cliente.rol,
                                date = cliente.date,
                                ciudad = cliente.ciudad,
                                sexo = cliente.sexo,
                                registro = cliente.registro,
                                rutaFoto = "http://127.0.0.1:3505/" + cliente.rutaFoto,
                            });
                        }
                    
                        //string allUsersInfo = "Usuarios en AllUsers:\n";

                        //foreach (var user in AllUsers)
                        //{
                        //    allUsersInfo += $"ID: {user.idUsuario}, Nombre: {user.nombre}, Apellido: {user.apellido}, Rol: {user.rol}\n";
                        //}

                        //MessageBox.Show(allUsersInfo+ $"\n Cantidad Usuarios : {AllUsers.Count()}" );

                        OnPropertyChanged("AllUsers");


                    }
                    else {
                        throw new Exception($"WPF : Error 500 : Administrador status: {responseAdministradores.StatusCode}, Empleado status: {responseEmpleados.StatusCode}, Cliente status: {responseCliente.StatusCode}");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("WPF : Error 404 : " + e.Message);
                }
            }

        }

        public async Task<HttpResponseMessage> EliminarPerfil(string id, string role) {

            string rutaEliminar = "";
            if (role == "Administrador")
                rutaEliminar = ApiUrlEliminarAdministrador;
            if (role == "Empleado")
                rutaEliminar = ApiUrlEliminarEmpleado;
            if (role == "Cliente")
                rutaEliminar = ApiUrlEliminarCliente;

            using (var client = new HttpClient()) {
                try {

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(UserSession.Instance.Token);
                    client.DefaultRequestHeaders.Add("x-role-user", UserSession.Instance.Data["rol"].ToString());

                    var json = JsonConvert.SerializeObject(new { _id = id, rol = role });
                    var content = new StringContent(json,Encoding.UTF8,"application/json");

                    var request = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(rutaEliminar),
                        Content = content
                    };

                    var response = await client.SendAsync(request);


                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine(response);
                        return response;

                    }
                    else {
                        Debug.WriteLine(response);
                        return response;
                    }

                }catch (Exception e) { throw new Exception(e.Message); }
            }
        }
    }
}
