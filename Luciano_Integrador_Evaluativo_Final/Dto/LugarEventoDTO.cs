using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    internal class LugarEventoDTO
    {
        private string _nombre;
        private decimal _precio;

        public LugarEventoDTO(string nom, decimal precio)
        {
            Nombre = nom;
            Precio = precio;
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
    }
}
