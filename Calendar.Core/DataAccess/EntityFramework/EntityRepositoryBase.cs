using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Calendar.Core.DataAccess.EntityFramework
{
    public class EntityRepositoryBase<TEntity, TContext, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TContext : DbContext, new()
    {
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new TContext();
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null ? await context.Set<TEntity>().ToListAsync() : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await using var context = new TContext();
            var addedEntity = context.Entry(entity);

            addedEntity.Entity.CreationTime = DateTime.Now;
            addedEntity.Entity.IsDeleted = false;

            addedEntity.State = EntityState.Added;

            await context.SaveChangesAsync();

            return addedEntity.Entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            await using var context = new TContext();
            var modifiedEntity = context.Entry(entity);

            modifiedEntity.Entity.LastModificationTime = DateTime.Now;

            modifiedEntity.State = EntityState.Modified;

            return await context.SaveChangesAsync() > 0;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await using var context = new TContext();
            var deletedEntity = context.Entry(entity);

            deletedEntity.Entity.DeletionTime = DateTime.Now;
            deletedEntity.Entity.IsDeleted = true;

            deletedEntity.State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            await using var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null) query = query.Where(filter);


            return await query.ToListAsync();
        }
    }
}