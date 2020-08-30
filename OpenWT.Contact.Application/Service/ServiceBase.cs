using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenWT.Contact.Data.Entity;

namespace OpenWT.Contact.Application.Service
{
    public abstract class ServiceBase
    {
        private readonly UserManager<UserEntity> _userManager;
        protected ClaimsPrincipal User { get; }
        protected Guid UserId => Guid.Parse(_userManager.GetUserId(User));
        
        public ServiceBase(IHttpContextAccessor httpContextAccessor, UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
            User = httpContextAccessor.HttpContext.User;
        }

    }
}