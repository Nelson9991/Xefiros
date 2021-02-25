using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xefiros.Shared.Models;

namespace Xefiros.Shared.Dtos
{
    public class VentaDto

    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? ClienteId { get; set; }
        public string TipoComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public decimal TotalAbonado { get; set; }
        public decimal CantidadPagar { get; set; }
        public string Estado { get; set; }
        public Cliente Cliente { get; set; }
    }
}