using GRR.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GRR.Data.Repository.Implementation
{
   public class CrudRepository<TEntity>: ReadRepository<TEntity>, ICrudRepository<TEntity> where TEntity:class
    {

       
        public readonly UserInfo userInfo;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudRepository.cs"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public CrudRepository(DbContext dbContext, UserInfo userInfo) : base(dbContext)
        {
            this.userInfo = userInfo;
        }

      



        
        

        /// <inheritdoc />
        public virtual void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }


        /// <inheritdoc />
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.DbSet.AddAsync(entity, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual void Add(IEnumerable<TEntity> entities)
        {
            this.DbSet.AddRange(entities);
        }

        public virtual IQueryable<TEntity> ExecuteQuery(FormattableString sql)
        {
            
            return this.DbSet.FromSqlInterpolated(sql).AsNoTracking();
        }



        /// <inheritdoc />
        public virtual async Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.DbSet.AddRangeAsync(entities, cancellationToken)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual void Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = this.DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                this.DbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                TEntity entity = this.DbSet.Find(id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
        }

        /// <inheritdoc />
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            this.DbSet.RemoveRange(entities);
        }

        /// <inheritdoc />
        public virtual void Delete(TEntity entity)
        {
            if (this.DbContext.Entry(entity).State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }
            this.DbSet.Remove(entity);
        }

        public virtual void SetAuditColumnForUpdate(TEntity entityUpdate, TEntity EntityModel)
        {
            EntityModel.GetType().GetProperty("CreatedBy").SetValue(EntityModel, entityUpdate.GetType().GetProperty("CreatedBy").GetValue(entityUpdate));
            EntityModel.GetType().GetProperty("CreatedOn").SetValue(EntityModel, entityUpdate.GetType().GetProperty("CreatedOn").GetValue(entityUpdate));
        }


        /// <inheritdoc />
        public virtual void Update(TEntity entity)
        {
            if (!this.DbContext.Entry(entity).IsKeySet)
            {
                throw new InvalidOperationException($"The primary key was not set on the entity class {entity.GetType().Name}");
            }

            TEntity PersistanceDto = null;
            setAuditEntriesForUpdate(entity,out PersistanceDto);
            DbContext.Entry(PersistanceDto).CurrentValues.SetValues(entity);
        }

        public virtual void setAuditEntriesForUpdate(TEntity entity,out TEntity PersistanceDto)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = DbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
             PersistanceDto = GetById(property.GetValue(entity));
            SetAuditColumnForUpdate(PersistanceDto, entity);
        }
        /// <inheritdoc />
        public virtual void Update(IEnumerable<TEntity> entities)
        {
             foreach(var obj in entities)
            {
                Update(obj);
            }
        }
    }
}
