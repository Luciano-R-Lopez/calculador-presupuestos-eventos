using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    internal class ItemMenu
    {
        private int _id;
        private int _cantidad;
        private Menu _item;
        private decimal _montoTotal;
        private bool _paraAdulto;

        public ItemMenu(int cantidad, Menu menu, bool paraAdulto)
        {
            Cantidad = cantidad;
            Item = menu;
            ParaAdulto = paraAdulto;
            CalcularMonto();
        }
        public ItemMenu(int id, int cantidad, Menu menu, bool paraAdulto)
        {
            Id = id;
            Cantidad = cantidad;
            Item = menu;
            ParaAdulto = paraAdulto;
            CalcularMonto();
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public Menu Item
        {
            get { return _item; }
            set { _item = value; }
        }
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        public bool ParaAdulto
        {
            get { return _paraAdulto; }
            set { _paraAdulto = value; }
        }

        public decimal CalcularMonto()
        {
            MontoTotal = Cantidad * Item.CalcularMonto();
            return MontoTotal;
        }
    }
}
