using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Dto
{
    // Datos de un presupuesto entre la View y el Service. Cliente, lugar y servicios viajan como ids;
    // los menús, como ItemMenuDTO.
    // Id 0 = lo arma la View para registrar o editar. Con id, viene del Service para mostrarlo:
    // trae además fecha de creación, estado, nombres de cliente y lugar, y el monto guardado.
    internal class PresupuestoDTO
    {
        private int _id;
        private DateTime _fechaCreacion;
        private DateTime _fechaEvento;
        private EstadoPresupuesto _estado;
        private int _cantidadHoras;
        private int _idCliente;
        private string _cliente;
        private int _idLugar;
        private string _lugar;
        private List<int> _idsServiciosAdicionales;
        private List<ItemMenuDTO> _menus;
        private decimal _montoTotal;

        public PresupuestoDTO(DateTime fechaEvento, int cantidadHoras, int idCliente, int idLugar,
            List<int> idsServiciosAdicionales, List<ItemMenuDTO> menus)
        {
            FechaEvento = fechaEvento;
            CantidadHoras = cantidadHoras;
            IdCliente = idCliente;
            IdLugar = idLugar;
            IdsServiciosAdicionales = idsServiciosAdicionales;
            Menus = menus;
            _cliente = "";
            _lugar = "";
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        public DateTime FechaEvento
        {
            get { return _fechaEvento; }
            set { _fechaEvento = value; }
        }
        public EstadoPresupuesto Estado
        {
            get { return _estado; }
            set { _estado = value; }
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
        // Nombre del cliente, para la grilla.
        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        public int IdLugar
        {
            get { return _idLugar; }
            set { _idLugar = value; }
        }
        // Nombre del lugar, para la grilla.
        public string Lugar
        {
            get { return _lugar; }
            set { _lugar = value; }
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
        // Monto guardado al cotizar (no se recalcula con los precios actuales).
        public decimal MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
    }
}
