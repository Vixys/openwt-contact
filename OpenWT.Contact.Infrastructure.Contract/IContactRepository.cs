using System;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Infrastructure.Contract
{
    public interface IContactRepository : IRepository<ContactEntity, Guid>
    {
    }
}