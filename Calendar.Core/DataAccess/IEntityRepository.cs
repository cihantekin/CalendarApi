using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Calendar.Core.DataAccess
{
    public interface IEntityRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}
