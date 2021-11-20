using Domain.DTO;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Common
{
    public interface IRepository<IEntity> where IEntity : class
    {
        IEnumerable<IEntity> GetAll();
        IEntity Add(IEntity entity);
        bool Any(Expression<Func<IEntity, bool>> where);
        int Count(Expression<Func<IEntity, bool>> where);

        IEnumerable<IEntity> AddRange(IEnumerable<IEntity> entities);
        IEntity Update(IEntity entity, bool disableAttach = false);
        IEnumerable<IEntity> UpdateRange(IEnumerable<IEntity> Entities);
        IEntity Remove(IEntity entity);
        IEnumerable<IEntity> RemoveRange(IEnumerable<IEntity> entities);
        IQueryable<IEntity> Find(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties);
        IEntity FirstOrDefault(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties);
        ListResponse<IEntity> GetList(Expression<Func<IEntity, bool>> predicate, int PageSize, int PageNumber, params Expression<Func<IEntity, object>>[] navigationProperties);
        void SaveChanges();
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
