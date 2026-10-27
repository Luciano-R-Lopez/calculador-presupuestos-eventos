namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Estados de un presupuesto. Flujo permitido: Nuevo -> Enviado -> Pagado (ver Presupuesto.CambiarEstado).
    internal enum EstadoPresupuesto
    {
        Nuevo,
        Enviado,
        Pagado
    }
}
