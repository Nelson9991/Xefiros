using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xefiros.Shared.Models;

namespace Xefiros.Shared.Dtos
{
    public class VentaCreateDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe elegir un cliente")]
        public int? ClienteId { get; set; }

        [Required(ErrorMessage = "El campo Tipo Comprobante es requerido")]
        public string TipoComprobante { get; set; }

        [Required(ErrorMessage = "El campo Número Comprobante es requerido")]
        public string NumeroComprobante { get; set; }

        [Required(ErrorMessage = "El campo Fecha Venta es requerido")]
        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Impuesto { get; set; } = 0.12M;

        public decimal Total { get; set; }
        public decimal TotalAbonado { get; set; }
        public decimal CantidadPagar { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Estado { get; set; }
        public List<Abono> Abonos { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
    }
}