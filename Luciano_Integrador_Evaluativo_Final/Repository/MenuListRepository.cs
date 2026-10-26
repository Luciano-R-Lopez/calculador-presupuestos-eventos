using Luciano_Integrador_Evaluativo_Final.Model;
using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class MenuListRepository : IRepository<Menu>
    {
        private List<Menu> _menus;
        private static MenuListRepository? _instancia;
        private int _proximoId = 1;

        private MenuListRepository()
        {
            _menus = new List<Menu>();
        }

        public static MenuListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new MenuListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(Menu entidad)
        {
            entidad.Id = GenerarProximoId();
            _menus.Add(entidad);
        }

        public List<Menu> GetTodos()
        {
            return _menus;
        }

        public Menu? GetPorId(int id)
        {
            // Busca un menu por su id. Devuelve el menu si lo encuentra, o null si no existe.
            return _menus.FirstOrDefault(menu => menu.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(Menu entidad)
        {
            for (int i = 0; i < _menus.Count; i++)
            {
                if (_menus[i].Id == entidad.Id)
                {
                    _menus[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            Menu? entidad = GetPorId(id);
            if (entidad != null)
            {
                _menus.Remove(entidad);
            }
        }
    }
}
