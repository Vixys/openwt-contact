using System;
using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Application.Contract
{
    public interface IContactService : IServiceBase<ContactDto, Guid>
    {
    }
}