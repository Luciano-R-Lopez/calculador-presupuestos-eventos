using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class PresupuestoController
    {
        private PresupuestoService _service;

        // Crea el Service con el que trabaja este Controller.
        public PresupuestoController()
        {
            _service = new PresupuestoService();
        }

        // Pide al Service que valide los datos y registre un presupuesto nuevo.
        public void RegistrarPresupuesto(PresupuestoDTO dto)
        {
            _service.RegistrarPresupuesto(dto);
        }

        // Calcula el monto con lo cargado hasta el momento, sin guardar nada. La View lo muestra al instante.
        public decimal CalcularMonto(PresupuestoDTO dto)
        {
            return _service.CalcularMonto(dto);
        }

        // Pide al Service cambiar el estado del presupuesto (respeta el flujo Nuevo -> Enviado -> Pagado).
        public void CambiarEstado(int id, EstadoPresupuesto estado)
        {
            _service.CambiarEstado(id, estado);
        }

        // Pide al Service crear un presupuesto nuevo igual al indicado.
        public void ClonarPresupuesto(int id)
        {
            _service.ClonarPresupuesto(id);
        }

        // Pide al Service generar el PDF del presupuesto en la ruta elegida.
        public void GenerarPdf(int id, string ruta)
        {
            _service.GenerarPdf(id, ruta);
        }

        // Lanza una excepción con el motivo si el presupuesto no se puede editar.
        public void ValidarEdicion(int id)
        {
            _service.ValidarEdicion(id);
        }

        // Pide al Service que modifique el presupuesto que tiene ese id con los datos nuevos.
        public void ActualizarPresupuesto(int id, PresupuestoDTO dto)
        {
            _service.ActualizarPresupuesto(id, dto);
        }

        // Pide al Service que elimine el presupuesto (se rechaza si está pagado).
        public void EliminarPresupuesto(int id)
        {
            _service.EliminarPresupuesto(id);
        }

        // Devuelve la lista de presupuestos para mostrarla en la View.
        public List<Presupuesto> GetPresupuestos()
        {
            return _service.GetPresupuestos();
        }
    }
}
