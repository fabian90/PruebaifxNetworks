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
    public class UserRepository : GenericRepository<User, IdentityDBContext>, IUserRepository
    {
        protected readonly IdentityDBContext _context;

        public UserRepository(IdentityDBContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.IsActive == true);
        }

        public async Task<RecordsResponse<UserResponseDTO>> Get(QueryFilter filter)
        {
            var response = await _context.Users.OrderBy(x => x.Id).Where(u => u.IsActive == true).GetPagedAsync(filter.Page, filter.Take);
            return response.MapTo<RecordsResponse<UserResponseDTO>>()!;
        }
    }
}
