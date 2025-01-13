using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using app.Models.Usuarios;
using Newtonsoft.Json;

namespace app.ViewModel.Usuarios.LogIn
{
    public class LogInViewModel
    {
        private const string ApiUrlLogIn= "http://127.0.0.1:3505/Usuario/logIn";

        public async Task<HttpResponseMessage> LogIn(Usuario UsuarioNuevo)
        {
            using (var cliente = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(UsuarioNuevo);
                Debug.WriteLine(json);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await cliente.PostAsync(ApiUrlLogIn, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Perfil logeado");
                        return response;
                    }
                    else
                    {
                        Debug.WriteLine($"Error: {response.StatusCode}");
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                    return null;
                }
            }
        }

    }
}
