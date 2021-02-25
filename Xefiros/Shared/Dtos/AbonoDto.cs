using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xefiros.Shared.Dtos
{
    public class AbonoDto
    {
        public int Id { get; set; }
        public DateTime FechaAbono { get; set; } = DateTime.Now;
        [Required] public decimal CantidadAbono { get; set; }
    }
}