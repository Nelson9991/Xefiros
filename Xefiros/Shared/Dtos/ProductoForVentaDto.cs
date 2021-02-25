using System.ComponentModel.DataAnnotations;

namespace Xefiros.Shared.Dtos
{
    public class ProductoForVentaDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public int Cantidad { get; set; } = 1;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Precio { get; set; }
    }
}