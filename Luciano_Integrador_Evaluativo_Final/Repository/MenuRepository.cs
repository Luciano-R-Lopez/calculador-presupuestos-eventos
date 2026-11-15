using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio con Entity Framework (hereda de EfRepository). Singleton.
    internal class MenuRepository : EfRepository<Menu>
    {
        private static MenuRepository? _instancia;

        private MenuRepository()
        {
        }

        public static MenuRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new MenuRepository();
            }
            return _instancia;
        }

        // Con sus platillos.
        protected override IQueryable<Menu> Consulta()
        {
            return _contexto.Menus.Include(menu => menu.Platillos);
        }
    }
}
