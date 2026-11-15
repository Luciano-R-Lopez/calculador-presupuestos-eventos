using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio con Entity Framework (hereda de EfRepository). Singleton.
    internal class LugarEventoRepository : EfRepository<LugarEvento>
    {
        private static LugarEventoRepository? _instancia;

        private LugarEventoRepository()
        {
        }

        public static LugarEventoRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new LugarEventoRepository();
            }
            return _instancia;
        }
    }
}
