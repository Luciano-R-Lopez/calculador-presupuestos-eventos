using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using Luciano_Integrador_Evaluativo_Final.Comun;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Reglas de negocio de presupuestos: armado, edición, estados, clonación y PDF.
    internal class PresupuestoService
    {
        private PresupuestoRepository _repository;
        private ClienteRepository _clienteRepository;
        private LugarEventoRepository _lugarRepository;
        private ServicioAdicionalRepository _servicioRepository;
        private ItemMenuService _itemMenuService;

        public PresupuestoService()
        {
            _repository = PresupuestoRepository.GetInstance();
            _clienteRepository = ClienteRepository.GetInstance();
            _lugarRepository = LugarEventoRepository.GetInstance();
            _servicioRepository = ServicioAdicionalRepository.GetInstance();
            _itemMenuService = new ItemMenuService();
        }

        // Fecha no anterior a hoy, horas mayores a cero y al menos un menú.
        public void ValidarRegistro(PresupuestoDTO dto)
        {
            if (dto.FechaEvento.Date < DateTime.Today)
                throw new ArgumentException("La fecha del evento no puede ser anterior a hoy.");

            if (dto.CantidadHoras <= 0)
                throw new ArgumentException("La cantidad de horas debe ser mayor a cero.");

            if (dto.Menus == null || dto.Menus.Count == 0)
                throw new ArgumentException("El presupuesto debe tener al menos un menú.");
        }

        // Arma el presupuesto sin guardarlo (cliente y lugar pueden faltar) y calcula el monto.
        private Presupuesto Construir(PresupuestoDTO dto)
        {
            Presupuesto presupuesto = new Presupuesto();
            presupuesto.FechaEvento = dto.FechaEvento;
            presupuesto.CantidadHoras = dto.CantidadHoras;

            // Guarda los precios actuales: si después cambian, el presupuesto no cambia.
            Cliente? cliente = _clienteRepository.GetPorId(dto.IdCliente);
            if (cliente != null)
            {
                presupuesto.Cliente = cliente;
            }

            LugarEvento? lugar = _lugarRepository.GetPorId(dto.IdLugar);
            if (lugar != null)
            {
                presupuesto.Lugar = lugar;
                presupuesto.PrecioLugar = lugar.Precio;
            }

            if (dto.IdsServiciosAdicionales != null)
            {
                foreach (int idServicio in dto.IdsServiciosAdicionales)
                {
                    ServicioAdicional? servicio = _servicioRepository.GetPorId(idServicio);
                    if (servicio == null)
                        throw new ArgumentException("El servicio adicional con id " + idServicio + " no existe.");

                    presupuesto.ServiciosAdicionales.Add(new ServicioPresupuestado(servicio));
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

        // Monto de lo cargado hasta ahora, sin guardar (para mostrarlo al instante).
        public decimal CalcularMonto(PresupuestoDTO dto)
        {
            return Construir(dto).MontoTotal;
        }

        // Cliente y lugar obligatorios. Los ítems se guardan junto con el presupuesto.
        public void RegistrarPresupuesto(PresupuestoDTO dto)
        {
            ValidarRegistro(dto);

            Presupuesto presupuesto = Construir(dto);

            if (presupuesto.Cliente == null)
                throw new ArgumentException("El cliente seleccionado no existe.");

            if (presupuesto.Lugar == null)
                throw new ArgumentException("El lugar seleccionado no existe.");

            _repository.Agregar(presupuesto);
        }

        // No se puede editar si está pagado o si el evento ya pasó.
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

        // Editar vuelve a cotizar con los precios actuales.
        // Conserva id, fecha de creación y estado; valida con un presupuesto temporal antes de tocar el original.
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
            presupuesto.PrecioLugar = temporal.PrecioLugar;

            presupuesto.ServiciosAdicionales.Clear();
            presupuesto.ServiciosAdicionales.AddRange(temporal.ServiciosAdicionales);

            // Al guardar, la base borra los renglones anteriores e inserta los nuevos.
            presupuesto.Menus.Clear();
            foreach (ItemMenu itemNuevo in temporal.Menus)
            {
                presupuesto.Menus.Add(itemNuevo);
            }

            presupuesto.CalcularMonto();

            _repository.Update(presupuesto);
        }

        // No se puede eliminar si está pagado. Sus renglones se borran en cascada.
        public void EliminarPresupuesto(int id)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            if (presupuesto.Estado == EstadoPresupuesto.Pagado)
                throw new InvalidOperationException("No se puede eliminar un presupuesto pagado.");

            _repository.Delete(id);
        }

        // El flujo de estados lo controla Presupuesto.
        // Al pasar a Pagado se reserva el lugar: se valida que no se superponga con otro pagado.
        public void CambiarEstado(int id, EstadoPresupuesto estado)
        {
            Presupuesto? presupuesto = _repository.GetPorId(id);
            if (presupuesto == null)
                throw new ArgumentException("El presupuesto no existe.");

            if (presupuesto.Estado == EstadoPresupuesto.Enviado && estado == EstadoPresupuesto.Pagado)
            {
                ValidarSuperposicion(presupuesto);
            }

            presupuesto.CambiarEstado(estado);
            _repository.Update(presupuesto);
        }

        // Se superponen si cada evento empieza antes de que termine el otro.
        // Solo cuentan los pagados (las cotizaciones no reservan el lugar).
        private void ValidarSuperposicion(Presupuesto presupuesto)
        {
            if (presupuesto.Lugar == null)
                return;

            DateTime inicio = presupuesto.FechaEvento;
            DateTime fin = presupuesto.FechaEvento.AddHours(presupuesto.CantidadHoras);

            foreach (Presupuesto otro in _repository.GetTodos())
            {
                if (otro.Id == presupuesto.Id || otro.Estado != EstadoPresupuesto.Pagado || otro.Lugar == null)
                    continue;

                if (otro.Lugar.Id != presupuesto.Lugar.Id)
                    continue;

                DateTime inicioOtro = otro.FechaEvento;
                DateTime finOtro = otro.FechaEvento.AddHours(otro.CantidadHoras);

                if (inicio < finOtro && inicioOtro < fin)
                {
                    throw new InvalidOperationException("No se puede marcar como pagado: " + presupuesto.Lugar.Nombre
                        + " ya está reservado por el presupuesto N° " + otro.Id + " (pagado) el "
                        + inicioOtro.ToString("dd/MM/yyyy") + " de " + inicioOtro.ToString("HH:mm")
                        + " a " + finOtro.ToString("HH:mm") + ", y se superpone con este horario.");
                }
            }
        }

        // Copia en memoria insertada como presupuesto nuevo.
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

        // La View recibe DTO, no entidades: cliente, lugar y servicios como ids (y nombres para mostrar),
        // y el monto guardado al cotizar.
        public List<PresupuestoDTO> GetPresupuestos()
        {
            List<PresupuestoDTO> presupuestos = new List<PresupuestoDTO>();
            foreach (Presupuesto presupuesto in _repository.GetTodos())
            {
                presupuestos.Add(ADTO(presupuesto));
            }
            return presupuestos;
        }

        private PresupuestoDTO ADTO(Presupuesto presupuesto)
        {
            int idCliente = 0;
            string cliente = "";
            if (presupuesto.Cliente != null)
            {
                idCliente = presupuesto.Cliente.Id;
                cliente = presupuesto.Cliente.Nombre;
            }

            int idLugar = 0;
            string lugar = "";
            if (presupuesto.Lugar != null)
            {
                idLugar = presupuesto.Lugar.Id;
                lugar = presupuesto.Lugar.Nombre;
            }

            List<int> idsServicios = new List<int>();
            foreach (ServicioPresupuestado renglon in presupuesto.ServiciosAdicionales)
            {
                idsServicios.Add(renglon.Servicio.Id);
            }

            List<ItemMenuDTO> menus = new List<ItemMenuDTO>();
            foreach (ItemMenu item in presupuesto.Menus)
            {
                menus.Add(new ItemMenuDTO(item.Cantidad, item.Item.Id, item.ParaAdulto, item.Item.Nombre));
            }

            PresupuestoDTO dto = new PresupuestoDTO(presupuesto.FechaEvento, presupuesto.CantidadHoras,
                idCliente, idLugar, idsServicios, menus);
            dto.Id = presupuesto.Id;
            dto.FechaCreacion = presupuesto.FechaCreacion;
            dto.Estado = presupuesto.Estado;
            dto.Cliente = cliente;
            dto.Lugar = lugar;
            dto.MontoTotal = presupuesto.MontoTotal;
            return dto;
        }
    }
}
