using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class PlatilloListRepository : IRepository<Platillo>
    {
        private List<Platillo> _platillos;
        private static PlatilloListRepository? _instancia;
        private int _proximoId = 1;

        private PlatilloListRepository()
        {
            _platillos = new List<Platillo>();
        }

        public static PlatilloListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PlatilloListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(Platillo entidad)
        {
            entidad.Id = GenerarProximoId();
            _platillos.Add(entidad);
        }

        public List<Platillo> GetTodos()
        {
            return _platillos;
        }

        public Platillo? GetPorId(int id)
        {
            // Busca un platillo por su id. Devuelve el platillo si lo encuentra, o null si no existe.
            return _platillos.FirstOrDefault(platillo => platillo.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(Platillo entidad)
        {
            for (int i = 0; i < _platillos.Count; i++)
            {
                if (_platillos[i].Id == entidad.Id)
                {
                    _platillos[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            Platillo? entidad = GetPorId(id);
            if (entidad != null)
            {
                _platillos.Remove(entidad);
            }
        }
    }
}
