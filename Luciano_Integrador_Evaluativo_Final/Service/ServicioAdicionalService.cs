using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    internal class ServicioAdicionalService
    {
        private ServicioAdicionalListRepository _repository;

        // Obtiene la única instancia (Singleton) del repositorio con el que trabaja.
        public ServicioAdicionalService()
        {
            _repository = ServicioAdicionalListRepository.GetInstance();
        }

        // Comprueba las reglas del servicio adicional: el nombre es obligatorio y el precio debe ser mayor a cero.
        // Si alguna falla, lanza ArgumentException con el motivo, que la View muestra al usuario.
        public void ValidarRegistro(ServicioAdicionalDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        // Valida el DTO, arma el servicio adicional con sus datos (sin id) y lo guarda en el Repository, que le asigna el id.
        public void RegistrarServicioAdicional(ServicioAdicionalDTO dto)
        {
            ValidarRegistro(dto);

            ServicioAdicional servicioA = new ServicioAdicional(dto.Nombre, dto.Precio);

            _repository.Agregar(servicioA);
        }

        // Valida el DTO, busca el servicio adicional por su id (error si no existe), le copia los datos nuevos y avisa al Repository.
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
        // Recorre los presupuestos y cuenta cuántos incluyen el servicio; si hay alguno lanza un error,
        // y si no, lo elimina del Repository.
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

        // Devuelve la lista de servicios adicionales que tiene guardada el Repository.
        public List<ServicioAdicional> GetServiciosAdicionales()
        {
            return _repository.GetTodos();
        }
    }
}
