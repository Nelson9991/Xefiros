using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Xefiros.Shared.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal Precio { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal Descuento { get; set; }
        [Column(TypeName = "decimal(11,2)")] public decimal Total { get; set; }
        [JsonIgnore] public Venta Venta { get; set; }
        [JsonIgnore] public Producto Producto { get; set; }
    }
}