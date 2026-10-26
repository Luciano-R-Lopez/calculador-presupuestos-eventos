using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class ServicioAdicionalService
    {
        private ServicioAdicionalListRepository _repository;

        public ServicioAdicionalService()
        {
            _repository = ServicioAdicionalListRepository.GetInstance();
        }

        public void ValidarRegistro(ServicioAdicionalDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        public void RegistrarServicioAdicional(ServicioAdicionalDTO dto)
        {
            ValidarRegistro(dto);

            ServicioAdicional servicioA = new ServicioAdicional(dto.Nombre, dto.Precio);

            _repository.Agregar(servicioA);
        }

        public void ActualizarServicioAdicional(int id, ServicioAdicionalDTO dto)
        {
            ValidarRegistro(dto);

            ServicioAdicional? servicio = _repository.GetPorId(id);
            if (servicio == null)
                throw new ArgumentException("El servicio adicional no existe.");

            servicio.Nombre = dto.Nombre;
            servicio.Precio = dto.Precio;

            _repository.Update(servicio);
        }

        // No se puede eliminar un servicio que está en algún presupuesto.
        public void EliminarServicioAdicional(int id)
        {
            int enUso = 0;
            foreach (Presupuesto presupuesto in PresupuestoListRepository.GetInstance().GetTodos())
            {
                foreach (ServicioAdicional servicio in presupuesto.ServiciosAdicionales)
                {
                    if (servicio.Id == id)
                    {
                        enUso++;
                    }
                }
            }

            if (enUso > 0)
                throw new InvalidOperationException("No se puede eliminar el servicio: lo usan " + enUso + " presupuesto(s).");

            _repository.Delete(id);
        }

        public List<ServicioAdicional> GetServiciosAdicionales()
        {
            return _repository.GetTodos();
        }
    }
}
