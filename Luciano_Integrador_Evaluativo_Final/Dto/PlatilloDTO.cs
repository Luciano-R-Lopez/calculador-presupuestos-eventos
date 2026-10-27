using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // DTO (objeto de transferencia de datos): lleva solo los datos que carga el usuario, sin id ni lógica.
    // Viaja de la View al Controller y al Service, que lo valida y lo convierte en un Platillo.
    internal class PlatilloDTO
    {
        private string _nombre;
        private decimal _precio;
        private TipoPlatillo _tipo;

        // Arma el DTO con los datos del formulario.
        public PlatilloDTO(string nombre, decimal precio, TipoPlatillo tipo)
        {
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
        }

        // Propiedades: dan acceso a los campos privados.
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        public TipoPlatillo Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
    }
}
