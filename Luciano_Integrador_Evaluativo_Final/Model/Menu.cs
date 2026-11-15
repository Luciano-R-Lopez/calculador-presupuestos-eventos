using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Conjunto de platillos; su monto es la suma de sus precios. Puede ser apto para ciertos comensales.
    internal class Menu
    {
        private int _id;
        private string _nombre;
        private decimal _montoTotal;
        private List<Platillo> _platillos;
        private List<Aptitud> _aptitud;

        // Para Entity Framework.
        private Menu()
        {
            _nombre = "";
            _platillos = new List<Platillo>();
            _aptitud = new List<Aptitud>();
        }
        // Menú nuevo, sin platillos ni aptitudes: la base le asigna el id.
        public Menu(string nombre)
        {
            Nombre = nombre;
            _platillos = new List<Platillo>();
            _aptitud = new List<Aptitud>();
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
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        // Platillos y Aptitudes se modifican con los métodos Agregar/Eliminar.
        public List<Platillo> Platillos
        {
            get { return _platillos; }
        }
        public List<Aptitud> Aptitudes
        {
            get { return _aptitud; }
        }

        public void AgregarPlatillo(Platillo platillo)
        {
            _platillos.Add(platillo);
            CalcularMonto();
        }
        public void EliminarPlatillo(Platillo platillo)
        {
            _platillos.Remove(platillo);
            CalcularMonto();
        }
        // No repite aptitudes.
        public void AgregarAptitud(Aptitud aptitud)
        {
            if (!_aptitud.Contains(aptitud))
                _aptitud.Add(aptitud);
        }
        public void EliminarAptitud(Aptitud aptitud)
        {
            _aptitud.Remove(aptitud);
        }
        // Costo por invitado: suma de los platillos.
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

        // Se muestra por su nombre en combos y grillas.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
