﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClienteRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataResponse<string>> Update<TDto>(int clienteId, TDto clienteDto)
            where TDto : class
        {
            var clienteDb = await _context.Clientes.FindAsync(clienteId);

            if (clienteDb is null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encontró el cliente para actualizar"
                };
            }

            _mapper.Map(clienteDto, clienteDb);

            await _context.SaveChangesAsync();

            return new DataResponse<string>
            {
                Sussces = true,
                Message = "Cliente actualizado correctamente!"
            };
        }

        public async Task<bool> ExisteClienteConCedula(string cedula, int clienteId = 0)
        {
            if (clienteId == 0)
            {
                return await _context.Clientes.AnyAsync(x => x.Cedula == cedula);
            }

            return await _context.Clientes.AnyAsync(x => x.Cedula == cedula && x.Id != clienteId);
        }

        public async Task<List<ClienteDropDownDto>> GetClientesForDropDown()
        {
            return await _context.Clientes.Select(x => new ClienteDropDownDto()
            {
                Id = x.Id,
                Nombres = x.Nombres,
                Apellidos = x.Apellidos,
                Cedula = x.Cedula,
                ClienteDetalles = $"{x.Nombres} {x.Apellidos} - Cédula: {x.Cedula}"
            }).ToListAsync();
        }
    }
}