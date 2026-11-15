using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Lugar del evento con su precio base (catálogo). El presupuesto guarda su precio al cotizar en PrecioLugar.
    internal class LugarEvento
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        // Para Entity Framework.
        private LugarEvento()
        {
            _nombre = "";
        }
        // Lugar nuevo: la base le asigna el id.
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

        // Se muestra por su nombre en combos y grillas.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
