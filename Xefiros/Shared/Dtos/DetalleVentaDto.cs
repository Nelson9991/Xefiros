using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xefiros.Shared.Models;

namespace Xefiros.Shared.Dtos
{
    public class DetalleVentaDto
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int Cantidad { get; set; } = 1;
        public int ProductoId { get; set; }
        public string CodigoProducto { get; set; }
        public int StockProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal Impuesto { get; set; }
    }
}