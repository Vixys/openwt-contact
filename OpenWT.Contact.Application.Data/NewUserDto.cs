using System;
using System.ComponentModel.DataAnnotations;

namespace OpenWT.Contact.Application.Data
{
    public class NewUserDto
    {
        [Required]  
        public string Username { get; set; }  
  
        [EmailAddress]
        [Required]  
        public string Email { get; set; }  
  
        [Required]  
        public string Password { get; set; }
    }
}