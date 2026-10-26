using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    public partial class PlatilloForm : Form
    {
        private PlatilloController _controller;

        // Id del platillo que se está editando. En 0 el formulario está en modo "agregar".
        private int _idEditando;

        public PlatilloForm()
        {
            InitializeComponent();
            Estilo.Aplicar(this);
            _controller = new PlatilloController();
            comboBoxTipo.DataSource = Enum.GetValues(typeof(TipoPlatillo));
            RefrescarGrilla();
            SalirDeEdicion();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal precio;
                if (!decimal.TryParse(textBoxPrecioPlatillo.Text, out precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PlatilloDTO dto = new PlatilloDTO(
                    textBoxNombrePlatillo.Text,
                    precio,
                    (TipoPlatillo)comboBoxTipo.SelectedItem
                );

                // El mismo botón agrega o guarda cambios según el modo en que esté el formulario.
                if (_idEditando == 0)
                {
                    _controller.RegistrarPlatillo(dto);
                }
                else
                {
                    _controller.ActualizarPlatillo(_idEditando, dto);
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

        // Carga en los campos el platillo seleccionado en la grilla y pasa a modo edición.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Platillo? platillo = null;
            if (dataGridViewPlatillos.CurrentRow != null)
            {
                platillo = dataGridViewPlatillos.CurrentRow.DataBoundItem as Platillo;
            }

            if (platillo == null)
            {
                MessageBox.Show("Seleccione un platillo de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = platillo.Id;
            textBoxNombrePlatillo.Text = platillo.Nombre;
            textBoxPrecioPlatillo.Text = platillo.Precio.ToString();
            comboBoxTipo.SelectedItem = platillo.Tipo;

            btnAgregar.Text = "Guardar";
            btnCancelar.Visible = true;
        }

        // Elimina el platillo seleccionado en la grilla, previa confirmación. Si está en uso, el Service
        // lo rechaza con un mensaje y no se borra.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Platillo? seleccionado = null;
            if (dataGridViewPlatillos.CurrentRow != null)
            {
                seleccionado = dataGridViewPlatillos.CurrentRow.DataBoundItem as Platillo;
            }

            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un platillo de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar platillo " + seleccionado.Nombre + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                _controller.EliminarPlatillo(seleccionado.Id);

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

        private void RefrescarGrilla()
        {
            dataGridViewPlatillos.DataSource =
                new BindingList<Platillo>(_controller.GetPlatillos());
        }

        private void LimpiarCampos()
        {
            textBoxNombrePlatillo.Clear();
            textBoxPrecioPlatillo.Clear();
        }
    }
}
