using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Presupuesto de un evento. Reúne el cliente, el lugar, los servicios adicionales y los menús,
    // y calcula el monto total. Es un registro: guarda COPIAS de esos datos tal como estaban al generarlo.
    // Todo lo que se cobra (lugar, servicios, menús y otros ítems) es un IItemPresupuesto, y el monto se
    // calcula recorriéndolos sin importar de qué tipo sea cada uno.
    internal class Presupuesto
    {
        private int _id;
        private DateTime _fechaCreacion;
        private DateTime _fechaEvento;
        private EstadoPresupuesto _estado;
        private int _cantidadHoras;
        private List<ServicioAdicional> _serviciosAdicionales;
        // El "?" marca estos campos como NULABLES: pueden valer null. Es necesario porque el presupuesto
        // se puede armar "a medias" mientras el usuario completa el formulario (para mostrar el monto
        // al instante) y todavía no eligió cliente o lugar. Con el "?" el compilador nos obliga a
        // comprobar null antes de usarlos, evitando errores en tiempo de ejecución.
        private Cliente? _cliente;
        private LugarEvento? _lugar;
        private List<ItemMenu> _menus;

        // Ítems de cualquier otro tipo que se cobren en el presupuesto (por ejemplo, un transporte).
        // Son IItemPresupuesto: el presupuesto no necesita conocer su clase concreta para sumarlos,
        // clonarlos ni detallarlos. Hoy no se carga ninguno; queda listo para sumar tipos nuevos.
        private List<IItemPresupuesto> _itemsExtra;
        private decimal _montoTotal;

        // Crea un presupuesto vacío: estado Nuevo, fecha de creación de ahora y listas sin elementos.
        // Los demás datos se completan desde el Service.
        public Presupuesto()
        {
            FechaCreacion = DateTime.Now;
            Estado = EstadoPresupuesto.Nuevo;
            _serviciosAdicionales = new List<ServicioAdicional>();
            _menus = new List<ItemMenu>();
            _itemsExtra = new List<IItemPresupuesto>();
        }

        // Propiedades: dan acceso a los campos privados. Las listas (servicios y menús) solo tienen get:
        // se modifican agregando o quitando elementos de la lista, no reemplazándola.
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
        public List<ServicioAdicional> ServiciosAdicionales
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

        // Reúne en una sola lista todo lo que se cobra en el presupuesto: el lugar (si ya se eligió),
        // los servicios adicionales, los menús y los ítems extra. Es una lista nueva cada vez: sirve para
        // recorrerla, no para modificar el presupuesto.
        public List<IItemPresupuesto> ObtenerItems()
        {
            List<IItemPresupuesto> items = new List<IItemPresupuesto>();

            // Como _lugar puede ser null, solo se incluye si existe.
            // Evita un error al calcular con un presupuesto todavía incompleto.
            if (_lugar != null)
            {
                items.Add(_lugar);
            }
            foreach (ServicioAdicional servicio in _serviciosAdicionales)
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

        // Monto = suma del monto de cada ítem (lugar, servicios adicionales, menús y extras).
        // Según la consigna, la cantidad de horas NO entra en el cálculo.
        // Presupuesto no sabe cómo calcula cada ítem su monto: se lo pide a cada uno (polimorfismo).
        // Por eso un tipo de ítem nuevo se suma sin cambiar este método.
        public decimal CalcularMonto()
        {
            decimal total = 0;
            foreach (IItemPresupuesto item in ObtenerItems())
            {
                total += item.CalcularMonto();
            }

            MontoTotal = total;
            return MontoTotal;
        }

        // Cambia el estado respetando el flujo: Nuevo -> Enviado -> Pagado.
        // Se puede volver de Enviado a Nuevo (por ejemplo, para corregirlo antes de reenviarlo),
        // pero un presupuesto Pagado ya no cambia más.
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

        // Crea un presupuesto nuevo con los mismos datos. El constructor ya le asigna estado Nuevo y
        // una fecha de creación propia, por eso no se copian esos dos datos.
        // La copia conserva los datos y los precios tal como estaban en el original (no se vuelve a
        // presupuestar con los valores actuales) y no comparte ningún objeto con él: cada dato se copia.
        public Presupuesto ClonarPresupuesto()
        {
            Presupuesto copia = new Presupuesto();
            copia.FechaEvento = FechaEvento;
            copia.CantidadHoras = CantidadHoras;

            if (_cliente != null)
            {
                copia.Cliente = _cliente.Copiar();
            }

            if (_lugar != null)
            {
                copia.Lugar = _lugar.Copiar();
            }

            foreach (ServicioAdicional servicio in _serviciosAdicionales)
            {
                copia.ServiciosAdicionales.Add(servicio.Copiar());
            }

            foreach (ItemMenu item in _menus)
            {
                copia.Menus.Add(new ItemMenu(item.Cantidad, item.Item.Copiar(), item.ParaAdulto));
            }

            // Los ítems extra se copian a través de la interfaz, sin conocer su clase concreta.
            foreach (IItemPresupuesto extra in _itemsExtra)
            {
                copia.ItemsExtra.Add(extra.CopiarItem());
            }

            copia.CalcularMonto();
            return copia;
        }
    }
}
