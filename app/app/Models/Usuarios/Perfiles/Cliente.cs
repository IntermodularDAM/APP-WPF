using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios.Perfiles
{
    public  class Cliente : Perfil
    {

        private string _vip;
        public string Vip { get => _vip; set => _vip= value; }

        public Cliente(
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
            string vip) 
            : base(id, nombre, apellido, dni, usuario, rol, sexo, registro, rutaFoto, date, ciudad)
        {
            _vip = vip; 
        }

    }
}
