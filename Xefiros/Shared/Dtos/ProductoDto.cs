using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xefiros.Shared.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Código es requerido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Stock { get; set; }

        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string ExtensionImagen { get; set; }
    }
}