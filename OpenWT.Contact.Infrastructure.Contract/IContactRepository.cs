using System;
using System.Collections.Generic;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Infrastructure.Contract
{
    public interface IContactRepository : IRepository<ContactEntity, Guid>
    {
        IEnumerable<ContactEntity> GetAllByUserId(Guid userId);
    }
}