using System;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Infrastructure.Contract
{
    public interface ISkillRepository : IRepository<SkillEntity, Guid>
    {
    }
}