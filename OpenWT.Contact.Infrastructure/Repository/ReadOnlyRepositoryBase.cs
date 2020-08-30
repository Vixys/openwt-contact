using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OpenWT.Contact.Data.Contract;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Infrastructure.Repository
{
    public abstract class ReadOnlyRepositoryBase<TContext, TEntity, TId> : IReadOnlyRepository<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        #region Fields

        internal readonly TContext Context;

        private DbSet<TEntity> _dbSet;
        internal DbSet<TEntity> DbSet => _dbSet ?? (_dbSet = Context.Set<TEntity>());

        #endregion

        #region Constructor

        protected ReadOnlyRepositoryBase(TContext context)
        {
            Context = context;
        }

        #endregion

        public TEntity GetById(TId entityId, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            return GetOne(entity => entity.Id.Equals(entityId), includeProperties);
        }
        
        public IEnumerable<TEntity> GetByIds(IEnumerable<TId> entityIds, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            return Get(entity => entityIds.Contains(entity.Id), includeProperties: includeProperties);
        }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            return Get(GetQuery(includeProperties));
        }

        #region Helpers

        protected TEntity GetOne(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            return Get(filter, includeProperties: includeProperties).SingleOrDefault();
        }

        protected TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)

        {
            return Get(filter, orderBy, includeProperties: includeProperties).FirstOrDefault();
        }

        protected IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? excludedRows = null,
            int? pageSize = null,
            bool? disableCache = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            return Get(GetQuery(includeProperties), filter, orderBy, excludedRows, pageSize, disableCache);
        }

        protected virtual IQueryable<TEntity> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> includeProperties = null)
        {
            return includeProperties?.Invoke(DbSet) ?? DbSet;
        }

        private IEnumerable<TEntity> Get(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? excludedRows = null,
            int? pageSize = null,
            bool? disableCache = null)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (excludedRows.HasValue && pageSize.HasValue)
            {
                orderBy ??= GetDefaultOrderBy();
                query = orderBy(query)
                    .Skip(excludedRows.Value)
                    .Take(pageSize.Value);
            }
            else if (orderBy != null)
            {
                query = orderBy(query);
            }

            return disableCache.HasValue && disableCache.Value
                ? query.AsNoTracking()
                : query;
        }

        private static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetDefaultOrderBy()
        {
            var keyProperties = typeof(TEntity).GetProperties().Where(p => p.IsDefined(typeof(KeyAttribute), false))
                .ToList();
            var first = keyProperties.Take(1).First();
            var thenList = keyProperties.Skip(1);

            return (entityQuery =>
            {
                var orderedQueryable = entityQuery.OrderBy(entity => first.GetValue(entity));

                foreach (var then in thenList)
                {
                    orderedQueryable = orderedQueryable.ThenBy(entity => then.GetValue(entity));
                }

                return orderedQueryable;
            });
        }

        #endregion
    }
}