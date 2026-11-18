using System;
using System.Collections.Generic;

namespace Luciano_Integrador_Evaluativo_Final.Model
{
    // Renglón de servicio del presupuesto: el servicio y su precio al cotizar (no cambia si después sube).
    internal class ServicioPresupuestado : IItemPresupuesto
    {
        private int _id;
        private ServicioAdicional _servicio;
        private decimal _precio;

        // Para Entity Framework.
        private ServicioPresupuestado()
        {
            _servicio = null!;
        }
        // Toma el precio actual del servicio.
        public ServicioPresupuestado(ServicioAdicional servicio)
        {
            Servicio = servicio;
            Precio = servicio.Precio;
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public ServicioAdicional Servicio
        {
            get { return _servicio; }
            set { _servicio = value; }
        }
        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }

        // Renglón nuevo con el mismo servicio y precio (para clonar).
        public ServicioPresupuestado Copiar()
        {
            ServicioPresupuestado copia = new ServicioPresupuestado(Servicio);
            copia.Precio = Precio;
            return copia;
        }

        // IItemPresupuesto: usa el precio guardado, no el actual.
        public decimal CalcularMonto()
        {
            return Precio;
        }
    }
}
