using System;
using Microsoft.AspNetCore.Mvc;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Api.Controllers
{
    [Route("api/skills")]
    public class SkillApiController : ApiControllerBase<ISkillService, SkillDto, Guid>
    {
        public SkillApiController(ISkillService service) : base(service)
        {
        }
    }
}