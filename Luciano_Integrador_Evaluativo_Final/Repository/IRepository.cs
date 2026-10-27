using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Contrato que cumplen todos los repositorios. T es el tipo de objeto que guardan.
    // Define qué operaciones ofrece un repositorio, sin importar cómo guarda los datos.
    internal interface IRepository<T>
    {
        // Guarda una entidad nueva y le asigna un id.
        void Agregar(T entidad);
        // Devuelve todas las entidades guardadas.
        List<T> GetTodos();
        // Busca una entidad por su id; devuelve null si no existe.
        T? GetPorId(int id);
        // Reemplaza la entidad guardada que tiene el mismo id.
        void Update(T entidad);
        // Elimina la entidad con ese id.
        void Delete(int id);
    }
}
