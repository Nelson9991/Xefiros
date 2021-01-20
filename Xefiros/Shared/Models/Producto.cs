using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xefiros.Shared.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required] public string Codigo { get; set; }
        [Required] public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(11,2)")]
        public decimal Precio { get; set; }

        [Required] public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
    }
}