using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Service;
using System;

namespace Luciano_Integrador_Evaluativo_Final.Controller
{
    // Controller: recibe los pedidos de la View y se los pasa al Service. No tiene lógica ni validaciones propias.
    internal class PlatilloController
    {
        private PlatilloService _service;

        // Crea el Service con el que trabaja este Controller.
        public PlatilloController()
        {
            _service = new PlatilloService();
        }

        // Pide al Service que valide los datos y registre un platillo nuevo.
        public void RegistrarPlatillo(PlatilloDTO dto)
        {
            _service.RegistrarPlatillo(dto);
        }

        // Pide al Service que modifique el platillo que tiene ese id con los datos nuevos.
        public void ActualizarPlatillo(int id, PlatilloDTO dto)
        {
            _service.ActualizarPlatillo(id, dto);
        }

        // Pide al Service que elimine el platillo (se rechaza si está en uso).
        public void EliminarPlatillo(int id)
        {
            _service.EliminarPlatillo(id);
        }

        // Devuelve la lista de platillos para mostrarla en la View.
        public List<Platillo> GetPlatillos()
        {
            return _service.GetPlatillos();
        }
    }
}
