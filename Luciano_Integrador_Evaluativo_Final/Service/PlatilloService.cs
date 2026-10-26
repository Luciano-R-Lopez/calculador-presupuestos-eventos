using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class PlatilloService
    {
        private PlatilloListRepository _repository;

        public PlatilloService()
        {
            _repository = PlatilloListRepository.GetInstance();
        }

        public void ValidarRegistro(PlatilloDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        public void RegistrarPlatillo(PlatilloDTO dto)
        {
            ValidarRegistro(dto);

            Platillo platillo = new Platillo(dto.Nombre, dto.Precio, dto.Tipo);

            _repository.Agregar(platillo);
        }

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

        public List<Platillo> GetPlatillos()
        {
            return _repository.GetTodos();
        }
    }
}
