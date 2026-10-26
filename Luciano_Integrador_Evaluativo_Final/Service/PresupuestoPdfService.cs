using Luciano_Integrador_Evaluativo_Final.Model;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    internal class PresupuestoPdfService
    {
        private const double Margen = 50;
        private const double AlturaLinea = 18;

        private PdfDocument _documento;
        private PdfPage _pagina;
        private XGraphics _grafico;
        private XFont _fuenteNormal;
        private XFont _fuenteNegrita;
        private XFont _fuenteTitulo;
        private double _y;

        // Esta clase usa PDFsharp, una librería externa (paquete NuGet) para crear PDFs desde código.
        // Se eligió porque .NET no trae soporte nativo para generar PDF y PDFsharp es gratuita (6.2.0;
        // la 6.5.0 exige licencia). Se separó en su propio Service para que la lógica de armado del
        // PDF no se mezcle con la del presupuesto ni con la View.
        public PresupuestoPdfService()
        {
            // PDFsharp necesita saber de dónde sacar las fuentes. Esta opción le indica que use las
            // que ya trae instaladas Windows (Arial), así no hay que incluir archivos de fuentes.
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            _documento = new PdfDocument();
            _fuenteNormal = new XFont("Arial", 11, XFontStyleEx.Regular);
            _fuenteNegrita = new XFont("Arial", 11, XFontStyleEx.Bold);
            _fuenteTitulo = new XFont("Arial", 18, XFontStyleEx.Bold);
        }

        // Dibuja el presupuesto línea por línea, de arriba hacia abajo, y guarda el archivo.
        // El texto se escribe con Escribir(), que va bajando la posición vertical (_y) y cambia de
        // página cuando se acaba el espacio, así un presupuesto largo no se corta.
        public void Generar(Presupuesto presupuesto, string ruta)
        {
            _documento = new PdfDocument();
            NuevaPagina();

            Escribir("Presupuesto N° " + presupuesto.Id, _fuenteTitulo, 10);
            Escribir("Fecha de creación: " + presupuesto.FechaCreacion.ToString("dd/MM/yyyy HH:mm"), _fuenteNormal);
            Escribir("Estado: " + presupuesto.Estado, _fuenteNormal, 10);

            Escribir("Cliente", _fuenteNegrita);
            if (presupuesto.Cliente != null)
            {
                Escribir("Nombre: " + presupuesto.Cliente.Nombre, _fuenteNormal);
                Escribir("Teléfono: " + presupuesto.Cliente.Telefono, _fuenteNormal);
                Escribir("Email: " + presupuesto.Cliente.Email, _fuenteNormal, 10);
            }

            Escribir("Evento", _fuenteNegrita);
            Escribir("Fecha del evento: " + presupuesto.FechaEvento.ToString("dd/MM/yyyy"), _fuenteNormal);
            Escribir("Duración: " + presupuesto.CantidadHoras + " horas", _fuenteNormal);
            if (presupuesto.Lugar != null)
            {
                Escribir("Lugar: " + presupuesto.Lugar.Nombre + " - " + presupuesto.Lugar.Precio.ToString("C"), _fuenteNormal, 10);
            }

            Escribir("Menús", _fuenteNegrita);
            foreach (ItemMenu item in presupuesto.Menus)
            {
                string destino = "niños";
                if (item.ParaAdulto)
                {
                    destino = "adultos";
                }

                Escribir(item.Item.Nombre + " - " + item.Cantidad + " " + destino + " - " + item.MontoTotal.ToString("C"),
                    _fuenteNormal);

                foreach (Platillo platillo in item.Item.Platillos)
                {
                    Escribir("    • " + platillo.Nombre + " (" + platillo.Tipo + ")", _fuenteNormal);
                }

                if (item.Item.Aptitudes.Count > 0)
                {
                    string aptitudes = "";
                    foreach (Aptitud aptitud in item.Item.Aptitudes)
                    {
                        aptitudes += aptitud + " ";
                    }
                    Escribir("    Apto para: " + aptitudes, _fuenteNormal);
                }
            }
            _y += 10;

            if (presupuesto.ServiciosAdicionales.Count > 0)
            {
                Escribir("Servicios adicionales", _fuenteNegrita);
                foreach (ServicioAdicional servicio in presupuesto.ServiciosAdicionales)
                {
                    Escribir(servicio.Nombre + " - " + servicio.Precio.ToString("C"), _fuenteNormal);
                }
                _y += 10;
            }

            Escribir("TOTAL: " + presupuesto.MontoTotal.ToString("C"), _fuenteTitulo);

            // Dispose libera el objeto de dibujo de la página; debe hacerse antes de guardar para
            // que todo lo dibujado quede escrito en el documento.
            _grafico.Dispose();
            _documento.Save(ruta);
        }

        // Agrega una página en blanco y reinicia la posición de escritura al margen superior.
        // XGraphics es el "lienzo" de PDFsharp sobre el que se dibuja el texto de esa página.
        private void NuevaPagina()
        {
            _pagina = _documento.AddPage();
            _grafico = XGraphics.FromPdfPage(_pagina);
            _y = Margen;
        }

        // Escribe una línea de texto sin dejar espacio adicional debajo.
        private void Escribir(string texto, XFont fuente)
        {
            Escribir(texto, fuente, 0);
        }

        // Escribe una línea de texto y baja la posición. "espacioExtra" es el espacio que se deja debajo.
        private void Escribir(string texto, XFont fuente, double espacioExtra)
        {
            if (_y + AlturaLinea > _pagina.Height.Point - Margen)
            {
                _grafico.Dispose();
                NuevaPagina();
            }

            _grafico.DrawString(texto, fuente, XBrushes.Black, new XPoint(Margen, _y + AlturaLinea - 4));
            _y += AlturaLinea + espacioExtra;
        }
    }
}
