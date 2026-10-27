using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class ClienteController
    {
        private ClienteService _service;

        // Crea el Service con el que trabaja este Controller.
        public ClienteController()
        {
            _service = new ClienteService();
        }

        // Pide al Service que valide los datos y registre un cliente nuevo.
        public void RegistrarCliente(ClienteDTO dto)
        {
            _service.RegistrarCliente(dto);
        }

        // Pide al Service que modifique el cliente que tiene ese id con los datos nuevos.
        public void ActualizarCliente(int id, ClienteDTO dto)
        {
            _service.ActualizarCliente(id, dto);
        }

        // Pide al Service que elimine el cliente (se rechaza si está en uso).
        public void EliminarCliente(int id)
        {
            _service.EliminarCliente(id);
        }

        // Devuelve la lista de clientes para mostrarla en la View.
        public List<Cliente> GetClientes()
        {
            return _service.GetClientes();
        }
    }
}