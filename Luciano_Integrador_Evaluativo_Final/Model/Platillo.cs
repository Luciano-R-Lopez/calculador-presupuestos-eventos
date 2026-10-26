using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    internal class Platillo
    {
        private int _id;
        private string _nombre;
        private decimal _precio;
        private TipoPlatillo _tipo;

        public Platillo(int id, string nombre, decimal precio, TipoPlatillo tipo)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
        }
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

        // Devuelve una copia independiente (con el mismo id). Los presupuestos guardan copias para
        // conservar los datos tal como estaban cuando se generaron.
        public Platillo Copiar()
        {
            return new Platillo(Id, Nombre, Precio, Tipo);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
