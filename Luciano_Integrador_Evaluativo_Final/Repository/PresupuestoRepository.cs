using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio con Entity Framework (hereda de EfRepository). Singleton.
    internal class PresupuestoRepository : EfRepository<Presupuesto>
    {
        private static PresupuestoRepository? _instancia;

        private PresupuestoRepository()
        {
        }

        public static PresupuestoRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PresupuestoRepository();
            }
            return _instancia;
        }

        // Con cliente, lugar, servicios y menús (con sus platillos).
        protected override IQueryable<Presupuesto> Consulta()
        {
            return _contexto.Presupuestos
                .Include(presupuesto => presupuesto.Cliente)
                .Include(presupuesto => presupuesto.Lugar)
                .Include(presupuesto => presupuesto.ServiciosAdicionales)
                    .ThenInclude(renglon => renglon.Servicio)
                .Include(presupuesto => presupuesto.Menus)
                    .ThenInclude(item => item.Item)
                        .ThenInclude(menu => menu.Platillos);
        }
    }
}
