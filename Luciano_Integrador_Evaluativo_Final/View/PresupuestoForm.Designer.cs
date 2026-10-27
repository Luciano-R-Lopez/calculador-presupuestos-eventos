namespace Luciano_Integrador_Evaluativo_Final.View
{
    partial class PresupuestoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelFecha = new Label();
            labelHoras = new Label();
            labelCliente = new Label();
            labelLugar = new Label();
            labelServicios = new Label();
            labelMenu = new Label();
            labelMonto = new Label();
            dateTimePickerEvento = new DateTimePicker();
            numericUpDownHoras = new NumericUpDown();
            comboBoxCliente = new ComboBox();
            comboBoxLugar = new ComboBox();
            checkedListBoxServicios = new CheckedListBox();
            textBoxBuscarServicio = new TextBox();
            comboBoxMenu = new ComboBox();
            numericUpDownCantidad = new NumericUpDown();
            checkBoxAdulto = new CheckBox();
            btnAgregarItem = new Button();
            btnEliminar = new Button();
            btnCancelarEdicion = new Button();
            btnEditar = new Button();
            btnQuitarItem = new Button();
            listBoxItems = new ListBox();
            btnRegistrar = new Button();
            dataGridViewPresupuestos = new DataGridView();
            comboBoxEstado = new ComboBox();
            btnCambiarEstado = new Button();
            btnClonar = new Button();
            btnGenerarPdf = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHoras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPresupuestos).BeginInit();
            SuspendLayout();
            //
            // labelFecha
            //
            labelFecha.AutoSize = true;
            labelFecha.Location = new Point(20, 10);
            labelFecha.Name = "labelFecha";
            labelFecha.Size = new Size(107, 20);
            labelFecha.TabIndex = 0;
            labelFecha.Text = "Fecha del evento";
            //
            // dateTimePickerEvento
            //
            dateTimePickerEvento.Location = new Point(20, 33);
            dateTimePickerEvento.Name = "dateTimePickerEvento";
            dateTimePickerEvento.Size = new Size(300, 27);
            dateTimePickerEvento.TabIndex = 1;
            //
            // labelHoras
            //
            labelHoras.AutoSize = true;
            labelHoras.Location = new Point(20, 70);
            labelHoras.Name = "labelHoras";
            labelHoras.Size = new Size(125, 20);
            labelHoras.TabIndex = 2;
            labelHoras.Text = "Duración (horas)";
            //
            // numericUpDownHoras
            //
            numericUpDownHoras.Location = new Point(20, 93);
            numericUpDownHoras.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numericUpDownHoras.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownHoras.Name = "numericUpDownHoras";
            numericUpDownHoras.Size = new Size(100, 27);
            numericUpDownHoras.TabIndex = 3;
            numericUpDownHoras.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // labelCliente
            //
            labelCliente.AutoSize = true;
            labelCliente.Location = new Point(20, 130);
            labelCliente.Name = "labelCliente";
            labelCliente.Size = new Size(56, 20);
            labelCliente.TabIndex = 4;
            labelCliente.Text = "Cliente";
            //
            // comboBoxCliente
            //
            comboBoxCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCliente.Location = new Point(20, 153);
            comboBoxCliente.Name = "comboBoxCliente";
            comboBoxCliente.Size = new Size(300, 28);
            comboBoxCliente.TabIndex = 5;
            comboBoxCliente.SelectedIndexChanged += comboBoxCliente_SelectedIndexChanged;
            //
            // labelLugar
            //
            labelLugar.AutoSize = true;
            labelLugar.Location = new Point(20, 190);
            labelLugar.Name = "labelLugar";
            labelLugar.Size = new Size(46, 20);
            labelLugar.TabIndex = 6;
            labelLugar.Text = "Lugar";
            //
            // comboBoxLugar
            //
            comboBoxLugar.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLugar.Location = new Point(20, 213);
            comboBoxLugar.Name = "comboBoxLugar";
            comboBoxLugar.Size = new Size(300, 28);
            comboBoxLugar.TabIndex = 7;
            comboBoxLugar.SelectedIndexChanged += comboBoxLugar_SelectedIndexChanged;
            //
            // labelServicios
            //
            labelServicios.AutoSize = true;
            labelServicios.Location = new Point(20, 250);
            labelServicios.Name = "labelServicios";
            labelServicios.Size = new Size(127, 20);
            labelServicios.TabIndex = 8;
            labelServicios.Text = "Servicios adicionales";
            //
            // checkedListBoxServicios
            //
            checkedListBoxServicios.CheckOnClick = true;
            checkedListBoxServicios.FormattingEnabled = true;
            checkedListBoxServicios.Location = new Point(20, 273);
            checkedListBoxServicios.Name = "checkedListBoxServicios";
            checkedListBoxServicios.Size = new Size(300, 92);
            checkedListBoxServicios.TabIndex = 9;
            checkedListBoxServicios.ItemCheck += checkedListBoxServicios_ItemCheck;
            //
            // labelMenu
            //
            labelMenu.AutoSize = true;
            labelMenu.Location = new Point(20, 375);
            labelMenu.Name = "labelMenu";
            labelMenu.Size = new Size(151, 20);
            labelMenu.TabIndex = 10;
            labelMenu.Text = "Menú y cantidad de invitados";
            //
            // comboBoxMenu
            //
            comboBoxMenu.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMenu.Location = new Point(20, 398);
            comboBoxMenu.Name = "comboBoxMenu";
            comboBoxMenu.Size = new Size(220, 28);
            comboBoxMenu.TabIndex = 11;
            comboBoxMenu.SelectedIndexChanged += comboBoxMenu_SelectedIndexChanged;
            //
            // numericUpDownCantidad
            //
            numericUpDownCantidad.Location = new Point(250, 399);
            numericUpDownCantidad.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            numericUpDownCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCantidad.Name = "numericUpDownCantidad";
            numericUpDownCantidad.Size = new Size(70, 27);
            numericUpDownCantidad.TabIndex = 12;
            numericUpDownCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // checkBoxAdulto
            //
            checkBoxAdulto.AutoSize = true;
            checkBoxAdulto.Checked = true;
            checkBoxAdulto.CheckState = CheckState.Checked;
            checkBoxAdulto.Location = new Point(20, 432);
            checkBoxAdulto.Name = "checkBoxAdulto";
            checkBoxAdulto.Size = new Size(120, 24);
            checkBoxAdulto.TabIndex = 13;
            checkBoxAdulto.Text = "Para adultos";
            checkBoxAdulto.UseVisualStyleBackColor = true;
            //
            // btnAgregarItem
            //
            btnAgregarItem.Location = new Point(20, 462);
            btnAgregarItem.Name = "btnAgregarItem";
            btnAgregarItem.Size = new Size(130, 29);
            btnAgregarItem.TabIndex = 14;
            btnAgregarItem.Text = "Agregar ítem";
            btnAgregarItem.UseVisualStyleBackColor = true;
            btnAgregarItem.Click += btnAgregarItem_Click;
            //
            // btnQuitarItem
            //
            btnQuitarItem.Location = new Point(160, 462);
            btnQuitarItem.Name = "btnQuitarItem";
            btnQuitarItem.Tag = "secundario";
            btnQuitarItem.Size = new Size(130, 29);
            btnQuitarItem.TabIndex = 15;
            btnQuitarItem.Text = "Quitar ítem";
            btnQuitarItem.UseVisualStyleBackColor = true;
            btnQuitarItem.Click += btnQuitarItem_Click;
            //
            // listBoxItems
            //
            listBoxItems.FormattingEnabled = true;
            listBoxItems.Location = new Point(20, 500);
            listBoxItems.Name = "listBoxItems";
            listBoxItems.Size = new Size(300, 84);
            listBoxItems.TabIndex = 16;
            //
            // labelMonto
            //
            labelMonto.AutoSize = true;
            labelMonto.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelMonto.Location = new Point(20, 600);
            labelMonto.Name = "labelMonto";
            labelMonto.Size = new Size(140, 28);
            labelMonto.TabIndex = 17;
            labelMonto.Text = "Monto total: -";
            //
            // btnRegistrar
            //
            btnRegistrar.Location = new Point(20, 645);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(150, 32);
            btnRegistrar.TabIndex = 18;
            btnRegistrar.Text = "Registrar presupuesto";
            btnRegistrar.UseVisualStyleBackColor = true;
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // dataGridViewPresupuestos
            //
            dataGridViewPresupuestos.AllowUserToAddRows = false;
            dataGridViewPresupuestos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPresupuestos.Location = new Point(350, 33);
            dataGridViewPresupuestos.MultiSelect = false;
            dataGridViewPresupuestos.Name = "dataGridViewPresupuestos";
            dataGridViewPresupuestos.ReadOnly = true;
            dataGridViewPresupuestos.RowHeadersWidth = 51;
            dataGridViewPresupuestos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPresupuestos.Size = new Size(730, 550);
            dataGridViewPresupuestos.TabIndex = 19;
            //
            // comboBoxEstado
            //
            comboBoxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstado.Location = new Point(350, 600);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(160, 28);
            comboBoxEstado.TabIndex = 20;
            //
            // btnCambiarEstado
            //
            btnCambiarEstado.Location = new Point(520, 599);
            btnCambiarEstado.Name = "btnCambiarEstado";
            btnCambiarEstado.Size = new Size(145, 30);
            btnCambiarEstado.TabIndex = 21;
            btnCambiarEstado.Text = "Cambiar estado";
            btnCambiarEstado.UseVisualStyleBackColor = true;
            btnCambiarEstado.Click += btnCambiarEstado_Click;
            //
            // btnClonar
            //
            btnClonar.Location = new Point(675, 599);
            btnClonar.Name = "btnClonar";
            btnClonar.Tag = "secundario";
            btnClonar.Size = new Size(85, 30);
            btnClonar.TabIndex = 22;
            btnClonar.Text = "Clonar";
            btnClonar.UseVisualStyleBackColor = true;
            btnClonar.Click += btnClonar_Click;
            // 
            // btnGenerarPdf
            // 
            btnGenerarPdf.Location = new Point(865, 599);
            btnGenerarPdf.Name = "btnGenerarPdf";
            btnGenerarPdf.Size = new Size(125, 30);
            btnGenerarPdf.TabIndex = 23;
            btnGenerarPdf.Text = "Generar PDF";
            btnGenerarPdf.UseVisualStyleBackColor = true;
            btnGenerarPdf.Click += btnGenerarPdf_Click;
            // 
            // textBoxBuscarServicio
            // 
            textBoxBuscarServicio.Location = new Point(190, 245);
            textBoxBuscarServicio.Name = "textBoxBuscarServicio";
            textBoxBuscarServicio.PlaceholderText = "Buscar servicio...";
            textBoxBuscarServicio.Size = new Size(130, 27);
            textBoxBuscarServicio.TabIndex = 24;
            textBoxBuscarServicio.TextChanged += textBoxBuscarServicio_TextChanged;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(770, 599);
            btnEditar.Name = "btnEditar";
            btnEditar.Tag = "secundario";
            btnEditar.Size = new Size(85, 30);
            btnEditar.TabIndex = 40;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnCancelarEdicion
            // 
            btnCancelarEdicion.Location = new Point(180, 645);
            btnCancelarEdicion.Name = "btnCancelarEdicion";
            btnCancelarEdicion.Tag = "secundario";
            btnCancelarEdicion.Size = new Size(140, 32);
            btnCancelarEdicion.TabIndex = 40;
            btnCancelarEdicion.Text = "Cancelar edición";
            btnCancelarEdicion.UseVisualStyleBackColor = true;
            btnCancelarEdicion.Click += btnCancelarEdicion_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1000, 599);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Tag = "peligro";
            btnEliminar.Size = new Size(90, 30);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // PresupuestoForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 700);
            Controls.Add(btnEliminar);
            Controls.Add(btnCancelarEdicion);
            Controls.Add(btnEditar);
            Controls.Add(labelFecha);
            Controls.Add(dateTimePickerEvento);
            Controls.Add(labelHoras);
            Controls.Add(numericUpDownHoras);
            Controls.Add(labelCliente);
            Controls.Add(comboBoxCliente);
            Controls.Add(labelLugar);
            Controls.Add(comboBoxLugar);
            Controls.Add(labelServicios);
            Controls.Add(textBoxBuscarServicio);
            Controls.Add(checkedListBoxServicios);
            Controls.Add(labelMenu);
            Controls.Add(comboBoxMenu);
            Controls.Add(numericUpDownCantidad);
            Controls.Add(checkBoxAdulto);
            Controls.Add(btnAgregarItem);
            Controls.Add(btnQuitarItem);
            Controls.Add(listBoxItems);
            Controls.Add(labelMonto);
            Controls.Add(btnRegistrar);
            Controls.Add(dataGridViewPresupuestos);
            Controls.Add(comboBoxEstado);
            Controls.Add(btnCambiarEstado);
            Controls.Add(btnClonar);
            Controls.Add(btnGenerarPdf);
            Name = "PresupuestoForm";
            Text = "Presupuestos";
            ((System.ComponentModel.ISupportInitialize)numericUpDownHoras).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPresupuestos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelFecha;
        private Label labelHoras;
        private Label labelCliente;
        private Label labelLugar;
        private Label labelServicios;
        private Label labelMenu;
        private Label labelMonto;
        private DateTimePicker dateTimePickerEvento;
        private NumericUpDown numericUpDownHoras;
        private ComboBox comboBoxCliente;
        private ComboBox comboBoxLugar;
        private CheckedListBox checkedListBoxServicios;
        private TextBox textBoxBuscarServicio;
        private ComboBox comboBoxMenu;
        private NumericUpDown numericUpDownCantidad;
        private CheckBox checkBoxAdulto;
        private Button btnEliminar;
        private Button btnCancelarEdicion;
        private Button btnEditar;
        private Button btnAgregarItem;
        private Button btnQuitarItem;
        private ListBox listBoxItems;
        private Button btnRegistrar;
        private DataGridView dataGridViewPresupuestos;
        private ComboBox comboBoxEstado;
        private Button btnCambiarEstado;
        private Button btnClonar;
        private Button btnGenerarPdf;
    }
}
