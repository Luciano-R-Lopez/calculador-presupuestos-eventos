using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    internal class PlatilloDTO
    {
        private string _nombre;
        private decimal _precio;
        private TipoPlatillo _tipo;

        public PlatilloDTO(string nombre, decimal precio, TipoPlatillo tipo)
        {
            Nombre = nombre;
            Precio = precio;
            Tipo = tipo;
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
    }
}
