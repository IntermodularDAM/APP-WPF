using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios.Perfiles
{
    public class Administrador : Perfil
    {
        private string _nivelDeAcceso; /*Superadmin, Gerente */
        private string _responsableDeArea; /*Recepcionista, Personal de apoyo, Limpieza interna, Servicio de comida, Cocina, Planificacion de eventos, Administracion*/
        private string _telefono;


        public string NivelDeAcceso { get => _nivelDeAcceso; set => _nivelDeAcceso = value; }
        public string ResponsableDeArea { get => _responsableDeArea; set => _responsableDeArea = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }

        public Administrador(
        string id,
        string nombre,
        string apellido,
        string dni,
        Usuario usuario,
        string rol,
        string date,
        string ciudad,
        string sexo,
        string registro,
        string rutaFoto,
        string nivelDeAcceso,
        string responsableDeArea,
        string telefono
)       : base(id, nombre, apellido, dni, usuario, rol,date,ciudad,sexo,registro,rutaFoto) // Llama al constructor base
        {
            _nivelDeAcceso = nivelDeAcceso;
            _responsableDeArea = responsableDeArea;
            _telefono = telefono;
        }
    }
}
