using System;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Reglas de negocio de clientes.
    internal class ClienteService
    {
        private ClienteRepository _repository;

        public ClienteService()
        {
            _repository = ClienteRepository.GetInstance();
        }

        // Nombre y teléfono obligatorios.
        public void ValidarRegistro(ClienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");
        }

        public void RegistrarCliente(ClienteDTO dto)
        {
            ValidarRegistro(dto);

            Cliente cliente = new Cliente(dto.Nombre, dto.Telefono, dto.Email);

            _repository.Agregar(cliente);
        }

        // Los presupuestos que lo usan muestran los datos nuevos.
        public void ActualizarCliente(int id, ClienteDTO dto)
        {
            ValidarRegistro(dto);

            Cliente? cliente = _repository.GetPorId(id);
            if (cliente == null)
                throw new ArgumentException("El cliente no existe.");

            cliente.Nombre = dto.Nombre;
            cliente.Telefono = dto.Telefono;
            cliente.Email = dto.Email;

            _repository.Update(cliente);
        }

        // No se puede eliminar si lo usa algún presupuesto.
        public void EliminarCliente(int id)
        {
            int enUso = 0;
            foreach (Presupuesto presupuesto in PresupuestoRepository.GetInstance().GetTodos())
            {
                if (presupuesto.Cliente != null && presupuesto.Cliente.Id == id)
                {
                    enUso++;
                }
            }

            if (enUso > 0)
                throw new InvalidOperationException("No se puede eliminar el cliente: lo usan " + enUso + " presupuesto(s).");

            _repository.Delete(id);
        }

        // La View recibe DTO, no entidades.
        public List<ClienteDTO> GetClientes()
        {
            List<ClienteDTO> clientes = new List<ClienteDTO>();
            foreach (Cliente cliente in _repository.GetTodos())
            {
                clientes.Add(new ClienteDTO(cliente.Id, cliente.Nombre, cliente.Telefono, cliente.Email));
            }
            return clientes;
        }
    }
}