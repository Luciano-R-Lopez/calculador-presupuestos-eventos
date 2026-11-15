using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Pasa los pedidos de la View al Service.
    internal class PlatilloController
    {
        private PlatilloService _service;

        public PlatilloController()
        {
            _service = new PlatilloService();
        }

        public void RegistrarPlatillo(PlatilloDTO dto)
        {
            _service.RegistrarPlatillo(dto);
        }

        public void ActualizarPlatillo(int id, PlatilloDTO dto)
        {
            _service.ActualizarPlatillo(id, dto);
        }

        // Se rechaza si está en uso.
        public void EliminarPlatillo(int id)
        {
            _service.EliminarPlatillo(id);
        }

        public List<Platillo> GetPlatillos()
        {
            return _service.GetPlatillos();
        }
    }
}
