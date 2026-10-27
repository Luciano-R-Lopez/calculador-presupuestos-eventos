using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de servicios adicionales: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class ServicioAdicionalListRepository : IRepository<ServicioAdicional>
    {
        private List<ServicioAdicional> _serviciosA;
        private static ServicioAdicionalListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private ServicioAdicionalListRepository()
        {
            _serviciosA = new List<ServicioAdicional>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static ServicioAdicionalListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new ServicioAdicionalListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(ServicioAdicional entidad)
        {
            entidad.Id = GenerarProximoId();
            _serviciosA.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<ServicioAdicional> GetTodos()
        {
            return _serviciosA;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
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
