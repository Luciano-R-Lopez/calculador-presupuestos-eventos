using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de clientes: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class ClienteListRepository : IRepository<Cliente>
    {
        private List<Cliente> _clientes;
        private static ClienteListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private ClienteListRepository()
        {
            _clientes = new List<Cliente>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static ClienteListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ClienteListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(Cliente entidad)
        {
            entidad.Id = GenerarProximoId();
            _clientes.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<Cliente> GetTodos()
        {
            return _clientes;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
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
