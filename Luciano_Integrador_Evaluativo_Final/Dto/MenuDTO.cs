using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // Datos de un menú entre la View y el Service. Los platillos viajan como ids.
    // Id 0 = menú nuevo (lo arma la View); con id, viene del Service para mostrarlo junto con su monto.
    internal class MenuDTO
    {
        private int _id;
        private string _nombre;
        private decimal _montoTotal;
        private List<int> _idsPlatillos;
        private List<Aptitud> _aptitudes;

        public MenuDTO(string nombre, List<int> idsPlatillos, List<Aptitud> aptitudes)
            : this(0, nombre, 0, idsPlatillos, aptitudes)
        {
        }

        public MenuDTO(int id, string nombre, decimal montoTotal, List<int> idsPlatillos, List<Aptitud> aptitudes)
        {
            Id = id;
            Nombre = nombre;
            MontoTotal = montoTotal;
            IdsPlatillos = idsPlatillos;
            Aptitudes = aptitudes;
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
        // Solo para mostrar: lo calcula el Model.
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        public List<int> IdsPlatillos
        {
            get { return _idsPlatillos; }
            set { _idsPlatillos = value; }
        }
        public List<Aptitud> Aptitudes
        {
            get { return _aptitudes; }
            set { _aptitudes = value; }
        }

        // Se muestra por su nombre en combos.
        public override string ToString()
        {
            return Nombre;
        }
    }
}
