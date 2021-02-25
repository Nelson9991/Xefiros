using System;
using System.Threading.Tasks;

namespace Xefiros.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IClienteRepository ClienteRepository { get; }
        public IProductoRepository ProductoRepository { get; }
        public IVentasRepository VentasRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }
        Task SaveAsync();
    }
}