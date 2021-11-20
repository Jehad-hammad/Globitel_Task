using Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.Context;
using Repository.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Common
{
    public class Repository<IEntity> : IRepository<IEntity> where IEntity : class
    {
        #region [Context]
        protected GlobitelDBContext Context;
        #endregion

        public Repository(GlobitelDBContext context)
        {
            Context = context;
        }

        #region GetAll
        public IEnumerable<IEntity> GetAll()
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();
            return dbQuery.AsNoTracking();
        }

        #endregion

        #region FirstOrDefault
        public IEntity FirstOrDefault(Expression<Func<IEntity, bool>> where, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<IEntity, object>(navigationProperty);
            }

            return dbQuery.Where(where).FirstOrDefault();
        }
        #endregion

        #region Find
        public IQueryable<IEntity> Find(Expression<Func<IEntity, bool>> predicate, params Expression<Func<IEntity, object>>[] navigationProperties)
        {
            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<IEntity, object>(navigationProperty);
            }

            return dbQuery.Where(predicate).AsNoTracking();
        }
        #endregion

        public ListResponse<IEntity> GetList(Expression<Func<IEntity, bool>> predicate,
                                             int PageSize,
                                             int PageNumber,
                                             params Expression<Func<IEntity, object>>[] navigationProperties)
        {

            IQueryable<IEntity> dbQuery = Context.Set<IEntity>();

            foreach (Expression<Func<IEntity, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<IEntity, object>(navigationProperty);
            }

            return new ListResponse<IEntity>
            {
                TotalCount = (dbQuery.Count(predicate) + PageSize - 1) / PageSize,
                TotalRecords = dbQuery.Count(predicate),
                entities = dbQuery.Where(predicate).Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList()
            };
        }

        #region Add
        public IEntity Add(IEntity entity)
        {
            Context.Set<IEntity>().Add(entity);
            SaveChanges();
            Context.Entry(entity).GetDatabaseValues();
            return entity;
        }
        #endregion

        #region AddRnage
        public IEnumerable<IEntity> AddRange(IEnumerable<IEntity> entities)
        {
            Context.ChangeTracker.Entries<IEntity>();
            Context.Set<IEntity>().AddRange(entities);
            SaveChanges();
            return entities;
        }
        #endregion

        #region Update
        public IEntity Update(IEntity entity, bool disableAttach = false)
        {

            Context.Set<IEntity>().Update(entity);
            Context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
            return entity;
        }
        #endregion

        #region UpdateRange
        public IEnumerable<IEntity> UpdateRange(IEnumerable<IEntity> Entities)
        {
            Context.Set<IEntity>().UpdateRange(Entities);
            SaveChanges();
            return Entities;
        }
        #endregion

        #region Remove
        public IEntity Remove(IEntity entity)
        {
            Context.Set<IEntity>().Remove(entity);
            SaveChanges();
            return entity;
        }
        #endregion

        #region RemoveRange
        public IEnumerable<IEntity> RemoveRange(IEnumerable<IEntity> entities)
        {
            Context.Set<IEntity>().RemoveRange(entities);
            SaveChanges();
            return entities;
        }
        #endregion

        #region SaveChanges
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        #endregion

        #region BeginTransaction
        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }
        #endregion

        #region CommitTransaction
        public void CommitTransaction()
        {
            Context.Database.CommitTransaction();
        }
        #endregion

        #region RollbackTransaction
        public void RollbackTransaction()
        {
            Context.Database.RollbackTransaction();
        }

        #endregion

        public bool Any(Expression<Func<IEntity, bool>> where)
        {
            return Context.Set<IEntity>().Any(where);
        }

        public int Count(Expression<Func<IEntity, bool>> where)
        {
            return Context.Set<IEntity>().Where(where).Count();
        }
    }
}
