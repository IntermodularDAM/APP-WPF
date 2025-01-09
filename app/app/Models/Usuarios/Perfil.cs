using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios
{
    public class Perfil
    {
        private string _id;
        private string _nombre;
        private string _apellido;
        private string _dni;
        private Usuario _usuario; // Referencia al objeto Usuario
        private string _rol;
        private string _date;
        private string _cuidad;
        private string _registro;
        private string _rutaFoto;

        public string Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Dni { get => _dni; set => _dni = value; }
        public Usuario Usuario { get => _usuario; set => _usuario = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string Date { get => _date; set => _date = value; }
        public string Ciudad { get => _cuidad; set => _cuidad = value; }
        public string Registro { get => _registro; set => _registro = value; }
        public string RutaFoto { get => _rutaFoto; set => _rutaFoto = value; }
        public Perfil(string id, string nombre, string apellido, string dni, Usuario usuario, string rol)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _dni = dni;
            _usuario = usuario;
            _rol = rol;
        }
    }
}
