using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Renglón de un presupuesto: un menú elegido, para una cantidad de invitados (adultos o niños).
    // Su monto es la cantidad de invitados por el costo del menú.
    internal class ItemMenu
    {
        private int _id;
        private int _cantidad;
        private Menu _item;
        private decimal _montoTotal;
        private bool _paraAdulto;

        // Constructor sin id: se usa al crear el ítem; calcula su monto al momento.
        public ItemMenu(int cantidad, Menu menu, bool paraAdulto)
        {
            Cantidad = cantidad;
            Item = menu;
            ParaAdulto = paraAdulto;
            CalcularMonto();
        }
        // Constructor con id: igual al anterior, para ítems que ya tienen id.
        public ItemMenu(int id, int cantidad, Menu menu, bool paraAdulto)
        {
            Id = id;
            Cantidad = cantidad;
            Item = menu;
            ParaAdulto = paraAdulto;
            CalcularMonto();
        }
        // Propiedades: dan acceso de lectura y escritura a los campos privados.
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

        // Calcula el monto del ítem: cantidad de invitados x costo del menú. Lo guarda en MontoTotal y lo devuelve.
        public decimal CalcularMonto()
        {
            MontoTotal = Cantidad * Item.CalcularMonto();
            return MontoTotal;
        }
    }
}
