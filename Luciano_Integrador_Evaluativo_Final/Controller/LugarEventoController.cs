using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class LugarEventoController
    {
        private LugarEventoService _service;

        // Crea el Service con el que trabaja este Controller.
        public LugarEventoController()
        {
            _service = new LugarEventoService();
        }

        // Pide al Service que valide los datos y registre un lugar nuevo.
        public void RegistrarLugarEvento(LugarEventoDTO dto)
        {
            _service.RegistrarLugarEvento(dto);
        }

        // Pide al Service que modifique el lugar que tiene ese id con los datos nuevos.
        public void ActualizarLugarEvento(int id, LugarEventoDTO dto)
        {
            _service.ActualizarLugarEvento(id, dto);
        }

        // Pide al Service que elimine el lugar (se rechaza si está en uso).
        public void EliminarLugarEvento(int id)
        {
            _service.EliminarLugarEvento(id);
        }

        // Devuelve la lista de lugares para mostrarla en la View.
        public List<LugarEvento> GetLugaresEvento()
        {
            return _service.GetLugaresEvento();
        }
    }
}
