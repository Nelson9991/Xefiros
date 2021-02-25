using System.Threading.Tasks;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<ApplicationUser>
    {
        public Task<DataResponse<string>> LockUnlockUserAsync(string id);
    }
}