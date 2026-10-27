using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Servicio extra que se puede sumar a un presupuesto: tiene una descripción (nombre) y un monto fijo,
    // que no depende del lugar, del menú ni de la cantidad de invitados.
    internal class ServicioAdicional
    {
        private int _id;
        private string _nombre;
        private decimal _precio;

        // Constructor completo, con id: se usa al copiar un servicio ya existente.
        public ServicioAdicional(int id, string nom, decimal precio)
        {
            Id = id;
            Nombre = nom;
            Precio = precio;
        }
        // Constructor sin id: se usa al crear un servicio nuevo; el Repository le asigna el id al guardarlo.
        public ServicioAdicional(string nom, decimal precio)
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
        public ServicioAdicional Copiar()
        {
            return new ServicioAdicional(Id, Nombre, Precio);
        }

        // Texto con el que se muestra el objeto en combos y grillas: su nombre.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
