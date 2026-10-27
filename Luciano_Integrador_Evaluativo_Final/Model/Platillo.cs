using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Plato individual (entrada, principal o postre) con su precio. Los platillos forman parte de los menús.
    internal class Platillo
    {
        private int _id;
        private string _nombre;
        private decimal _precio;
        private TipoPlatillo _tipo;

        // Constructor completo, con id: se usa al copiar un platillo ya existente.
        public Platillo(int id, string nombre, decimal precio, TipoPlatillo tipo)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
        }
        // Constructor sin id: se usa al crear un platillo nuevo; el Repository le asigna el id al guardarlo.
        public Platillo(string nombre, decimal precio, TipoPlatillo tipo)
        {
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
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

        // Texto con el que se muestra el objeto en combos y grillas: su nombre.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
