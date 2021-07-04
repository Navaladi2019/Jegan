
namespace GRR.Data.Repository
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.EntityFrameworkCore.Query;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public class RepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets DB Context object
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// Gets DB Set (table) object
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase.cs"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        protected RepositoryBase(DbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "The Database context is null");
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        /// <summary>
        /// Create a IQueryable object to query database context for the specified filter condition
        /// </summary>        
        ///  <param name="filter">The filter condition</param>
        /// <param name="orderBy">The order by condition</param>
        /// <param name="includeProperties">The properties to be included in the select query</param>
        /// <param name="skip">The number of records to be skipped</param>
        /// <param name="take">The number of records required</param>
        /// <param name="asNoTracking">True if table tracking required</param>
        /// <returns>IQueryable entity</returns>
        protected IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>>  includes = null, int? skip = null, int? take = null,
            bool asNoTracking = true)
        {
            IQueryable<TEntity> query = this.DbSet;

            if(includes != null)
            {
                query = includes(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (asNoTracking)
            {
                return query.AsNoTracking<TEntity>();
            }

            return query;
        }



    }
}
