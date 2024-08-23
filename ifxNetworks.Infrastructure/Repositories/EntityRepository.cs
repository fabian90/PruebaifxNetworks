using Commons.Repository.Repository;
using Commons.Mapping;
using Commons.RequestFilter;
using Commons.Response;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Repositories;
using ifxNetworks.Infrastructure.Data;
using Commons.Paging;
using Microsoft.EntityFrameworkCore;

namespace ifxNetworks.Infrastructure.Repositories
{
    public class EntityRepository : GenericRepository<Entity, IdentityDBContext>, IEntityRepository
    {
        protected readonly IdentityDBContext _context;

        public EntityRepository(IdentityDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Entity?> GetByName(string name)
        {
            return await _context.Entity.FirstOrDefaultAsync(e => e.Name == name && e.IsActive);
        }

        public async Task<RecordsResponse<EntityResponseDTO>> Get(QueryFilter filter)
        {
            if (filter.Page < 1) filter.Page = 1; // Asegura que la página mínima sea 1
            if (filter.Take < 1) filter.Take = 10;
            var response = await _context.Entity
                .Where(e => e.IsActive)
                .OrderBy(x => x.Id)
                .GetPagedAsync(filter.Page, filter.Take);

            return response.MapTo<RecordsResponse<EntityResponseDTO>>()!;
        }
    }
}
