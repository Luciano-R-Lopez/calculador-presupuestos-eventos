using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    internal class MenuDTO
    {
        private string _nombre;
        private List<int> _idsPlatillos;
        private List<Aptitud> _aptitudes;

        public MenuDTO(string nombre, List<int> idsPlatillos, List<Aptitud> aptitudes)
        {
            Nombre = nombre;
            IdsPlatillos = idsPlatillos;
            Aptitudes = aptitudes;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
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
    }
}
