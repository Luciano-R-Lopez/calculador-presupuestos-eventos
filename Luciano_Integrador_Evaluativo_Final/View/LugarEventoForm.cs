using Luciano_Integrador_Evaluativo_Final.Controller;
using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantalla de lugares de evento: formulario para cargar, editar y eliminar, con la grilla de los ya cargados.
    // Solo captura datos y muestra resultados; las reglas están en el Service.
    public partial class LugarEventoForm : Form
    {
        private LugarEventoController _controller;

        // Id del lugar que se está editando. En 0 el formulario está en modo "agregar".
        private int _idEditando;

        // Arma la pantalla: aplica el estilo, crea el Controller, carga la grilla y deja el formulario en modo agregar.
        public LugarEventoForm()
        {
            InitializeComponent();
            Estilo.Aplicar(this);
            _controller = new LugarEventoController();
            RefrescarGrilla();
            SalirDeEdicion();
        }

        // Botón Agregar/Guardar: arma el DTO con lo escrito y lo envía al Controller, y refresca la grilla.
        // En modo edición guarda los cambios del elemento; si no, agrega uno nuevo. Los errores se muestran en un cuadro.
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Convierte el texto a decimal. Si no es un número válido, avisa y corta antes de continuar.
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

                // El mismo botón agrega o guarda cambios según el modo en que esté el formulario.
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

        // Carga en los campos el lugar seleccionado en la grilla y pasa a modo edición.
        private void btnEditar_Click(object sender, EventArgs e)
        {
            LugarEvento? lugar = null;
            if (dataGridViewLugares.CurrentRow != null)
            {
                lugar = dataGridViewLugares.CurrentRow.DataBoundItem as LugarEvento;
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

        // Elimina el lugar seleccionado en la grilla, previa confirmación. Si está en uso, el Service
        // lo rechaza con un mensaje y no se borra.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            LugarEvento? seleccionado = null;
            if (dataGridViewLugares.CurrentRow != null)
            {
                seleccionado = dataGridViewLugares.CurrentRow.DataBoundItem as LugarEvento;
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

        // Botón Cancelar: descarta lo escrito y vuelve al modo agregar.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            SalirDeEdicion();
        }

        // Vuelve al modo agregar: sin elemento en edición, botón con el texto Agregar y sin botón Cancelar.
        private void SalirDeEdicion()
        {
            _idEditando = 0;
            btnAgregar.Text = "Agregar";
            btnCancelar.Visible = false;
        }

        // Vuelve a cargar la grilla con la lista actual que devuelve el Controller.
        private void RefrescarGrilla()
        {
            dataGridViewLugares.DataSource =
                new BindingList<LugarEvento>(_controller.GetLugaresEvento());
        }

        // Vacía los campos del formulario.
        private void LimpiarCampos()
        {
            textBoxNombreLugar.Clear();
            textBoxPrecioLugar.Clear();
        }
    }
}