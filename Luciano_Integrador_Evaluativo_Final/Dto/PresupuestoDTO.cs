using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // DTO (objeto de transferencia de datos): lleva solo los datos que carga el usuario, sin id ni lógica.
    // Cliente, lugar y servicios viajan como ids; los menús, como una lista de ItemMenuDTO.
    internal class PresupuestoDTO
    {
        private DateTime _fechaEvento;
        private int _cantidadHoras;
        private int _idCliente;
        private int _idLugar;
        private List<int> _idsServiciosAdicionales;
        private List<ItemMenuDTO> _menus;

        // Arma el DTO con todo lo que se carga en el formulario de presupuestos.
        public PresupuestoDTO(DateTime fechaEvento, int cantidadHoras, int idCliente, int idLugar,
            List<int> idsServiciosAdicionales, List<ItemMenuDTO> menus)
        {
            FechaEvento = fechaEvento;
            CantidadHoras = cantidadHoras;
            IdCliente = idCliente;
            IdLugar = idLugar;
            IdsServiciosAdicionales = idsServiciosAdicionales;
            Menus = menus;
        }

        // Propiedades: dan acceso a los campos privados.
        public DateTime FechaEvento
        {
            get { return _fechaEvento; }
            set { _fechaEvento = value; }
        }
        public int CantidadHoras
        {
            get { return _cantidadHoras; }
            set { _cantidadHoras = value; }
        }
        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }
        public int IdLugar
        {
            get { return _idLugar; }
            set { _idLugar = value; }
        }
        public List<int> IdsServiciosAdicionales
        {
            get { return _idsServiciosAdicionales; }
            set { _idsServiciosAdicionales = value; }
        }
        public List<ItemMenuDTO> Menus
        {
            get { return _menus; }
            set { _menus = value; }
        }
    }
}
