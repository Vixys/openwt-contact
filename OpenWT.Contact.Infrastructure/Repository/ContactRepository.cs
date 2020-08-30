using System;
using System.Collections.Generic;
using OpenWT.Contact.Data.Context;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Infrastructure.Repository
{
    public class ContactRepository : RepositoryBase<ContactContext, ContactEntity, Guid>, IContactRepository
    {
        public ContactRepository(ContactContext context) : base(context)
        {
        }

        public IEnumerable<ContactEntity> GetAllByUserId(Guid userId)
        {
            return Get(entity => entity.UserId == userId);
        }
    }
}