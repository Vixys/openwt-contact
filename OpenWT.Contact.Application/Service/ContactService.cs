using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Common.Exception;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public class ContactService : CrudServiceBase<IContactRepository, ContactDto, ContactEntity>, IContactService
    {
        public ContactService(IHttpContextAccessor httpContextAccessor, IMapper mapper, IContactRepository contactRepository, UserManager<UserEntity> userManager) : base(httpContextAccessor, mapper, contactRepository, userManager)
        {
        }

        public override IEnumerable<ContactDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ContactDto>>(_repository.GetAllByUserId(UserId));
        }

        public override ContactDto Create(ContactDto contactCreation)
        {
            if (contactCreation.Email == null && contactCreation.MobilePhoneNumber == null)
            {
                throw new BadParameterException("You need at least to set either the email or the mobile phone number.");
            }

            contactCreation.UserId = UserId;
            
            return base.Create(contactCreation);
        }

        public override ContactDto Update(Guid entityId, ContactDto contact)
        {
            contact.UserId = UserId;
            
            return base.Update(entityId, contact);
        }

        public bool CanActOnContact(Guid entityId)
        {
            var contactEntity = _repository.GetById(entityId);

            return contactEntity.UserId == UserId;
        }
    }
}