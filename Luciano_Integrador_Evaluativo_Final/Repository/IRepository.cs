using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Operaciones comunes de todos los repositorios.
    internal interface IRepository<T>
    {
        // Guarda y le asigna un id.
        void Agregar(T entidad);
        List<T> GetTodos();
        // null si no existe.
        T? GetPorId(int id);
        // Guarda los cambios de la entidad.
        void Update(T entidad);
        void Delete(int id);
    }
}
