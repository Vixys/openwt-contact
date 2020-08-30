using System;
using Microsoft.AspNetCore.Mvc;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Api.Controllers
{
    [Route("api/contacts")]
    public class ContactApiController : ApiControllerBase<IContactService, ContactDto, Guid>
    {
        public ContactApiController(IContactService service) : base(service)
        {
        }
    }
}