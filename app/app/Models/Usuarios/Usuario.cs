using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Models.Usuarios
{
    public class Usuario
    {
        private string _id;
        private string _email;
        private string _password;
        private string _registro;
        private string _verificationCode;
        private string _codeExpiresAt;
        private string _isVerified;


        public string email { get => _email; set => _email = value; }
        public string password { get => _password; set => _password = value; }
        public string registro { get => _registro; set => _registro = value; }
        public string verificationCode { get => _verificationCode; set => _verificationCode = value; }
        public string codeExpiresAt { get => _codeExpiresAt; set => _codeExpiresAt = value; }
        public string isVerified { get => _isVerified; set => _isVerified = value; }
        public string id { get => _id; set => _id = value; }
    }
}
