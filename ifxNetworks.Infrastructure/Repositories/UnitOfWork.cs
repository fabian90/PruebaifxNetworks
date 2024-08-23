using ifxNetworks.Core.Interfaces.Repositories;
using ifxNetworks.Infrastructure.Data;

namespace ifxNetworks.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDBContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticateRepository _authenticateRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEntityRepository _entityRepository;
        
        public UnitOfWork(IdentityDBContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        public IAuthenticateRepository AuthenticateRepository => _authenticateRepository ?? new AuthenticateRepository(_context);
        public IEmployeeRepository EmployeeRepository => _employeeRepository ?? new EmployeeRepository(_context); // Instantiate EmployeeRepository
        public IEntityRepository EntityRepository => _entityRepository ?? new EntityRepository(_context); // Instantiate EntityRepository

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            
        }
    }
}
