using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class PresupuestoListRepository : IRepository<Presupuesto>
    {
        private List<Presupuesto> _presupuestos;
        private static PresupuestoListRepository? _instancia;
        private int _proximoId = 1;

        private PresupuestoListRepository()
        {
            _presupuestos = new List<Presupuesto>();
        }

        public static PresupuestoListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PresupuestoListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(Presupuesto entidad)
        {
            entidad.Id = GenerarProximoId();
            _presupuestos.Add(entidad);
        }

        public List<Presupuesto> GetTodos()
        {
            return _presupuestos;
        }

        public Presupuesto? GetPorId(int id)
        {
            // Busca un presupuesto por su id. Devuelve el presupuesto si lo encuentra, o null si no existe.
            return _presupuestos.FirstOrDefault(presupuesto => presupuesto.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(Presupuesto entidad)
        {
            for (int i = 0; i < _presupuestos.Count; i++)
            {
                if (_presupuestos[i].Id == entidad.Id)
                {
                    _presupuestos[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            Presupuesto? entidad = GetPorId(id);
            if (entidad != null)
            {
                _presupuestos.Remove(entidad);
            }
        }
    }
}
