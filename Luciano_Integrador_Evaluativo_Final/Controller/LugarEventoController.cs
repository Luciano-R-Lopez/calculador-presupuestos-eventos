using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    internal class LugarEventoController
    {
        private LugarEventoService _service;

        public LugarEventoController()
        {
            _service = new LugarEventoService();
        }

        public void RegistrarLugarEvento(LugarEventoDTO dto)
        {
            _service.RegistrarLugarEvento(dto);
        }

        public void ActualizarLugarEvento(int id, LugarEventoDTO dto)
        {
            _service.ActualizarLugarEvento(id, dto);
        }

        public void EliminarLugarEvento(int id)
        {
            _service.EliminarLugarEvento(id);
        }

        public List<LugarEvento> GetLugaresEvento()
        {
            return _service.GetLugaresEvento();
        }
    }
}
