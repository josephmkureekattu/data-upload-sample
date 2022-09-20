using Microsoft.EntityFrameworkCore;
using SharedKernal;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Persistence
{
    public class EFRepository<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext dbContext;

        /// <inheritdoc/>
        public EFRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Add(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        ///<inheritdoc/>
        public virtual async Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default)
        {
            await dbContext.Set<T>().AddRangeAsync(entities);

            await SaveChangesAsync(cancellationToken);

            return entities;
        }
        public virtual async Task<List<T>> UpdateEntities(List<T> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
            }
            await SaveChangesAsync(cancellationToken);
            return entities;
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().RemoveRange(entities);

            await SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = dbContext.Set<T>().AsNoTracking();
            if (filter != null)
                query = query.Where(filter);

            dbContext.Set<T>().RemoveRange(query);

            await SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);

        }

        public virtual async Task<List<T?>> Exists(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = dbContext.Set<T>().AsNoTracking();
            if (filter != null)
                query = query.Where(filter);

            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = dbContext.Set<T>().AsNoTracking();
            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().ToListAsync(cancellationToken);
        }


        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().CountAsync(cancellationToken);
        }


        /// <inheritdoc/>
        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().AnyAsync(cancellationToken);
        }

        public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<List<T>> GetAsync(string sqlQuery, CancellationToken cancellationToken = default, params object[] sqlParameters)
        {
            return await dbContext.Set<T>().FromSqlRaw(sqlQuery, sqlParameters).ToListAsync(cancellationToken);
        }

        public virtual IQueryable<T> Query(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public IQueryable<TResult> GetWhereResult<TResult>(Expression<Func<T, bool>> predicate,
                                                           Expression<Func<T, TResult>> selector,
                                                           string[]? includeNavigationProperties = null,
                                                           string? sort = null)
        {
            var queryable = GetAllResult().Where(predicate);
            if (includeNavigationProperties != null && includeNavigationProperties.Any())
            {
                foreach (var prop in includeNavigationProperties)
                {
                    var hierarchyProps = prop.Split(".");
                    for (var i = 0; i < hierarchyProps.Length; i++)

                    {
                        var navigationPropertyPath = string.Join(".", hierarchyProps.Take(i + 1));
                        queryable = queryable.Include(navigationPropertyPath);
                    }
                }
            }
            var resQueryable = queryable.Select(selector);
            if (!string.IsNullOrEmpty(sort))
                resQueryable = resQueryable.OrderBy(sort);
            return resQueryable;
        }

        private DbSet<T> GetAllResult()
        {
            return dbContext.Set<T>();
        }
    }
}
