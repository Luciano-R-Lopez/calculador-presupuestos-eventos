using Luciano_Integrador_Evaluativo_Final.View;

namespace Luciano_Integrador_Evaluativo_Final
{
    // Ventana principal: barra de pestañas y, debajo, la pantalla abierta.
    public partial class MenuPrincipalForm : Form
    {
        // Una pantalla por pestaña; se crea una vez y al cambiar de pestaña solo se oculta.
        private Dictionary<Button, Form> _pantallas;

        private Form? _pantallaActual;
        private Button? _pestanaActiva;

        public MenuPrincipalForm()
        {
            InitializeComponent();
            Estilo.EstilarBarra(pnlBarra, lblTitulo, pnlContenido);
            _pantallas = new Dictionary<Button, Form>();
        }

        // Arranca en Presupuestos.
        private void MenuPrincipalForm_Load(object sender, EventArgs e)
        {
            AbrirPestana(btnPresupuestos);
        }

        // Pantallas embebidas (sin encabezado propio).
        private Form CrearPantalla(Button pestana)
        {
            if (pestana == btnClientes)
                return new ClienteForm(true);
            if (pestana == btnLugares)
                return new LugarEventoForm(true);
            if (pestana == btnServiciosAdicionales)
                return new ServicioAdicionalForm(true);
            if (pestana == btnPlatillos)
                return new PlatilloForm(true);
            if (pestana == btnMenus)
                return new MenuForm(true);
            return new PresupuestoForm(true);
        }

        // Muestra la pantalla de la pestaña (la crea si no existe) y la marca como activa.
        // Si ya existía, relee sus datos.
        private void AbrirPestana(Button pestana)
        {
            if (pestana == _pestanaActiva)
                return;

            Form? pantalla;
            if (_pantallas.TryGetValue(pestana, out pantalla))
            {
                IPantallaActualizable? actualizable = pantalla as IPantallaActualizable;
                if (actualizable != null)
                {
                    actualizable.ActualizarDatos();
                }
            }
            else
            {
                // Para incrustar un Form: no de nivel superior, sin borde y ocupando todo el panel.
                pantalla = CrearPantalla(pestana);
                pantalla.TopLevel = false;
                pantalla.FormBorderStyle = FormBorderStyle.None;
                pantalla.Dock = DockStyle.Fill;
                pnlContenido.Controls.Add(pantalla);
                _pantallas.Add(pestana, pantalla);
            }

            if (_pantallaActual != null)
            {
                _pantallaActual.Hide();
            }
            pantalla.Show();
            pantalla.BringToFront();
            _pantallaActual = pantalla;

            if (_pestanaActiva != null)
            {
                Estilo.MarcarPestana(_pestanaActiva, false);
            }
            _pestanaActiva = pestana;
            Estilo.MarcarPestana(_pestanaActiva, true);
        }

        private void btnPresupuestos_Click(object sender, EventArgs e)
        {
            AbrirPestana(btnPresupuestos);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            AbrirPestana(btnClientes);
        }

        private void btnLugares_Click(object sender, EventArgs e)
        {
            AbrirPestana(btnLugares);
        }

        private void btnServiciosAdicionales_Click(object sender, EventArgs e)
        {
            AbrirPestana(btnServiciosAdicionales);
        }

        private void btnPlatillos_Click(object sender, EventArgs e)
        {
            AbrirPestana(btnPlatillos);
        }

        private void btnMenus_Click(object sender, EventArgs e)
        {
            AbrirPestana(btnMenus);
        }
    }
}
