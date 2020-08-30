using System;
using System.ComponentModel.DataAnnotations;

namespace OpenWT.Contact.Application.Data
{
    public class ContactDto : IDto<Guid>
    {
        public Guid? Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }

        public string Email { get; set; }

        public string MobilePhoneNumber { get; set; }
    }
}