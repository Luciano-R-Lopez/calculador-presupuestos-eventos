using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio en memoria de presupuestos: guarda los objetos en una lista mientras el programa está abierto.
    // Es un Singleton: hay una única instancia compartida por toda la aplicación (se obtiene con GetInstance).
    internal class PresupuestoListRepository : IRepository<Presupuesto>
    {
        private List<Presupuesto> _presupuestos;
        private static PresupuestoListRepository? _instancia;
        private int _proximoId = 1;

        // Constructor privado: nadie puede crear otra instancia con new; solo GetInstance() lo usa.
        private PresupuestoListRepository()
        {
            _presupuestos = new List<Presupuesto>();
        }

        // Devuelve la única instancia. La primera vez la crea; las siguientes devuelve la misma.
        public static PresupuestoListRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PresupuestoListRepository();
            }
            return _instancia;
        }

        // Entrega el siguiente id disponible (1, 2, 3...). Los ids no se reutilizan aunque se elimine algo.
        private int GenerarProximoId()
        {
            return _proximoId++;
        }

        // Le asigna un id nuevo a la entidad y la agrega a la lista.
        public void Agregar(Presupuesto entidad)
        {
            entidad.Id = GenerarProximoId();
            _presupuestos.Add(entidad);
        }

        // Devuelve la lista completa. Es la misma lista que usa el repositorio (no una copia).
        public List<Presupuesto> GetTodos()
        {
            return _presupuestos;
        }

        // Busca por id con LINQ (FirstOrDefault + lambda): devuelve el primero que coincide o null si no hay.
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
