using GRR.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GRR.Data.Repository
{
    /// <summary>
    /// The repository class that performs all read operations
    /// </summary>
    /// <typeparam name="TEntity">The datatable entity type</typeparam>
    public class ReadRepository<TEntity>: RepositoryBase<TEntity>, IReadRepository<TEntity> where TEntity :class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadRepository.cs"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public ReadRepository(DbContext dbContext) : base(dbContext)
        {

        }




        public virtual IQueryable<TEntity> PrepareSearch(Expression<Func<TEntity, bool>> filter,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = true)
        {
            return this.GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking);
        }


        /// <inheritdoc />
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = true)
        {
            return this.GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking).ToList();
        }



        /// <inheritdoc />
        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = true)
        {
            return this.GetQueryable(null, orderBy, includeProperties, skip, take, asNoTracking).ToList();
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, int? skip = null, int? take = null, bool asNoTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(null, orderBy, includeProperties, skip, take, asNoTracking)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, int? skip = null,
            int? take = null, bool asNoTracking = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter, orderBy, includeProperties, skip, take, asNoTracking)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public TEntity GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.DbSet
                .FindAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        /// <inheritdoc />
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter)
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.GetQueryable(filter).Any();
        }



        /// <inheritdoc />
        public async Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, bool asNoTracking = true)
        {
            return this.GetQueryable(filter, orderBy, includeProperties, asNoTracking: asNoTracking)
                .FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null, bool asNoTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter, orderBy, includeProperties, asNoTracking: asNoTracking)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public TEntity GetOne(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null,
            bool asNoTracking = true)
        {
            return this.GetQueryable(filter, includes: includeProperties, asNoTracking: asNoTracking)
                .SingleOrDefault();


        }

        /// <inheritdoc />        
        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity ,object>> includeProperties = null,
            bool asNoTracking = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.GetQueryable(filter, includes: includeProperties, asNoTracking: asNoTracking)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }


        public async Task<List<TProjectedType>> GetListAsync<T, TProjectedType>(
           
           Expression<Func<TEntity, TProjectedType>> selectExpression,
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeProperties = null,
           bool asNoTracking = false,
           CancellationToken cancellationToken = default)
           where T : class
        {
            var query=  this.GetQueryable(filter, includes: includeProperties, asNoTracking: asNoTracking);
            return await query.Select(selectExpression).ToListAsync(cancellationToken);
           
        }


    }
}
