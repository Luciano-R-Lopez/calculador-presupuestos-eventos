using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Renglón de menú del presupuesto: menú, invitados y precio por invitado al cotizar (PrecioMenu).
    internal class ItemMenu : IItemPresupuesto
    {
        private int _id;
        private int _cantidad;
        private Menu _item;
        private decimal _precioMenu;
        private decimal _montoTotal;
        private bool _paraAdulto;

        // Para Entity Framework.
        private ItemMenu()
        {
            _item = null!;
        }
        // Toma el precio actual del menú y calcula el monto.
        public ItemMenu(int cantidad, Menu menu, bool paraAdulto)
        {
            Cantidad = cantidad;
            Item = menu;
            ParaAdulto = paraAdulto;
            PrecioMenu = menu.CalcularMonto();
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
        // Precio por invitado al cotizar.
        public decimal PrecioMenu
        {
            get { return _precioMenu; }
            set { _precioMenu = value; }
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

        // Monto = invitados x precio guardado.
        public decimal CalcularMonto()
        {
            MontoTotal = Cantidad * PrecioMenu;
            return MontoTotal;
        }

        // Renglón nuevo con el mismo menú, cantidad y precio (para clonar).
        public ItemMenu Copiar()
        {
            ItemMenu copia = new ItemMenu(Cantidad, Item, ParaAdulto);
            copia.PrecioMenu = PrecioMenu;
            copia.CalcularMonto();
            return copia;
        }
    }
}
