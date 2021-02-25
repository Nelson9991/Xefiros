using System.Collections.Generic;
using System.Threading.Tasks;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IVentasRepository : IRepository<Venta>
    {
        public Task<DataResponse<string>> Update(int ventaId, VentaCreateDto ventaDto);
        public Task<DataResponse<List<AbonoDto>>> ObtenerAbonos(int ventaId);
        public Task<DataResponse<string>> EliminarAbono(int abonoId);
        public Task<DataResponse<List<DetalleVentaDto>>> ObtenerDetalles(int ventaId);
        public Task<DataResponse<string>> EliminarDetalle(int detalleVentaId);
        public Task<DataResponse<ResumenVentasPorTrimestreDto>> GetResumenVentasTrimestral();
        public Task<ResumenVentasAnualesDto> GetResumenVentasAnual();
    }
}