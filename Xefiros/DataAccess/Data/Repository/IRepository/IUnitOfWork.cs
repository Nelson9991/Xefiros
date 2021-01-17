using System;
using System.Threading.Tasks;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IClienteRepository ClienteRepository { get; }
        Task SaveAsync();
    }
}