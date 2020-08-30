using System;
using System.ComponentModel.DataAnnotations;

namespace OpenWT.Contact.Application.Data
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime ExpireTo { get; set; }
    }
}