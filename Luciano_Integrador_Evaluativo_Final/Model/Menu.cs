using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    internal class Menu
    {
        private int _id;
        private string _nombre;
        private decimal _montoTotal;
        private List<Platillo> _platillos;
        private List<Aptitud> _aptitud;

        public Menu(string nombre)
        {
            Nombre = nombre;
            _platillos = new List<Platillo>();
            _aptitud = new List<Aptitud>();
        }
        public Menu(int id, string nombre)
        {
            Id = id;
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
        public void AgregarAptitud(Aptitud aptitud)
        {
            if (!_aptitud.Contains(aptitud))
                _aptitud.Add(aptitud);
        }
        public void EliminarAptitud(Aptitud aptitud)
        {
            _aptitud.Remove(aptitud);
        }
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

        public override string ToString()
        {
            return Nombre;
        }
    }
}
