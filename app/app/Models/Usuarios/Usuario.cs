using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios
{
    public class Usuario
    {
        private string _email;
        private string _password;

        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
