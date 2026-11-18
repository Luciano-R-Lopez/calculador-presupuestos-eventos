using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // Datos de un lugar entre la View y el Service (sin lógica). El Service los valida.
    // Id 0 = lugar nuevo (lo arma la View); con id, viene del Service para mostrarlo.
    internal class LugarEventoDTO
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        public LugarEventoDTO(string nom, decimal precio) : this(0, nom, precio)
        {
        }

        public LugarEventoDTO(int id, string nom, decimal precio)
        {
            Id = id;
            Nombre = nom;
            Precio = precio;
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

        // Se muestra por su nombre en combos.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
