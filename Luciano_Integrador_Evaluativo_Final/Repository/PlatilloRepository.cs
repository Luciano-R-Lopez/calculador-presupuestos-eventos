using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Repositorio con Entity Framework (hereda de EfRepository). Singleton.
    internal class PlatilloRepository : EfRepository<Platillo>
    {
        private static PlatilloRepository? _instancia;

        private PlatilloRepository()
        {
        }

        public static PlatilloRepository GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new PlatilloRepository();
            }
            return _instancia;
        }
    }
}
