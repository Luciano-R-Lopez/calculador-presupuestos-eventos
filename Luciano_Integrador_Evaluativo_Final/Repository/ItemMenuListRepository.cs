using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de ítems de menú: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class ItemMenuListRepository : IRepository<ItemMenu>
    {
        private List<ItemMenu> _items;
        private static ItemMenuListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private ItemMenuListRepository()
        {
            _items = new List<ItemMenu>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static ItemMenuListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ItemMenuListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(ItemMenu entidad)
        {
            entidad.Id = GenerarProximoId();
            _items.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<ItemMenu> GetTodos()
        {
            return _items;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
        public ItemMenu? GetPorId(int id)
        {
            // Busca un item por su id. Devuelve el item si lo encuentra, o null si no existe.
            return _items.FirstOrDefault(item => item.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(ItemMenu entidad)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Id == entidad.Id)
                {
                    _items[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            ItemMenu? entidad = GetPorId(id);
            if (entidad != null)
            {
                _items.Remove(entidad);
            }
        }
    }
}
