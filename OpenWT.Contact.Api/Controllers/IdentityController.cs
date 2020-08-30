using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace OpenWT.Contact.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    [Produces("application/json")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [SwaggerResponse(200, "All contacts were retrieved")]
        [SwaggerOperation(Summary = "Create a new user", OperationId = "CreateUser")]
        public async Task Create([FromBody] NewUserDto newUser)
        {
            await _identityService.CreateUser(newUser);
        }

        [HttpPost("login")]
        [SwaggerResponse(200, "All contacts were retrieved")]
        [SwaggerOperation(Summary = "Login", OperationId = "Login")]
        public async Task<JwtDto> Login([FromBody] LoginDto login)
        {
            return await _identityService.Login(login);
        }
    }
}