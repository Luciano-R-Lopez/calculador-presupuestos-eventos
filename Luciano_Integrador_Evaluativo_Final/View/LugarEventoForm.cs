using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla de lugares: alta, edición y baja.
    public partial class LugarEventoForm : Form, IPantallaActualizable
    {
        private LugarEventoController _controller;

        // Id en edición; 0 = modo agregar.
        private int _idEditando;

        public LugarEventoForm() : this(false)
        {
        }

        // embebido = dentro de una pestaña (sin encabezado); si no, ventana aparte.
        public LugarEventoForm(bool embebido)
        {
            InitializeComponent();
            Estilo.Aplicar(this, !embebido);
            _controller = new LugarEventoController();
            RefrescarGrilla();
            SalirDeEdicion();
        }

        // Agrega o guarda cambios según el modo.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal precio;
                if (!decimal.TryParse(textBoxPrecioLugar.Text, out precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LugarEventoDTO dto = new LugarEventoDTO(
                    textBoxNombreLugar.Text,
                    precio
                );

                if (_idEditando == 0)
                {
                    _controller.RegistrarLugarEvento(dto);
                }
                else
                {
                    _controller.ActualizarLugarEvento(_idEditando, dto);
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
            LugarEventoDTO? lugar = null;
            if (dataGridViewLugares.CurrentRow != null)
            {
                lugar = dataGridViewLugares.CurrentRow.DataBoundItem as LugarEventoDTO;
            }

            if (lugar == null)
            {
                MessageBox.Show("Seleccione un lugar de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = lugar.Id;
            textBoxNombreLugar.Text = lugar.Nombre;
            textBoxPrecioLugar.Text = lugar.Precio.ToString();

            btnAgregar.Text = "Guardar";
            btnCancelar.Visible = true;
        }

        // Pide confirmación; el Service lo rechaza si está en uso.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            LugarEventoDTO? seleccionado = null;
            if (dataGridViewLugares.CurrentRow != null)
            {
                seleccionado = dataGridViewLugares.CurrentRow.DataBoundItem as LugarEventoDTO;
            }

            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un lugar de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar lugar " + seleccionado.Nombre + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                _controller.EliminarLugarEvento(seleccionado.Id);

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

        // Al volver a la pestaña se relee la grilla.
        public void ActualizarDatos()
        {
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            dataGridViewLugares.DataSource =
                new BindingList<LugarEventoDTO>(_controller.GetLugaresEvento());
        }

        private void LimpiarCampos()
        {
            textBoxNombreLugar.Clear();
            textBoxPrecioLugar.Clear();
        }
    }
}