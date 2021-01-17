using System.ComponentModel.DataAnnotations;

namespace Xefiros.Shared.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "Solo debe ingresar un máximo de 150 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "Solo debe ingresar un máximo de 150 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [StringLength(10, ErrorMessage = "Solo debe ingresar un máximo de 10 dígitos")]
        [RegularExpression(@"^[0-9]+$")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Ingrese una dirección")]
        [StringLength(550, ErrorMessage = "Solo debe ingresar un máximo de 150 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$",
            ErrorMessage = "Ingrese un teléfono válido")]
        public string PhoneNumber { get; set; }
    }
}