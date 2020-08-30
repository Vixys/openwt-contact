using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Data;
using OpenWT.Contact.Common.Exception;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Infrastructure.Contract;

namespace OpenWT.Contact.Application.Service
{
    public class ContactSkillService : IContactSkillService
    {
        private readonly IMapper _mapper;
        private readonly IContactSkillRepository _contactSkillRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ISkillRepository _skillRepository;

        public ContactSkillService(IMapper mapper, IContactSkillRepository contactSkillRepository, IContactRepository contactRepository, ISkillRepository skillRepository)
        {
            _mapper = mapper;
            _contactSkillRepository = contactSkillRepository;
            _contactRepository = contactRepository;
            _skillRepository = skillRepository;
        }

        public void AddSkillsToContact(Guid contactId, IEnumerable<Guid> skillIds)
        {
            var skillList = skillIds.Distinct().ToList();
            
            SkillsMustExist(skillList);

            _contactSkillRepository.Insert(skillList.Select(skillId => new ContactSkillEntity() { ContactId = contactId, SkillId = skillId }));
        }

        public void RemoveSkillsFromContact(Guid contactId, IEnumerable<Guid> skillIds)
        {
            var skillList = skillIds.Distinct().ToList();
            
            SkillsMustExist(skillList);
            
            _contactSkillRepository.Delete(skillList.Select(skillId => new object[] { contactId, skillId }).ToList());
        }

        public IEnumerable<SkillDto> GetAllSkillsFromContact(Guid contactId)
        {
            return _mapper.Map<IEnumerable<SkillDto>>(GetContact(contactId).ContactSkills.Select(contactSkill => contactSkill.Skill));
        }

        #region Helpers

        private ContactEntity GetContact(Guid contactId)
        {
            var contactEntity = _contactRepository.GetById(contactId,
                contacts => contacts.Include(contact => contact.ContactSkills).ThenInclude(contactSkill => contactSkill.Skill));

            if (contactEntity == null)
            {
                throw new NotFoundException($"The contact with Id {contactId} does not exist.");
            }

            return contactEntity;
        }
        
        private void SkillsMustExist(IList<Guid> skillIds)
        {
            if (_skillRepository.GetByIds(skillIds).Count() != skillIds.Count())
            {
                throw new NotFoundException("One or more skill ids does not exist.");
            }
        }

        #endregion
    }
}