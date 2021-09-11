using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Domain.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private AppDbContext context;

        private DbSet<TEntity> dbSet;
        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException(entity.GetType().Name);

            dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new NullReferenceException(entity.GetType().Name);

            dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return dbSet;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Single(predicate);
        }

        public TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.SingleOrDefault(predicate);
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>>[] includes)
        {
            IIncludableQueryable<TEntity, object> query = null;

            if (includes.Length > 0)
            {
                query = dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? dbSet : (IQueryable<TEntity>)query;
        }
    }
}
