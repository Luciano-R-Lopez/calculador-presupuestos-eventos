using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // Datos de un cliente entre la View y el Service (sin lógica). El Service los valida.
    // Id 0 = cliente nuevo (lo arma la View); con id, viene del Service para mostrarlo.
    internal class ClienteDTO
    {
        private int _id;
        private string _nombre;
        private string _telefono;
        private string _email;

        public ClienteDTO(string nom, string tel, string email) : this(0, nom, tel, email)
        {
        }

        public ClienteDTO(int id, string nom, string tel, string email)
        {
            Id = id;
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

        // Se muestra por su nombre en combos.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
