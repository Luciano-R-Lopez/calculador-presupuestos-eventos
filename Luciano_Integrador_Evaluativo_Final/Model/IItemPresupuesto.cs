using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Contrato que cumple todo lo que se cobra en un presupuesto (lugar, servicios adicionales, menús...).
    // El presupuesto trabaja con esta interfaz y no con cada tipo concreto: para sumar un tipo de ítem nuevo
    // (por ejemplo un transporte) alcanza con crear una clase que la implemente, sin tocar el cálculo del
    // monto, la clonación ni el PDF. Es polimorfismo: cada ítem sabe calcular su propio monto.
    internal interface IItemPresupuesto
    {
        // Texto con el que el ítem aparece detallado (por ejemplo en el PDF).
        string Descripcion { get; }

        // Monto que el ítem suma al presupuesto.
        decimal CalcularMonto();

        // Copia independiente del ítem. El presupuesto guarda copias para conservar los datos tal como
        // estaban cuando se generó.
        IItemPresupuesto CopiarItem();
    }
}
