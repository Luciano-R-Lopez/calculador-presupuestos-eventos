using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Servicio extra con precio fijo (catálogo). En el presupuesto se cobra a través de ServicioPresupuestado.
    internal class ServicioAdicional
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        // Para Entity Framework.
        private ServicioAdicional()
        {
            _nombre = "";
        }
        // Servicio nuevo: la base le asigna el id.
        public ServicioAdicional(string nom, decimal precio)
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
