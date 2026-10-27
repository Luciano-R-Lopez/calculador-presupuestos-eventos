using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Menú: conjunto de platillos con un monto que es la suma de sus precios.
    // Puede indicar para qué tipos de comensales es apto (veganos, vegetarianos, celíacos).
    internal class Menu
    {
        private int _id;
        private string _nombre;
        private decimal _montoTotal;
        private List<Platillo> _platillos;
        private List<Aptitud> _aptitud;

        // Constructor sin id: se usa al crear un menú nuevo; arranca sin platillos ni aptitudes.
        // El Repository le asigna el id al guardarlo.
        public Menu(string nombre)
        {
            Nombre = nombre;
            _platillos = new List<Platillo>();
            _aptitud = new List<Aptitud>();
        }
        // Constructor con id: se usa al copiar un menú ya existente.
        public Menu(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            _platillos = new List<Platillo>();
            _aptitud = new List<Aptitud>();
        }
        // Propiedades: dan acceso de lectura y escritura a los campos privados.
        // Platillos y Aptitudes solo tienen get: la lista se modifica con los métodos Agregar/Eliminar.
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
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        public List<Platillo> Platillos
        {
            get { return _platillos; }
        }
        public List<Aptitud> Aptitudes
        {
            get { return _aptitud; }
        }

        // Agrega un platillo al menú y recalcula el monto del menú.
        public void AgregarPlatillo(Platillo platillo)
        {
            _platillos.Add(platillo);
            CalcularMonto();
        }
        // Quita un platillo del menú y recalcula el monto del menú.
        public void EliminarPlatillo(Platillo platillo)
        {
            _platillos.Remove(platillo);
            CalcularMonto();
        }
        // Marca el menú como apto para un tipo de comensal. Si ya la tenía, no la repite.
        public void AgregarAptitud(Aptitud aptitud)
        {
            if (!_aptitud.Contains(aptitud))
                _aptitud.Add(aptitud);
        }
        // Quita una aptitud del menú.
        public void EliminarAptitud(Aptitud aptitud)
        {
            _aptitud.Remove(aptitud);
        }
        // Suma el precio de todos los platillos, guarda el resultado en MontoTotal y lo devuelve.
        // Es el costo del menú por invitado.
        public decimal CalcularMonto()
        {
            decimal total = 0;
            foreach (Platillo platillo in _platillos)
            {
                total += platillo.Precio;
            }
            MontoTotal = total;
            return MontoTotal;
        }

        // Devuelve una copia independiente (con el mismo id), incluidos copias de sus platillos y
        // sus aptitudes. Los presupuestos guardan copias para conservar los datos tal como estaban
        // cuando se generaron.
        public Menu Copiar()
        {
            Menu copia = new Menu(Id, Nombre);
            foreach (Platillo platillo in _platillos)
            {
                copia.AgregarPlatillo(platillo.Copiar());
            }
            foreach (Aptitud aptitud in _aptitud)
            {
                copia.AgregarAptitud(aptitud);
            }
            return copia;
        }

        // Texto con el que se muestra el objeto en combos y grillas: su nombre.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
