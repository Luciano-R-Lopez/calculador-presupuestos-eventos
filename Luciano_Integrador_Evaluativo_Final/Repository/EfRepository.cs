using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Implementación de IRepository<T> con Entity Framework, común a todos los repositorios.
    internal abstract class EfRepository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _contexto;

        protected EfRepository()
        {
            _contexto = AppDbContext.GetInstance();
        }

        // Los repositorios que necesitan datos relacionados la redefinen con Include.
        protected virtual IQueryable<T> Consulta()
        {
            return _contexto.Set<T>();
        }

        public void Agregar(T entidad)
        {
            _contexto.Set<T>().Add(entidad);
            Guardar();
        }

        public List<T> GetTodos()
        {
            return Consulta().ToList();
        }

        // EF.Property lee la columna Id sin conocer la clase.
        public T? GetPorId(int id)
        {
            return Consulta().FirstOrDefault(entidad => EF.Property<int>(entidad, "Id") == id);
        }

        // La entidad ya está seguida por el contexto compartido: alcanza con guardar.
        public void Update(T entidad)
        {
            if (_contexto.Entry(entidad).State == EntityState.Detached)
            {
                _contexto.Set<T>().Update(entidad);
            }
            Guardar();
        }

        public void Delete(int id)
        {
            T? entidad = GetPorId(id);
            if (entidad != null)
            {
                _contexto.Set<T>().Remove(entidad);
                Guardar();
            }
        }

        // Si la base rechaza el cambio (ej. clave foránea), se descarta y se avisa con un error.
        protected void Guardar()
        {
            try
            {
                _contexto.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                DescartarCambios();
                string detalle = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                throw new InvalidOperationException("No se pudieron guardar los cambios en la base de datos: " + detalle);
            }
        }

        // Vuelve el contexto al estado anterior a los cambios que fallaron.
        private void DescartarCambios()
        {
            foreach (var entrada in _contexto.ChangeTracker.Entries().ToList())
            {
                if (entrada.State == EntityState.Added)
                {
                    entrada.State = EntityState.Detached;
                }
                else if (entrada.State == EntityState.Modified)
                {
                    entrada.CurrentValues.SetValues(entrada.OriginalValues);
                    entrada.State = EntityState.Unchanged;
                }
                else if (entrada.State == EntityState.Deleted)
                {
                    entrada.State = EntityState.Unchanged;
                }
            }
        }
    }
}
