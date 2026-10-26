using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    internal class ClienteDTO
    {
        private string _nombre;
        private string _telefono;
        private string _email;

        public ClienteDTO(string nom, string tel, string email)
        {
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}