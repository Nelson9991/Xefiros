using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xefiros.DataAccess.Data.Repository.IRepository;
using Xefiros.Shared.Dtos;
using Xefiros.Utility.Helpers;

namespace Xefiros.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<UserDto>>> GetAllAsync(
            int pageIndex = 0,
            int pageSize = 10,
            string filterColumn = null,
            string filterQuery = null,
            string includeProperties = null)
        {
            return await _unitOfWork.UsuarioRepository.GetAllWithPaging<UserDto>(pageIndex, pageSize, filterColumn,
                filterQuery);
        }

        [HttpPost("lockUnlock")]
        public async Task<ActionResult<DataResponse<string>>> LockUnlockAsync([FromBody] string id)
        {
            var response = await _unitOfWork.UsuarioRepository.LockUnlockUserAsync(id);

            if (!response.Sussces)
            {
                return NotFound(response.Message);
            }

            return response;
        }
    }
}