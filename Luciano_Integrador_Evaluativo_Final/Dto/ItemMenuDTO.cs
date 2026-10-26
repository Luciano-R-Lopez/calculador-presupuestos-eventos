using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    internal class ItemMenuDTO
    {
        private int _cantidad;
        private int _idMenu;
        private bool _paraAdulto;

        public ItemMenuDTO(int cantidad, int idMenu, bool paraAdulto)
        {
            Cantidad = cantidad;
            IdMenu = idMenu;
            ParaAdulto = paraAdulto;
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
    }
}
