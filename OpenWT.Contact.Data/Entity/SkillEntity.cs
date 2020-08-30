using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenWT.Contact.Data.Entity
{
    [Table("Skill")]
    public class SkillEntity
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; }
        
        public ICollection<ContactSkillEntity> ContactSkills { get; set; }
    }
}