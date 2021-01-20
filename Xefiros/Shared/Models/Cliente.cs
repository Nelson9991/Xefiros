using System.ComponentModel.DataAnnotations;

namespace Xefiros.Shared.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required] [StringLength(150)] public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        public string Cedula { get; set; }

        [Required] [StringLength(550)] public string Direccion { get; set; }

        [Required]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
        public string PhoneNumber { get; set; }
    }
}