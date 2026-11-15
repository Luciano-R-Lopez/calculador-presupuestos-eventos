using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Renglón de un presupuesto (ServicioPresupuestado, ItemMenu): lo que se cobra, con su precio al cotizar.
    // Cada renglón calcula su monto (polimorfismo), así un tipo nuevo se suma sin cambiar el presupuesto.
    internal interface IItemPresupuesto
    {
        // Texto del ítem (por ejemplo, en el PDF).
        string Descripcion { get; }

        // Monto que suma al presupuesto.
        decimal CalcularMonto();

        // Renglón nuevo con los mismos datos y precio (para clonar).
        IItemPresupuesto CopiarItem();
    }
}
