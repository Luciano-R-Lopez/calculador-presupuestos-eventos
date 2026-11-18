using Luciano_Integrador_Evaluativo_Final.Model;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Luciano_Integrador_Evaluativo_Final.Comun;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Genera el PDF de un presupuesto.
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

        // Usa PDFsharp 6.2.0 (gratuita; la 6.5.0 exige licencia).
        public PresupuestoPdfService()
        {
            // Usa las fuentes instaladas en Windows (Arial).
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
            _documento = new PdfDocument();
            _fuenteNormal = new XFont("Arial", 11, XFontStyleEx.Regular);
            _fuenteNegrita = new XFont("Arial", 11, XFontStyleEx.Bold);
            _fuenteTitulo = new XFont("Arial", 18, XFontStyleEx.Bold);
        }

        // Escribe línea por línea; Escribir() baja la posición y cambia de página si hace falta.
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
            Escribir("Inicio del evento: " + presupuesto.FechaEvento.ToString("dd/MM/yyyy HH:mm"), _fuenteNormal);
            Escribir("Duración: " + presupuesto.CantidadHoras + " horas", _fuenteNormal);
            if (presupuesto.Lugar != null)
            {
                // Precio guardado al cotizar.
                Escribir("Lugar: " + presupuesto.Lugar.Nombre + " - " + presupuesto.PrecioLugar.ToString("C"), _fuenteNormal, 10);
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
                foreach (ServicioPresupuestado renglon in presupuesto.ServiciosAdicionales)
                {
                    Escribir(renglon.Servicio.Nombre + " - " + renglon.Precio.ToString("C"), _fuenteNormal);
                }
                _y += 10;
            }

            // Otros ítems: se detallan a través de IItemPresupuesto.
            if (presupuesto.ItemsExtra.Count > 0)
            {
                Escribir("Otros ítems", _fuenteNegrita);
                foreach (IItemPresupuesto extra in presupuesto.ItemsExtra)
                {
                    Escribir(extra.Descripcion + " - " + extra.CalcularMonto().ToString("C"), _fuenteNormal);
                }
                _y += 10;
            }

            Escribir("TOTAL: " + presupuesto.MontoTotal.ToString("C"), _fuenteTitulo);

            // Dispose antes de guardar, para que se escriba todo lo dibujado.
            _grafico.Dispose();
            _documento.Save(ruta);
        }

        // Página nueva; XGraphics es el lienzo donde se dibuja.
        private void NuevaPagina()
        {
            _pagina = _documento.AddPage();
            _grafico = XGraphics.FromPdfPage(_pagina);
            _y = Margen;
        }

        private void Escribir(string texto, XFont fuente)
        {
            Escribir(texto, fuente, 0);
        }

        // "espacioExtra": espacio que se deja debajo de la línea.
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
