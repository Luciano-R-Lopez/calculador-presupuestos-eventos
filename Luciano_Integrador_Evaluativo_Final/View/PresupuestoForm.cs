using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla principal de presupuestos: se arma un presupuesto (fecha, horas, cliente, lugar, servicios y menús),
    // con el monto calculándose al instante, y debajo la grilla para cambiar el estado, clonar, editar, eliminar
    // y generar el PDF. No calcula nada: arma un DTO y le pide los resultados al Controller.
    public partial class PresupuestoForm : Form
    {
        private PresupuestoController _controller;
        private ClienteController _clienteController;
        private LugarEventoController _lugarController;
        private ServicioAdicionalController _servicioController;
        private MenuController _menuController;

        // Ítems de menú cargados en el formulario, antes de registrar el presupuesto.
        private List<ItemMenuDTO> _items;

        // Texto de la última opción de cada combo, que abre la pestaña de alta.
        private const string TextoAgregar = "+ Agregar nuevo...";

        // Evita que se abra una pestaña de alta mientras se recargan los combos.
        private bool _cargando;

        // Último índice elegido en cada combo, para volver a él si no se agrega nada.
        private int _indiceCliente;
        private int _indiceLugar;
        private int _indiceMenu;

        // Ids de los servicios tildados. Se guardan aparte porque, al buscar, la lista solo muestra
        // los servicios que coinciden y los tildados que quedan ocultos no se deben perder.
        private List<int> _idsServiciosTildados;

        // Evita registrar cambios de tilde mientras se vuelve a armar la lista de servicios.
        private bool _filtrando;

        // Id del presupuesto que se está editando. En 0 el formulario está en modo "registrar".
        private int _idEditando;

        // Arma la pantalla: aplica el estilo, crea los Controllers, carga los combos, la lista de servicios, la grilla y el monto.
        public PresupuestoForm() : this(false)
        {
        }

        // Si embebido es true la pantalla se muestra dentro de la ventana principal (en una pestaña), sin encabezado
        // propio. Si es false se muestra como ventana aparte (por ejemplo desde "+ Agregar nuevo...").
        public PresupuestoForm(bool embebido)
        {
            InitializeComponent();
            Estilo.Aplicar(this, !embebido);

            // La hora de inicio arranca en las 20:00 (un horario habitual para un evento); se puede cambiar.
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

            // El monto se destaca por ser el dato principal de la pantalla.
            labelMonto.ForeColor = Estilo.ColorAcento;

            ConfigurarColumnas();
            RefrescarGrilla();
            RecalcularMonto();
            SalirDeEdicion();
        }

        // Carga cada combo con los datos y, al final, la opción para abrir la pestaña de alta.
        // La lista es de object porque mezcla los objetos del modelo con el texto de la opción.
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

        // Igual que CargarClientes, para el combo de lugares.
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

        // Igual que CargarClientes, para el combo de menús.
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

        // Al elegir un cliente recuerda la posición y recalcula el monto. Si se elige la opción "+ Agregar nuevo...",
        // abre la pantalla de clientes y, al volver, recarga el combo y deja elegido el cliente recién agregado.
        private void comboBoxCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargando)
                return;

            if (comboBoxCliente.SelectedItem != null && comboBoxCliente.SelectedItem.ToString() == TextoAgregar)
            {
                // Se compara la cantidad antes y después de cerrar la pestaña:
                // si aumentó se agregó uno nuevo (queda al final) y se selecciona.
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

        // Igual que el combo de clientes, pero con la pantalla de lugares.
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

        // Igual que el combo de clientes, pero con la pantalla de menús.
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

        // La grilla muestra Presupuesto, que tiene listas (menús, servicios). Con columnas automáticas
        // aparecerían columnas inútiles de colecciones, por eso se definen a mano solo las que interesan.
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

            // Id y Horas son datos cortos: se les da menos ancho que al resto.
            dataGridViewPresupuestos.Columns["colId"].FillWeight = 28;
            dataGridViewPresupuestos.Columns["colCantidadHoras"].FillWeight = 58;
            dataGridViewPresupuestos.Columns["colFechaCreacion"].FillWeight = 140;
            dataGridViewPresupuestos.Columns["colFechaEvento"].FillWeight = 125;
            dataGridViewPresupuestos.Columns["colEstado"].FillWeight = 65;
            dataGridViewPresupuestos.Columns["colCliente"].FillWeight = 115;
            dataGridViewPresupuestos.Columns["colLugar"].FillWeight = 115;
            dataGridViewPresupuestos.Columns["colMontoTotal"].FillWeight = 122;
        }

        // Agrega una columna a la grilla, enlazada a una propiedad del presupuesto y con su título.
        private void AgregarColumna(string propiedad, string titulo)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.DataPropertyName = propiedad;
            columna.HeaderText = titulo;
            columna.Name = "col" + propiedad;
            dataGridViewPresupuestos.Columns.Add(columna);
        }

        // Arma el DTO solo con lo que capturó el formulario. El cálculo lo hace el Controller.
        private PresupuestoDTO ArmarDTO()
        {
            // "as" convierte el elemento elegido y devuelve null si no es de ese tipo
            // (por ejemplo, si está seleccionada la opción "+ Agregar nuevo...").
            // Con id 0 el Service interpreta que no hay nada elegido.
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

            // La fecha y la hora de inicio se eligen en dos selectores: se toma el día del primero y la
            // hora del segundo (Date + TimeOfDay) para armar el DateTime con el inicio del evento.
            return new PresupuestoDTO(
                dateTimePickerEvento.Value.Date + dateTimePickerHora.Value.TimeOfDay,
                (int)numericUpDownHoras.Value,
                idCliente,
                idLugar,
                idsServicios,
                new List<ItemMenuDTO>(_items)
            );
        }

        // Muestra el monto al instante. La View no calcula nada: arma el DTO y le pide el resultado al
        // Controller. Mientras el formulario está incompleto el Service lanza excepciones; en ese caso
        // se muestra "-" en lugar de un mensaje de error.
        private void RecalcularMonto()
        {
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

        // Se asigna null primero y luego una lista nueva para forzar a la grilla a releer los datos.
        private void RefrescarGrilla()
        {
            dataGridViewPresupuestos.DataSource = null;
            dataGridViewPresupuestos.DataSource = new BindingList<Presupuesto>(_controller.GetPresupuestos());
        }

        // Botón Agregar ítem: agrega a la lista el menú elegido con su cantidad de invitados (adultos o niños)
        // y recalcula el monto.
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

        // Texto con el que se muestra cada ítem de menú en la lista, por ejemplo "Menú Clásico x50 (adultos)".
        private string TextoItem(string nombreMenu, int cantidad, bool paraAdulto)
        {
            string destino = "niños";
            if (paraAdulto)
            {
                destino = "adultos";
            }

            return nombreMenu + " x" + cantidad + " (" + destino + ")";
        }

        // Botón Quitar ítem: saca de la lista el ítem seleccionado y recalcula el monto.
        private void btnQuitarItem_Click(object sender, EventArgs e)
        {
            int indice = listBoxItems.SelectedIndex;
            if (indice < 0)
                return;

            _items.RemoveAt(indice);
            listBoxItems.Items.RemoveAt(indice);
            RecalcularMonto();
        }

        // Botón Registrar/Guardar cambios: envía el DTO al Controller. En modo edición actualiza el presupuesto;
        // si no, registra uno nuevo. Luego refresca la grilla y limpia el formulario.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // El mismo botón registra o guarda cambios según el modo en que esté el formulario.
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

        // Carga en el formulario el presupuesto seleccionado en la grilla y pasa a modo edición.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Presupuesto? seleccionado = ObtenerSeleccionado();
            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un presupuesto de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si el presupuesto no se puede editar (pagado o evento ya pasado), se avisa el motivo.
            try
            {
                _controller.ValidarEdicion(seleccionado.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se puede editar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = seleccionado.Id;
            dateTimePickerEvento.Value = seleccionado.FechaEvento;
            dateTimePickerHora.Value = seleccionado.FechaEvento;
            numericUpDownHoras.Value = seleccionado.CantidadHoras;

            // Se vuelve a cargar cada combo dejando elegidos el cliente y el lugar del presupuesto.
            List<Cliente> clientes = _clienteController.GetClientes();
            for (int i = 0; i < clientes.Count; i++)
            {
                if (seleccionado.Cliente != null && clientes[i].Id == seleccionado.Cliente.Id)
                {
                    CargarClientes(i);
                }
            }

            List<LugarEvento> lugares = _lugarController.GetLugaresEvento();
            for (int i = 0; i < lugares.Count; i++)
            {
                if (seleccionado.Lugar != null && lugares[i].Id == seleccionado.Lugar.Id)
                {
                    CargarLugares(i);
                }
            }

            // Se tildan los servicios del presupuesto (la búsqueda se limpia para que se vean todos).
            _idsServiciosTildados.Clear();
            foreach (ServicioAdicional servicio in seleccionado.ServiciosAdicionales)
            {
                _idsServiciosTildados.Add(servicio.Id);
            }
            textBoxBuscarServicio.Clear();
            FiltrarServicios();

            // Se cargan los ítems de menú del presupuesto.
            _items.Clear();
            listBoxItems.Items.Clear();
            foreach (ItemMenu item in seleccionado.Menus)
            {
                _items.Add(new ItemMenuDTO(item.Cantidad, item.Item.Id, item.ParaAdulto));
                listBoxItems.Items.Add(TextoItem(item.Item.Nombre, item.Cantidad, item.ParaAdulto));
            }

            btnRegistrar.Text = "Guardar cambios";
            btnCancelarEdicion.Visible = true;
            RecalcularMonto();
        }

        // Elimina el presupuesto seleccionado en la grilla, previa confirmación. Si está pagado, el
        // Service lo rechaza con un mensaje y no se borra.
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

                // Si se eliminó justo el que se estaba editando, se sale del modo edición.
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

        // Botón Cancelar edición: descarta lo cargado y vuelve al modo registrar.
        private void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            SalirDeEdicion();
        }

        // Vuelve al modo registrar: sin presupuesto en edición, botón con su texto original y sin botón Cancelar.
        private void SalirDeEdicion()
        {
            _idEditando = 0;
            btnRegistrar.Text = "Registrar presupuesto";
            btnCancelarEdicion.Visible = false;
        }

        // Devuelve el presupuesto de la fila seleccionada, o null si no hay ninguna.
        private Presupuesto? ObtenerSeleccionado()
        {
            if (dataGridViewPresupuestos.CurrentRow == null)
            {
                return null;
            }
            return dataGridViewPresupuestos.CurrentRow.DataBoundItem as Presupuesto;
        }

        // Botón Cambiar estado: pasa el presupuesto seleccionado al estado elegido en el combo.
        // Si el cambio no está permitido, el Controller lanza un error que se muestra en un cuadro.
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
                // El combo se llenó con los valores del enum, por eso se convierte con un cast.
                _controller.CambiarEstado(seleccionado.Id, (EstadoPresupuesto)comboBoxEstado.SelectedItem);
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Botón Clonar: crea un presupuesto nuevo igual al seleccionado.
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

        // Botón Generar PDF: pide dónde guardar el archivo y le pide al Controller generar el PDF del presupuesto seleccionado.
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

        // Vuelve a armar la lista mostrando solo los servicios cuyo nombre contiene el texto buscado.
        // Cada servicio aparece tildado si su id está entre los tildados.
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

        // Se ejecuta cada vez que se escribe en el buscador: vuelve a filtrar la lista de servicios.
        private void textBoxBuscarServicio_TextChanged(object sender, EventArgs e)
        {
            FiltrarServicios();
        }

        // Se ejecuta al tildar o destildar un servicio: actualiza la lista de ids tildados y recalcula el monto.
        private void checkedListBoxServicios_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_filtrando)
                return;

            // Se guarda o se quita el id del servicio según quede tildado o destildado.
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

            // ItemCheck se dispara ANTES de que el servicio quede tildado o destildado, así que si se
            // recalculara acá el monto saldría con el estado viejo. BeginInvoke deja el recálculo en la
            // cola del formulario, para cuando el evento ya terminó y el tilde ya se aplicó.
            BeginInvoke(new Action(RecalcularMonto));
        }

        // Vacía el formulario: ítems, horas, cantidad, buscador y servicios tildados, y recalcula el monto.
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
