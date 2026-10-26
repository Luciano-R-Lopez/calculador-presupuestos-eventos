namespace Luciano_Integrador_Evaluativo_Final.View
{
    partial class PlatilloForm
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
            labelNombre = new Label();
            labelPrecio = new Label();
            labelTipo = new Label();
            textBoxNombrePlatillo = new TextBox();
            textBoxPrecioPlatillo = new TextBox();
            comboBoxTipo = new ComboBox();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            dataGridViewPlatillos = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlatillos).BeginInit();
            SuspendLayout();
            //
            // labelNombre
            //
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(20, 42);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(64, 20);
            labelNombre.TabIndex = 0;
            labelNombre.Text = "Nombre";
            //
            // textBoxNombrePlatillo
            //
            textBoxNombrePlatillo.Location = new Point(20, 65);
            textBoxNombrePlatillo.Name = "textBoxNombrePlatillo";
            textBoxNombrePlatillo.Size = new Size(173, 27);
            textBoxNombrePlatillo.TabIndex = 1;
            //
            // labelPrecio
            //
            labelPrecio.AutoSize = true;
            labelPrecio.Location = new Point(20, 116);
            labelPrecio.Name = "labelPrecio";
            labelPrecio.Size = new Size(50, 20);
            labelPrecio.TabIndex = 2;
            labelPrecio.Text = "Precio";
            //
            // textBoxPrecioPlatillo
            //
            textBoxPrecioPlatillo.Location = new Point(20, 139);
            textBoxPrecioPlatillo.Name = "textBoxPrecioPlatillo";
            textBoxPrecioPlatillo.Size = new Size(173, 27);
            textBoxPrecioPlatillo.TabIndex = 3;
            //
            // labelTipo
            //
            labelTipo.AutoSize = true;
            labelTipo.Location = new Point(20, 190);
            labelTipo.Name = "labelTipo";
            labelTipo.Size = new Size(36, 20);
            labelTipo.TabIndex = 4;
            labelTipo.Text = "Tipo";
            //
            // comboBoxTipo
            //
            comboBoxTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTipo.Location = new Point(20, 213);
            comboBoxTipo.Name = "comboBoxTipo";
            comboBoxTipo.Size = new Size(173, 28);
            comboBoxTipo.TabIndex = 5;
            //
            // btnAgregar
            //
            btnAgregar.Location = new Point(20, 270);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            //
            // dataGridViewPlatillos
            //
            dataGridViewPlatillos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPlatillos.Location = new Point(262, 45);
            dataGridViewPlatillos.Name = "dataGridViewPlatillos";
            dataGridViewPlatillos.ReadOnly = true;
            dataGridViewPlatillos.RowHeadersWidth = 51;
            dataGridViewPlatillos.Size = new Size(519, 364);
            dataGridViewPlatillos.TabIndex = 7;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(20, 306);
            btnEditar.Name = "btnEditar";
            btnEditar.Tag = "secundario";
            btnEditar.Size = new Size(94, 29);
            btnEditar.TabIndex = 40;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(124, 270);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Tag = "secundario";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 40;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(124, 306);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Tag = "peligro";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // PlatilloForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnCancelar);
            Controls.Add(btnEditar);
            Controls.Add(labelNombre);
            Controls.Add(textBoxNombrePlatillo);
            Controls.Add(labelPrecio);
            Controls.Add(textBoxPrecioPlatillo);
            Controls.Add(labelTipo);
            Controls.Add(comboBoxTipo);
            Controls.Add(btnAgregar);
            Controls.Add(dataGridViewPlatillos);
            Name = "PlatilloForm";
            Text = "Platillos";
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlatillos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNombre;
        private Label labelPrecio;
        private Label labelTipo;
        private TextBox textBoxNombrePlatillo;
        private TextBox textBoxPrecioPlatillo;
        private ComboBox comboBoxTipo;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnEditar;
        private Button btnAgregar;
        private DataGridView dataGridViewPlatillos;
    }
}
