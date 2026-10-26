using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal class ServicioAdicionalListRepository : IRepository<ServicioAdicional>
    {
        private List<ServicioAdicional> _serviciosA;
        private static ServicioAdicionalListRepository? _instancia;
        private int _proximoId = 1;

        private ServicioAdicionalListRepository()
        {
            _serviciosA = new List<ServicioAdicional>();
        }

        public static ServicioAdicionalListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ServicioAdicionalListRepository();
            }
            return _instancia;
        }

        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        public void Agregar(ServicioAdicional entidad)
        {
            entidad.Id = GenerarProximoId();
            _serviciosA.Add(entidad);
        }

        public List<ServicioAdicional> GetTodos()
        {
            return _serviciosA;
        }

        public ServicioAdicional? GetPorId(int id)
        {
            // Busca un servicio adicional por su id. Devuelve el servicio si lo encuentra, o null si no existe.
            return _serviciosA.FirstOrDefault(servicio => servicio.Id == id);
        }

        // Reemplaza la entidad que tiene el mismo id. Si no existe, no hace nada.
        public void Update(ServicioAdicional entidad)
        {
            for (int i = 0; i < _serviciosA.Count; i++)
            {
                if (_serviciosA[i].Id == entidad.Id)
                {
                    _serviciosA[i] = entidad;
                    return;
                }
            }
        }

        // Quita de la lista la entidad con ese id. Si no existe, no hace nada.
        public void Delete(int id)
        {
            ServicioAdicional? entidad = GetPorId(id);
            if (entidad != null)
            {
                _serviciosA.Remove(entidad);
            }
        }
    }
}
