using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // DTO (objeto de transferencia de datos): lleva solo los datos que carga el usuario, sin id ni lógica.
    // Representa un menú elegido en un presupuesto: viaja el id del menú, no el objeto.
    internal class ItemMenuDTO
    {
        private int _cantidad;
        private int _idMenu;
        private bool _paraAdulto;

        // Arma el DTO con la cantidad de invitados, el id del menú y si es para adultos.
        public ItemMenuDTO(int cantidad, int idMenu, bool paraAdulto)
        {
            Cantidad = cantidad;
            IdMenu = idMenu;
            ParaAdulto = paraAdulto;
        }

        // Propiedades: dan acceso a los campos privados.
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public int IdMenu
        {
            get { return _idMenu; }
            set { _idMenu = value; }
        }
        public bool ParaAdulto
        {
            get { return _paraAdulto; }
            set { _paraAdulto = value; }
        }
    }
}
