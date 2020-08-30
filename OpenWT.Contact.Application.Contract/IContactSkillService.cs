using System;
using System.Collections.Generic;
using OpenWT.Contact.Application.Data;

namespace OpenWT.Contact.Application.Contract
{
    public interface IContactSkillService
    {
        void AddSkillsToContact(Guid contactId, IEnumerable<Guid> skillIds);
        void RemoveSkillsFromContact(Guid contactId, IEnumerable<Guid> skillIds);
        IEnumerable<SkillDto> GetAllSkillsFromContact(Guid contactId);
    }
}