using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    // Es el Service principal: arma presupuestos a partir de un DTO, controla su edición y cambios de
    // estado, y genera el PDF. Trabaja con los repositorios de cliente, lugar y servicios, y con ItemMenuService.
    internal class PresupuestoService
    {
        private PresupuestoListRepository _repository;
        private ClienteListRepository _clienteRepository;
        private LugarEventoListRepository _lugarRepository;
        private ServicioAdicionalListRepository _servicioRepository;
        private ItemMenuService _itemMenuService;

        // Obtiene los repositorios (Singleton) que necesita y crea el ItemMenuService.
        public PresupuestoService()
        {
            _repository = PresupuestoListRepository.GetInstance();
            _clienteRepository = ClienteListRepository.GetInstance();
            _lugarRepository = LugarEventoListRepository.GetInstance();
            _servicioRepository = ServicioAdicionalListRepository.GetInstance();
            _itemMenuService = new ItemMenuService();
        }

        // Reglas para registrar o editar: la fecha del evento no puede ser anterior a hoy, las horas deben
        // ser mayores a cero y debe haber al menos un menú. Si falla alguna, lanza ArgumentException.
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
        // Cómo: busca cada dato por id en su repositorio, guarda una copia (Copiar) de cada uno, crea los
        // ítems de menú y calcula el monto.
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

        // Devuelve el monto de lo cargado hasta el momento, sin guardar nada: arma el presupuesto y toma su total.
        // Lo usa la View para mostrar el monto al instante.
        public decimal CalcularMonto(PresupuestoDTO dto)
        {
            return Construir(dto).MontoTotal;
        }

        // Valida el DTO, arma el presupuesto y comprueba que existan el cliente y el lugar. Guarda sus ítems
        // de menú y el presupuesto en los repositorios.
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
        // Cómo: busca el presupuesto (error si no existe), y lanza un error si está pagado o si su evento ya pasó.
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
        // Cómo: valida que se pueda editar, arma un presupuesto temporal con los datos nuevos y, si todo es
        // válido, se los copia al original (listas incluidas) y recalcula el monto.
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
        // Cómo: busca el presupuesto, rechaza si está pagado, elimina sus ítems de menú y lo elimina.
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

        // Busca el presupuesto (error si no existe) y le pide cambiar de estado; el flujo permitido lo controla
        // el propio Presupuesto (Nuevo -> Enviado -> Pagado).
        public void CambiarEstado(int id, EstadoPresupuesto estado)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            presupuesto.CambiarEstado(estado);
        }

        // Busca el presupuesto, le pide una copia (ClonarPresupuesto) y la guarda como un presupuesto nuevo.
        public void ClonarPresupuesto(int id)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            _repository.Agregar(presupuesto.ClonarPresupuesto());
        }

        // Busca el presupuesto, comprueba la ruta y le pide a PresupuestoPdfService que genere el archivo PDF.
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

        // Devuelve la lista de presupuestos que tiene guardada el Repository.
        public List<Presupuesto> GetPresupuestos()
        {
            return _repository.GetTodos();
        }
    }
}
