using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla de menús: nombre, platillos y aptitudes; alta, edición y baja.
    public partial class MenuForm : Form, IPantallaActualizable
    {
        private MenuController _controller;
        private PlatilloController _platilloController;

        // Ids tildados aparte: el buscador oculta platillos y no se deben perder sus tildes.
        private List<int> _idsPlatillosTildados;

        // Evita registrar tildes mientras se rearma la lista.
        private bool _filtrando;

        // Id en edición; 0 = modo agregar.
        private int _idEditando;

        public MenuForm() : this(false)
        {
        }

        // embebido = dentro de una pestaña (sin encabezado); si no, ventana aparte.
        public MenuForm(bool embebido)
        {
            InitializeComponent();
            Estilo.Aplicar(this, !embebido);
            _controller = new MenuController();
            _platilloController = new PlatilloController();
            _idsPlatillosTildados = new List<int>();

            FiltrarPlatillos();
            checkedListBoxAptitudes.DataSource = Enum.GetValues(typeof(Aptitud));
            RefrescarGrilla();
            SalirDeEdicion();
        }

        // Agrega o guarda cambios según el modo.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> idsPlatillos = new List<int>(_idsPlatillosTildados);

                List<Aptitud> aptitudes = new List<Aptitud>();
                foreach (object item in checkedListBoxAptitudes.CheckedItems)
                    aptitudes.Add((Aptitud)item);

                MenuDTO dto = new MenuDTO(textBoxNombreMenu.Text, idsPlatillos, aptitudes);

                if (_idEditando == 0)
                {
                    _controller.RegistrarMenu(dto);
                }
                else
                {
                    _controller.ActualizarMenu(_idEditando, dto);
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

        // Carga el seleccionado en los campos y pasa a modo edición.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            MenuDTO? menu = null;
            if (dataGridViewMenus.CurrentRow != null)
            {
                menu = dataGridViewMenus.CurrentRow.DataBoundItem as MenuDTO;
            }

            if (menu == null)
            {
                MessageBox.Show("Seleccione un menú de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = menu.Id;
            textBoxNombreMenu.Text = menu.Nombre;

            // Se limpia la búsqueda para que se vean todos.
            _idsPlatillosTildados = new List<int>(menu.IdsPlatillos);
            textBoxBuscarPlatillo.Clear();
            FiltrarPlatillos();

            for (int i = 0; i < checkedListBoxAptitudes.Items.Count; i++)
            {
                Aptitud aptitud = (Aptitud)checkedListBoxAptitudes.Items[i];
                checkedListBoxAptitudes.SetItemChecked(i, menu.Aptitudes.Contains(aptitud));
            }

            btnAgregar.Text = "Guardar";
            btnCancelar.Visible = true;
        }

        // Pide confirmación; el Service lo rechaza si está en uso.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MenuDTO? seleccionado = null;
            if (dataGridViewMenus.CurrentRow != null)
            {
                seleccionado = dataGridViewMenus.CurrentRow.DataBoundItem as MenuDTO;
            }

            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un menú de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar menú " + seleccionado.Nombre + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                _controller.EliminarMenu(seleccionado.Id);

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            SalirDeEdicion();
        }

        private void SalirDeEdicion()
        {
            _idEditando = 0;
            btnAgregar.Text = "Agregar";
            btnCancelar.Visible = false;
        }

        // Muestra los platillos que coinciden con la búsqueda, conservando los tildados.
        private void FiltrarPlatillos()
        {
            _filtrando = true;

            string texto = textBoxBuscarPlatillo.Text.Trim().ToLower();

            checkedListBoxPlatillos.Items.Clear();
            foreach (PlatilloDTO platillo in _platilloController.GetPlatillos())
            {
                if (texto == "" || platillo.Nombre.ToLower().Contains(texto))
                {
                    checkedListBoxPlatillos.Items.Add(platillo, _idsPlatillosTildados.Contains(platillo.Id));
                }
            }

            _filtrando = false;
        }

        private void textBoxBuscarPlatillo_TextChanged(object sender, EventArgs e)
        {
            FiltrarPlatillos();
        }

        private void checkedListBoxPlatillos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_filtrando)
                return;

            PlatilloDTO platillo = (PlatilloDTO)checkedListBoxPlatillos.Items[e.Index];

            if (e.NewValue == CheckState.Checked)
            {
                if (!_idsPlatillosTildados.Contains(platillo.Id))
                {
                    _idsPlatillosTildados.Add(platillo.Id);
                }
            }
            else
            {
                _idsPlatillosTildados.Remove(platillo.Id);
            }
        }

        // Abre el alta de platillos; los nuevos quedan tildados.
        private void btnNuevoPlatillo_Click(object sender, EventArgs e)
        {
            int cantidad = _platilloController.GetPlatillos().Count;
            new PlatilloForm().ShowDialog();

            List<PlatilloDTO> platillos = _platilloController.GetPlatillos();
            for (int i = cantidad; i < platillos.Count; i++)
            {
                _idsPlatillosTildados.Add(platillos[i].Id);
            }

            FiltrarPlatillos();
        }

        // Al volver a la pestaña se releen platillos y grilla, sin perder lo cargado.
        public void ActualizarDatos()
        {
            FiltrarPlatillos();
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            dataGridViewMenus.DataSource = new BindingList<MenuDTO>(_controller.GetMenus());
        }

        private void LimpiarCampos()
        {
            textBoxNombreMenu.Clear();
            textBoxBuscarPlatillo.Clear();
            _idsPlatillosTildados.Clear();
            FiltrarPlatillos();
            for (int i = 0; i < checkedListBoxAptitudes.Items.Count; i++)
            {
                checkedListBoxAptitudes.SetItemChecked(i, false);
            }
        }
    }
}
