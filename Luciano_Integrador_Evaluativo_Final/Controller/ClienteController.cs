using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    internal class ClienteController
    {
        private ClienteService _service;

        public ClienteController()
        {
            _service = new ClienteService();
        }

        public void RegistrarCliente(ClienteDTO dto)
        {
            _service.RegistrarCliente(dto);
        }

        public void ActualizarCliente(int id, ClienteDTO dto)
        {
            _service.ActualizarCliente(id, dto);
        }

        public void EliminarCliente(int id)
        {
            _service.EliminarCliente(id);
        }

        public List<Cliente> GetClientes()
        {
            return _service.GetClientes();
        }
    }
}