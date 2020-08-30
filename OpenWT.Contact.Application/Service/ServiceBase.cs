using System;
using System.Collections.Generic;
using AutoMapper;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Common.Exception;
using OpenWT.Contact.Data.Contract;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public abstract class ServiceBase<TRepository, TDto, TEntity> : IServiceBase<TDto, Guid>
        where TRepository : IRepository<TEntity, Guid>
        where TDto : IDto<Guid>
        where TEntity : IEntity<Guid>
    {
        private readonly IMapper _mapper;
        private readonly TRepository _repository;

        public ServiceBase(IMapper mapper, TRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<TDto> GetAll()
        {
            return _mapper.Map<IEnumerable<TDto>>(_repository.GetAll());
        }

        public virtual TDto Create(TDto contactCreation)
        {
            contactCreation.Id = Guid.NewGuid();

            var contactEntity = _mapper.Map<TEntity>(contactCreation);

            return _mapper.Map<TDto>(_repository.Insert(contactEntity));
        }

        public TDto GetById(Guid entityId)
        {
            var entity = _repository.GetById(entityId);
            
            if (entity == null)
            {
                throw new NotFoundException($"The {typeof(TDto).Name.Replace("Dto", "")} with id {entityId} does not exist.");
            }
            
            return _mapper.Map<TDto>(entity);
        }

        public void DeleteById(Guid entityId)
        {
            if (_repository.GetById(entityId) == null)
            {
                throw new NotFoundException($"The {typeof(TDto).Name.Replace("Dto", "")} with id {entityId} does not exist.");
            }
            
            _repository.Delete(entityId);
        }

        public TDto Update(Guid entityId, TDto contact)
        {
            if (_repository.GetById(entityId) == null)
            {
                throw new NotFoundException($"The {typeof(TDto).Name.Replace("Dto", "")} with id {entityId} does not exist.");
            }
            
            contact.Id = entityId;
            
            var contactEntity = _mapper.Map<TEntity>(contact);

            return _mapper.Map<TDto>(_repository.Update(contactEntity));
        }
    }
}