using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthorWorld.Domain.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetQuery();
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);
        void Add(TEntity entity);
        void Delete(TEntity entity);
    }
}
