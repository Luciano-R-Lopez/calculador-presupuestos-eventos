using Luciano_Integrador_Evaluativo_Final.View;

namespace Luciano_Integrador_Evaluativo_Final
{
    // Ventana principal: una barra de pestañas arriba (una por pantalla) y, debajo, la pantalla abierta.
    // Cada pantalla se muestra dentro de esta misma ventana, en el panel de contenido.
    public partial class MenuPrincipalForm : Form
    {
        // Pantalla de cada pestaña. Se crea la primera vez que se abre la pestaña y después se reutiliza:
        // al cambiar de pestaña solo se oculta, así los campos que el usuario ya completó no se pierden.
        private Dictionary<Button, Form> _pantallas;

        // Pantalla que se está mostrando y pestaña que la abrió (para marcarla como activa).
        private Form? _pantallaActual;
        private Button? _pestanaActiva;

        // Arma la ventana y le da el estilo de la barra de pestañas.
        public MenuPrincipalForm()
        {
            InitializeComponent();
            Estilo.EstilarBarra(pnlBarra, lblTitulo, pnlContenido);
            _pantallas = new Dictionary<Button, Form>();
        }

        // Al abrir la ventana se muestra la primera pestaña: Presupuestos.
        private void MenuPrincipalForm_Load(object sender, EventArgs e)
        {
            AbrirPestana(btnPresupuestos);
        }

        // Crea la pantalla que corresponde a cada pestaña, en modo embebido (sin encabezado propio).
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

        // Muestra la pantalla de una pestaña dentro del panel de contenido y marca la pestaña como activa.
        // Si la pantalla todavía no existe se crea; si ya existía se vuelve a mostrar tal como quedó y se le
        // pide que relea los datos (pudieron cambiar en otra pestaña). La pantalla anterior solo se oculta.
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
                // Una pantalla común es una ventana, así que para incrustarla se le indica que no es de
                // nivel superior, sin borde y que ocupe todo el panel.
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
