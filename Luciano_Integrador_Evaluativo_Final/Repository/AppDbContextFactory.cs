using Microsoft.EntityFrameworkCore.Design;

namespace Luciano_Integrador_Evaluativo_Final.Repository
{
    // Solo para dotnet ef (migraciones): le indica cómo obtener el contexto.
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            return AppDbContext.GetInstance();
        }
    }
}
