using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // Datos de un platillo entre la View y el Service (sin lógica). El Service los valida.
    // Id 0 = platillo nuevo (lo arma la View); con id, viene del Service para mostrarlo.
    internal class PlatilloDTO
    {
        private int _id;
        private string _nombre;
        private decimal _precio;
        private TipoPlatillo _tipo;

        public PlatilloDTO(string nombre, decimal precio, TipoPlatillo tipo) : this(0, nombre, precio, tipo)
        {
        }

        public PlatilloDTO(int id, string nombre, decimal precio, TipoPlatillo tipo)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
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

        // Se muestra por su nombre en listas.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
