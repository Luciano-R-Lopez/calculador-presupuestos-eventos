using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Cliente del evento y sus datos de contacto.
    internal class Cliente
    {
        private int _id;
        private string _nombre;
        private string _telefono;
        private string _email;

        // Para Entity Framework.
        private Cliente()
        {
            _nombre = "";
            _telefono = "";
            _email = "";
        }
        public Cliente(int id, string nom, string tel, string email)
        {
            Id = id;
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }
        // Cliente nuevo: la base le asigna el id.
        public Cliente(string nom, string tel, string email)
        {
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
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

        // Se muestra por su nombre en combos y grillas.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
