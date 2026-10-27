using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    // Maneja los ítems de menú (un menú con su cantidad de invitados) que forman parte de un presupuesto.
    internal class ItemMenuService
    {
        private ItemMenuListRepository _repository;
        private MenuListRepository _menuRepository;

        // Obtiene los repositorios (Singleton) de ítems y de menús.
        public ItemMenuService()
        {
            _repository = ItemMenuListRepository.GetInstance();
            _menuRepository = MenuListRepository.GetInstance();
        }

        // Comprueba que la cantidad de invitados sea mayor a cero; si no, lanza ArgumentException.
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

        // Guarda el ítem en el Repository (que le asigna el id).
        public void GuardarItemMenu(ItemMenu item)
        {
            _repository.Agregar(item);
        }

        // Elimina el ítem del Repository por su id.
        public void EliminarItemMenu(ItemMenu item)
        {
            _repository.Delete(item.Id);
        }

        // Crea el ítem (CrearItemMenu), lo guarda y lo devuelve.
        public ItemMenu RegistrarItemMenu(ItemMenuDTO dto)
        {
            ItemMenu item = CrearItemMenu(dto);

            GuardarItemMenu(item);
            return item;
        }

        // Devuelve la lista de ítems de menú que tiene guardada el Repository.
        public List<ItemMenu> GetItemsMenu()
        {
            return _repository.GetTodos();
        }
    }
}
