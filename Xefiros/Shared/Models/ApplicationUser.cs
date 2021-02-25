using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Xefiros.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required] public string Nombre { get; set; }
        [Required] public string Apellidos { get; set; }
    }
}
