using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Todo lo que se cobra en un presupuesto. Cada ítem calcula su monto (polimorfismo), así un tipo nuevo se suma sin cambiar el presupuesto.
    internal interface IItemPresupuesto
    {
        // Texto del ítem (por ejemplo, en el PDF).
        string Descripcion { get; }

        // Monto que suma al presupuesto.
        decimal CalcularMonto();

        // Copia independiente del ítem.
        IItemPresupuesto CopiarItem();
    }
}
