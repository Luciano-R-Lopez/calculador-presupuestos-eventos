using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Presupuesto de un evento. Apunta a cliente, lugar, servicios y menús, y guarda sus precios al cotizar
    // (PrecioLugar y los renglones), así no cambia si después suben.
    internal class Presupuesto
    {
        private int _id;
        private DateTime _fechaCreacion;
        private DateTime _fechaEvento;
        private EstadoPresupuesto _estado;
        private int _cantidadHoras;
        private List<ServicioPresupuestado> _serviciosAdicionales;
        // Pueden ser null mientras el formulario está a medio completar.
        private Cliente? _cliente;
        private LugarEvento? _lugar;
        // Precio del lugar al cotizar.
        private decimal _precioLugar;
        private List<ItemMenu> _menus;

        // Otros ítems a cobrar (hoy ninguno); no se guardan en la base.
        private List<IItemPresupuesto> _itemsExtra;
        private decimal _montoTotal;

        // Presupuesto vacío: estado Nuevo y fecha de creación actual.
        public Presupuesto()
        {
            FechaCreacion = DateTime.Now;
            Estado = EstadoPresupuesto.Nuevo;
            _serviciosAdicionales = new List<ServicioPresupuestado>();
            _menus = new List<ItemMenu>();
            _itemsExtra = new List<IItemPresupuesto>();
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        public DateTime FechaEvento
        {
            get { return _fechaEvento; }
            set { _fechaEvento = value; }
        }
        public EstadoPresupuesto Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public int CantidadHoras
        {
            get { return _cantidadHoras; }
            set { _cantidadHoras = value; }
        }
        public List<ServicioPresupuestado> ServiciosAdicionales
        {
            get { return _serviciosAdicionales; }
        }
        public Cliente? Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        public LugarEvento? Lugar
        {
            get { return _lugar; }
            set { _lugar = value; }
        }
        public decimal PrecioLugar
        {
            get { return _precioLugar; }
            set { _precioLugar = value; }
        }
        public List<ItemMenu> Menus
        {
            get { return _menus; }
        }
        public List<IItemPresupuesto> ItemsExtra
        {
            get { return _itemsExtra; }
        }
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }

        // Renglones del presupuesto (servicios, menús y extras). El lugar va aparte, en PrecioLugar.
        public List<IItemPresupuesto> ObtenerItems()
        {
            List<IItemPresupuesto> items = new List<IItemPresupuesto>();

            foreach (ServicioPresupuestado servicio in _serviciosAdicionales)
            {
                items.Add(servicio);
            }
            foreach (ItemMenu menu in _menus)
            {
                items.Add(menu);
            }
            foreach (IItemPresupuesto extra in _itemsExtra)
            {
                items.Add(extra);
            }
            return items;
        }

        // Total = precio del lugar + monto de cada renglón. Las horas no influyen.
        public decimal CalcularMonto()
        {
            decimal total = 0;
            if (_lugar != null)
            {
                total += PrecioLugar;
            }

            foreach (IItemPresupuesto item in ObtenerItems())
            {
                total += item.CalcularMonto();
            }

            MontoTotal = total;
            return MontoTotal;
        }

        // Flujo: Nuevo -> Enviado -> Pagado. Se puede volver de Enviado a Nuevo; Pagado es definitivo.
        public void CambiarEstado(EstadoPresupuesto estado)
        {
            if (estado == Estado)
                return;

            if (Estado == EstadoPresupuesto.Pagado)
                throw new InvalidOperationException("Un presupuesto pagado no puede cambiar de estado.");

            if (Estado == EstadoPresupuesto.Nuevo && estado == EstadoPresupuesto.Pagado)
                throw new InvalidOperationException("Un presupuesto nuevo debe enviarse antes de marcarse como pagado.");

            Estado = estado;
        }

        // Copia en memoria para insertar como presupuesto nuevo (estado Nuevo y fecha actual).
        // Mismos cliente, lugar, servicios y menús; renglones nuevos con los mismos precios.
        public Presupuesto ClonarPresupuesto()
        {
            Presupuesto copia = new Presupuesto();
            copia.FechaEvento = FechaEvento;
            copia.CantidadHoras = CantidadHoras;
            copia.Cliente = _cliente;
            copia.Lugar = _lugar;
            copia.PrecioLugar = PrecioLugar;

            foreach (ServicioPresupuestado servicio in _serviciosAdicionales)
            {
                copia.ServiciosAdicionales.Add(servicio.Copiar());
            }

            foreach (ItemMenu item in _menus)
            {
                copia.Menus.Add(item.Copiar());
            }

            foreach (IItemPresupuesto extra in _itemsExtra)
            {
                copia.ItemsExtra.Add(extra.CopiarItem());
            }

            copia.CalcularMonto();
            return copia;
        }
    }
}
