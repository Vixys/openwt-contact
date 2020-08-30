using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Common.Exception;
using OpenWT.Contact.Data.Contract;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public abstract class CrudServiceBase<TRepository, TDto, TEntity> : ServiceBase, IServiceBase<TDto, Guid>
        where TRepository : IRepository<TEntity, Guid>
        where TDto : IDto<Guid>
        where TEntity : IEntity<Guid>
    {
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;

        public CrudServiceBase(IHttpContextAccessor httpContextAccessor, IMapper mapper, TRepository repository, UserManager<UserEntity> userManager) : base(httpContextAccessor, userManager)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            return _mapper.Map<IEnumerable<TDto>>(_repository.GetAll());
        }

        public virtual TDto Create(TDto contactCreation)
        {
            contactCreation.Id = Guid.NewGuid();

            var contactEntity = _mapper.Map<TEntity>(contactCreation);

            return _mapper.Map<TDto>(_repository.Insert(contactEntity));
        }

        public virtual TDto GetById(Guid entityId)
        {
            var entity = _repository.GetById(entityId);
            
            if (entity == null)
            {
                throw new NotFoundException($"The {typeof(TDto).Name.Replace("Dto", "")} with id {entityId} does not exist.");
            }
            
            return _mapper.Map<TDto>(entity);
        }

        public virtual void DeleteById(Guid entityId)
        {
            if (GetById(entityId) == null)
            {
                throw new NotFoundException($"The {typeof(TDto).Name.Replace("Dto", "")} with id {entityId} does not exist.");
            }
            
            _repository.Delete(entityId);
        }

        public virtual TDto Update(Guid entityId, TDto contact)
        {
            if (GetById(entityId) == null)
            {
                throw new NotFoundException($"The {typeof(TDto).Name.Replace("Dto", "")} with id {entityId} does not exist.");
            }
            
            contact.Id = entityId;
            
            var contactEntity = _mapper.Map<TEntity>(contact);

            return _mapper.Map<TDto>(_repository.Update(contactEntity));
        }
    }
}