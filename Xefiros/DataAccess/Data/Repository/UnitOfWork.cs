using System.Threading.Tasks;
using AutoMapper;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.DataAccess.Services.IServices;

namespace Xefiros.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IMapper mapper, IFileUpload fileUpload)
        {
            _context = context;
            ClienteRepository = new ClienteRepository(context, mapper);
            ProductoRepository = new ProductoRepository(context, mapper, fileUpload);
        }

        public IClienteRepository ClienteRepository { get; private set; }
        public IProductoRepository ProductoRepository { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}