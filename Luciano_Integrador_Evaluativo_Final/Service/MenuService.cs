using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class MenuService
    {
        private MenuListRepository _repository;
        private PlatilloListRepository _platilloRepository;

        public MenuService()
        {
            _repository = MenuListRepository.GetInstance();
            _platilloRepository = PlatilloListRepository.GetInstance();
        }

        public void ValidarRegistro(MenuDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.IdsPlatillos == null || dto.IdsPlatillos.Count == 0)
                throw new ArgumentException("El menú debe tener al menos un platillo.");
        }

        // Arma el menú a partir del DTO sin guardarlo. Valida que los platillos existan.
        private Menu ArmarMenu(MenuDTO dto)
        {
            Menu menu = new Menu(dto.Nombre);

            foreach (int idPlatillo in dto.IdsPlatillos)
            {
                Platillo? platillo = _platilloRepository.GetPorId(idPlatillo);
                if (platillo == null)
                    throw new ArgumentException("El platillo con id " + idPlatillo + " no existe.");

                menu.AgregarPlatillo(platillo);
            }

            if (dto.Aptitudes != null)
            {
                foreach (Aptitud aptitud in dto.Aptitudes)
                    menu.AgregarAptitud(aptitud);
            }

            return menu;
        }

        public void RegistrarMenu(MenuDTO dto)
        {
            ValidarRegistro(dto);

            _repository.Agregar(ArmarMenu(dto));
        }

        // Modifica el menú existente. Los presupuestos ya generados no se ven afectados porque guardan
        // una copia del menú. Primero se arma un menú temporal para validar todo antes de tocar el original.
        public void ActualizarMenu(int id, MenuDTO dto)
        {
            ValidarRegistro(dto);

            Menu? menu = _repository.GetPorId(id);
            if (menu == null)
                throw new ArgumentException("El menú no existe.");

            Menu temporal = ArmarMenu(dto);

            menu.Nombre = dto.Nombre;

            menu.Platillos.Clear();
            foreach (Platillo platillo in temporal.Platillos)
            {
                menu.AgregarPlatillo(platillo);
            }

            menu.Aptitudes.Clear();
            foreach (Aptitud aptitud in temporal.Aptitudes)
            {
                menu.AgregarAptitud(aptitud);
            }

            _repository.Update(menu);
        }

        // No se puede eliminar un menú que está en algún presupuesto.
        public void EliminarMenu(int id)
        {
            int enUso = 0;
            foreach (Presupuesto presupuesto in PresupuestoListRepository.GetInstance().GetTodos())
            {
                foreach (ItemMenu item in presupuesto.Menus)
                {
                    if (item.Item.Id == id)
                    {
                        enUso++;
                    }
                }
            }

            if (enUso > 0)
                throw new InvalidOperationException("No se puede eliminar el menú: lo usan " + enUso + " presupuesto(s).");

            _repository.Delete(id);
        }

        public List<Menu> GetMenus()
        {
            return _repository.GetTodos();
        }
    }
}
