using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataResponse<string>> EliminarUserAsync(string id)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encotró el usuario ha borrar"
                };
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return new DataResponse<string>
            {
                Sussces = true,
                Message = "Usuario Eliminado con éxito"
            };
        }

        public async Task<DataResponse<string>> LockUnlockUserAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return new DataResponse<string>()
                {
                    Sussces = false,
                    Message = "No se pudo encontrar el usuario"
                };
            }

            if (user.LockoutEnd is not null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTimeOffset.Now;
            }
            else
            {
                user.LockoutEnd = DateTimeOffset.Now.AddYears(100);
            }

            await _context.SaveChangesAsync();
            return new DataResponse<string>()
            {
                Sussces = true,
                Message = "Bloqueo/Desbloqueo exitoso"
            };
        }
    }
}