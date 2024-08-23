using Commons.Response;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ifxNetworks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _service;

        public AuthenticateController(IAuthenticateService service)
        {
            _service = service;
        }

        // GET: api/<AuthenticateController>
        [HttpGet("Authenticate")]
        public ApiResponse<UserAuthResponseDTO> Get(string username, string password)
        {
            try
            {
                UserAuthResponseDTO response = null;

                response = _service.ValidateUser(username, password); 

                if (response == null)
                {
                    return new ApiResponse<UserAuthResponseDTO>()
                    {
                        Message = "Usuario o contraseña incorrecta",
                        Success = false,
                    };
                }
                else
                {
                    return new ApiResponse<UserAuthResponseDTO>()
                    {
                        Message = "Usuario validado correctamente",
                        Success = true,
                        Data = response
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserAuthResponseDTO>()
                {
                    Message = "Se ha producido un error. Cosulte con el administrador del sistema",
                    Success = false,
                };
            }
        }
    }
}
