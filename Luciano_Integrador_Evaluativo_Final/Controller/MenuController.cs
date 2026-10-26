using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    internal class MenuController
    {
        private MenuService _service;

        public MenuController()
        {
            _service = new MenuService();
        }

        public void RegistrarMenu(MenuDTO dto)
        {
            _service.RegistrarMenu(dto);
        }

        public void ActualizarMenu(int id, MenuDTO dto)
        {
            _service.ActualizarMenu(id, dto);
        }

        public void EliminarMenu(int id)
        {
            _service.EliminarMenu(id);
        }

        public List<Menu> GetMenus()
        {
            return _service.GetMenus();
        }
    }
}
