using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRoleRepository
    {
        UserRole Add(UserRole entity);
        IEnumerable<UserRole> Find(Expression<Func<UserRole, bool>> predicate, params Expression<Func<UserRole, object>>[] navigationProperties);
        UserRole FirstOrDefault(Expression<Func<UserRole, bool>> where, params Expression<Func<UserRole, object>>[] navigationProperties);
    }
}
