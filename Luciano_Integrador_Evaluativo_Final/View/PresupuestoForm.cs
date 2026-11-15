using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla de presupuestos: formulario con el monto al instante y grilla con las acciones.
    // No calcula nada: arma un DTO y se lo pasa al Controller.
    public partial class PresupuestoForm : Form, IPantallaActualizable
    {
        private PresupuestoController _controller;
        private ClienteController _clienteController;
        private LugarEventoController _lugarController;
        private ServicioAdicionalController _servicioController;
        private MenuController _menuController;

        // Ítems cargados antes de registrar.
        private List<ItemMenuDTO> _items;

        // Última opción de cada combo: abre el alta.
        private const string TextoAgregar = "+ Agregar nuevo...";

        // Evita abrir el alta mientras se recargan los combos.
        private bool _cargando;

        // Último índice elegido, para volver a él si no se agrega nada.
        private int _indiceCliente;
        private int _indiceLugar;
        private int _indiceMenu;

        // Ids tildados aparte: el buscador oculta servicios y no se deben perder sus tildes.
        private List<int> _idsServiciosTildados;

        // Evita registrar tildes mientras se rearma la lista.
        private bool _filtrando;

        // Id en edición; 0 = modo registrar.
        private int _idEditando;

        // Id en vista (campos deshabilitados); 0 = ninguno.
        private int _idViendo;

        // Ignora los cambios de selección mientras se recarga la grilla.
        private bool _refrescandoGrilla;

        // Lo que se estaba cargando antes de abrir la vista; vuelve al cerrarla.
        private DateTime _borradorFecha;
        private int _borradorHoras;
        private int _borradorIdCliente;
        private int _borradorIdLugar;
        private int _borradorIdMenu;
        private int _borradorCantidad;
        private bool _borradorAdulto;
        private List<int> _borradorServicios = new List<int>();
        private List<ItemMenuDTO> _borradorItems = new List<ItemMenuDTO>();
        private List<string> _borradorTextosItems = new List<string>();

        public PresupuestoForm() : this(false)
        {
        }

        // embebido = dentro de una pestaña (sin encabezado); si no, ventana aparte.
        public PresupuestoForm(bool embebido)
        {
            InitializeComponent();
            Estilo.Aplicar(this, !embebido);

            // Hora por defecto: 20:00.
            dateTimePickerHora.Value = DateTime.Today.AddHours(20);
            _controller = new PresupuestoController();
            _clienteController = new ClienteController();
            _lugarController = new LugarEventoController();
            _servicioController = new ServicioAdicionalController();
            _menuController = new MenuController();
            _items = new List<ItemMenuDTO>();
            _idsServiciosTildados = new List<int>();

            CargarClientes(0);
            CargarLugares(0);
            CargarMenus(0);
            FiltrarServicios();
            comboBoxEstado.DataSource = Enum.GetValues(typeof(EstadoPresupuesto));

            labelMonto.ForeColor = Estilo.ColorAcento;

            ConfigurarColumnas();
            RefrescarGrilla();
            RecalcularMonto();
            SalirDeEdicion();
        }

        // Al volver a la pestaña relee combos, servicios y grilla, sin perder lo elegido.
        public void ActualizarDatos()
        {
            SeleccionarCliente(IdClienteElegido());
            SeleccionarLugar(IdLugarElegido());
            SeleccionarMenu(IdMenuElegido());

            // Quita los tildes de servicios que ya no existen.
            List<int> idsExistentes = new List<int>();
            foreach (ServicioAdicional servicio in _servicioController.GetServiciosAdicionales())
            {
                idsExistentes.Add(servicio.Id);
            }
            for (int i = _idsServiciosTildados.Count - 1; i >= 0; i--)
            {
                if (!idsExistentes.Contains(_idsServiciosTildados[i]))
                {
                    _idsServiciosTildados.RemoveAt(i);
                }
            }
            FiltrarServicios();

            RefrescarGrilla();
            RecalcularMonto();
        }

        // 0 si no hay nada elegido.
        private int IdClienteElegido()
        {
            Cliente? cliente = comboBoxCliente.SelectedItem as Cliente;
            if (cliente == null)
                return 0;
            return cliente.Id;
        }

        private int IdLugarElegido()
        {
            LugarEvento? lugar = comboBoxLugar.SelectedItem as LugarEvento;
            if (lugar == null)
                return 0;
            return lugar.Id;
        }

        private int IdMenuElegido()
        {
            Menu? menu = comboBoxMenu.SelectedItem as Menu;
            if (menu == null)
                return 0;
            return menu.Id;
        }

        // Recarga el combo con ese cliente elegido (vacío si ya no existe).
        private void SeleccionarCliente(int id)
        {
            int indice = -1;
            List<Cliente> clientes = _clienteController.GetClientes();
            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].Id == id)
                    indice = i;
            }
            CargarClientes(indice);
        }

        private void SeleccionarLugar(int id)
        {
            int indice = -1;
            List<LugarEvento> lugares = _lugarController.GetLugaresEvento();
            for (int i = 0; i < lugares.Count; i++)
            {
                if (lugares[i].Id == id)
                    indice = i;
            }
            CargarLugares(indice);
        }

        private void SeleccionarMenu(int id)
        {
            int indice = -1;
            List<Menu> menus = _menuController.GetMenus();
            for (int i = 0; i < menus.Count; i++)
            {
                if (menus[i].Id == id)
                    indice = i;
            }
            CargarMenus(indice);
        }

        // Datos y, al final, la opción de alta (por eso la lista es de object).
        private void CargarClientes(int indice)
        {
            _cargando = true;

            List<object> opciones = new List<object>();
            foreach (Cliente cliente in _clienteController.GetClientes())
            {
                opciones.Add(cliente);
            }
            opciones.Add(TextoAgregar);

            comboBoxCliente.DataSource = opciones;
            if (indice < opciones.Count - 1)
            {
                comboBoxCliente.SelectedIndex = indice;
            }
            else
            {
                comboBoxCliente.SelectedIndex = -1;
            }
            _indiceCliente = comboBoxCliente.SelectedIndex;

            _cargando = false;
        }

        private void CargarLugares(int indice)
        {
            _cargando = true;

            List<object> opciones = new List<object>();
            foreach (LugarEvento lugar in _lugarController.GetLugaresEvento())
            {
                opciones.Add(lugar);
            }
            opciones.Add(TextoAgregar);

            comboBoxLugar.DataSource = opciones;
            if (indice < opciones.Count - 1)
            {
                comboBoxLugar.SelectedIndex = indice;
            }
            else
            {
                comboBoxLugar.SelectedIndex = -1;
            }
            _indiceLugar = comboBoxLugar.SelectedIndex;

            _cargando = false;
        }

        private void CargarMenus(int indice)
        {
            _cargando = true;

            List<object> opciones = new List<object>();
            foreach (Menu menu in _menuController.GetMenus())
            {
                opciones.Add(menu);
            }
            opciones.Add(TextoAgregar);

            comboBoxMenu.DataSource = opciones;
            if (indice < opciones.Count - 1)
            {
                comboBoxMenu.SelectedIndex = indice;
            }
            else
            {
                comboBoxMenu.SelectedIndex = -1;
            }
            _indiceMenu = comboBoxMenu.SelectedIndex;

            _cargando = false;
        }

        // Si se elige "+ Agregar nuevo...", abre el alta y al volver selecciona el recién agregado.
        private void comboBoxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando)
                return;

            if (comboBoxCliente.SelectedItem != null && comboBoxCliente.SelectedItem.ToString() == TextoAgregar)
            {
                // Si aumentó la cantidad, el nuevo está al final.
                int cantidad = _clienteController.GetClientes().Count;
                ClienteForm form = new ClienteForm();
                form.ShowDialog();

                int total = _clienteController.GetClientes().Count;
                if (total > cantidad)
                {
                    CargarClientes(total - 1);
                }
                else
                {
                    CargarClientes(_indiceCliente);
                }
            }
            else
            {
                _indiceCliente = comboBoxCliente.SelectedIndex;
            }

            RecalcularMonto();
        }

        private void comboBoxLugar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando)
                return;

            if (comboBoxLugar.SelectedItem != null && comboBoxLugar.SelectedItem.ToString() == TextoAgregar)
            {
                int cantidad = _lugarController.GetLugaresEvento().Count;
                LugarEventoForm form = new LugarEventoForm();
                form.ShowDialog();

                int total = _lugarController.GetLugaresEvento().Count;
                if (total > cantidad)
                {
                    CargarLugares(total - 1);
                }
                else
                {
                    CargarLugares(_indiceLugar);
                }
            }
            else
            {
                _indiceLugar = comboBoxLugar.SelectedIndex;
            }

            RecalcularMonto();
        }

        private void comboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando)
                return;

            if (comboBoxMenu.SelectedItem != null && comboBoxMenu.SelectedItem.ToString() == TextoAgregar)
            {
                int cantidad = _menuController.GetMenus().Count;
                MenuForm form = new MenuForm();
                form.ShowDialog();

                int total = _menuController.GetMenus().Count;
                if (total > cantidad)
                {
                    CargarMenus(total - 1);
                }
                else
                {
                    CargarMenus(_indiceMenu);
                }
            }
            else
            {
                _indiceMenu = comboBoxMenu.SelectedIndex;
            }

            RecalcularMonto();
        }

        // Columnas definidas a mano para no mostrar las listas del presupuesto.
        private void ConfigurarColumnas()
        {
            dataGridViewPresupuestos.AutoGenerateColumns = false;
            AgregarColumna("Id", "Id");
            AgregarColumna("FechaCreacion", "Creación");
            AgregarColumna("FechaEvento", "Fecha evento");
            AgregarColumna("Estado", "Estado");
            AgregarColumna("CantidadHoras", "Horas");
            AgregarColumna("Cliente", "Cliente");
            AgregarColumna("Lugar", "Lugar");
            AgregarColumna("MontoTotal", "Monto total");

            dataGridViewPresupuestos.Columns["colFechaCreacion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridViewPresupuestos.Columns["colFechaEvento"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            Estilo.FormatoMoneda(dataGridViewPresupuestos, "colMontoTotal");

            dataGridViewPresupuestos.Columns["colId"].FillWeight = 28;
            dataGridViewPresupuestos.Columns["colCantidadHoras"].FillWeight = 58;
            dataGridViewPresupuestos.Columns["colFechaCreacion"].FillWeight = 140;
            dataGridViewPresupuestos.Columns["colFechaEvento"].FillWeight = 125;
            dataGridViewPresupuestos.Columns["colEstado"].FillWeight = 65;
            dataGridViewPresupuestos.Columns["colCliente"].FillWeight = 115;
            dataGridViewPresupuestos.Columns["colLugar"].FillWeight = 115;
            dataGridViewPresupuestos.Columns["colMontoTotal"].FillWeight = 122;
        }

        private void AgregarColumna(string propiedad, string titulo)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.DataPropertyName = propiedad;
            columna.HeaderText = titulo;
            columna.Name = "col" + propiedad;
            dataGridViewPresupuestos.Columns.Add(columna);
        }

        // Solo lo cargado en el formulario; el cálculo lo hace el Controller.
        private PresupuestoDTO ArmarDTO()
        {
            // "as" da null si no es de ese tipo (ej. la opción de alta). Id 0 = nada elegido.
            int idCliente = 0;
            Cliente? cliente = comboBoxCliente.SelectedItem as Cliente;
            if (cliente != null)
            {
                idCliente = cliente.Id;
            }

            int idLugar = 0;
            LugarEvento? lugar = comboBoxLugar.SelectedItem as LugarEvento;
            if (lugar != null)
            {
                idLugar = lugar.Id;
            }

            List<int> idsServicios = new List<int>(_idsServiciosTildados);

            // Día del primer selector + hora del segundo.
            return new PresupuestoDTO(
                dateTimePickerEvento.Value.Date + dateTimePickerHora.Value.TimeOfDay,
                (int)numericUpDownHoras.Value,
                idCliente,
                idLugar,
                idsServicios,
                new List<ItemMenuDTO>(_items)
            );
        }

        // Monto al instante. Si el formulario está incompleto se muestra "-".
        private void RecalcularMonto()
        {
            // En la vista se muestra el monto guardado.
            if (_idViendo != 0)
                return;

            try
            {
                decimal monto = _controller.CalcularMonto(ArmarDTO());
                labelMonto.Text = "Monto total: " + monto.ToString("C");
            }
            catch (Exception)
            {
                labelMonto.Text = "Monto total: -";
            }
        }

        // null primero para forzar la relectura.
        private void RefrescarGrilla()
        {
            _refrescandoGrilla = true;

            // Copia de la lista: si la del repositorio cambiara por debajo, la grilla pediría filas inexistentes.
            dataGridViewPresupuestos.DataSource = null;
            dataGridViewPresupuestos.DataSource = new BindingList<Presupuesto>(new List<Presupuesto>(_controller.GetPresupuestos()));

            // Deja marcada la fila en vista, o ninguna (la grilla elige sola la primera).
            MarcarFila(_idViendo);

            _refrescandoGrilla = false;
        }

        // Al aparecer, la grilla elige sola la primera fila: luego (BeginInvoke) se corrige.
        private void dataGridViewPresupuestos_VisibleChanged(object? sender, EventArgs e)
        {
            if (dataGridViewPresupuestos.Visible && dataGridViewPresupuestos.IsHandleCreated)
            {
                BeginInvoke(new Action(() => MarcarFila(_idViendo)));
            }
        }

        private void MarcarFila(int id)
        {
            bool anterior = _refrescandoGrilla;
            _refrescandoGrilla = true;

            dataGridViewPresupuestos.CurrentCell = null;
            dataGridViewPresupuestos.ClearSelection();
            foreach (DataGridViewRow fila in dataGridViewPresupuestos.Rows)
            {
                Presupuesto? presupuesto = fila.DataBoundItem as Presupuesto;
                if (presupuesto != null && presupuesto.Id == id)
                {
                    dataGridViewPresupuestos.CurrentCell = fila.Cells[0];
                }
            }

            _refrescandoGrilla = anterior;
        }

        private void btnAgregarItem_Click(object sender, EventArgs e)
        {
            Menu? menu = comboBoxMenu.SelectedItem as Menu;
            if (menu == null)
            {
                MessageBox.Show("Seleccione un menú.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _items.Add(new ItemMenuDTO((int)numericUpDownCantidad.Value, menu.Id, checkBoxAdulto.Checked));
            listBoxItems.Items.Add(TextoItem(menu.Nombre, (int)numericUpDownCantidad.Value, checkBoxAdulto.Checked));
            RecalcularMonto();
        }

        // Ej: "Menú Clásico x50 (adultos)".
        private string TextoItem(string nombreMenu, int cantidad, bool paraAdulto)
        {
            string destino = "niños";
            if (paraAdulto)
            {
                destino = "adultos";
            }

            return nombreMenu + " x" + cantidad + " (" + destino + ")";
        }

        private void btnQuitarItem_Click(object sender, EventArgs e)
        {
            int indice = listBoxItems.SelectedIndex;
            if (indice < 0)
                return;

            _items.RemoveAt(indice);
            listBoxItems.Items.RemoveAt(indice);
            RecalcularMonto();
        }

        // Registra o guarda cambios según el modo; luego limpia el formulario.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idEditando == 0)
                {
                    _controller.RegistrarPresupuesto(ArmarDTO());
                }
                else
                {
                    _controller.ActualizarPresupuesto(_idEditando, ArmarDTO());
                }

                RefrescarGrilla();
                LimpiarCampos();
                SalirDeEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Carga el seleccionado y pasa a modo edición.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un presupuesto de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Avisa si está pagado o el evento ya pasó.
            try
            {
                _controller.ValidarEdicion(seleccionado.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se puede editar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Al editar se sale de la vista.
            if (_idViendo != 0)
            {
                _idViendo = 0;
                HabilitarCampos(true);
            }

            _idEditando = seleccionado.Id;
            CargarEnFormulario(seleccionado);

            btnRegistrar.Text = "Guardar cambios";
            // Se achica para dejar lugar a Cancelar edición, en la misma fila.
            btnRegistrar.Width = btnCancelarEdicion.Width;
            btnCancelarEdicion.Visible = true;
            RecalcularMonto();
        }

        // Lo usan Editar y la vista.
        private void CargarEnFormulario(Presupuesto seleccionado)
        {
            dateTimePickerEvento.Value = seleccionado.FechaEvento;
            dateTimePickerHora.Value = seleccionado.FechaEvento;
            numericUpDownHoras.Value = seleccionado.CantidadHoras;

            int idCliente = 0;
            if (seleccionado.Cliente != null)
                idCliente = seleccionado.Cliente.Id;
            SeleccionarCliente(idCliente);

            int idLugar = 0;
            if (seleccionado.Lugar != null)
                idLugar = seleccionado.Lugar.Id;
            SeleccionarLugar(idLugar);

            // Se limpia la búsqueda para que se vean todos.
            _idsServiciosTildados.Clear();
            foreach (ServicioPresupuestado renglon in seleccionado.ServiciosAdicionales)
            {
                _idsServiciosTildados.Add(renglon.Servicio.Id);
            }
            textBoxBuscarServicio.Clear();
            FiltrarServicios();

            _items.Clear();
            listBoxItems.Items.Clear();
            foreach (ItemMenu item in seleccionado.Menus)
            {
                _items.Add(new ItemMenuDTO(item.Cantidad, item.Item.Id, item.ParaAdulto));
                listBoxItems.Items.Add(TextoItem(item.Item.Nombre, item.Cantidad, item.ParaAdulto));
            }
        }

        // Al elegir una fila (clic o flechas) se muestra ese presupuesto con los campos
        // deshabilitados. Lo que se estaba cargando vuelve con "Cerrar vista".
        private void dataGridViewPresupuestos_SelectionChanged(object? sender, EventArgs e)
        {
            if (_refrescandoGrilla)
                return;

            // Solo si la grilla tiene el foco: así no cuenta la selección automática al aparecer.
            if (!dataGridViewPresupuestos.Focused)
                return;

            // Mientras se edita, no cambia el formulario.
            if (_idEditando != 0)
                return;

            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null || seleccionado.Id == _idViendo)
                return;

            VerPresupuesto(seleccionado);
        }

        private void VerPresupuesto(Presupuesto seleccionado)
        {
            // El borrador se guarda solo al entrar a la vista.
            if (_idViendo == 0)
            {
                GuardarBorrador();
                HabilitarCampos(false);
            }

            _idViendo = seleccionado.Id;
            CargarEnFormulario(seleccionado);
            labelMonto.Text = "Monto total: " + seleccionado.MontoTotal.ToString("C");
            btnCerrarVista.Text = "Cerrar vista del presupuesto N° " + seleccionado.Id;
        }

        private void btnCerrarVista_Click(object sender, EventArgs e)
        {
            CerrarVista();
        }

        private void CerrarVista()
        {
            _idViendo = 0;
            HabilitarCampos(true);
            RestaurarBorrador();

            // Sin selección, para poder volver a ver la misma fila.
            MarcarFila(0);
        }

        // Deshabilitados = vista: Registrar se oculta y aparece Cerrar vista.
        private void HabilitarCampos(bool habilitado)
        {
            dateTimePickerEvento.Enabled = habilitado;
            dateTimePickerHora.Enabled = habilitado;
            numericUpDownHoras.Enabled = habilitado;
            comboBoxCliente.Enabled = habilitado;
            comboBoxLugar.Enabled = habilitado;
            textBoxBuscarServicio.Enabled = habilitado;
            checkedListBoxServicios.Enabled = habilitado;
            comboBoxMenu.Enabled = habilitado;
            numericUpDownCantidad.Enabled = habilitado;
            checkBoxAdulto.Enabled = habilitado;
            listBoxItems.Enabled = habilitado;

            // Los botones planos no se ven grises: se ocultan.
            btnAgregarItem.Visible = habilitado;
            btnQuitarItem.Visible = habilitado;

            btnRegistrar.Visible = habilitado;
            btnCerrarVista.Visible = !habilitado;
        }

        private void GuardarBorrador()
        {
            _borradorFecha = dateTimePickerEvento.Value.Date + dateTimePickerHora.Value.TimeOfDay;
            _borradorHoras = (int)numericUpDownHoras.Value;
            _borradorIdCliente = IdClienteElegido();
            _borradorIdLugar = IdLugarElegido();
            _borradorIdMenu = IdMenuElegido();
            _borradorCantidad = (int)numericUpDownCantidad.Value;
            _borradorAdulto = checkBoxAdulto.Checked;
            _borradorServicios = new List<int>(_idsServiciosTildados);
            _borradorItems = new List<ItemMenuDTO>(_items);

            _borradorTextosItems = new List<string>();
            foreach (object texto in listBoxItems.Items)
            {
                _borradorTextosItems.Add(texto.ToString() ?? "");
            }
        }

        private void RestaurarBorrador()
        {
            dateTimePickerEvento.Value = _borradorFecha;
            dateTimePickerHora.Value = _borradorFecha;
            numericUpDownHoras.Value = _borradorHoras;
            SeleccionarCliente(_borradorIdCliente);
            SeleccionarLugar(_borradorIdLugar);
            SeleccionarMenu(_borradorIdMenu);
            numericUpDownCantidad.Value = _borradorCantidad;
            checkBoxAdulto.Checked = _borradorAdulto;

            _idsServiciosTildados = new List<int>(_borradorServicios);
            textBoxBuscarServicio.Clear();
            FiltrarServicios();

            _items = new List<ItemMenuDTO>(_borradorItems);
            listBoxItems.Items.Clear();
            foreach (string texto in _borradorTextosItems)
            {
                listBoxItems.Items.Add(texto);
            }

            RecalcularMonto();
        }

        // Pide confirmación; el Service lo rechaza si está pagado.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un presupuesto de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar el presupuesto N° " + seleccionado.Id + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                _controller.EliminarPresupuesto(seleccionado.Id);

                if (_idViendo == seleccionado.Id)
                {
                    CerrarVista();
                }

                if (_idEditando == seleccionado.Id)
                {
                    LimpiarCampos();
                    SalirDeEdicion();
                }

                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            SalirDeEdicion();
        }

        private void SalirDeEdicion()
        {
            _idEditando = 0;
            btnRegistrar.Text = "Registrar presupuesto";
            // Ancho completo de la columna, igual que Cerrar vista.
            btnRegistrar.Width = btnCerrarVista.Width;
            btnCancelarEdicion.Visible = false;
        }

        // null si no hay ninguna.
        private Presupuesto? ObtenerSeleccionado()
        {
            // SelectedRows y no CurrentRow: SelectionChanged llega antes de que se actualice CurrentRow.
            if (dataGridViewPresupuestos.SelectedRows.Count == 0)
            {
                return null;
            }
            return dataGridViewPresupuestos.SelectedRows[0].DataBoundItem as Presupuesto;
        }

        // El Controller rechaza los cambios de estado no permitidos.
        private void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un presupuesto de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _controller.CambiarEstado(seleccionado.Id, (EstadoPresupuesto)comboBoxEstado.SelectedItem);
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClonar_Click(object sender, EventArgs e)
        {
            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un presupuesto de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _controller.ClonarPresupuesto(seleccionado.Id);
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un presupuesto de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
            dialogo.FileName = "Presupuesto_" + seleccionado.Id + ".pdf";
            if (dialogo.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                _controller.GenerarPdf(seleccionado.Id, dialogo.FileName);
                MessageBox.Show("PDF generado correctamente.", "Presupuesto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Muestra los servicios que coinciden con la búsqueda, conservando los tildados.
        private void FiltrarServicios()
        {
            _filtrando = true;

            string texto = textBoxBuscarServicio.Text.Trim().ToLower();

            checkedListBoxServicios.Items.Clear();
            foreach (ServicioAdicional servicio in _servicioController.GetServiciosAdicionales())
            {
                if (texto == "" || servicio.Nombre.ToLower().Contains(texto))
                {
                    checkedListBoxServicios.Items.Add(servicio, _idsServiciosTildados.Contains(servicio.Id));
                }
            }

            _filtrando = false;
        }

        private void textBoxBuscarServicio_TextChanged(object sender, EventArgs e)
        {
            FiltrarServicios();
        }

        private void checkedListBoxServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_filtrando)
                return;

            ServicioAdicional servicio = (ServicioAdicional)checkedListBoxServicios.Items[e.Index];
            if (e.NewValue == CheckState.Checked)
            {
                if (!_idsServiciosTildados.Contains(servicio.Id))
                {
                    _idsServiciosTildados.Add(servicio.Id);
                }
            }
            else
            {
                _idsServiciosTildados.Remove(servicio.Id);
            }

            // ItemCheck llega antes de aplicar el tilde: BeginInvoke recalcula después.
            BeginInvoke(new Action(RecalcularMonto));
        }

        private void LimpiarCampos()
        {
            _items.Clear();
            listBoxItems.Items.Clear();
            numericUpDownHoras.Value = numericUpDownHoras.Minimum;
            numericUpDownCantidad.Value = numericUpDownCantidad.Minimum;
            checkBoxAdulto.Checked = true;
            textBoxBuscarServicio.Clear();
            _idsServiciosTildados.Clear();
            FiltrarServicios();
            RecalcularMonto();
        }
    }
}
