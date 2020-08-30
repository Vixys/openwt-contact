using System;
using OpenWT.Contact.Data.Context;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Infrastructure.Repository
{
    public class SkillRepository : RepositoryBase<ContactContext, SkillEntity, Guid>, ISkillRepository
    {
        public SkillRepository(ContactContext context) : base(context)
        {
        }
    }
}