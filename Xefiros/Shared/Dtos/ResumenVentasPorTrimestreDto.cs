namespace Xefiros.Shared.Dtos
{
    public class ResumenVentasPorTrimestreDto
    {
        public int Anio { get; set; }
        public decimal VentasQ1 { get; set; }
        public decimal VentasQ2 { get; set; }
        public decimal VentasQ3 { get; set; }
        public decimal VentasQ4 { get; set; }
    }
}