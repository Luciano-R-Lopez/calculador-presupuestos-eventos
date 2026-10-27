using Luciano_Integrador_Evaluativo_Final.View;

namespace Luciano_Integrador_Evaluativo_Final
{
    // Ventana principal: una barra de pestañas arriba (una por pantalla) y, debajo, la pantalla abierta.
    // Cada pantalla se muestra dentro de esta misma ventana, en el panel de contenido.
    public partial class MenuPrincipalForm : Form
    {
        // Pantalla que se está mostrando y pestaña que la abrió (para marcarla como activa).
        private Form? _pantallaActual;
        private Button? _pestanaActiva;

        // Arma la ventana y le da el estilo de la barra de pestañas.
        public MenuPrincipalForm()
        {
            InitializeComponent();
            Estilo.EstilarBarra(pnlBarra, lblTitulo, pnlContenido);
        }

        // Al abrir la ventana se muestra la primera pestaña: Presupuestos.
        private void MenuPrincipalForm_Load(object sender, EventArgs e)
        {
            AbrirEnPanel(new PresupuestoForm(true), btnPresupuestos);
        }

        // Muestra una pantalla dentro del panel de contenido y marca su pestaña como la activa.
        // Primero cierra la pantalla que estaba abierta. Una pantalla común es una ventana, así que
        // para incrustarla se le indica que no es de nivel superior, sin borde y que ocupe todo el panel.
        private void AbrirEnPanel(Form pantalla, Button pestana)
        {
            if (_pantallaActual != null)
            {
                pnlContenido.Controls.Remove(_pantallaActual);
                _pantallaActual.Dispose();
            }

            pantalla.TopLevel = false;
            pantalla.FormBorderStyle = FormBorderStyle.None;
            pantalla.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(pantalla);
            pantalla.Show();
            _pantallaActual = pantalla;

            if (_pestanaActiva != null)
            {
                Estilo.MarcarPestana(_pestanaActiva, false);
            }
            _pestanaActiva = pestana;
            Estilo.MarcarPestana(_pestanaActiva, true);
        }

        // Cada pestaña abre su pantalla en modo embebido (sin encabezado propio).
        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new PresupuestoForm(true), btnPresupuestos);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new ClienteForm(true), btnClientes);
        }

        private void btnLugares_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new LugarEventoForm(true), btnLugares);
        }

        private void btnServiciosAdicionales_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new ServicioAdicionalForm(true), btnServiciosAdicionales);
        }

        private void btnPlatillos_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new PlatilloForm(true), btnPlatillos);
        }

        private void btnMenus_Click(object sender, EventArgs e)
        {
            AbrirEnPanel(new MenuForm(true), btnMenus);
        }
    }
}
