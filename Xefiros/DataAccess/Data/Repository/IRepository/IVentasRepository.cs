using System.Threading.Tasks;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IVentasRepository : IRepository<Venta>
    {
        public Task<DataResponse<string>> Update(int ventaId, VentaCreateDto ventaDto);
    }
}