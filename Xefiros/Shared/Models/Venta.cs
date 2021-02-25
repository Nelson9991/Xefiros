using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xefiros.Shared.Models
{
    public class Venta
    {
        public int Id { get; set; }
        [Required] public string UserId { get; set; }
        [Required] public string UserName { get; set; }
        public int? ClienteId { get; set; }
        [Required] public string TipoComprobante { get; set; }
        [Required] public string NumeroComprobante { get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(4,2)")] public decimal Impuesto { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal Total { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal TotalAbonado { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal CantidadPagar { get; set; }
        public string Estado { get; set; }
        public Cliente Cliente { get; set; }
        public List<Abono> Abonos { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
    }
}