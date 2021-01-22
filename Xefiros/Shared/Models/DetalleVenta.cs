using System.ComponentModel.DataAnnotations.Schema;

namespace Xefiros.Shared.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int? ProductoId { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal Precio { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal Descuento { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
    }
}