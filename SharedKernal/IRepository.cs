using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernal
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <typeparamref name="T" />.
        /// </returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Add Range of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken = default);
        /// <summary>
        /// Update Range of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> UpdateEntities(List<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an entity in the database
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Removes an entity in the database
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Removes the given entities in the database
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        /// <summary>
        /// Remove enties in range based on expression
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// Persists changes to the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// get firstordefault entity based on expression
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    }
}
