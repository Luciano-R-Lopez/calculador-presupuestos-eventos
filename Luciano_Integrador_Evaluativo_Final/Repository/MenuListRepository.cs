using Luciano_Integrador_Evaluativo_Final.Model;
using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de menús: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class MenuListRepository : IRepository<Menu>
    {
        private List<Menu> _menus;
        private static MenuListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private MenuListRepository()
        {
            _menus = new List<Menu>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static MenuListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new MenuListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(Menu entidad)
        {
            entidad.Id = GenerarProximoId();
            _menus.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<Menu> GetTodos()
        {
            return _menus;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
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
