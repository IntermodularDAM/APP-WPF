using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Habitaciones
{
    public class Habitacion
    {
        public string _id { get; set; }
        public int num_planta { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int capacidad { get; set; }
        public string descripcion { get; set; }
        public Opciones opciones { get; set; }
        public double precio_noche { get; set; }
        public decimal? precio_noche_original { get; set; }
        public bool tieneOferta { get; set; }
        public bool estado { get; set; }
        public string ImagenBase64 { get; set; }

    }

    public class Opciones
    {
        public bool CamaExtra { get; set; }
        public bool Cuna { get; set; }
    }
}
