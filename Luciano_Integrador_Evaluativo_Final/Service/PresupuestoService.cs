using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class PresupuestoService
    {
        private PresupuestoListRepository _repository;
        private ClienteListRepository _clienteRepository;
        private LugarEventoListRepository _lugarRepository;
        private ServicioAdicionalListRepository _servicioRepository;
        private ItemMenuService _itemMenuService;

        public PresupuestoService()
        {
            _repository = PresupuestoListRepository.GetInstance();
            _clienteRepository = ClienteListRepository.GetInstance();
            _lugarRepository = LugarEventoListRepository.GetInstance();
            _servicioRepository = ServicioAdicionalListRepository.GetInstance();
            _itemMenuService = new ItemMenuService();
        }

        public void ValidarRegistro(PresupuestoDTO dto)
        {
            if (dto.FechaEvento.Date < DateTime.Today)
                throw new ArgumentException("La fecha del evento no puede ser anterior a hoy.");

            if (dto.CantidadHoras <= 0)
                throw new ArgumentException("La cantidad de horas debe ser mayor a cero.");

            if (dto.Menus == null || dto.Menus.Count == 0)
                throw new ArgumentException("El presupuesto debe tener al menos un menú.");
        }

        // Arma el presupuesto a partir del DTO sin guardarlo. Cliente y lugar pueden quedar sin asignar.
        private Presupuesto Construir(PresupuestoDTO dto)
        {
            Presupuesto presupuesto = new Presupuesto();
            presupuesto.FechaEvento = dto.FechaEvento;
            presupuesto.CantidadHoras = dto.CantidadHoras;

            // El presupuesto es un registro: guarda COPIAS del cliente, el lugar, los servicios y los
            // menús tal como están en este momento. Si después se modifica un precio o un dato, los
            // presupuestos ya generados no cambian (ni su monto ni su PDF).
            Cliente? cliente = _clienteRepository.GetPorId(dto.IdCliente);
            if (cliente != null)
            {
                presupuesto.Cliente = cliente.Copiar();
            }

            LugarEvento? lugar = _lugarRepository.GetPorId(dto.IdLugar);
            if (lugar != null)
            {
                presupuesto.Lugar = lugar.Copiar();
            }

            if (dto.IdsServiciosAdicionales != null)
            {
                foreach (int idServicio in dto.IdsServiciosAdicionales)
                {
                    ServicioAdicional? servicio = _servicioRepository.GetPorId(idServicio);
                    if (servicio == null)
                        throw new ArgumentException("El servicio adicional con id " + idServicio + " no existe.");

                    presupuesto.ServiciosAdicionales.Add(servicio.Copiar());
                }
            }

            if (dto.Menus != null)
            {
                foreach (ItemMenuDTO itemDto in dto.Menus)
                    presupuesto.Menus.Add(_itemMenuService.CrearItemMenu(itemDto));
            }

            presupuesto.CalcularMonto();
            return presupuesto;
        }

        public decimal CalcularMonto(PresupuestoDTO dto)
        {
            return Construir(dto).MontoTotal;
        }

        public void RegistrarPresupuesto(PresupuestoDTO dto)
        {
            ValidarRegistro(dto);

            Presupuesto presupuesto = Construir(dto);

            if (presupuesto.Cliente == null)
                throw new ArgumentException("El cliente seleccionado no existe.");

            if (presupuesto.Lugar == null)
                throw new ArgumentException("El lugar seleccionado no existe.");

            foreach (ItemMenu item in presupuesto.Menus)
                _itemMenuService.GuardarItemMenu(item);

            _repository.Agregar(presupuesto);
        }

        // Comprueba que el presupuesto se pueda editar y lo devuelve. No se puede editar si ya está
        // pagado ni si la fecha del evento ya pasó: en ambos casos queda como registro cerrado.
        public Presupuesto ValidarEdicion(int id)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            if (presupuesto.Estado == EstadoPresupuesto.Pagado)
                throw new InvalidOperationException("No se puede editar un presupuesto pagado.");

            if (presupuesto.FechaEvento.Date < DateTime.Today)
                throw new InvalidOperationException("No se puede editar un presupuesto cuyo evento ya pasó.");

            return presupuesto;
        }

        // Modifica el presupuesto existente. Se conservan su id, su fecha de creación y su estado.
        // Al editarlo se vuelve a presupuestar: los datos y precios se toman nuevamente de los valores
        // actuales. Primero se arma un presupuesto temporal para validar todo antes de tocar el original.
        public void ActualizarPresupuesto(int id, PresupuestoDTO dto)
        {
            Presupuesto presupuesto = ValidarEdicion(id);

            ValidarRegistro(dto);

            Presupuesto temporal = Construir(dto);

            if (temporal.Cliente == null)
                throw new ArgumentException("El cliente seleccionado no existe.");

            if (temporal.Lugar == null)
                throw new ArgumentException("El lugar seleccionado no existe.");

            presupuesto.FechaEvento = temporal.FechaEvento;
            presupuesto.CantidadHoras = temporal.CantidadHoras;
            presupuesto.Cliente = temporal.Cliente;
            presupuesto.Lugar = temporal.Lugar;

            presupuesto.ServiciosAdicionales.Clear();
            presupuesto.ServiciosAdicionales.AddRange(temporal.ServiciosAdicionales);

            // Los ítems de menú anteriores se eliminan del repositorio y se guardan los nuevos.
            foreach (ItemMenu itemViejo in presupuesto.Menus)
                _itemMenuService.EliminarItemMenu(itemViejo);

            presupuesto.Menus.Clear();
            foreach (ItemMenu itemNuevo in temporal.Menus)
            {
                _itemMenuService.GuardarItemMenu(itemNuevo);
                presupuesto.Menus.Add(itemNuevo);
            }

            presupuesto.CalcularMonto();

            _repository.Update(presupuesto);
        }

        // Un presupuesto pagado no se puede eliminar. Al eliminarlo se borran también sus ítems de menú,
        // porque forman parte del presupuesto.
        public void EliminarPresupuesto(int id)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            if (presupuesto.Estado == EstadoPresupuesto.Pagado)
                throw new InvalidOperationException("No se puede eliminar un presupuesto pagado.");

            foreach (ItemMenu item in presupuesto.Menus)
                _itemMenuService.EliminarItemMenu(item);

            _repository.Delete(id);
        }

        public void CambiarEstado(int id, EstadoPresupuesto estado)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            presupuesto.CambiarEstado(estado);
        }

        public void ClonarPresupuesto(int id)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            _repository.Agregar(presupuesto.ClonarPresupuesto());
        }

        public void GenerarPdf(int id, string ruta)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            if (string.IsNullOrWhiteSpace(ruta))
                throw new ArgumentException("Debe indicar dónde guardar el PDF.");

            PresupuestoPdfService pdfService = new PresupuestoPdfService();
            pdfService.Generar(presupuesto, ruta);
        }

        public List<Presupuesto> GetPresupuestos()
        {
            return _repository.GetTodos();
        }
    }
}
