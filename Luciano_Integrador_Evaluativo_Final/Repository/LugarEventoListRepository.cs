using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de lugares: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class LugarEventoListRepository : IRepository<LugarEvento>
    {
        private List<LugarEvento> _lugares;
        private static LugarEventoListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private LugarEventoListRepository()
        {
            _lugares = new List<LugarEvento>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static LugarEventoListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LugarEventoListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(LugarEvento entidad)
        {
            entidad.Id = GenerarProximoId();
            _lugares.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<LugarEvento> GetTodos()
        {
            return _lugares;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
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
