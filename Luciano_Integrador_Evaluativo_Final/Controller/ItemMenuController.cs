using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class ItemMenuController
    {
        private ItemMenuService _service;

        // Crea el Service con el que trabaja este Controller.
        public ItemMenuController()
        {
            _service = new ItemMenuService();
        }

        // Pide al Service crear y guardar un ítem de menú, y lo devuelve.
        public ItemMenu RegistrarItemMenu(ItemMenuDTO dto)
        {
            return _service.RegistrarItemMenu(dto);
        }

        // Devuelve la lista de ítems de menú guardados.
        public List<ItemMenu> GetItemsMenu()
        {
            return _service.GetItemsMenu();
        }
    }
}
