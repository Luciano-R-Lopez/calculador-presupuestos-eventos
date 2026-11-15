using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Pasa los pedidos de la View al Service.
    internal class ServicioAdicionalController
    {
        private ServicioAdicionalService _service;

        public ServicioAdicionalController()
        {
            _service = new ServicioAdicionalService();
        }

        public void RegistrarServicioAdicional(ServicioAdicionalDTO dto)
        {
            _service.RegistrarServicioAdicional(dto);
        }

        public void ActualizarServicioAdicional(int id, ServicioAdicionalDTO dto)
        {
            _service.ActualizarServicioAdicional(id, dto);
        }

        // Se rechaza si está en uso.
        public void EliminarServicioAdicional(int id)
        {
            _service.EliminarServicioAdicional(id);
        }

        public List<ServicioAdicional> GetServiciosAdicionales()
        {
            return _service.GetServiciosAdicionales();
        }
    }
}
