using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class LugarEventoListRepository : IRepository<LugarEvento>
    {
        private List<LugarEvento> _lugares;
        private static LugarEventoListRepository? _instancia;
        private int _proximoId = 1;

        private LugarEventoListRepository()
        {
            _lugares = new List<LugarEvento>();
        }

        public static LugarEventoListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LugarEventoListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(LugarEvento entidad)
        {
            entidad.Id = GenerarProximoId();
            _lugares.Add(entidad);
        }

        public List<LugarEvento> GetTodos()
        {
            return _lugares;
        }

        public LugarEvento? GetPorId(int id)
        {
            // Busca un lugar por su id. Devuelve el lugar si lo encuentra, o null si no existe.
            return _lugares.FirstOrDefault(lugar => lugar.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(LugarEvento entidad)
        {
            for (int i = 0; i < _lugares.Count; i++)
            {
                if (_lugares[i].Id == entidad.Id)
                {
                    _lugares[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            LugarEvento? entidad = GetPorId(id);
            if (entidad != null)
            {
                _lugares.Remove(entidad);
            }
        }
    }
}
