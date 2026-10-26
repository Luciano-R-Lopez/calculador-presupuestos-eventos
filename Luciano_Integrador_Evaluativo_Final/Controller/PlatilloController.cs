using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
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
