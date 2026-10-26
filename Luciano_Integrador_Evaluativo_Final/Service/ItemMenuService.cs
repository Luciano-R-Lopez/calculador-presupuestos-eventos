using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class ItemMenuService
    {
        private ItemMenuListRepository _repository;
        private MenuListRepository _menuRepository;

        public ItemMenuService()
        {
            _repository = ItemMenuListRepository.GetInstance();
            _menuRepository = MenuListRepository.GetInstance();
        }

        public void ValidarRegistro(ItemMenuDTO dto)
        {
            if (dto.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
        }

        // Arma el ItemMenu a partir del DTO sin guardarlo (sirve para calcular montos).
        public ItemMenu CrearItemMenu(ItemMenuDTO dto)
        {
            ValidarRegistro(dto);

            Menu? menu = _menuRepository.GetPorId(dto.IdMenu);
            if (menu == null)
                throw new ArgumentException("El menú seleccionado no existe.");

            // El ítem guarda una copia del menú: si el menú se modifica después, los presupuestos
            // ya generados conservan el menú y los precios tal como estaban.
            return new ItemMenu(dto.Cantidad, menu.Copiar(), dto.ParaAdulto);
        }

        public void GuardarItemMenu(ItemMenu item)
        {
            _repository.Agregar(item);
        }

        public void EliminarItemMenu(ItemMenu item)
        {
            _repository.Delete(item.Id);
        }

        public ItemMenu RegistrarItemMenu(ItemMenuDTO dto)
        {
            ItemMenu item = CrearItemMenu(dto);

            GuardarItemMenu(item);
            return item;
        }

        public List<ItemMenu> GetItemsMenu()
        {
            return _repository.GetTodos();
        }
    }
}
