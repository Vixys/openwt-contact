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
using Swashbuckle.AspNetCore.Annotations;

namespace OpenWT.Contact.Api.Controllers
{
    [Authorize]
    [Route("api/contacts/{contactId}/skills")]
    [ApiController]
    [Produces("application/json")]
    public class ContactSkillApiController : ControllerBase
    {
        private readonly IContactSkillService _contactSkillService;

        public ContactSkillApiController(IContactSkillService contactSkillService)
        {
            _contactSkillService = contactSkillService;
        }

        [HttpGet]
        [SwaggerResponse(200, "All skills of the contact were retrieved", typeof(IEnumerable<SkillDto>))]
        [SwaggerOperation(Summary = "Retrieved all contact's skills", OperationId = "GetAllContactSkill")]
        public virtual ActionResult<IEnumerable<SkillDto>> GetAll([FromRoute, SwaggerRequestBody("Contact id", Required = true)] Guid contactId)
        {
            return _contactSkillService.GetAllSkillsFromContact(contactId).ToList();
        }

        
        [HttpPost]
        [SwaggerResponse(200, "Skills were successfully added to contact")]
        [SwaggerResponse(400, "There is invalid data", typeof(ApiExceptionModel))]
        [SwaggerOperation(Summary = "Add skill to contact", OperationId = "AddSkillContact")]
        public IActionResult Add([FromRoute, SwaggerRequestBody("Contact id", Required = true)] Guid contactId, [FromBody, SwaggerRequestBody("Skill ids to add to contact", Required = true)] IEnumerable<Guid> skillIds)
        {
            _contactSkillService.AddSkillsToContact(contactId, skillIds);
            return Ok();
        }

        [HttpDelete()]
        [SwaggerResponse(200, "The contact was deleted")]
        [SwaggerResponse(404, "The contact to delete was not found", typeof(ApiExceptionModel))]
        [SwaggerOperation(Summary = "Delete a contact", OperationId = "DeleteContact")]
        public IActionResult Delete([FromRoute, SwaggerRequestBody("Contact id", Required = true)] Guid contactId, [FromBody, SwaggerRequestBody("Skill ids to add to contact", Required = true)] IEnumerable<Guid> skillIds)
        {
            _contactSkillService.RemoveSkillsFromContact(contactId, skillIds);
            return Ok();
        }
    }
}