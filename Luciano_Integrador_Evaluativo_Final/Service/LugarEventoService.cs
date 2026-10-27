using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Service: contiene la lógica de negocio (validaciones y reglas). Usa el Repository para guardar y buscar.
    internal class LugarEventoService
    {
        private LugarEventoListRepository _repository;

        // Obtiene la única instancia (Singleton) del repositorio con el que trabaja.
        public LugarEventoService()
        {
            _repository = LugarEventoListRepository.GetInstance();
        }

        // Comprueba las reglas del lugar: el nombre es obligatorio y el precio debe ser mayor a cero.
        // Si alguna falla, lanza ArgumentException con el motivo, que la View muestra al usuario.
        public void ValidarRegistro(LugarEventoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        // Valida el DTO, arma el lugar con sus datos (sin id) y lo guarda en el Repository, que le asigna el id.
        public void RegistrarLugarEvento(LugarEventoDTO dto)
        {
            ValidarRegistro(dto);

            LugarEvento lugar = new LugarEvento(dto.Nombre, dto.Precio);

            _repository.Agregar(lugar);
        }

        // Valida el DTO, busca el lugar por su id (error si no existe), le copia los datos nuevos y avisa al Repository.
        public void ActualizarLugarEvento(int id, LugarEventoDTO dto)
        {
            ValidarRegistro(dto);

            LugarEvento? lugar = _repository.GetPorId(id);
            if (lugar == null)
                throw new ArgumentException("El lugar no existe.");

            lugar.Nombre = dto.Nombre;
            lugar.Precio = dto.Precio;

            _repository.Update(lugar);
        }

        // No se puede eliminar un lugar que tiene presupuestos: quedarían con un lugar que ya no existe.
        // Cuenta los presupuestos que usan al lugar; si hay alguno lanza un error con la cantidad,
        // y si no, lo elimina del Repository.
        public void EliminarLugarEvento(int id)
        {
            int enUso = 0;
            foreach (Presupuesto presupuesto in PresupuestoListRepository.GetInstance().GetTodos())
            {
                if (presupuesto.Lugar != null && presupuesto.Lugar.Id == id)
                {
                    enUso++;
                }
            }

            if (enUso > 0)
                throw new InvalidOperationException("No se puede eliminar el lugar: lo usan " + enUso + " presupuesto(s).");

            _repository.Delete(id);
        }

        // Devuelve la lista de lugares que tiene guardada el Repository.
        public List<LugarEvento> GetLugaresEvento()
        {
            return _repository.GetTodos();
        }
    }
}
