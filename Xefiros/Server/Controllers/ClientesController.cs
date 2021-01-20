using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared;
using Xefiros.Shared.Dtos;
using Xefiros.Shared.Models;
using Xefiros.Utility.Helpers;

namespace Xefiros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<ClienteDto>>> GetAllAsync(
            int pageIndex = 0,
            int pageSize = 10,
            string filterColumn = null,
            string filterQuery = null,
            string includeProperties = null)
        {
            return await _unitOfWork.ClienteRepository.GetAll<ClienteDto>(pageIndex, pageSize, filterColumn,
                filterQuery);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteDto>> GetClienteAsync(int id)
        {
            var response = await _unitOfWork.ClienteRepository.Get<ClienteDto>(id);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response.Data;
        }

        [HttpGet("existe/{id}/{cedula}")]
        public async Task<bool> ExisteCliente(int id, string cedula)
        {
            return await _unitOfWork.ClienteRepository.ExisteClienteConCedula(cedula, id);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> PostAsync(ClienteDto cliente)
        {
            await _unitOfWork.ClienteRepository.Add(cliente);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("editar/{id:int}")]
        public async Task<ActionResult<DataResponse<string>>> PutAsync(int id, ClienteDto clienteDto)
        {
            var response = await _unitOfWork.ClienteRepository.Update(id, clienteDto);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DataResponse<string>>> DeleteAsync(int id)
        {
            var response = await _unitOfWork.ClienteRepository.Remove(id);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            await _unitOfWork.SaveAsync();
            return response;
        }
    }
}