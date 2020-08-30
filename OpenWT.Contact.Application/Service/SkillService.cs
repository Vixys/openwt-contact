using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public class SkillService : CrudServiceBase<ISkillRepository, SkillDto, SkillEntity>, ISkillService
    {
        public SkillService(IHttpContextAccessor httpContextAccessor, IMapper mapper, ISkillRepository repository, UserManager<UserEntity> userManager) : base(httpContextAccessor, mapper, repository, userManager)
        {
        }
    }
}