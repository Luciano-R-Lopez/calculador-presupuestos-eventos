using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    public partial class ClienteForm : Form
    {
        private ClienteController _controller;

        // Id del cliente que se está editando. En 0 el formulario está en modo "agregar".
        private int _idEditando;

        public ClienteForm()
        {
            InitializeComponent();
            Estilo.Aplicar(this);
            _controller = new ClienteController();
            RefrescarGrilla();
            SalirDeEdicion();
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDTO dto = new ClienteDTO(
                    textBoxNombreCliente.Text,   // nombre
                    textBoxTelefonoCliente.Text,   // telefono
                    textBoxEmailCliente.Text    // email
                );

                // El mismo botón agrega o guarda cambios según el modo en que esté el formulario.
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

        // Carga en los campos el cliente seleccionado en la grilla y pasa a modo edición.
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

        // Elimina el cliente seleccionado en la grilla, previa confirmación. Si está en uso, el Service
        // lo rechaza con un mensaje y no se borra.
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
            Agregar.Text = "Agregar";
            btnCancelar.Visible = false;
        }

        private void RefrescarGrilla()
        {
            /*
             * Envuelve la lista en un BindingList para que el DataGridView maneje correctamente el índice de fila.
             * Con List<T> directo, al origen el grid quedaba con índice -1 y rompía al clickear el encabezado.
            */
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
