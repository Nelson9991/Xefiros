using System;
using System.Collections.Generic;

namespace Xefiros.Shared.Dtos
{
    public class ResumenVentasAnualesDto
    {
        public List<ItemResumenAnualVenta> AnioAnterior { get; set; }
        public List<ItemResumenAnualVenta> AnioActual { get; set; }
    }

    public class ItemResumenAnualVenta
    {
        public DateTime Fecha { get; set; }
        public decimal TotalVentas { get; set; }
    }
}