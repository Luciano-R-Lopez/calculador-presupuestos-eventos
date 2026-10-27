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
            pnlBarra = new Panel();
            lblTitulo = new Label();
            btnPresupuestos = new Button();
            btnClientes = new Button();
            btnLugares = new Button();
            btnServiciosAdicionales = new Button();
            btnPlatillos = new Button();
            btnMenus = new Button();
            pnlContenido = new Panel();
            pnlBarra.SuspendLayout();
            SuspendLayout();
            //
            // pnlBarra
            //
            pnlBarra.Controls.Add(btnMenus);
            pnlBarra.Controls.Add(btnPlatillos);
            pnlBarra.Controls.Add(btnServiciosAdicionales);
            pnlBarra.Controls.Add(btnLugares);
            pnlBarra.Controls.Add(btnClientes);
            pnlBarra.Controls.Add(btnPresupuestos);
            pnlBarra.Controls.Add(lblTitulo);
            pnlBarra.Dock = DockStyle.Top;
            pnlBarra.Location = new Point(0, 0);
            pnlBarra.Name = "pnlBarra";
            pnlBarra.Size = new Size(1280, 60);
            pnlBarra.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 17);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(280, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Calculador de Presupuestos";
            //
            // btnPresupuestos
            //
            btnPresupuestos.Location = new Point(340, 0);
            btnPresupuestos.Name = "btnPresupuestos";
            btnPresupuestos.Size = new Size(130, 60);
            btnPresupuestos.TabIndex = 1;
            btnPresupuestos.Text = "Presupuestos";
            btnPresupuestos.Click += btnPresupuestos_Click;
            //
            // btnClientes
            //
            btnClientes.Location = new Point(470, 0);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(100, 60);
            btnClientes.TabIndex = 2;
            btnClientes.Text = "Clientes";
            btnClientes.Click += btnClientes_Click;
            //
            // btnLugares
            //
            btnLugares.Location = new Point(570, 0);
            btnLugares.Name = "btnLugares";
            btnLugares.Size = new Size(100, 60);
            btnLugares.TabIndex = 3;
            btnLugares.Text = "Lugares";
            btnLugares.Click += btnLugares_Click;
            //
            // btnServiciosAdicionales
            //
            btnServiciosAdicionales.Location = new Point(670, 0);
            btnServiciosAdicionales.Name = "btnServiciosAdicionales";
            btnServiciosAdicionales.Size = new Size(215, 60);
            btnServiciosAdicionales.TabIndex = 4;
            btnServiciosAdicionales.Text = "Servicios adicionales";
            btnServiciosAdicionales.Click += btnServiciosAdicionales_Click;
            //
            // btnPlatillos
            //
            btnPlatillos.Location = new Point(885, 0);
            btnPlatillos.Name = "btnPlatillos";
            btnPlatillos.Size = new Size(100, 60);
            btnPlatillos.TabIndex = 5;
            btnPlatillos.Text = "Platillos";
            btnPlatillos.Click += btnPlatillos_Click;
            //
            // btnMenus
            //
            btnMenus.Location = new Point(985, 0);
            btnMenus.Name = "btnMenus";
            btnMenus.Size = new Size(90, 60);
            btnMenus.TabIndex = 6;
            btnMenus.Text = "Menús";
            btnMenus.Click += btnMenus_Click;
            //
            // pnlContenido
            //
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 60);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1280, 700);
            pnlContenido.TabIndex = 1;
            //
            // MenuPrincipalForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 760);
            Controls.Add(pnlContenido);
            Controls.Add(pnlBarra);
            MinimumSize = new Size(1100, 700);
            Name = "MenuPrincipalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Calculador de Presupuestos";
            WindowState = FormWindowState.Maximized;
            Load += MenuPrincipalForm_Load;
            pnlBarra.ResumeLayout(false);
            pnlBarra.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlBarra;
        private Label lblTitulo;
        private Button btnPresupuestos;
        private Button btnClientes;
        private Button btnLugares;
        private Button btnServiciosAdicionales;
        private Button btnPlatillos;
        private Button btnMenus;
        private Panel pnlContenido;
    }
}
