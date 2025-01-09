using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios.Perfiles
{
    public class Empleado : Perfil
    {
        private string _puestoDeTrabajo; /*Recepcionista, Personal de apoyo, Limpieza interna, Servicio de comida, Cocina, Planificacion de eventos, Administracion*/
        private int _sueldo;
        /*Posibles atributos extras: Horario, Fecha de contratación, Supervisor, Estado Laboral, Departamento*/

        public string PuestoDeTrabajo { get => _puestoDeTrabajo; set => _puestoDeTrabajo = value; }
        public int Sueldo { get => _sueldo; set => _sueldo = value; }

        public Empleado(
            string id, 
            string nombre, 
            string apellido, 
            string dni, 
            Usuario usuario, 
            string rol, 
            string sexo, 
            string registro, 
            string rutaFoto, 
            string date, 
            string ciudad,
            string puestoDeTrabajo,
            int sueldo) 
            : base(id, nombre, apellido, dni, usuario, rol, sexo, registro, rutaFoto, date, ciudad)
        {
            _puestoDeTrabajo = puestoDeTrabajo;
            _sueldo = sueldo;
        }
    }
}
