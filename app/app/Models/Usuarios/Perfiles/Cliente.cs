﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios.Perfiles
{
    public  class Cliente : Usuario
    {
        private Usuario _usuario;
        private string _id;
        private string _nombre;
        private string _apellidos;
        private string _dni;
        private string _rol;
        private string _date;
        private string _cuidad;
        private string _registro;
        private string _rutaFoto;
        private string _vip;

        public Usuario Usuario { get => _usuario; set => _usuario = value; }
        public string Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellidos; set => _apellidos = value; }
        public string Dni { get => _dni; set => _dni = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public string Date { get => _date; set => _date = value; }
        public string Ciudad { get => _cuidad; set => _cuidad = value; }
        public string Registro { get => _registro; set => _registro = value; }
        public string RutaFoto { get => _rutaFoto; set => _rutaFoto = value; }
        public string Vip { get => _vip; set => _vip= value; }
    }
}
