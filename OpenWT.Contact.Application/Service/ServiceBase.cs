using System;
using AutoMapper;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
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
        
        public TDto Create(TDto contactCreation)
        {
            contactCreation.Id = Guid.NewGuid();

            var contactEntity = _mapper.Map<TEntity>(contactCreation);

            return _mapper.Map<TDto>(_repository.Insert(contactEntity));
        }

        public TDto GetById(Guid contactId)
        {
            var entity = _repository.GetById(contactId);
            
            if (entity == null)
            {
                // TODO: throw exception
            }
            
            return _mapper.Map<TDto>(entity);
        }

        public void DeleteById(Guid contactId)
        {
            if (_repository.GetById(contactId) == null)
            {
                // TODO: throw exception
            }
            
            _repository.Delete(contactId);
        }

        public TDto Update(Guid contactId, TDto contact)
        {
            if (_repository.GetById(contactId) == null)
            {
                // TODO: throw exception
            }
            
            contact.Id = contactId;
            
            var contactEntity = _mapper.Map<TEntity>(contact);

            return _mapper.Map<TDto>(_repository.Update(contactEntity));
        }
    }
}