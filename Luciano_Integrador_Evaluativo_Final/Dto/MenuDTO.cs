using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // DTO (objeto de transferencia de datos): lleva solo los datos que carga el usuario, sin id ni lógica.
    // Los platillos viajan como lista de ids: el Service busca cada platillo en el repositorio.
    internal class MenuDTO
    {
        private string _nombre;
        private List<int> _idsPlatillos;
        private List<Aptitud> _aptitudes;

        // Arma el DTO con el nombre, los ids de los platillos elegidos y las aptitudes marcadas.
        public MenuDTO(string nombre, List<int> idsPlatillos, List<Aptitud> aptitudes)
        {
            Nombre = nombre;
            IdsPlatillos = idsPlatillos;
            Aptitudes = aptitudes;
        }

        // Propiedades: dan acceso a los campos privados.
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
