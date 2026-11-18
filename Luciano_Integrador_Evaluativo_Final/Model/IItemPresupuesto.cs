using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Renglón de un presupuesto (ServicioPresupuestado, ItemMenu): lo que se cobra, con su precio al cotizar.
    // Cada renglón calcula su monto (polimorfismo), así un tipo nuevo se suma sin cambiar el presupuesto.
    internal interface IItemPresupuesto
    {
        // Monto que suma al presupuesto.
        decimal CalcularMonto();
    }
}
