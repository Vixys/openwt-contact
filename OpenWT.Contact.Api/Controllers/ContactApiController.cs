using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OpenWT.Contact.Api.Middleware;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Data.Entity;
using Swashbuckle.AspNetCore.Annotations;

namespace OpenWT.Contact.Api.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class ContactApiController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactApiController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [SwaggerResponse(200, "All contacts were retrieved", typeof(IEnumerable<ContactDto>))]
        [SwaggerOperation(Summary = "Retrieved all contacts", OperationId = "GetAllContact")]
        public virtual ActionResult<IEnumerable<ContactDto>> GetAll()
        {
            return _contactService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "The contact was successfully retrieve", typeof(ContactDto))]
        [SwaggerResponse(404, "The contact to retrieve was not found", typeof(ApiExceptionModel))]
        [SwaggerOperation(Summary = "Retrieve a contact", OperationId = "GetContact")]
        public ActionResult<ContactDto> Get([FromRoute] Guid id)
        {
            return _contactService.GetById(id);
        }

        
        [HttpPost]
        [SwaggerResponse(201, "The contact was created", typeof(ContactDto))]
        [SwaggerResponse(400, "The contact data is invalid", typeof(ApiExceptionModel))]
        [SwaggerOperation(Summary = "Create a new contact", OperationId = "CreateContact")]
        public ActionResult<ContactDto> Create([FromBody, SwaggerRequestBody("The contact payload", Required = true)] ContactDto contactToCreate)
        {
            var createdContact = _contactService.Create(contactToCreate);
            return Created(Url.Action("Get", new { id = createdContact.Id }), createdContact);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "The contact was deleted")]
        [SwaggerResponse(404, "The contact to delete was not found", typeof(ApiExceptionModel))]
        [SwaggerOperation(Summary = "Delete a contact", OperationId = "DeleteContact")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _contactService.DeleteById(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "The contact was successfully updated", typeof(ContactDto))]
        [SwaggerResponse(404, "The contact to update was not found", typeof(ApiExceptionModel))]
        [SwaggerOperation(Summary = "Update a contact", OperationId = "UpdateContact")]
        public ActionResult<ContactDto> Update([FromRoute] Guid id, [FromBody, SwaggerRequestBody("The updated contact payload", Required = true)] ContactDto updateBody)
        {
            return _contactService.Update(id, updateBody);
        }
    }
}