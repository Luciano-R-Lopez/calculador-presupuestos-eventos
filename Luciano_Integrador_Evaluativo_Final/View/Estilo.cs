using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Estilo común de todas las pantallas; los colores se definen solo acá.
    internal static class Estilo
    {
        public static readonly Color ColorFondo = Color.FromArgb(243, 245, 248);
        public static readonly Color ColorPrimario = Color.FromArgb(20, 38, 62);
        public static readonly Color ColorAcento = Color.FromArgb(31, 95, 168);
        public static readonly Color ColorAcentoHover = Color.FromArgb(22, 72, 130);
        public static readonly Color ColorTexto = Color.FromArgb(38, 52, 69);
        public static readonly Color ColorTextoSuave = Color.FromArgb(200, 212, 230);
        public static readonly Color ColorFilaAlterna = Color.FromArgb(246, 248, 251);
        public static readonly Color ColorSeleccion = Color.FromArgb(220, 231, 245);
        public static readonly Color ColorPeligro = Color.FromArgb(176, 42, 48);
        public static readonly Color ColorBorde = Color.FromArgb(203, 211, 222);

        // Barra de pestañas (oscura).
        public static readonly Color ColorBarra = Color.FromArgb(21, 24, 29);
        public static readonly Color ColorBarraHover = Color.FromArgb(35, 40, 49);
        public static readonly Color ColorTextoBarra = Color.FromArgb(154, 163, 174);
        public static readonly Color ColorTextoBarraActivo = Color.FromArgb(227, 230, 234);

        private const int AlturaEncabezado = 56;

        // Alto de la barra de título y de pestañas, para que la pantalla incrustada entre en el espacio libre.
        private const int AlturaVentanaPrincipal = 120;

        // 1.2 = 20% más grande.
        private const float EscalaDeseada = 1.2f;

        // Escala aplicada: menor que la deseada si la pantalla no entra en el monitor.
        private static float _escala = 1f;

        public static void Aplicar(Form formulario)
        {
            Aplicar(formulario, true);
        }

        // conEncabezado = ventana propia con título; si no, va dentro de una pestaña.
        public static void Aplicar(Form formulario, bool conEncabezado)
        {
            formulario.BackColor = ColorFondo;
            formulario.ForeColor = ColorTexto;
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.FormBorderStyle = FormBorderStyle.FixedSingle;
            formulario.MaximizeBox = false;

            // Primero se agranda, así los estilos parten de las medidas finales.
            Agrandar(formulario, conEncabezado);

            AplicarAControles(formulario);

            if (conEncabezado)
            {
                AgregarEncabezado(formulario);
            }

            EstirarGrillas(formulario);
        }

        // Ancla las grillas a los cuatro bordes para que crezcan con la ventana.
        // Si hay controles debajo, esos quedan anclados abajo; si no, la grilla se estira hasta el borde.
        private static void EstirarGrillas(Form formulario)
        {
            foreach (Control control in formulario.Controls)
            {
                DataGridView? grilla = control as DataGridView;
                if (grilla == null)
                    continue;

                bool hayControlesDebajo = false;
                foreach (Control otro in formulario.Controls)
                {
                    if (otro != grilla && otro.Dock == DockStyle.None && otro.Top >= grilla.Bottom - 5
                        && otro.Right > grilla.Left && otro.Left < grilla.Right)
                    {
                        hayControlesDebajo = true;
                        otro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                    }
                }

                if (!hayControlesDebajo)
                {
                    grilla.Height = formulario.ClientSize.Height - grilla.Top - (int)(20 * _escala);
                }
                grilla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        // Agranda posiciones, tamaños y fuentes.
        private static void Agrandar(Form formulario, bool conEncabezado)
        {
            // Evita escalar dos veces por el cambio de fuente.
            formulario.AutoScaleMode = AutoScaleMode.None;

            // Reduce la escala (mínimo 1) si no entra en el monitor.
            Rectangle area = SystemInformation.WorkingArea;
            float extra;
            if (conEncabezado)
            {
                extra = AlturaEncabezado * formulario.DeviceDpi / 96f;
            }
            else
            {
                extra = AlturaVentanaPrincipal * formulario.DeviceDpi / 96f;
            }
            float maxAncho = area.Width * 0.95f / formulario.Width;
            float maxAlto = area.Height * 0.95f / (formulario.Height + extra);

            _escala = EscalaDeseada;
            if (maxAncho < _escala)
            {
                _escala = maxAncho;
            }
            if (maxAlto < _escala)
            {
                _escala = maxAlto;
            }
            if (_escala < 1f)
            {
                _escala = 1f;
            }

            AgrandarFuentes(formulario);
            formulario.Scale(new SizeF(_escala, _escala));
        }

        // De adentro hacia afuera: los hijos toman su fuente antes de que cambie la del contenedor.
        private static void AgrandarFuentes(Control control)
        {
            foreach (Control hijo in control.Controls)
            {
                AgrandarFuentes(hijo);
            }

            control.Font = new Font(control.Font.FontFamily, control.Font.Size * _escala, control.Font.Style);
        }

        // Franja con el título; baja los controles y agranda la ventana en la misma medida.
        private static void AgregarEncabezado(Form formulario)
        {
            int alto = (int)(AlturaEncabezado * _escala * formulario.DeviceDpi / 96.0);

            foreach (Control control in formulario.Controls)
            {
                if (control.Dock == DockStyle.None)
                {
                    control.Top += alto;
                }
            }
            formulario.ClientSize = new Size(formulario.ClientSize.Width, formulario.ClientSize.Height + alto);

            Panel encabezado = new Panel();
            encabezado.Dock = DockStyle.Top;
            encabezado.Height = alto;
            encabezado.BackColor = ColorPrimario;

            Label titulo = new Label();
            titulo.AutoSize = true;
            titulo.Text = formulario.Text;
            titulo.Font = new Font("Segoe UI", 14F * _escala, FontStyle.Bold);
            titulo.ForeColor = Color.White;
            titulo.Location = new Point((int)(20 * _escala), (int)((alto - 30 * _escala) / 2));

            // Línea de acento inferior.
            Panel linea = new Panel();
            linea.Dock = DockStyle.Bottom;
            linea.Height = 3;
            linea.BackColor = ColorAcento;

            encabezado.Controls.Add(titulo);
            encabezado.Controls.Add(linea);
            formulario.Controls.Add(encabezado);
        }

        // Incluye los controles anidados.
        private static void AplicarAControles(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is Button)
                {
                    EstiloBoton((Button)control);
                }
                else if (control is DataGridView)
                {
                    EstiloGrilla((DataGridView)control);
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is Label)
                {
                    // Respeta los títulos con color propio.
                    if (control.ForeColor == SystemColors.ControlText)
                    {
                        control.ForeColor = ColorTexto;
                    }
                }

                AplicarAControles(control);
            }
        }

        // Tag "secundario": fondo blanco con borde. Tag "peligro": igual pero en rojo. Resto: botón principal azul.
        private static void EstiloBoton(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.Cursor = Cursors.Hand;
            boton.UseVisualStyleBackColor = false;

            if (boton.Tag != null && boton.Tag.ToString() == "secundario")
            {
                boton.BackColor = Color.White;
                boton.ForeColor = ColorAcento;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = ColorAcento;
                boton.FlatAppearance.MouseOverBackColor = ColorSeleccion;
            }
            else if (boton.Tag != null && boton.Tag.ToString() == "peligro")
            {
                boton.BackColor = Color.White;
                boton.ForeColor = ColorPeligro;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = ColorPeligro;
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(251, 228, 228);
            }
            else
            {
                boton.BackColor = ColorAcento;
                boton.ForeColor = Color.White;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = ColorPrimario;
                boton.FlatAppearance.MouseOverBackColor = ColorAcentoHover;
            }
        }

        // Encabezado oscuro, filas alternadas, selección suave y columnas a lo ancho.
        private static void EstiloGrilla(DataGridView grilla)
        {
            grilla.BackgroundColor = Color.White;
            grilla.BorderStyle = BorderStyle.FixedSingle;
            grilla.GridColor = ColorBorde;
            grilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grilla.RowHeadersVisible = false;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToResizeRows = false;
            grilla.ReadOnly = true;
            grilla.MultiSelect = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.RowTemplate.Height = (int)(28 * _escala);

            // Sin esto Windows usa los colores del sistema en el encabezado.
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = ColorPrimario;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorPrimario;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font(grilla.Font, FontStyle.Bold);
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.ColumnHeadersHeight = (int)(36 * _escala);

            grilla.DefaultCellStyle.SelectionBackColor = ColorSeleccion;
            grilla.DefaultCellStyle.SelectionForeColor = ColorTexto;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = ColorFilaAlterna;

            // Las columnas automáticas existen recién al terminar el enlace de datos.
            grilla.DataBindingComplete += grilla_DataBindingComplete;
        }

        // Decimales como moneda; columnas de listas, ocultas.
        private static void grilla_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grilla = (DataGridView)sender;

            foreach (DataGridViewColumn columna in grilla.Columns)
            {
                if (columna.ValueType == typeof(decimal))
                {
                    columna.DefaultCellStyle.Format = "C";
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (columna.ValueType != null && columna.ValueType.IsGenericType)
                {
                    columna.Visible = false;
                }
            }
        }

        // Ej: $ 1.500,00
        public static void FormatoMoneda(DataGridView grilla, string nombreColumna)
        {
            if (grilla.Columns[nombreColumna] != null)
            {
                grilla.Columns[nombreColumna].DefaultCellStyle.Format = "C";
                grilla.Columns[nombreColumna].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // Ventana principal: barra oscura con título y pestañas, y panel de contenido claro.
        public static void EstilarBarra(Panel barra, Label titulo, Panel contenido)
        {
            barra.BackColor = ColorBarra;
            titulo.BackColor = Color.Transparent;
            titulo.ForeColor = ColorTextoBarraActivo;
            titulo.Font = new Font("Segoe UI Semibold", 13F);
            contenido.BackColor = ColorFondo;

            foreach (Control control in barra.Controls)
            {
                Button? pestana = control as Button;
                if (pestana != null)
                {
                    EstilarPestana(pestana);
                }
            }

            UsarBarraDeTituloOscura(barra.FindForm());
        }

        // Pestaña: botón plano sin borde.
        private static void EstilarPestana(Button pestana)
        {
            pestana.FlatStyle = FlatStyle.Flat;
            pestana.FlatAppearance.BorderSize = 0;
            pestana.FlatAppearance.MouseOverBackColor = ColorBarraHover;
            pestana.FlatAppearance.MouseDownBackColor = ColorBarraHover;
            pestana.UseVisualStyleBackColor = false;
            pestana.BackColor = ColorBarra;
            pestana.ForeColor = ColorTextoBarra;
            pestana.Font = new Font("Segoe UI", 10.5F);
            pestana.Cursor = Cursors.Hand;

            // Sin TabStop, para que no se dibuje el recuadro de foco.
            pestana.TabStop = false;
        }

        // Pestaña activa: texto claro y fondo resaltado.
        public static void MarcarPestana(Button pestana, bool activa)
        {
            if (activa)
            {
                pestana.ForeColor = ColorTextoBarraActivo;
                pestana.BackColor = ColorBarraHover;
            }
            else
            {
                pestana.ForeColor = ColorTextoBarra;
                pestana.BackColor = ColorBarra;
            }
        }

        // Barra de título oscura (Windows 10 20H1+; en versiones anteriores no hace nada).
        private static void UsarBarraDeTituloOscura(Form? formulario)
        {
            if (formulario == null)
                return;

            if (formulario.IsHandleCreated)
            {
                ActivarBarraOscura(formulario);
            }
            else
            {
                formulario.HandleCreated += AlCrearseLaVentana;
            }
        }

        private static void AlCrearseLaVentana(object? sender, EventArgs e)
        {
            Form? formulario = sender as Form;
            if (formulario != null)
            {
                ActivarBarraOscura(formulario);
            }
        }

        private static void ActivarBarraOscura(Form formulario)
        {
            int activado = 1;
            DwmSetWindowAttribute(formulario.Handle, 20, ref activado, sizeof(int));
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamanio);
    }
}
