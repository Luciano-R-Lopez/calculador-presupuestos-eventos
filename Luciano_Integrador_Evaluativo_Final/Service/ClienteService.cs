using System;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class ClienteService
    {
        private ClienteListRepository _repository;

        public ClienteService()
        {
            _repository = ClienteListRepository.GetInstance();
        }

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

        // Modifica el cliente existente. Los presupuestos ya generados no se ven afectados porque
        // guardan una copia de los datos del cliente.
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

        // No se puede eliminar un cliente que tiene presupuestos: quedarían con un cliente que ya no existe.
        public void EliminarCliente(int id)
        {
            int enUso = 0;
            foreach (Presupuesto presupuesto in PresupuestoListRepository.GetInstance().GetTodos())
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

        public List<Cliente> GetClientes()
        {
            return _repository.GetTodos();
        }
    }
}