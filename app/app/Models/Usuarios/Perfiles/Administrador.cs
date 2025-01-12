using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios.Perfiles
{
    public class Administrador
    {
        private string _id;
        private string _idUsuario;
        private string _nombre;
        private string _apellido;
        private string _dni;
        private string _rol;
        private string _date;
        private string _ciudad;
        private string _sexo;
        private string _registro;
        private string _rutaFoto;

        private string _nivelDeAcceso;
        private string _responsableDeArea;

        public string id { get => _id; set => _id = value; }
        public string idUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string nombre { get => _nombre; set => _nombre = value; }
        public string apellido { get => _apellido; set => _apellido = value; }
        public string dni { get => _dni; set => _dni = value; }
        public string rol { get => _rol; set => _rol = value; }
        public string date { get => _date; set => _date = value; }
        public string ciudad { get => _ciudad; set => _ciudad = value; }
        public string sexo { get => _sexo; set => _sexo = value; }
        public string registro { get => _registro; set => _registro = value; }
        public string rutaFoto { get => _rutaFoto; set => _rutaFoto = value; }

        public string nivelDeAcceso { get => _nivelDeAcceso; set => _nivelDeAcceso = value; }
        public string responsableDeArea { get => _responsableDeArea; set => _responsableDeArea = value; }
    }
}
