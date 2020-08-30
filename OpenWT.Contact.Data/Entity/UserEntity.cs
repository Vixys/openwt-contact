using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using OpenWT.Contact.Data.Contract;

namespace OpenWT.Contact.Data.Entity
{
    [Table("User")]
    public class UserEntity : IdentityUser<Guid>, IEntity<Guid>
    {
    }
}