using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xefiros.Shared.Models
{
    public class Abono
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public DateTime FechaAbono { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(11,2)")] public decimal CantidadAbono { get; set; }
        public Venta Venta { get; set; }
    }
}