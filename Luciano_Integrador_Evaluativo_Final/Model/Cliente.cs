using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Cliente del evento: guarda sus datos de contacto (nombre, teléfono y email).
    internal class Cliente
    {
        private int _id;
        private string _nombre;
        private string _telefono;
        private string _email;

        // Constructor completo, con id: se usa al copiar un cliente ya existente.
        public Cliente(int id, string nom, string tel, string email)
        {
            Id = id;
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }
        // Constructor sin id: se usa al crear un cliente nuevo; el Repository le asigna el id al guardarlo.
        public Cliente(string nom, string tel, string email)
        {
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }
        // Propiedades: dan acceso de lectura y escritura a los campos privados.
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

        // Texto con el que se muestra el objeto en combos y grillas: su nombre.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
