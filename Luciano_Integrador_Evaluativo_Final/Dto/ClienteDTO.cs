using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // DTO (objeto de transferencia de datos): lleva solo los datos que carga el usuario, sin id ni lógica.
    // Viaja de la View al Controller y al Service, que lo valida y lo convierte en un Cliente.
    internal class ClienteDTO
    {
        private string _nombre;
        private string _telefono;
        private string _email;

        // Arma el DTO con los datos del formulario.
        public ClienteDTO(string nom, string tel, string email)
        {
            Nombre = nom;
            Telefono = tel;
            Email = email;
        }

        // Propiedades: dan acceso a los campos privados.
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