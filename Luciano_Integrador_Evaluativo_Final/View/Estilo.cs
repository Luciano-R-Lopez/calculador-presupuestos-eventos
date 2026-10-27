using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Aspecto corporativo común de todas las pantallas. Los colores se definen en un solo lugar para
    // que cambiar el estilo de la aplicación no requiera tocar cada formulario.
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

        // Colores de la barra de pestañas de la ventana principal (oscura, a diferencia de las pantallas).
        public static readonly Color ColorBarra = Color.FromArgb(21, 24, 29);
        public static readonly Color ColorBarraHover = Color.FromArgb(35, 40, 49);
        public static readonly Color ColorTextoBarra = Color.FromArgb(154, 163, 174);
        public static readonly Color ColorTextoBarraActivo = Color.FromArgb(227, 230, 234);
        public static readonly Color ColorAcentoClaro = Color.FromArgb(143, 179, 230);

        // Altura del encabezado que se agrega a cada pantalla.
        private const int AlturaEncabezado = 56;

        // Alto aproximado que ocupan la barra de título de Windows y la barra de pestañas de la ventana
        // principal. Se usa para que una pantalla incrustada en ella no quede más alta que el espacio libre.
        private const int AlturaVentanaPrincipal = 120;

        // Cuánto se agranda cada pantalla (1.2 = 20% más grande).
        private const float EscalaDeseada = 1.2f;

        // Escala realmente aplicada a la pantalla que se está armando. Puede ser menor que la deseada
        // si la pantalla agrandada no entraría en el área visible del monitor.
        private static float _escala = 1f;

        // Aplica el estilo al formulario y a todos sus controles, con un encabezado con su título.
        public static void Aplicar(Form formulario)
        {
            Aplicar(formulario, true);
        }

        // Si conEncabezado es true la pantalla se muestra como ventana propia y lleva su encabezado con el
        // título. Si es false está pensada para mostrarse dentro de la ventana principal (en una pestaña),
        // que ya indica en qué pantalla se está, así que no lleva encabezado.
        public static void Aplicar(Form formulario, bool conEncabezado)
        {
            formulario.BackColor = ColorFondo;
            formulario.ForeColor = ColorTexto;
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.FormBorderStyle = FormBorderStyle.FixedSingle;
            formulario.MaximizeBox = false;

            // Primero se agranda la pantalla: así los estilos que se aplican después (por ejemplo los
            // de las grillas) parten de las fuentes y medidas ya agrandadas.
            Agrandar(formulario, conEncabezado);

            AplicarAControles(formulario);

            if (conEncabezado)
            {
                AgregarEncabezado(formulario);
            }

            EstirarGrillas(formulario);
        }

        // Estira cada grilla hasta el borde inferior de la ventana, dejando el mismo margen que arriba,
        // para aprovechar el espacio. Si hay controles debajo de la grilla (por ejemplo los botones de
        // acciones de Presupuestos) la grilla no se estira ahora, pero esos controles quedan anclados
        // abajo. En los dos casos la grilla queda anclada a los cuatro bordes: cuando la pantalla se
        // muestra dentro de la ventana principal (y cambia de tamaño), la grilla crece con ella.
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

        // Agranda el formulario y todos sus controles (posiciones, tamaños y fuentes).
        private static void Agrandar(Form formulario, bool conEncabezado)
        {
            // Se desactiva el autoescalado por fuente para que el cambio de fuente no escale dos veces.
            formulario.AutoScaleMode = AutoScaleMode.None;

            // Si la pantalla agrandada no entra en el área visible del monitor, se reduce la escala
            // (sin bajar de 1) para que no quede cortada. Se cuenta también el alto del encabezado.
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

        // Agranda la fuente de cada control. Se hace de adentro hacia afuera: los controles hijos
        // toman su fuente actual antes de que cambie la de su contenedor.
        private static void AgrandarFuentes(Control control)
        {
            foreach (Control hijo in control.Controls)
            {
                AgrandarFuentes(hijo);
            }

            control.Font = new Font(control.Font.FontFamily, control.Font.Size * _escala, control.Font.Style);
        }

        // Agrega una franja superior con el título de la pantalla. Como los controles tienen posición
        // fija, se bajan todos la altura del encabezado y se agranda la ventana en la misma medida.
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

            // Línea de acento en el borde inferior del encabezado.
            Panel linea = new Panel();
            linea.Dock = DockStyle.Bottom;
            linea.Height = 3;
            linea.BackColor = ColorAcento;

            encabezado.Controls.Add(titulo);
            encabezado.Controls.Add(linea);
            formulario.Controls.Add(encabezado);
        }

        // Recorre los controles del formulario, incluidos los que están dentro de otros.
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
                    // Solo se cambia el color por defecto; los títulos con color propio se respetan.
                    if (control.ForeColor == SystemColors.ControlText)
                    {
                        control.ForeColor = ColorTexto;
                    }
                }

                AplicarAControles(control);
            }
        }

        // Los botones con Tag = "secundario" son de acciones menos importantes: fondo blanco y borde.
        // Los de Tag = "peligro" son acciones destructivas. El resto son botones principales: fondo azul.
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
                // Acciones destructivas (eliminar): mismo estilo que el secundario pero en rojo.
                boton.BackColor = Color.White;
                boton.ForeColor = ColorPeligro;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = ColorPeligro;
                boton.FlatAppearance.MouseOverBackColor = Color.FromArgb(251, 228, 228);
            }
            else
            {
                boton.BackColor = ColorAcento;
                // Los botones principales también llevan contorno (en un azul más oscuro que el relleno).
                boton.ForeColor = Color.White;
                boton.FlatAppearance.BorderSize = 1;
                boton.FlatAppearance.BorderColor = ColorPrimario;
                boton.FlatAppearance.MouseOverBackColor = ColorAcentoHover;
            }
        }

        // Da el aspecto corporativo a una grilla: encabezado oscuro, filas alternadas, selección suave,
        // sin columna lateral y con las columnas repartidas a lo ancho.
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

            // Sin esto Windows ignora los colores del encabezado y usa los del sistema.
            grilla.EnableHeadersVisualStyles = false;
            grilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grilla.ColumnHeadersDefaultCellStyle.BackColor = ColorPrimario;
            grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = ColorPrimario;
            grilla.ColumnHeadersDefaultCellStyle.Font = new Font(grilla.Font, FontStyle.Bold);
            grilla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grilla.ColumnHeadersHeight = (int)(36 * _escala);

            // Selección suave (celeste con texto oscuro): más sobria que un azul fuerte.
            grilla.DefaultCellStyle.SelectionBackColor = ColorSeleccion;
            grilla.DefaultCellStyle.SelectionForeColor = ColorTexto;
            grilla.AlternatingRowsDefaultCellStyle.BackColor = ColorFilaAlterna;

            // Las columnas automáticas recién existen cuando la grilla termina de enlazar los datos,
            // por eso los formatos se aplican en este evento y no justo después de asignar DataSource.
            grilla.DataBindingComplete += grilla_DataBindingComplete;
        }

        // Las columnas decimales (precios y montos) se muestran como moneda y las columnas que son
        // listas (por ejemplo los platillos de un menú) se ocultan, porque no se leen bien en una celda.
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

        // Muestra una columna numérica como moneda (por ejemplo $ 1.500,00).
        public static void FormatoMoneda(DataGridView grilla, string nombreColumna)
        {
            if (grilla.Columns[nombreColumna] != null)
            {
                grilla.Columns[nombreColumna].DefaultCellStyle.Format = "C";
                grilla.Columns[nombreColumna].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // Estilo de la ventana principal: barra superior oscura con el título de la aplicación y las
        // pestañas (botones planos), y el panel de contenido con el fondo claro de las pantallas.
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

        // Una pestaña es un botón plano, sin borde, con texto gris que se aclara al pasar el mouse.
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
            pestana.Paint += DibujarMarcaPestana;
        }

        // Marca una pestaña como la abierta (texto claro, fondo resaltado y línea de acento debajo)
        // o la deja como una pestaña común.
        public static void MarcarPestana(Button pestana, bool activa)
        {
            if (activa)
            {
                pestana.Tag = "activa";
                pestana.ForeColor = ColorTextoBarraActivo;
                pestana.BackColor = ColorBarraHover;
            }
            else
            {
                pestana.Tag = null;
                pestana.ForeColor = ColorTextoBarra;
                pestana.BackColor = ColorBarra;
            }
            pestana.Invalidate();
        }

        // Dibuja la línea de acento en el borde inferior de la pestaña abierta.
        private static void DibujarMarcaPestana(object? sender, PaintEventArgs e)
        {
            Button? pestana = sender as Button;
            if (pestana != null && "activa".Equals(pestana.Tag))
            {
                using (SolidBrush marca = new SolidBrush(ColorAcentoClaro))
                {
                    e.Graphics.FillRectangle(marca, 0, pestana.Height - 4, pestana.Width, 4);
                }
            }
        }

        // Pide a Windows (10 20H1 en adelante) la barra de título oscura, para que combine con la barra
        // de pestañas. En versiones anteriores no hace nada.
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
