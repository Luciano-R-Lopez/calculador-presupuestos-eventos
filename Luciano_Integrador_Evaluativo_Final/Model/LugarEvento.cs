using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    internal class LugarEvento
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        public LugarEvento(int id, string nom, decimal precio)
        {
            Id = id;
            Nombre = nom;
            Precio = precio;
        }
        public LugarEvento(string nom, decimal precio)
        {
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

        // Devuelve una copia independiente (con el mismo id). Los presupuestos guardan copias para
        // conservar los datos tal como estaban cuando se generaron.
        public LugarEvento Copiar()
        {
            return new LugarEvento(Id, Nombre, Precio);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
