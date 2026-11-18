using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Base de datos SQLite (Entity Framework): cada DbSet es una tabla.
    // Singleton: toda la app comparte el mismo contexto.
    internal class AppDbContext : DbContext
    {
        private static AppDbContext? _instancia;

        // Archivo de la base, al lado del ejecutable.
        private static readonly string RutaBaseDeDatos = Path.Combine(AppContext.BaseDirectory, "presupuestos.db");

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<LugarEvento> Lugares { get; set; } = null!;
        public DbSet<ServicioAdicional> ServiciosAdicionales { get; set; } = null!;
        public DbSet<Platillo> Platillos { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Presupuesto> Presupuestos { get; set; } = null!;

        // Privado: se obtiene con GetInstance().
        private AppDbContext()
        {
        }

        public static AppDbContext GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new AppDbContext();
            }
            return _instancia;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + RutaBaseDeDatos);
        }

        // Tablas y relaciones (Fluent API).
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<LugarEvento>().ToTable("Lugares");
            modelBuilder.Entity<ServicioAdicional>().ToTable("ServiciosAdicionales");
            modelBuilder.Entity<Platillo>().ToTable("Platillos");

            // Menú <-> platillos (tabla MenuPlatillo). No se puede borrar un platillo usado en un menú.
            modelBuilder.Entity<Menu>(menu =>
            {
                menu.ToTable("Menus");
                menu.HasMany(m => m.Platillos)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "MenuPlatillo",
                        fila => fila.HasOne<Platillo>().WithMany().HasForeignKey("PlatilloId").OnDelete(DeleteBehavior.Restrict),
                        fila => fila.HasOne<Menu>().WithMany().HasForeignKey("MenuId").OnDelete(DeleteBehavior.Cascade));

                // Aptitudes como texto separado por comas, ej. "Veganos,Celiacos".
                menu.Property(m => m.Aptitudes)
                    .HasField("_aptitud")
                    .HasConversion(
                        lista => AptitudesATexto(lista),
                        texto => TextoAAptitudes(texto),
                        new ValueComparer<List<Aptitud>>(
                            (a, b) => AptitudesATexto(a!) == AptitudesATexto(b!),
                            lista => AptitudesATexto(lista).GetHashCode(),
                            lista => lista.ToList()));
            });

            // Presupuesto -> cliente y lugar (claves foráneas) y sus renglones de servicios y menús.
            // No se puede borrar un cliente o lugar en uso; al borrar el presupuesto se borran sus renglones.
            modelBuilder.Entity<Presupuesto>(presupuesto =>
            {
                presupuesto.ToTable("Presupuestos");
                presupuesto.HasOne(p => p.Cliente).WithMany().HasForeignKey("ClienteId").IsRequired().OnDelete(DeleteBehavior.Restrict);
                presupuesto.HasOne(p => p.Lugar).WithMany().HasForeignKey("LugarId").IsRequired().OnDelete(DeleteBehavior.Restrict);
                presupuesto.HasMany(p => p.ServiciosAdicionales).WithOne().HasForeignKey("PresupuestoId").IsRequired().OnDelete(DeleteBehavior.Cascade);
                presupuesto.HasMany(p => p.Menus).WithOne().HasForeignKey("PresupuestoId").IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            // Renglón de servicio con su precio al cotizar. No se puede borrar un servicio en uso.
            modelBuilder.Entity<ServicioPresupuestado>(renglon =>
            {
                renglon.ToTable("PresupuestoServicio");
                renglon.HasOne(r => r.Servicio).WithMany().HasForeignKey("ServicioAdicionalId").IsRequired().OnDelete(DeleteBehavior.Restrict);
            });

            // Renglón de menú con su precio al cotizar. No se puede borrar un menú en uso.
            modelBuilder.Entity<ItemMenu>(item =>
            {
                item.ToTable("ItemsMenu");
                item.HasOne(i => i.Item).WithMany().HasForeignKey("MenuId").IsRequired().OnDelete(DeleteBehavior.Restrict);
            });
        }

        // [Veganos, Celiacos] -> "Veganos,Celiacos"
        private static string AptitudesATexto(List<Aptitud> aptitudes)
        {
            return string.Join(",", aptitudes);
        }

        // "Veganos,Celiacos" -> [Veganos, Celiacos]
        private static List<Aptitud> TextoAAptitudes(string texto)
        {
            List<Aptitud> aptitudes = new List<Aptitud>();
            foreach (string nombre in texto.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                aptitudes.Add(Enum.Parse<Aptitud>(nombre));
            }
            return aptitudes;
        }
    }
}
