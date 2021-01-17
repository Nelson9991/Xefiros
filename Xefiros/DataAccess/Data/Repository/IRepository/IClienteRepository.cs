using System.Threading.Tasks;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<DataResponse<string>> Update<TDto>(int clienteId, TDto clienteDto) where TDto : class;
    }
}