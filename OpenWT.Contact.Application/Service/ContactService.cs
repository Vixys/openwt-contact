using System;
using AutoMapper;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public class ContactService : ServiceBase<IContactRepository, ContactDto, ContactEntity>, IContactService
    {
        public ContactService(IMapper mapper, IContactRepository contactRepository) : base(mapper, contactRepository)
        {
        }
    }
}