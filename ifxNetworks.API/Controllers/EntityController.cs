using AutoMapper;
using Commons.RequestFilter;
using Commons.Response;
using FluentValidation;
using ifxNetworks.Core.DTOs.Request;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IEntityService _entityService;
        private readonly IMapper _mapper;
        private readonly IValidator<EntityRequestDTOCreate> _validatorCreate;
        private readonly IValidator<EntityRequestDTOUpdate> _validatorUpdate;
        public EntityController(IEntityService entityService, IMapper mapper,
            IValidator<EntityRequestDTOCreate> validatorCreate, IValidator<EntityRequestDTOUpdate> validatorUpdate)
        {
            _entityService = entityService;
            _mapper = mapper;
            _validatorUpdate = validatorUpdate;
            _validatorCreate = validatorCreate;
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost]

        public async Task<IActionResult> Post([FromBody] EntityRequestDTOCreate request)
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

            var response = await _entityService.Add(request);
            return Ok(response);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut]

        public async Task<IActionResult> Put([FromBody] EntityRequestDTOUpdate request)
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

            var response = await _entityService.Update(request);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve all entities
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RecordsResponse<EntityResponseDTO>> Get([FromQuery] QueryFilter filter)
        {
            var response = await _entityService.Get(filter);
            var result = _mapper.Map<RecordsResponse<EntityResponseDTO>>(response);

            return result;
        }

        /// <summary>
        /// Delete entity record
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _entityService.Delete(id);
            return Ok(response);
        }

        // GET: api/Entity/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityResponseDTO>> GetEntity(int id)
        {
            var entity = await _entityService.GetById(id);
            if (entity == null)
            {
                return NotFound(); // Retorna un 404 si la entidad no se encuentra
            }

            return Ok(entity); // Retorna la entidad si se encuentra
        }
    }
}
