using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios.Perfiles
{
    public class Administrador
    {
        private string _nivelDeAcceso;
        private string _responsableDeArea;
        private string _telefono;


        public string NivelDeAcceso { get => _nivelDeAcceso; set => _nivelDeAcceso = value; }
        public string ResponsableDeArea { get => _responsableDeArea; set => _responsableDeArea = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }


    }
}
