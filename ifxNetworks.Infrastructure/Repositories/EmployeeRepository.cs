using Commons.Mapping;
using Commons.Paging;
using Commons.Repository.Repository;
using Commons.RequestFilter;
using Commons.Response;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Repositories;
using ifxNetworks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ifxNetworks.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee, IdentityDBContext>, IEmployeeRepository
    {
        protected readonly IdentityDBContext _context;

        public EmployeeRepository(IdentityDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee> GetByEmail(string? FirstName)
        {
            return await _context.Employee.FirstOrDefaultAsync(u => u.FirstName == FirstName);
        }

        public async Task<RecordsResponse<EmployeeResponseDTO>> Get(QueryFilter filter)
        {
            if (filter.Page < 1) filter.Page = 1; // Asegura que la página mínima sea 1
            if (filter.Take < 1) filter.Take = 10;
            // Obtiene los empleados activos, ordenados por Id y aplicando la paginación
            var response = await _context.Employee
                .Where(x => x.IsActive) // Filtra solo los empleados activos
                .OrderBy(x => x.Id) // Ordena por Id
                .Select(x => new EmployeeResponseDTO
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Phone = x.Phone, // Campo adicional
                    Position = x.Position, // Campo adicional
                    IsActive = x.IsActive,
                    EntityId = x.EntityId ?? 0, // Clave foránea a la Entidad
                    EntityName = x.Entity.Name ??"sin empresa" // Obtiene el nombre de la entidad asociada
                })
                .GetPagedAsync(filter.Page, filter.Take); // Aplica paginación
            return response.MapTo<RecordsResponse<EmployeeResponseDTO>>()!;
        }
    }
}
