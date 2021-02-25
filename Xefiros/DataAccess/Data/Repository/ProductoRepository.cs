using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.DataAccess.Services.IServices;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility;
using Xefiros.Utility.Helpers;

namespace Xefiros.DataAccess.Data.Repository
{
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileUpload _fileUpload;

        public ProductoRepository(ApplicationDbContext context, IMapper mapper, IFileUpload fileUpload) : base(context,
            mapper)
        {
            _context = context;
            _mapper = mapper;
            _fileUpload = fileUpload;
        }

        public async Task AddProducto(ProductoDto productoDto)
        {
            var entity = _mapper.Map<Producto>(productoDto);

            if (!string.IsNullOrEmpty(productoDto.Imagen))
            {
                var imagen = Convert.FromBase64String(productoDto.Imagen);
                entity.Imagen = await _fileUpload.GuardarArchivo(imagen, productoDto.ExtensionImagen,
                    StorageContainerNames.ContenedorProductos);
            }

            await DbSet.AddAsync(entity);
        }

        public async Task<DataResponse<Producto>> RemoveProductoWithImage(int id)
        {
            var dataResponse = await Remove(id);

            if (dataResponse.Sussces)
            {
                await _fileUpload.EliminarArchivo(dataResponse.Data.Imagen, StorageContainerNames.ContenedorProductos);
            }

            return dataResponse;
        }

        public async Task<List<ProductoForVentaDto>> ObtenerProductosParaVenta()
        {
            return await _context.Productos.Select(x => new ProductoForVentaDto()
            {
                Codigo = x.Codigo,
                Id = x.Id,
                Nombre = x.Nombre,
                Stock = x.Stock,
                Precio = x.Precio,
            }).ToListAsync();
        }

        public async Task<DataResponse<string>> DisminuirStock(int prodId, int cantidadDisminuir)
        {
            var productoDb = await _context.Productos.FindAsync(prodId);

            if (productoDb is null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encontró el producto"
                };
            }

            productoDb.Stock -= cantidadDisminuir;
            await _context.SaveChangesAsync();

            return new DataResponse<string>()
            {
                Sussces = true
            };
        }

        public async Task<DataResponse<string>> Update(int prodId, ProductoDto prodDto)
        {
            var productoDb = await _context.Productos.FindAsync(prodId);

            if (productoDb is null)
            {
                return new DataResponse<string>
                {
                    Sussces = false,
                    Message = "No se encontró el producto para actualizar"
                };
            }

            _mapper.Map(prodDto, productoDb);

            if (!string.IsNullOrEmpty(prodDto.Imagen))
            {
                var imagen = Convert.FromBase64String(prodDto.Imagen);
                productoDb.Imagen = await _fileUpload.EditarArchivo(imagen, prodDto.ExtensionImagen,
                    StorageContainerNames.ContenedorProductos, productoDb.Imagen);
            }

            await _context.SaveChangesAsync();

            return new DataResponse<string>
            {
                Sussces = true,
                Message = "Producto actualizado correctamente!"
            };
        }

        public async Task<bool> ExisteProductoCodigo(string codigo, int prodId = 0)
        {
            if (prodId == 0)
            {
                return await _context.Productos.AnyAsync(x => x.Codigo.ToLower() == codigo.ToLower());
            }

            return await _context.Productos.AnyAsync(x => x.Codigo.ToLower() == codigo.ToLower() && x.Id != prodId);
        }
    }
}