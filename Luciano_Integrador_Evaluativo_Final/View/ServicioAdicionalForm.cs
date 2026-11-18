using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla de servicios adicionales: alta, edición y baja.
    public partial class ServicioAdicionalForm : Form, IPantallaActualizable
    {
        private ServicioAdicionalController _controller;

        // Id en edición; 0 = modo agregar.
        private int _idEditando;

        public ServicioAdicionalForm() : this(false)
        {
        }

        // embebido = dentro de una pestaña (sin encabezado); si no, ventana aparte.
        public ServicioAdicionalForm(bool embebido)
        {
            InitializeComponent();
            Estilo.Aplicar(this, !embebido);
            _controller = new ServicioAdicionalController();
            RefrescarGrilla();
            SalirDeEdicion();
        }

        // Agrega o guarda cambios según el modo.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal precio;
                if (!decimal.TryParse(textBoxPrecioServicio.Text, out precio))
                {
                    MessageBox.Show("El precio debe ser un número válido.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ServicioAdicionalDTO dto = new ServicioAdicionalDTO(
                    textBoxNombreServicio.Text,
                    precio
                );

                if (_idEditando == 0)
                {
                    _controller.RegistrarServicioAdicional(dto);
                }
                else
                {
                    _controller.ActualizarServicioAdicional(_idEditando, dto);
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
            ServicioAdicionalDTO? servicio = null;
            if (dataGridViewServiciosAdicionales.CurrentRow != null)
            {
                servicio = dataGridViewServiciosAdicionales.CurrentRow.DataBoundItem as ServicioAdicionalDTO;
            }

            if (servicio == null)
            {
                MessageBox.Show("Seleccione un servicio de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = servicio.Id;
            textBoxNombreServicio.Text = servicio.Nombre;
            textBoxPrecioServicio.Text = servicio.Precio.ToString();

            btnAgregar.Text = "Guardar";
            btnCancelar.Visible = true;
        }

        // Pide confirmación; el Service lo rechaza si está en uso.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ServicioAdicionalDTO? seleccionado = null;
            if (dataGridViewServiciosAdicionales.CurrentRow != null)
            {
                seleccionado = dataGridViewServiciosAdicionales.CurrentRow.DataBoundItem as ServicioAdicionalDTO;
            }

            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un servicio de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar servicio " + seleccionado.Nombre + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                _controller.EliminarServicioAdicional(seleccionado.Id);

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
            dataGridViewServiciosAdicionales.DataSource =
                new BindingList<ServicioAdicionalDTO>(_controller.GetServiciosAdicionales());
        }

        private void LimpiarCampos()
        {
            textBoxNombreServicio.Clear();
            textBoxPrecioServicio.Clear();
        }
    }
}