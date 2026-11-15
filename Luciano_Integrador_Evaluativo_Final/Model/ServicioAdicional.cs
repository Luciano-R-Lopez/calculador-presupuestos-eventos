using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Servicio extra con precio fijo.
    internal class ServicioAdicional : IItemPresupuesto
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        // Para Entity Framework.
        private ServicioAdicional()
        {
            _nombre = "";
        }
        public ServicioAdicional(int id, string nom, decimal precio)
        {
            Id = id;
            Nombre = nom;
            Precio = precio;
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

        // Copia con el mismo id.
        public ServicioAdicional Copiar()
        {
            return new ServicioAdicional(Id, Nombre, Precio);
        }

        // Se muestra por su nombre en combos y grillas.
        public override string ToString()
        {
            return Nombre;
        }

        // IItemPresupuesto

        public string Descripcion
        {
            get { return Nombre; }
        }

        public decimal CalcularMonto()
        {
            return Precio;
        }

        public IItemPresupuesto CopiarItem()
        {
            return Copiar();
        }
    }
}
