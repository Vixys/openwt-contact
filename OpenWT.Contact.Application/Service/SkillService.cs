using AutoMapper;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public class SkillService : ServiceBase<ISkillRepository, SkillDto, SkillEntity>, ISkillService
    {
        public SkillService(IMapper mapper, ISkillRepository repository) : base(mapper, repository)
        {
        }
    }
}