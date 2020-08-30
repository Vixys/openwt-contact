using System;
using AutoMapper;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Common.Exception;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public class ContactService : ServiceBase<IContactRepository, ContactDto, ContactEntity>, IContactService
    {
        public ContactService(IMapper mapper, IContactRepository contactRepository) : base(mapper, contactRepository)
        {
        }

        public override ContactDto Create(ContactDto contactCreation)
        {
            if (contactCreation.Email == null && contactCreation.MobilePhoneNumber == null)
            {
                throw new BadParameterException("You need at least to set either the email or the mobile phone number.");
            }
            
            return base.Create(contactCreation);
        }
    }
}