using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    internal class ItemMenuController
    {
        private ItemMenuService _service;

        public ItemMenuController()
        {
            _service = new ItemMenuService();
        }

        public ItemMenu RegistrarItemMenu(ItemMenuDTO dto)
        {
            return _service.RegistrarItemMenu(dto);
        }

        public List<ItemMenu> GetItemsMenu()
        {
            return _service.GetItemsMenu();
        }
    }
}
