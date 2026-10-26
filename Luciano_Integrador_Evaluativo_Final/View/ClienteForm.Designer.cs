namespace Luciano_Integrador_Evaluativo_Final.View
{
    partial class ClienteForm
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
            textBoxNombreCliente = new TextBox();
            textBoxTelefonoCliente = new TextBox();
            textBoxEmailCliente = new TextBox();
            dataGridViewClientes = new DataGridView();
            Agregar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            labelNombre = new Label();
            labelTelefono = new Label();
            labelEmail = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).BeginInit();
            SuspendLayout();
            // 
            // textBoxNombreCliente
            // 
            textBoxNombreCliente.Location = new Point(27, 32);
            textBoxNombreCliente.Name = "textBoxNombreCliente";
            textBoxNombreCliente.Size = new Size(173, 27);
            textBoxNombreCliente.TabIndex = 0;
            // 
            // textBoxTelefonoCliente
            // 
            textBoxTelefonoCliente.Location = new Point(27, 106);
            textBoxTelefonoCliente.Name = "textBoxTelefonoCliente";
            textBoxTelefonoCliente.Size = new Size(173, 27);
            textBoxTelefonoCliente.TabIndex = 1;
            // 
            // textBoxEmailCliente
            // 
            textBoxEmailCliente.Location = new Point(27, 185);
            textBoxEmailCliente.Name = "textBoxEmailCliente";
            textBoxEmailCliente.Size = new Size(173, 27);
            textBoxEmailCliente.TabIndex = 2;
            // 
            // dataGridViewClientes
            // 
            dataGridViewClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewClientes.Location = new Point(269, 12);
            dataGridViewClientes.Name = "dataGridViewClientes";
            dataGridViewClientes.ReadOnly = true;
            dataGridViewClientes.RowHeadersWidth = 51;
            dataGridViewClientes.Size = new Size(519, 364);
            dataGridViewClientes.TabIndex = 3;
            // 
            // Agregar
            // 
            Agregar.Location = new Point(27, 231);
            Agregar.Name = "Agregar";
            Agregar.Size = new Size(94, 29);
            Agregar.TabIndex = 4;
            Agregar.Text = "Agregar";
            Agregar.UseVisualStyleBackColor = true;
            Agregar.Click += buttonAgregar_Click;
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(27, 9);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(64, 20);
            labelNombre.TabIndex = 5;
            labelNombre.Text = "Nombre";
            // 
            // labelTelefono
            // 
            labelTelefono.AutoSize = true;
            labelTelefono.Location = new Point(27, 83);
            labelTelefono.Name = "labelTelefono";
            labelTelefono.Size = new Size(67, 20);
            labelTelefono.TabIndex = 6;
            labelTelefono.Text = "Teléfono";
            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(27, 162);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(46, 20);
            labelEmail.TabIndex = 7;
            labelEmail.Text = "Email";
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(27, 267);
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
            btnCancelar.Location = new Point(131, 231);
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
            btnEliminar.Location = new Point(131, 267);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Tag = "peligro";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // ClienteForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnCancelar);
            Controls.Add(btnEditar);
            Controls.Add(labelEmail);
            Controls.Add(labelTelefono);
            Controls.Add(labelNombre);
            Controls.Add(Agregar);
            Controls.Add(dataGridViewClientes);
            Controls.Add(textBoxEmailCliente);
            Controls.Add(textBoxTelefonoCliente);
            Controls.Add(textBoxNombreCliente);
            Name = "ClienteForm";
            Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)dataGridViewClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxNombreCliente;
        private TextBox textBoxTelefonoCliente;
        private TextBox textBoxEmailCliente;
        private DataGridView dataGridViewClientes;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnEditar;
        private Button Agregar;
        private Label labelNombre;
        private Label labelTelefono;
        private Label labelEmail;
    }
}