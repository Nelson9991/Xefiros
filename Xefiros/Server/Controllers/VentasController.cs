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
    public class VentasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VentasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<VentaDto>>> GetAllAsync(
            int pageIndex = 0,
            int pageSize = 10,
            string filterColumn = null,
            string filterQuery = null,
            string includeProperties = null)
        {
            return await _unitOfWork.VentasRepository.GetAll<VentaDto>(pageIndex, pageSize, filterColumn,
                filterQuery, includeProperties);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VentaDto>> GetVentaWithRelationDataAsync(int id,
            [FromQuery] string includeProperties = null)
        {
            var response =
                await _unitOfWork.VentasRepository.GetFirstOrDefault<VentaDto>(x => x.Id == id, includeProperties);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response.Data;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> PostAsync(VentaCreateDto ventaCreateDto)
        {
            await _unitOfWork.VentasRepository.Add(ventaCreateDto);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("editar/{id:int}")]
        public async Task<ActionResult<DataResponse<string>>> PutAsync(int id, VentaCreateDto ventaCreateDto)
        {
            var response = await _unitOfWork.VentasRepository.Update(id, ventaCreateDto);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DataResponse<Venta>>> DeleteAsync(int id)
        {
            var response = await _unitOfWork.VentasRepository.Remove(id);

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
