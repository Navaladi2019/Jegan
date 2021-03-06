using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GRR.Data.Repository.Abstract
{

    /// <summary>
    /// Interface for repository doing create, update and delete operations
    /// </summary>
    /// <typeparam name="TEnity">The datatable entity type</typeparam>
    public interface ICudRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add or create new entity to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entity">New entity</param>
        void Add(TEntity entity);


        

        /// <summary>
        /// Adds set of new entities to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entities">List of new entity</param>
        void Add(IEnumerable<TEntity> entities);

        /// <summary>
        /// Add or create new entity to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entity">New entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task that is awaitable</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds set of new entities to database set (table) and add it to underlying database through ORM tool
        /// </summary>
        /// <param name="entities">List of new entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns task that is awaitable</returns>
        Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update modified record into database table
        /// </summary>
        /// <param name="entity">The modified entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// Update list of modified record into database table
        /// </summary>
        /// <param name="entities">The modified entity list</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete entity from database table
        /// </summary>        
        /// <param name="id">Entity id that is going to be deleted</param>
        /// <param name="cancellationToken">Cancellation token</param>
        void Delete(object id);

        /// <summary>
        /// Delete entity from database table
        /// </summary>
        /// <param name="entity">Entity that is going to be deleted</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete list of entities from database table
        /// </summary>
        /// <param name="entities">Entities that are going to be deleted</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Executes a formatable string and returns data of context
        /// </summary>
        /// <param name="sql"> FormattableString</param>
        IQueryable<TEntity> ExecuteQuery(FormattableString sql);
    }
}
