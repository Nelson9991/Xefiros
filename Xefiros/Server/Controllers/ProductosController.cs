using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<ProductoDto>>> GetAllWithPagingAsync(
            int pageIndex = 0,
            int pageSize = 10,
            string filterColumn = null,
            string filterQuery = null,
            string includeProperties = null)
        {
            return await _unitOfWork.ProductoRepository.GetAllWithPaging<ProductoDto>(pageIndex, pageSize, filterColumn,
                filterQuery);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ProductoDto>>> GetAllNoPagingAsync()
        {
            return await _unitOfWork.ProductoRepository.GetAllNoPaging<ProductoDto>();
        }

        [HttpGet("venta")]
        public async Task<ActionResult<List<ProductoForVentaDto>>> GetAllForVentaAsync()
        {
            return await _unitOfWork.ProductoRepository.ObtenerProductosParaVenta();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoDto>> GetProductoAsync(int id)
        {
            var response = await _unitOfWork.ProductoRepository.Get<ProductoDto>(id);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response.Data;
        }

        [HttpGet("existe/{id}/{codigo}")]
        public async Task<bool> ExisteProducto(int id, string codigo)
        {
            return await _unitOfWork.ProductoRepository.ExisteProductoCodigo(codigo, id);
        }

        [HttpPost("disminuir-stock/{id:int}")]
        public async Task<IActionResult> DisminuirStockAsync([FromRoute] int id, [FromBody] int cantidadDisminuir)
        {
            var response = await _unitOfWork.ProductoRepository.DisminuirStock(id, cantidadDisminuir);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return Ok();
        }

        [HttpPost("crear")]
        public async Task<IActionResult> PostAsync(ProductoDto producto)
        {
            await _unitOfWork.ProductoRepository.AddProducto(producto);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("editar/{id:int}")]
        public async Task<ActionResult<DataResponse<string>>> PutAsync(int id, ProductoDto productoDto)
        {
            var response = await _unitOfWork.ProductoRepository.Update(id, productoDto);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DataResponse<Producto>>> DeleteAsync(int id)
        {
            var response = await _unitOfWork.ProductoRepository.RemoveProductoWithImage(id);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            await _unitOfWork.SaveAsync();
            response.Data = null;
            return response;
        }
    }
}
