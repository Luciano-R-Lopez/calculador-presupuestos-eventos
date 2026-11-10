namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Contrato de las pantallas que se muestran en las pestañas de la ventana principal.
    // Cada pantalla se crea una sola vez y se conserva al cambiar de pestaña (así no se pierde lo que
    // el usuario estaba cargando). Como mientras tanto los datos pueden cambiar en otra pestaña
    // (por ejemplo, se agrega un cliente), al volver a mostrarla se le pide que relea los datos,
    // sin borrar lo que ya estaba escrito en sus campos.
    internal interface IPantallaActualizable
    {
        void ActualizarDatos();
    }
}
