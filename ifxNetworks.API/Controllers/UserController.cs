using AutoMapper;
using Commons.RequestFilter;
using Commons.Response;
using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    //[Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<UserRequestDTO> _validator;

        public UserController(IUserService userService, IMapper mapper, IPasswordService passwordService, 
            IValidator<UserRequestDTO> validator)
        {
            _userService = userService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequestDTO request)
        {
            var response = await _userService.Add(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserRequestDTO request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new ValidationResult()
                {
                    Code = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var response = await _userService.Update(request);

            return Ok(response);
        }

        /// <summary>
        /// Retrieve all users
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RecordsResponse<UserResponseDTO>> Get([FromQuery] QueryFilter filter)
        {
            var response = await _userService.Get(filter);
            var result = _mapper.Map<RecordsResponse<UserResponseDTO>>(response);

            return result;
        }

        /// <summary>
        /// Delete user account
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.Delete(id);
            return Ok(response);
        }

    }
}
