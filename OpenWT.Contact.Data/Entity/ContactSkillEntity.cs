using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenWT.Contact.Data.Entity
{
    [Table("ContactSkill")]
    public class ContactSkillEntity
    {
        [Required]
        public Guid ContactId { get; set; }

        [Required]
        public Guid SkillId { get; set; }

        public ContactEntity Contact { get; set; }
        
        public SkillEntity Skill { get; set; }
    }
}