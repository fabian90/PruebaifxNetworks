namespace ifxNetworks.Core.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IAuthenticateRepository AuthenticateRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IEntityRepository EntityRepository { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
