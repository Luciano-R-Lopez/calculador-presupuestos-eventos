using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class ItemMenuListRepository : IRepository<ItemMenu>
    {
        private List<ItemMenu> _items;
        private static ItemMenuListRepository? _instancia;
        private int _proximoId = 1;

        private ItemMenuListRepository()
        {
            _items = new List<ItemMenu>();
        }

        public static ItemMenuListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ItemMenuListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(ItemMenu entidad)
        {
            entidad.Id = GenerarProximoId();
            _items.Add(entidad);
        }

        public List<ItemMenu> GetTodos()
        {
            return _items;
        }

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
