using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Repository;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class LugarEventoService
    {
        private LugarEventoListRepository _repository;

        public LugarEventoService()
        {
            _repository = LugarEventoListRepository.GetInstance();
        }

        public void ValidarRegistro(LugarEventoDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        public void RegistrarLugarEvento(LugarEventoDTO dto)
        {
            ValidarRegistro(dto);

            LugarEvento lugar = new LugarEvento(dto.Nombre, dto.Precio);

            _repository.Agregar(lugar);
        }

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

        public List<LugarEvento> GetLugaresEvento()
        {
            return _repository.GetTodos();
        }
    }
}
