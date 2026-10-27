using System;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    internal class ClienteService
    {
        private ClienteListRepository _repository;

        // Obtiene la única instancia (Singleton) del repositorio con el que trabaja.
        public ClienteService()
        {
            _repository = ClienteListRepository.GetInstance();
        }

        // Comprueba las reglas del cliente: el nombre y el teléfono son obligatorios.
        // Si alguna falla, lanza ArgumentException con el motivo, que la View muestra al usuario.
        public void ValidarRegistro(ClienteDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Telefono))
                throw new ArgumentException("El teléfono es obligatorio.");
        }

        // Valida el DTO, arma el cliente con sus datos (sin id) y lo guarda en el Repository, que le asigna el id.
        public void RegistrarCliente(ClienteDTO dto)
        {
            ValidarRegistro(dto);

            Cliente cliente = new Cliente(dto.Nombre, dto.Telefono, dto.Email);

            _repository.Agregar(cliente);
        }

        // Modifica el cliente existente. Los presupuestos ya generados no se ven afectados porque
        // guardan una copia de los datos del cliente.
        // Valida el DTO, busca el cliente por su id (error si no existe), le copia los datos nuevos y avisa al Repository.
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
        // Cuenta los presupuestos que usan al cliente; si hay alguno lanza un error con la cantidad,
        // y si no, lo elimina del Repository.
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

        // Devuelve la lista de clientes que tiene guardada el Repository.
        public List<Cliente> GetClientes()
        {
            return _repository.GetTodos();
        }
    }
}