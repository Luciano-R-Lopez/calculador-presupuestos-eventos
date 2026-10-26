namespace Luciano_Integrador_Evaluativo_Final.View
{
    partial class MenuForm
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
            labelPlatillos = new Label();
            labelAptitudes = new Label();
            textBoxNombreMenu = new TextBox();
            checkedListBoxPlatillos = new CheckedListBox();
            textBoxBuscarPlatillo = new TextBox();
            checkedListBoxAptitudes = new CheckedListBox();
            btnAgregar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            btnEditar = new Button();
            btnNuevoPlatillo = new Button();
            dataGridViewMenus = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMenus).BeginInit();
            SuspendLayout();
            //
            // labelNombre
            //
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(20, 20);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(64, 20);
            labelNombre.TabIndex = 0;
            labelNombre.Text = "Nombre";
            //
            // textBoxNombreMenu
            //
            textBoxNombreMenu.Location = new Point(20, 43);
            textBoxNombreMenu.Name = "textBoxNombreMenu";
            textBoxNombreMenu.Size = new Size(220, 27);
            textBoxNombreMenu.TabIndex = 1;
            //
            // labelPlatillos
            //
            labelPlatillos.AutoSize = true;
            labelPlatillos.Location = new Point(20, 85);
            labelPlatillos.Name = "labelPlatillos";
            labelPlatillos.Size = new Size(66, 20);
            labelPlatillos.TabIndex = 2;
            labelPlatillos.Text = "Platillos";
            //
            // checkedListBoxPlatillos
            //
            checkedListBoxPlatillos.CheckOnClick = true;
            checkedListBoxPlatillos.FormattingEnabled = true;
            checkedListBoxPlatillos.Location = new Point(20, 140);
            checkedListBoxPlatillos.Name = "checkedListBoxPlatillos";
            checkedListBoxPlatillos.Size = new Size(220, 104);
            checkedListBoxPlatillos.TabIndex = 3;
            checkedListBoxPlatillos.ItemCheck += checkedListBoxPlatillos_ItemCheck;
            //
            // labelAptitudes
            //
            labelAptitudes.AutoSize = true;
            labelAptitudes.Location = new Point(20, 255);
            labelAptitudes.Name = "labelAptitudes";
            labelAptitudes.Size = new Size(72, 20);
            labelAptitudes.TabIndex = 4;
            labelAptitudes.Text = "Aptitudes";
            //
            // checkedListBoxAptitudes
            //
            checkedListBoxAptitudes.CheckOnClick = true;
            checkedListBoxAptitudes.FormattingEnabled = true;
            checkedListBoxAptitudes.Location = new Point(20, 278);
            checkedListBoxAptitudes.Name = "checkedListBoxAptitudes";
            checkedListBoxAptitudes.Size = new Size(220, 70);
            checkedListBoxAptitudes.TabIndex = 5;
            //
            // btnAgregar
            //
            btnAgregar.Location = new Point(20, 365);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(94, 29);
            btnAgregar.TabIndex = 6;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            //
            // dataGridViewMenus
            //
            dataGridViewMenus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMenus.Location = new Point(262, 43);
            dataGridViewMenus.Name = "dataGridViewMenus";
            dataGridViewMenus.ReadOnly = true;
            dataGridViewMenus.RowHeadersWidth = 51;
            dataGridViewMenus.Size = new Size(519, 364);
            dataGridViewMenus.TabIndex = 7;
            // 
            // btnNuevoPlatillo
            // 
            btnNuevoPlatillo.Location = new Point(130, 80);
            btnNuevoPlatillo.Name = "btnNuevoPlatillo";
            btnNuevoPlatillo.Tag = "secundario";
            btnNuevoPlatillo.Size = new Size(110, 26);
            btnNuevoPlatillo.TabIndex = 8;
            btnNuevoPlatillo.Text = "Nuevo...";
            btnNuevoPlatillo.UseVisualStyleBackColor = true;
            btnNuevoPlatillo.Click += btnNuevoPlatillo_Click;
            // 
            // textBoxBuscarPlatillo
            // 
            textBoxBuscarPlatillo.Location = new Point(20, 108);
            textBoxBuscarPlatillo.Name = "textBoxBuscarPlatillo";
            textBoxBuscarPlatillo.PlaceholderText = "Buscar platillo...";
            textBoxBuscarPlatillo.Size = new Size(220, 27);
            textBoxBuscarPlatillo.TabIndex = 9;
            textBoxBuscarPlatillo.TextChanged += textBoxBuscarPlatillo_TextChanged;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(20, 400);
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
            btnCancelar.Location = new Point(124, 365);
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
            btnEliminar.Location = new Point(124, 400);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Tag = "peligro";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 41;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            //
            // MenuForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnCancelar);
            Controls.Add(btnEditar);
            Controls.Add(labelNombre);
            Controls.Add(textBoxNombreMenu);
            Controls.Add(labelPlatillos);
            Controls.Add(btnNuevoPlatillo);
            Controls.Add(textBoxBuscarPlatillo);
            Controls.Add(checkedListBoxPlatillos);
            Controls.Add(labelAptitudes);
            Controls.Add(checkedListBoxAptitudes);
            Controls.Add(btnAgregar);
            Controls.Add(dataGridViewMenus);
            Name = "MenuForm";
            Text = "Menús";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMenus).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNombre;
        private Label labelPlatillos;
        private Label labelAptitudes;
        private TextBox textBoxNombreMenu;
        private CheckedListBox checkedListBoxPlatillos;
        private TextBox textBoxBuscarPlatillo;
        private CheckedListBox checkedListBoxAptitudes;
        private Button btnEliminar;
        private Button btnCancelar;
        private Button btnEditar;
        private Button btnAgregar;
        private Button btnNuevoPlatillo;
        private DataGridView dataGridViewMenus;
    }
}
