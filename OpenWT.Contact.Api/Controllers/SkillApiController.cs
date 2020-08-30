using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace OpenWT.Contact.Api.Controllers
{
    [Route("api/skills")]
    [ApiController]
    [Produces("application/json")]
    public class SkillApiController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillApiController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        [SwaggerResponse(200, "All skills were retrieved", typeof(IEnumerable<SkillDto>))]
        [SwaggerOperation(Summary = "Retrieved all skills", OperationId = "GetAllSkill")]
        public virtual ActionResult<IEnumerable<SkillDto>> GetAll()
        {
            return _skillService.GetAll().ToList();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "The skill was successfully retrieve", typeof(SkillDto))]
        [SwaggerResponse(404, "The skill to retrieve was not found")]
        [SwaggerOperation(Summary = "Retrieve a skill", OperationId = "GetSkill")]
        public ActionResult<SkillDto> Get([FromRoute] Guid id)
        {
            return _skillService.GetById(id);
        }

        [HttpPost]
        [SwaggerResponse(201, "The skill was created", typeof(SkillDto))]
        [SwaggerResponse(400, "The skill data is invalid")]
        [SwaggerOperation(Summary = "Create a new skill", OperationId = "CreateSkill")]
        public ActionResult<SkillDto> Create([FromBody] SkillDto createBody)
        {
            return Created("", _skillService.Create(createBody));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(200, "The skill was deleted")]
        [SwaggerResponse(404, "The skill to delete was not found")]
        [SwaggerOperation(Summary = "Delete a skill", OperationId = "DeleteSkill")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            _skillService.DeleteById(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "The skill was successfully updated", typeof(SkillDto))]
        [SwaggerResponse(404, "The skill to update was not found")]
        [SwaggerOperation(Summary = "Update a skill", OperationId = "UpdateSkill")]
        public ActionResult<SkillDto> Update([FromRoute] Guid id, [FromBody] SkillDto createBody)
        {
            return _skillService.Update(id, createBody);
        }
    }
}