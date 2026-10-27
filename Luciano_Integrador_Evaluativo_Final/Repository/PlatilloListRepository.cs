using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de platillos: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class PlatilloListRepository : IRepository<Platillo>
    {
        private List<Platillo> _platillos;
        private static PlatilloListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private PlatilloListRepository()
        {
            _platillos = new List<Platillo>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static PlatilloListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PlatilloListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(Platillo entidad)
        {
            entidad.Id = GenerarProximoId();
            _platillos.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<Platillo> GetTodos()
        {
            return _platillos;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
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
