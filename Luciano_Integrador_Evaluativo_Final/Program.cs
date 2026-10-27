using Luciano_Integrador_Evaluativo_Final.Service;
using Luciano_Integrador_Evaluativo_Final.View;

namespace Luciano_Integrador_Evaluativo_Final
{
    internal static class Program
    {
        // Punto de entrada de la aplicación: carga los datos de ejemplo y abre el menú principal.
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Datos de ejemplo para armar presupuestos sin cargar todo a mano.
            DatosDePruebaService datosDePrueba = new DatosDePruebaService();
            datosDePrueba.Cargar();

            Application.Run(new MenuPrincipalForm());
        }
    }
}