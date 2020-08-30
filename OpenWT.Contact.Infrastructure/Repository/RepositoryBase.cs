using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OpenWT.Contact.Data.Contract;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Infrastructure.Repository
{
    public abstract class RepositoryBase<TContext, TEntity, TId> : ReadOnlyRepositoryBase<TContext, TEntity, TId>,
        IRepository<TEntity, TId>
        where TContext : DbContext
        where TEntity : class, IEntity<TId>
        where TId : IEquatable<TId>
    {
        protected RepositoryBase(TContext context) : base(context)
        {
        }

        /// <summary>
        /// Update one entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity just updated</returns>
        public TEntity Update(TEntity entity)
        {
            var result = UpdateEntity(entity);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Update a list of entities
        /// </summary>
        /// <param name="entities">The list of entities to update</param>
        /// <returns>The entities list just updated</returns>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            var result = UpdateEntities(entities);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Insert a new entity
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <returns>The inserted entity</returns>
        public TEntity Insert(TEntity entity)
        {
            var result = InsertEntity(entity);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Insert several entities at once
        /// </summary>
        /// <param name="entities">Entities to insert</param>
        /// <returns>The inserted entities</returns>
        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            var result = InsertEntities(entities);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Delete entity by Id.
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(params object[] primaryKeys)
        {
            DeleteEntity(primaryKeys);

            SaveChanges();
        }

        /// <summary>
        /// Delete entities by list of Id.
        /// </summary>
        /// <param name="ids">The ids list of the entities to delete</param>
        /// <exception cref="NotImplementedException">Tes</exception>
        public void Delete(List<object[]> primaryKeysList)
        {
            foreach (var primaryKeys in primaryKeysList)
            {
                DeleteEntity(primaryKeys);
            }
        }

        #region Helpers

        private TEntity InsertEntity(TEntity entity)
        {
            var entityModel = entity;

            DbSet.Add(entityModel);

            return entityModel;
        }

        private void DeleteEntity(params object[] primaryKeys)
        {
            var entityModel = DbSet.Find(primaryKeys);

            if (entityModel != null)
                DbSet.Remove(entityModel);
        }

        private IEnumerable<TEntity> InsertEntities(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();

            foreach (var entity in entities)
            {
                result.Add(InsertEntity(entity));
            }

            return result;
        }

        private TEntity UpdateEntity(TEntity entity)
        {
            var entityModel = entity;
            entityModel = DbSet.Attach(entityModel).Entity;
            Context.Entry(entityModel).State = EntityState.Modified;

            return entityModel;
        }

        private IEnumerable<TEntity> UpdateEntities(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();

            foreach (var entity in entities)
            {
                result.Add(UpdateEntity(entity));
            }

            return result;
        }

        protected virtual void ThrowEnhancedValidationException(DbUpdateException e)
        {
            throw e;
        }

        private void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        #endregion
    }
}