using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // Menú elegido en un presupuesto: viaja el id del menú, no el objeto.
    // NombreMenu solo se completa al volver del Service, para mostrarlo.
    internal class ItemMenuDTO
    {
        private int _cantidad;
        private int _idMenu;
        private bool _paraAdulto;
        private string _nombreMenu;

        public ItemMenuDTO(int cantidad, int idMenu, bool paraAdulto) : this(cantidad, idMenu, paraAdulto, "")
        {
        }

        public ItemMenuDTO(int cantidad, int idMenu, bool paraAdulto, string nombreMenu)
        {
            Cantidad = cantidad;
            IdMenu = idMenu;
            ParaAdulto = paraAdulto;
            NombreMenu = nombreMenu;
        }

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
        public string NombreMenu
        {
            get { return _nombreMenu; }
            set { _nombreMenu = value; }
        }
    }
}
