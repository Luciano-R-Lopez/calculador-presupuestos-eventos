using Luciano_Integrador_Evaluativo_Final.Dto;
using Luciano_Integrador_Evaluativo_Final.Model;
using Luciano_Integrador_Evaluativo_Final.Comun;
using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Service
{
    // Datos de ejemplo. Usa los mismos Services que la app (mismas validaciones).
    internal class DatosDePruebaService
    {
        private ClienteService _clienteService;
        private LugarEventoService _lugarService;
        private ServicioAdicionalService _servicioService;
        private PlatilloService _platilloService;
        private MenuService _menuService;

        public DatosDePruebaService()
        {
            _clienteService = new ClienteService();
            _lugarService = new LugarEventoService();
            _servicioService = new ServicioAdicionalService();
            _platilloService = new PlatilloService();
            _menuService = new MenuService();
        }

        // En orden: los menús necesitan los platillos.
        public void Cargar()
        {
            // Solo si la base está vacía (primera vez que se abre).
            if (_clienteService.GetClientes().Count > 0 || _lugarService.GetLugaresEvento().Count > 0
                || _servicioService.GetServiciosAdicionales().Count > 0 || _platilloService.GetPlatillos().Count > 0
                || _menuService.GetMenus().Count > 0)
                return;

            CargarClientes();
            CargarLugares();
            CargarServicios();
            CargarPlatillos();
            CargarMenus();
        }

        private void CargarClientes()
        {
            _clienteService.RegistrarCliente(new ClienteDTO("María González", "351-4567890", "maria.gonzalez@mail.com"));
            _clienteService.RegistrarCliente(new ClienteDTO("Empresa Altos del Sur S.A.", "351-4123456", "eventos@altosdelsur.com"));
        }

        private void CargarLugares()
        {
            _lugarService.RegistrarLugarEvento(new LugarEventoDTO("Salón Los Álamos", 450000));
            _lugarService.RegistrarLugarEvento(new LugarEventoDTO("Quinta La Estancia", 780000));
        }

        private void CargarServicios()
        {
            _servicioService.RegistrarServicioAdicional(new ServicioAdicionalDTO("DJ y sonido", 120000));
            _servicioService.RegistrarServicioAdicional(new ServicioAdicionalDTO("Decoración temática", 95000));
        }

        private void CargarPlatillos()
        {
            // Ids en orden: 1 = Ensalada Caprese, 2 = Milanesa con papas.
            _platilloService.RegistrarPlatillo(new PlatilloDTO("Ensalada Caprese", 3500, TipoPlatillo.Entrada));
            _platilloService.RegistrarPlatillo(new PlatilloDTO("Milanesa con papas", 7500, TipoPlatillo.Principal));
        }

        // Usan los platillos anteriores (por id).
        private void CargarMenus()
        {
            List<int> platillosClasico = new List<int>();
            platillosClasico.Add(1);
            platillosClasico.Add(2);
            _menuService.RegistrarMenu(new MenuDTO("Menú Clásico", platillosClasico, new List<Aptitud>()));

            List<int> platillosVegetariano = new List<int>();
            platillosVegetariano.Add(1);
            List<Aptitud> aptitudes = new List<Aptitud>();
            aptitudes.Add(Aptitud.Vegetarianos);
            _menuService.RegistrarMenu(new MenuDTO("Menú Vegetariano", platillosVegetariano, aptitudes));
        }
    }
}
