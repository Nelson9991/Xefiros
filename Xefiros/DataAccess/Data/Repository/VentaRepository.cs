using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository
{
    public class VentaRepository : Repository<Venta>, IVentasRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VentaRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataResponse<string>> Update(int ventaId, VentaCreateDto ventaCreateDto)
        {
            var ventaDb = await _context.Ventas.FindAsync(ventaId);

            if (ventaDb is null)
            {
                return new DataResponse<string>()
                {
                    Sussces = false,
                    Message = "No se encontró la venta para actualizar"
                };
            }

            _mapper.Map(ventaCreateDto, ventaDb);
            await _context.SaveChangesAsync();

            return new DataResponse<string>()
            {
                Sussces = false,
                Message = "Venta actualizada con éxito"
            };
        }
    }
}