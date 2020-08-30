using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenWT.Contact.Data.Entity
{
    [Table("Contact")]
    public class ContactEntity
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        public string Email { get; set; }

        public string MobilePhoneNumber { get; set; }

        public ICollection<ContactSkillEntity> ContactSkills { get; set; }
    }
}