using System;
using System.Collections.Generic;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Infrastructure.Contract
{
    public interface IContactSkillRepository
    {
        ContactSkillEntity Update(ContactSkillEntity entity);

        IEnumerable<ContactSkillEntity> Update(IEnumerable<ContactSkillEntity> entities);

        ContactSkillEntity Insert(ContactSkillEntity entity);

        IEnumerable<ContactSkillEntity> Insert(IEnumerable<ContactSkillEntity> entities);

        void Delete(params object[] primaryKeys);

        void Delete(List<object[]> primaryKeysList);
    }
}