using AutoMapper;
using Commons.RequestFilter;
using Commons.Response;
using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Services;
using ifxNetworks.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeRequestDTOUpdate> _validatorUpdate;
        private readonly IValidator<EmployeeRequestDTOCreate> _validatorCreate;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper,
            IValidator<EmployeeRequestDTOUpdate> validatorUpdate, IValidator<EmployeeRequestDTOCreate> validatorCreate)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _validatorUpdate = validatorUpdate;
            _validatorCreate = validatorCreate;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeRequestDTOCreate request)
        {
            var validation = await _validatorCreate.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new ValidationResult()
                {
                    Code = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var response = await _employeeService.Add(request);
            return Ok(response);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeRequestDTOUpdate request)
        {
            var validation = await _validatorUpdate.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors?.Select(e => new ValidationResult()
                {
                    Code = e.ErrorCode,
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                }));
            }

            var response = await _employeeService.Update(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all employees
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RecordsResponse<EmployeeResponseDTO>> Get([FromQuery] QueryFilter filter)
        {
            var response = await _employeeService.Get(filter);
            var result = _mapper.Map<RecordsResponse<EmployeeResponseDTO>>(response);

            return result;
        }

        /// <summary>
        /// Delete employee record
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _employeeService.Delete(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeResponseDTO>> GetEntity(int id)
        {
            var entity = await _employeeService.GetById(id);
            if (entity == null)
            {
                return NotFound(); // Retorna un 404 si la entidad no se encuentra
            }

            return Ok(entity); // Retorna la entidad si se encuentra
        }
    }
}
