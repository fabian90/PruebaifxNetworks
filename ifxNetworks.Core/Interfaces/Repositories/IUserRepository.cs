using Commons.Repository.Interfaces;
using Commons.RequestFilter;
using Commons.Response;
using ifxNetworks.Core.DTOs.Response;
using ifxNetworks.Core.Entities;

namespace ifxNetworks.Core.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByUserName(string userName);
        Task<RecordsResponse<UserResponseDTO>> Get(QueryFilter filter);

    }
}
