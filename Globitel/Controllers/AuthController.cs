using Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Globitel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        public AuthController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO oLoginRequestDTO)
        {
            LoginResponseDTO tokenResponseDTO = await _serviceUnitOfWork.Auth.Value.Login(oLoginRequestDTO);
            return Ok(tokenResponseDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO oRegisterDTO)
        {
            try
            {
                var user = await _serviceUnitOfWork.Auth.Value.Register(oRegisterDTO);
                return Ok(user);
            }
            catch (Exception e)
            {

                throw;
            }
           
        }
    }
}
