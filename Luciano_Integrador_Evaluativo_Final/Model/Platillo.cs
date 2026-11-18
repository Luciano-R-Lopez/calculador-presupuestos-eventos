using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Plato (entrada, principal o postre) con su precio.
    internal class Platillo
    {
        private int _id;
        private string _nombre;
        private decimal _precio;
        private TipoPlatillo _tipo;

        // Para Entity Framework.
        private Platillo()
        {
            _nombre = "";
        }
        // Platillo nuevo: la base le asigna el id.
        public Platillo(string nombre, decimal precio, TipoPlatillo tipo)
        {
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

        // Se muestra por su nombre en combos y grillas.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
