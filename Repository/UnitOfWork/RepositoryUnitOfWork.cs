using Repository.Context;
using Repository.Interfaces;
using Repository.Repositories;
using System;

namespace Repository.UnitOfWork
{
    public class RepositoryUnitOfWork : IRepositoryUnitOfWork
    {
        private GlobitelDBContext _Context;

       
        public Lazy<IUserRoleRepository> UserRole { get; set; }

        public RepositoryUnitOfWork(GlobitelDBContext context )
        {
            _Context = context;



            UserRole = new Lazy<IUserRoleRepository>(() => new UserRoleRepository(_Context));
            
        }

        public void Dispose()
        {
            
            UserRole = null;
        }
    }
}
