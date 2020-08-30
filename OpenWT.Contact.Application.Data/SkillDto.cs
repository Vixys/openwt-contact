using System;
using System.ComponentModel.DataAnnotations;

namespace OpenWT.Contact.Application.Data
{
    public class SkillDto : IDto<Guid>
    {
        public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Level { get; set; }
    }
}