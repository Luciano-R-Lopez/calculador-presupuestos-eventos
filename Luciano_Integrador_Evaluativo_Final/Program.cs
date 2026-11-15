using Luciano_Integrador_Evaluativo_Final.Repository;
using Luciano_Integrador_Evaluativo_Final.Service;
using Luciano_Integrador_Evaluativo_Final.View;
using Microsoft.EntityFrameworkCore;

namespace Luciano_Integrador_Evaluativo_Final
{
    internal static class Program
    {
        // Prepara la base, carga los datos de ejemplo (solo la primera vez) y abre el menú principal.
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Crea la base si no existe y aplica las migraciones pendientes.
            AppDbContext.GetInstance().Database.Migrate();

            DatosDePruebaService datosDePrueba = new DatosDePruebaService();
            datosDePrueba.Cargar();

            Application.Run(new MenuPrincipalForm());
        }
    }
}
