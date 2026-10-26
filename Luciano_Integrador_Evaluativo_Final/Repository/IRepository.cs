using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    internal interface IRepository<T>
    {
        void Agregar(T entidad);
        List<T> GetTodos();
        T? GetPorId(int id);
        void Update(T entidad);
        void Delete(int id);
    }
}
