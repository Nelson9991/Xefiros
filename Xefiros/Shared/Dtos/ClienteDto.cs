using System.ComponentModel.DataAnnotations;

namespace Xefiros.Shared.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "Solo debe ingresar un máximo de 150 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(150, ErrorMessage = "Solo debe ingresar un máximo de 150 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La cédula es requerida")]
        [MaxLength(10, ErrorMessage = "Solo debe ingresar un máximo de 10 dígitos")]
        [MinLength(10, ErrorMessage = "Debe ingresar un minimo de 10 dígitos")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "La cédula solo debe contener números")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        [MaxLength(550, ErrorMessage = "Solo debe ingresar un máximo de 150 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$",
            ErrorMessage = "Ingrese un teléfono válido")]
        public string PhoneNumber { get; set; }
    }
}