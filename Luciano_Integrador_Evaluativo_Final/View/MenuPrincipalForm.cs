using Luciano_Integrador_Evaluativo_Final.View;

namespace Luciano_Integrador_Evaluativo_Final
{
    public partial class MenuPrincipalForm : Form
    {
        public MenuPrincipalForm()
        {
            InitializeComponent();
            Estilo.Aplicar(this, false);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClienteForm form = new ClienteForm();
            form.ShowDialog();
        }

        private void btnServiciosAdicionales_Click(object sender, EventArgs e)
        {
            ServicioAdicionalForm form = new ServicioAdicionalForm();
            form.ShowDialog();
        }

        private void btnLugares_Click(object sender, EventArgs e)
        {
            LugarEventoForm form = new LugarEventoForm();
            form.ShowDialog();
        }

        private void btnPlatillos_Click(object sender, EventArgs e)
        {
            PlatilloForm form = new PlatilloForm();
            form.ShowDialog();
        }

        private void btnMenus_Click(object sender, EventArgs e)
        {
            MenuForm form = new MenuForm();
            form.ShowDialog();
        }

        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            PresupuestoForm form = new PresupuestoForm();
            form.ShowDialog();
        }
    }
}
