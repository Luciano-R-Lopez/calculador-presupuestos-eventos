using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Pasa los pedidos de la View al Service.
    internal class PresupuestoController
    {
        private PresupuestoService _service;

        public PresupuestoController()
        {
            _service = new PresupuestoService();
        }

        public void RegistrarPresupuesto(PresupuestoDTO dto)
        {
            _service.RegistrarPresupuesto(dto);
        }

        // Monto de lo cargado hasta ahora, sin guardar.
        public decimal CalcularMonto(PresupuestoDTO dto)
        {
            return _service.CalcularMonto(dto);
        }

        // Respeta el flujo Nuevo -> Enviado -> Pagado.
        public void CambiarEstado(int id, EstadoPresupuesto estado)
        {
            _service.CambiarEstado(id, estado);
        }

        public void ClonarPresupuesto(int id)
        {
            _service.ClonarPresupuesto(id);
        }

        public void GenerarPdf(int id, string ruta)
        {
            _service.GenerarPdf(id, ruta);
        }

        // Lanza una excepción con el motivo si no se puede editar.
        public void ValidarEdicion(int id)
        {
            _service.ValidarEdicion(id);
        }

        public void ActualizarPresupuesto(int id, PresupuestoDTO dto)
        {
            _service.ActualizarPresupuesto(id, dto);
        }

        // Se rechaza si está pagado.
        public void EliminarPresupuesto(int id)
        {
            _service.EliminarPresupuesto(id);
        }

        public List<Presupuesto> GetPresupuestos()
        {
            return _service.GetPresupuestos();
        }
    }
}
