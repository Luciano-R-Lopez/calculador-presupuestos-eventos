using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    internal class Cliente
    {
        private int _id;
        private string _nombre;
        private string _telefono;
        private string _email;

        public Cliente(int id, string nom, string tel, string email)
        {
            Id = id;
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }
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


        // Devuelve una copia independiente (con el mismo id). Los presupuestos guardan copias para
        // conservar los datos tal como estaban cuando se generaron.
        public Cliente Copiar()
        {
            return new Cliente(Id, Nombre, Telefono, Email);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
