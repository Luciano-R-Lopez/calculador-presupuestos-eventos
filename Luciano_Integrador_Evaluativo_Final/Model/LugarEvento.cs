using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Salón o lugar donde se hace el evento. Su precio es el costo base del lugar.
    internal class LugarEvento : IItemPresupuesto
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        // Constructor completo, con id: se usa al copiar un lugar ya existente.
        public LugarEvento(int id, string nom, decimal precio)
        {
            Id = id;
            Nombre = nom;
            Precio = precio;
        }
        // Constructor sin id: se usa al crear un lugar nuevo; el Repository le asigna el id al guardarlo.
        public LugarEvento(string nom, decimal precio)
        {
            Nombre = nom;
            Precio = precio;
        }
        // Propiedades: dan acceso de lectura y escritura a los campos privados.
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

        // Texto con el que se muestra el objeto en combos y grillas: su nombre.
        public override string ToString()
        {
            return Nombre;
        }

        // --- Implementación de IItemPresupuesto: el lugar es un ítem más del presupuesto ---

        // Cómo se detalla el lugar en el presupuesto.
        public string Descripcion
        {
            get { return "Lugar: " + Nombre; }
        }

        // El lugar suma su costo base.
        public decimal CalcularMonto()
        {
            return Precio;
        }

        // Copia del lugar, como ítem del presupuesto.
        public IItemPresupuesto CopiarItem()
        {
            return Copiar();
        }
    }
}
