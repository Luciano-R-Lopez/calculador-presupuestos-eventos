using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Lugar del evento con su precio base.
    internal class LugarEvento : IItemPresupuesto
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        // Para Entity Framework.
        private LugarEvento()
        {
            _nombre = "";
        }
        public LugarEvento(int id, string nom, decimal precio)
        {
            Id = id;
            Nombre = nom;
            Precio = precio;
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

        // Copia con el mismo id.
        public LugarEvento Copiar()
        {
            return new LugarEvento(Id, Nombre, Precio);
        }

        // Se muestra por su nombre en combos y grillas.
        public override string ToString()
        {
            return Nombre;
        }

        // IItemPresupuesto

        public string Descripcion
        {
            get { return "Lugar: " + Nombre; }
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
