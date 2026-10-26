using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class ClienteListRepository : IRepository<Cliente>
    {
        private List<Cliente> _clientes;
        private static ClienteListRepository? _instancia;
        private int _proximoId = 1;

        private ClienteListRepository()
        {
            _clientes = new List<Cliente>();
        }

        public static ClienteListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ClienteListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(Cliente entidad)
        {
            entidad.Id = GenerarProximoId();
            _clientes.Add(entidad);
        }

        public List<Cliente> GetTodos()
        {
            return _clientes;
        }

        public Cliente? GetPorId(int id)
        {
            // Busca un cliente por su id. Devuelve el cliente si lo encuentra, o null si no existe.
            return _clientes.FirstOrDefault(cliente => cliente.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(Cliente entidad)
        {
            for (int i = 0; i < _clientes.Count; i++)
            {
                if (_clientes[i].Id == entidad.Id)
                {
                    _clientes[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            Cliente? entidad = GetPorId(id);
            if (entidad != null)
            {
                _clientes.Remove(entidad);
            }
        }
    }
}
