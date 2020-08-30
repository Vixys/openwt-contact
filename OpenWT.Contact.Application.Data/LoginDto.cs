﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OpenWT.Contact.Application.Data
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}