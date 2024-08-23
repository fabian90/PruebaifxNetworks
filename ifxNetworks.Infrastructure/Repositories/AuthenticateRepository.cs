using Commons.Repository.Repository;
using ifxNetworks.Core.Entities;
using ifxNetworks.Core.Interfaces.Repositories;
using ifxNetworks.Infrastructure.Data;

namespace ifxNetworks.Infrastructure.Repositories
{
    public class AuthenticateRepository : GenericRepository<User, IdentityDBContext>, IAuthenticateRepository
    {
        protected readonly IdentityDBContext _context;

        public AuthenticateRepository(IdentityDBContext context) : base(context)
        {
            _context = context;
        }     
    }
}

