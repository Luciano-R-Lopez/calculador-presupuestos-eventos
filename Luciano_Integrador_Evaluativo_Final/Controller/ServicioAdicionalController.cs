using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class ServicioAdicionalController
    {
        private ServicioAdicionalService _service;

        // Crea el Service con el que trabaja este Controller.
        public ServicioAdicionalController()
        {
            _service = new ServicioAdicionalService();
        }

        // Pide al Service que valide los datos y registre un servicio adicional nuevo.
        public void RegistrarServicioAdicional(ServicioAdicionalDTO dto)
        {
            _service.RegistrarServicioAdicional(dto);
        }

        // Pide al Service que modifique el servicio adicional que tiene ese id con los datos nuevos.
        public void ActualizarServicioAdicional(int id, ServicioAdicionalDTO dto)
        {
            _service.ActualizarServicioAdicional(id, dto);
        }

        // Pide al Service que elimine el servicio adicional (se rechaza si está en uso).
        public void EliminarServicioAdicional(int id)
        {
            _service.EliminarServicioAdicional(id);
        }

        // Devuelve la lista de servicios adicionales para mostrarla en la View.
        public List<ServicioAdicional> GetServiciosAdicionales()
        {
            return _service.GetServiciosAdicionales();
        }
    }
}
