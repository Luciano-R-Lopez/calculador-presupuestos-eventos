using Menu = Luciano_Integrador_Evaluativo_Final.Model.Menu;
using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    public partial class MenuForm : Form
    {
        private MenuController _controller;
        private PlatilloController _platilloController;

        // Ids de los platillos tildados. Se guardan aparte porque, al buscar, la lista solo muestra
        // los platillos que coinciden y los tildados que quedan ocultos no se deben perder.
        private List<int> _idsPlatillosTildados;

        // Evita registrar cambios de tilde mientras se vuelve a armar la lista.
        private bool _filtrando;

        // Id del menú que se está editando. En 0 el formulario está en modo "agregar".
        private int _idEditando;

        public MenuForm()
        {
            InitializeComponent();
            Estilo.Aplicar(this);
            _controller = new MenuController();
            _platilloController = new PlatilloController();
            _idsPlatillosTildados = new List<int>();

            FiltrarPlatillos();
            checkedListBoxAptitudes.DataSource = Enum.GetValues(typeof(Aptitud));
            RefrescarGrilla();
            SalirDeEdicion();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> idsPlatillos = new List<int>(_idsPlatillosTildados);

                List<Aptitud> aptitudes = new List<Aptitud>();
                foreach (object item in checkedListBoxAptitudes.CheckedItems)
                    aptitudes.Add((Aptitud)item);

                MenuDTO dto = new MenuDTO(textBoxNombreMenu.Text, idsPlatillos, aptitudes);

                // El mismo botón agrega o guarda cambios según el modo en que esté el formulario.
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

        // Carga en los campos el menú seleccionado en la grilla y pasa a modo edición.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Menu? menu = null;
            if (dataGridViewMenus.CurrentRow != null)
            {
                menu = dataGridViewMenus.CurrentRow.DataBoundItem as Menu;
            }

            if (menu == null)
            {
                MessageBox.Show("Seleccione un menú de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = menu.Id;
            textBoxNombreMenu.Text = menu.Nombre;

            // Se tildan los platillos del menú (la búsqueda se limpia para que se vean todos).
            _idsPlatillosTildados.Clear();
            foreach (Platillo platillo in menu.Platillos)
            {
                _idsPlatillosTildados.Add(platillo.Id);
            }
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

        // Elimina el menú seleccionado en la grilla, previa confirmación. Si está en uso, el Service
        // lo rechaza con un mensaje y no se borra.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Menu? seleccionado = null;
            if (dataGridViewMenus.CurrentRow != null)
            {
                seleccionado = dataGridViewMenus.CurrentRow.DataBoundItem as Menu;
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

        // Vuelve a armar la lista mostrando solo los platillos cuyo nombre contiene el texto buscado.
        // Cada platillo aparece tildado si su id está entre los tildados.
        private void FiltrarPlatillos()
        {
            _filtrando = true;

            string texto = textBoxBuscarPlatillo.Text.Trim().ToLower();

            checkedListBoxPlatillos.Items.Clear();
            foreach (Platillo platillo in _platilloController.GetPlatillos())
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

        // Guarda o quita el id del platillo según quede tildado o destildado.
        private void checkedListBoxPlatillos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_filtrando)
                return;

            Platillo platillo = (Platillo)checkedListBoxPlatillos.Items[e.Index];

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

        // Abre la pestaña de alta de platillos y recarga la lista conservando lo que ya estaba tildado.
        private void btnNuevoPlatillo_Click(object sender, EventArgs e)
        {
            int cantidad = _platilloController.GetPlatillos().Count;
            new PlatilloForm().ShowDialog();

            // Los platillos recién agregados quedan tildados.
            List<Platillo> platillos = _platilloController.GetPlatillos();
            for (int i = cantidad; i < platillos.Count; i++)
            {
                _idsPlatillosTildados.Add(platillos[i].Id);
            }

            FiltrarPlatillos();
        }

        private void RefrescarGrilla()
        {
            dataGridViewMenus.DataSource = new BindingList<Menu>(_controller.GetMenus());
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
