using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla de clientes: alta, edición y baja.
    public partial class ClienteForm : Form, IPantallaActualizable
    {
        private ClienteController _controller;

        // Id en edición; 0 = modo agregar.
        private int _idEditando;

        public ClienteForm() : this(false)
        {
        }

        // embebido = dentro de una pestaña (sin encabezado); si no, ventana aparte.
        public ClienteForm(bool embebido)
        {
            InitializeComponent();
            Estilo.Aplicar(this, !embebido);
            _controller = new ClienteController();
            RefrescarGrilla();
            SalirDeEdicion();
        }

        // Agrega o guarda cambios según el modo.
        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDTO dto = new ClienteDTO(
                    textBoxNombreCliente.Text,   // nombre
                    textBoxTelefonoCliente.Text,   // telefono
                    textBoxEmailCliente.Text    // email
                );

                if (_idEditando == 0)
                {
                    _controller.RegistrarCliente(dto);
                }
                else
                {
                    _controller.ActualizarCliente(_idEditando, dto);
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
            Cliente? cliente = null;
            if (dataGridViewClientes.CurrentRow != null)
            {
                cliente = dataGridViewClientes.CurrentRow.DataBoundItem as Cliente;
            }

            if (cliente == null)
            {
                MessageBox.Show("Seleccione un cliente de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _idEditando = cliente.Id;
            textBoxNombreCliente.Text = cliente.Nombre;
            textBoxTelefonoCliente.Text = cliente.Telefono;
            textBoxEmailCliente.Text = cliente.Email;

            Agregar.Text = "Guardar";
            btnCancelar.Visible = true;
        }

        // Pide confirmación; el Service lo rechaza si está en uso.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Cliente? seleccionado = null;
            if (dataGridViewClientes.CurrentRow != null)
            {
                seleccionado = dataGridViewClientes.CurrentRow.DataBoundItem as Cliente;
            }

            if (seleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente de la grilla.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show("¿Eliminar cliente " + seleccionado.Nombre + "?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                _controller.EliminarCliente(seleccionado.Id);

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
            Agregar.Text = "Agregar";
            btnCancelar.Visible = false;
        }

        // Al volver a la pestaña se relee la grilla.
        public void ActualizarDatos()
        {
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            // BindingList: con List<T> la grilla fallaba al hacer clic en el encabezado.
            dataGridViewClientes.DataSource = new BindingList<Cliente>(_controller.GetClientes());
        }

        private void LimpiarCampos()
        {
            textBoxNombreCliente.Clear();
            textBoxTelefonoCliente.Clear();
            textBoxEmailCliente.Clear();
        }

    }
}
