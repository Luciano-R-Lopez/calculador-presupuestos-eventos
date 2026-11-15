namespace Luciano_Integrador_Evaluativo_Final.View
{
    // Pantallas de las pestañas: se conservan al cambiar de pestaña y, al volver,
    // releen los datos sin borrar lo que el usuario ya escribió.
    internal interface IPantallaActualizable
    {
        void ActualizarDatos();
    }
}
