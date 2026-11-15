using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Arma los ítems de menú de un presupuesto (se guardan junto con él).
    internal class ItemMenuService
    {
        private MenuRepository _menuRepository;

        public ItemMenuService()
        {
            _menuRepository = MenuRepository.GetInstance();
        }

        // Cantidad de invitados mayor a cero.
        public void ValidarRegistro(ItemMenuDTO dto)
        {
            if (dto.Cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
        }

        // Arma el ítem sin guardarlo, con el precio actual del menú.
        public ItemMenu CrearItemMenu(ItemMenuDTO dto)
        {
            ValidarRegistro(dto);

            Menu? menu = _menuRepository.GetPorId(dto.IdMenu);
            if (menu == null)
                throw new ArgumentException("El menú seleccionado no existe.");

            return new ItemMenu(dto.Cantidad, menu, dto.ParaAdulto);
        }
    }
}
