using Luciano_Integrador_Evaluativo_Final.View;

namespace Luciano_Integrador_Evaluativo_Final
{
    // Menú principal: un botón por cada pantalla de la aplicación.
    public partial class MenuPrincipalForm : Form
    {
        // Arma la pantalla con el estilo común, sin el encabezado automático (ya tiene el suyo).
        public MenuPrincipalForm()
        {
            InitializeComponent();
            Estilo.Aplicar(this, false);
        }

        // Los botones abren la pantalla correspondiente como ventana modal (ShowDialog): hay que cerrarla
        // para volver al menú.
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClienteForm form = new ClienteForm();
            form.ShowDialog();
        }

        // Abre la pantalla de servicios adicionales.
        private void btnServiciosAdicionales_Click(object sender, EventArgs e)
        {
            ServicioAdicionalForm form = new ServicioAdicionalForm();
            form.ShowDialog();
        }

        // Abre la pantalla de lugares de evento.
        private void btnLugares_Click(object sender, EventArgs e)
        {
            LugarEventoForm form = new LugarEventoForm();
            form.ShowDialog();
        }

        // Abre la pantalla de platillos.
        private void btnPlatillos_Click(object sender, EventArgs e)
        {
            PlatilloForm form = new PlatilloForm();
            form.ShowDialog();
        }

        // Abre la pantalla de menús.
        private void btnMenus_Click(object sender, EventArgs e)
        {
            MenuForm form = new MenuForm();
            form.ShowDialog();
        }

        // Abre la pantalla de presupuestos.
        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            PresupuestoForm form = new PresupuestoForm();
            form.ShowDialog();
        }
    }
}
