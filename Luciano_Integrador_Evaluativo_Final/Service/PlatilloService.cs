using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    internal class PlatilloService
    {
        private PlatilloListRepository _repository;

        // Obtiene la única instancia (Singleton) del repositorio con el que trabaja.
        public PlatilloService()
        {
            _repository = PlatilloListRepository.GetInstance();
        }

        // Comprueba las reglas del platillo: el nombre es obligatorio y el precio debe ser mayor a cero.
        // Si alguna falla, lanza ArgumentException con el motivo, que la View muestra al usuario.
        public void ValidarRegistro(PlatilloDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        // Valida el DTO, arma el platillo con sus datos (sin id) y lo guarda en el Repository, que le asigna el id.
        public void RegistrarPlatillo(PlatilloDTO dto)
        {
            ValidarRegistro(dto);

            Platillo platillo = new Platillo(dto.Nombre, dto.Precio, dto.Tipo);

            _repository.Agregar(platillo);
        }

        // Valida el DTO, busca el platillo por su id (error si no existe), le copia los datos nuevos y avisa al Repository.
        public void ActualizarPlatillo(int id, PlatilloDTO dto)
        {
            ValidarRegistro(dto);

            Platillo? platillo = _repository.GetPorId(id);
            if (platillo == null)
                throw new ArgumentException("El platillo no existe.");

            platillo.Nombre = dto.Nombre;
            platillo.Precio = dto.Precio;
            platillo.Tipo = dto.Tipo;

            _repository.Update(platillo);
        }

        // No se puede eliminar un platillo que forma parte de algún menú.
        // Recorre los menús y cuenta cuántos incluyen el platillo; si hay alguno lanza un error,
        // y si no, lo elimina del Repository.
        public void EliminarPlatillo(int id)
        {
            int enUso = 0;
            foreach (Menu menu in MenuListRepository.GetInstance().GetTodos())
            {
                foreach (Platillo platillo in menu.Platillos)
                {
                    if (platillo.Id == id)
                    {
                        enUso++;
                    }
                }
            }

            if (enUso > 0)
                throw new InvalidOperationException("No se puede eliminar el platillo: lo usan " + enUso + " menú(s).");

            _repository.Delete(id);
        }

        // Devuelve la lista de platillos que tiene guardada el Repository.
        public List<Platillo> GetPlatillos()
        {
            return _repository.GetTodos();
        }
    }
}
