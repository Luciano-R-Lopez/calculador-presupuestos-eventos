using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class MenuController
    {
        private MenuService _service;

        // Crea el Service con el que trabaja este Controller.
        public MenuController()
        {
            _service = new MenuService();
        }

        // Pide al Service que valide los datos y registre un menú nuevo.
        public void RegistrarMenu(MenuDTO dto)
        {
            _service.RegistrarMenu(dto);
        }

        // Pide al Service que modifique el menú que tiene ese id con los datos nuevos.
        public void ActualizarMenu(int id, MenuDTO dto)
        {
            _service.ActualizarMenu(id, dto);
        }

        // Pide al Service que elimine el menú (se rechaza si está en uso).
        public void EliminarMenu(int id)
        {
            _service.EliminarMenu(id);
        }

        // Devuelve la lista de menús para mostrarla en la View.
        public List<Menu> GetMenus()
        {
            return _service.GetMenus();
        }
    }
}
