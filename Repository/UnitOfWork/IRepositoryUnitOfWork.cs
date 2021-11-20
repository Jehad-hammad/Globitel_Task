using Repository.Interfaces;
using System;

namespace Repository.UnitOfWork
{
    public interface IRepositoryUnitOfWork : IDisposable
    {
        
        Lazy<IUserRoleRepository> UserRole { get; set; }
    }
}
