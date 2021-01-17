using System.Threading.Tasks;
using AutoMapper;
using Xefiros.DataAccess.Data.Repository.IRepository;

namespace Xefiros.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            ClienteRepository = new ClienteRepository(context, mapper);
        }

        public IClienteRepository ClienteRepository { get; private set; }

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