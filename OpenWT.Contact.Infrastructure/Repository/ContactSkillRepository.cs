using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OpenWT.Contact.Data.Context;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Infrastructure.Repository
{
    public class ContactSkillRepository : IContactSkillRepository
    {
        private readonly ContactContext _context;
        private DbSet<ContactSkillEntity> _dbSet;
        internal DbSet<ContactSkillEntity> DbSet => _dbSet ?? (_dbSet = _context.Set<ContactSkillEntity>());

        public ContactSkillRepository(ContactContext context)
        {
            _context = context;
        }
        
                /// <summary>
        /// Update one entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The entity just updated</returns>
        public ContactSkillEntity Update(ContactSkillEntity entity)
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
        public IEnumerable<ContactSkillEntity> Update(IEnumerable<ContactSkillEntity> entities)
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
        public ContactSkillEntity Insert(ContactSkillEntity entity)
        {
            var result = InsercontactSkillEntity(entity);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Insert several entities at once
        /// </summary>
        /// <param name="entities">Entities to insert</param>
        /// <returns>The inserted entities</returns>
        public IEnumerable<ContactSkillEntity> Insert(IEnumerable<ContactSkillEntity> entities)
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
            
            SaveChanges();
        }

        #region Helpers

        private ContactSkillEntity InsercontactSkillEntity(ContactSkillEntity entity)
        {
            var entityModel = entity;

            DbSet.Add(entity);

            return entityModel;
        }

        private void DeleteEntity(params object[] primaryKeys)
        {
            var entityModel = DbSet.Find(primaryKeys);

            if (entityModel != null)
                DbSet.Remove(entityModel);
        }

        private IEnumerable<ContactSkillEntity> InsertEntities(IEnumerable<ContactSkillEntity> entities)
        {
            var result = new List<ContactSkillEntity>();

            foreach (var entity in entities)
            {
                result.Add(InsercontactSkillEntity(entity));
            }

            return result;
        }

        private ContactSkillEntity UpdateEntity(ContactSkillEntity entity)
        {
            var entityModel = entity;
            entityModel = DbSet.Attach(entityModel).Entity;
            _context.Entry(entityModel).State = EntityState.Modified;

            return entityModel;
        }

        private IEnumerable<ContactSkillEntity> UpdateEntities(IEnumerable<ContactSkillEntity> entities)
        {
            var result = new List<ContactSkillEntity>();

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
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }

        #endregion
    }
}