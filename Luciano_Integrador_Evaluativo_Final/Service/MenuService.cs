using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    internal class MenuService
    {
        private MenuListRepository _repository;
        private PlatilloListRepository _platilloRepository;

        // Obtiene la única instancia (Singleton) del repositorio con el que trabaja.
        public MenuService()
        {
            _repository = MenuListRepository.GetInstance();
            _platilloRepository = PlatilloListRepository.GetInstance();
        }

        // Comprueba las reglas del menú: el nombre es obligatorio y debe tener al menos un platillo.
        // Si alguna falla, lanza ArgumentException con el motivo, que la View muestra al usuario.
        public void ValidarRegistro(MenuDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.IdsPlatillos == null || dto.IdsPlatillos.Count == 0)
                throw new ArgumentException("El menú debe tener al menos un platillo.");
        }

        // Arma el menú a partir del DTO sin guardarlo. Valida que los platillos existan.
        // Cómo: busca cada platillo por id (error si no existe) y lo agrega al menú, y luego agrega las aptitudes.
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

        // Valida el DTO, arma el menú (ArmarMenu) y lo guarda en el Repository, que le asigna el id.
        public void RegistrarMenu(MenuDTO dto)
        {
            ValidarRegistro(dto);

            _repository.Agregar(ArmarMenu(dto));
        }

        // Modifica el menú existente. Los presupuestos ya generados no se ven afectados porque guardan
        // una copia del menú. Primero se arma un menú temporal para validar todo antes de tocar el original.
        // Valida el DTO, busca el menú por su id (error si no existe), le copia los datos nuevos y avisa al Repository.
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
        // Recorre los presupuestos y sus ítems y cuenta cuántos usan el menú; si hay alguno lanza un error,
        // y si no, lo elimina del Repository.
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

        // Devuelve la lista de menús que tiene guardada el Repository.
        public List<Menu> GetMenus()
        {
            return _repository.GetTodos();
        }
    }
}
