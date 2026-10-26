namespace Luciano_Integrador_Evaluativo_Final.View
{
    partial class LugarEventoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelPrecio = new Label();
            labelNombre = new Label();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            dataGridViewLugares = new DataGridView();
            textBoxPrecioLugar = new TextBox();
            textBoxNombreLugar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLugares).BeginInit();
            SuspendLayout();
            // 
            // labelPrecio
            // 
            labelPrecio.AutoSize = true;
            labelPrecio.Location = new Point(20, 116);
            labelPrecio.Name = "labelPrecio";
            labelPrecio.Size = new Size(50, 20);
            labelPrecio.TabIndex = 12;
            labelPrecio.Text = "Precio";
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(20, 42);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(64, 20);
            labelNombre.TabIndex = 11;
            labelNombre.Text = "Nombre";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(20, 203);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 10;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // dataGridViewLugares
            // 
            dataGridViewLugares.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLugares.Location = new Point(262, 45);
            dataGridViewLugares.Name = "dataGridViewLugares";
            dataGridViewLugares.ReadOnly = true;
            dataGridViewLugares.RowHeadersWidth = 51;
            dataGridViewLugares.Size = new Size(519, 364);
            dataGridViewLugares.TabIndex = 9;
            // 
            // textBoxPrecioLugar
            // 
            textBoxPrecioLugar.Location = new Point(20, 139);
            textBoxPrecioLugar.Name = "textBoxPrecioLugar";
            textBoxPrecioLugar.Size = new Size(173, 27);
            textBoxPrecioLugar.TabIndex = 8;
            // 
            // textBoxNombreLugar
            // 
            textBoxNombreLugar.Location = new Point(20, 65);
            textBoxNombreLugar.Name = "textBoxNombreLugar";
            textBoxNombreLugar.Size = new Size(173, 27);
            textBoxNombreLugar.TabIndex = 7;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(20, 239);
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
            btnCancelar.Location = new Point(124, 203);
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
            btnEliminar.Location = new Point(124, 239);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Tag = "peligro";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // LugarEventoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnCancelar);
            Controls.Add(btnEditar);
            Controls.Add(labelPrecio);
            Controls.Add(labelNombre);
            Controls.Add(btnAgregar);
            Controls.Add(dataGridViewLugares);
            Controls.Add(textBoxPrecioLugar);
            Controls.Add(textBoxNombreLugar);
            Name = "LugarEventoForm";
            Text = "Lugares de Evento";
            ((System.ComponentModel.ISupportInitialize)dataGridViewLugares).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelPrecio;
        private Label labelNombre;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnEditar;
        private Button btnAgregar;
        private DataGridView dataGridViewLugares;
        private TextBox textBoxPrecioLugar;
        private TextBox textBoxNombreLugar;
    }
}