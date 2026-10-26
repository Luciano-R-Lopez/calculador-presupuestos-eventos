namespace Luciano_Integrador_Evaluativo_Final
{
    partial class MenuPrincipalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelEncabezado = new Panel();
            labelTitulo = new Label();
            labelSubtitulo = new Label();
            btnClientes = new Button();
            btnServiciosAdicionales = new Button();
            btnLugares = new Button();
            btnPlatillos = new Button();
            btnMenus = new Button();
            btnPresupuestos = new Button();
            panelEncabezado.SuspendLayout();
            SuspendLayout();
            //
            // panelEncabezado
            //
            panelEncabezado.BackColor = Color.FromArgb(31, 58, 95);
            panelEncabezado.Controls.Add(labelSubtitulo);
            panelEncabezado.Controls.Add(labelTitulo);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Location = new Point(0, 0);
            panelEncabezado.Name = "panelEncabezado";
            panelEncabezado.Size = new Size(620, 100);
            panelEncabezado.TabIndex = 0;
            //
            // labelTitulo
            //
            labelTitulo.AutoSize = true;
            labelTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitulo.ForeColor = Color.White;
            labelTitulo.Location = new Point(24, 16);
            labelTitulo.Name = "labelTitulo";
            labelTitulo.Size = new Size(380, 41);
            labelTitulo.TabIndex = 0;
            labelTitulo.Text = "Calculador de Presupuestos";
            //
            // labelSubtitulo
            //
            labelSubtitulo.AutoSize = true;
            labelSubtitulo.ForeColor = Color.FromArgb(200, 212, 230);
            labelSubtitulo.Location = new Point(27, 62);
            labelSubtitulo.Name = "labelSubtitulo";
            labelSubtitulo.Size = new Size(213, 20);
            labelSubtitulo.TabIndex = 1;
            labelSubtitulo.Text = "Gestión de eventos y presupuestos";
            //
            // btnPresupuestos
            //
            btnPresupuestos.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPresupuestos.Location = new Point(30, 125);
            btnPresupuestos.Name = "btnPresupuestos";
            btnPresupuestos.Size = new Size(560, 60);
            btnPresupuestos.TabIndex = 1;
            btnPresupuestos.Text = "Presupuestos";
            btnPresupuestos.UseVisualStyleBackColor = true;
            btnPresupuestos.Click += btnPresupuestos_Click;
            //
            // btnClientes
            //
            btnClientes.Location = new Point(30, 210);
            btnClientes.Name = "btnClientes";
            btnClientes.Tag = "secundario";
            btnClientes.Size = new Size(270, 50);
            btnClientes.TabIndex = 2;
            btnClientes.Text = "Clientes";
            btnClientes.UseVisualStyleBackColor = true;
            btnClientes.Click += btnClientes_Click;
            //
            // btnLugares
            //
            btnLugares.Location = new Point(320, 210);
            btnLugares.Name = "btnLugares";
            btnLugares.Tag = "secundario";
            btnLugares.Size = new Size(270, 50);
            btnLugares.TabIndex = 3;
            btnLugares.Text = "Lugares";
            btnLugares.UseVisualStyleBackColor = true;
            btnLugares.Click += btnLugares_Click;
            //
            // btnMenus
            //
            btnMenus.Location = new Point(30, 275);
            btnMenus.Name = "btnMenus";
            btnMenus.Tag = "secundario";
            btnMenus.Size = new Size(270, 50);
            btnMenus.TabIndex = 4;
            btnMenus.Text = "Menús";
            btnMenus.UseVisualStyleBackColor = true;
            btnMenus.Click += btnMenus_Click;
            //
            // btnPlatillos
            //
            btnPlatillos.Location = new Point(320, 275);
            btnPlatillos.Name = "btnPlatillos";
            btnPlatillos.Tag = "secundario";
            btnPlatillos.Size = new Size(270, 50);
            btnPlatillos.TabIndex = 5;
            btnPlatillos.Text = "Platillos";
            btnPlatillos.UseVisualStyleBackColor = true;
            btnPlatillos.Click += btnPlatillos_Click;
            //
            // btnServiciosAdicionales
            //
            btnServiciosAdicionales.Location = new Point(30, 340);
            btnServiciosAdicionales.Name = "btnServiciosAdicionales";
            btnServiciosAdicionales.Tag = "secundario";
            btnServiciosAdicionales.Size = new Size(560, 50);
            btnServiciosAdicionales.TabIndex = 6;
            btnServiciosAdicionales.Text = "Servicios Adicionales";
            btnServiciosAdicionales.UseVisualStyleBackColor = true;
            btnServiciosAdicionales.Click += btnServiciosAdicionales_Click;
            //
            // MenuPrincipalForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 420);
            Controls.Add(panelEncabezado);
            Controls.Add(btnPresupuestos);
            Controls.Add(btnClientes);
            Controls.Add(btnLugares);
            Controls.Add(btnMenus);
            Controls.Add(btnPlatillos);
            Controls.Add(btnServiciosAdicionales);
            Name = "MenuPrincipalForm";
            Text = "Menú Principal";
            panelEncabezado.ResumeLayout(false);
            panelEncabezado.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelEncabezado;
        private Label labelTitulo;
        private Label labelSubtitulo;
        private Button btnClientes;
        private Button btnServiciosAdicionales;
        private Button btnLugares;
        private Button btnPlatillos;
        private Button btnMenus;
        private Button btnPresupuestos;
    }
}
