using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWT.Contact.Data.Context;
using OpenWT.Contact.Data.Entity;
using OpenWT.Contact.Data.Interop;
using OpenWT.Contact.Infrastructure.Contract;
using OpenWT.Contact.Infrastructure.Repository;

namespace OpenWT.Contact.Infrastructure.Interop
{
    public static class ContactInfrastructureServiceConfiguration
    {
        public static void AddContactInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactSkillRepository, ContactSkillRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            
            services.AddIdentity<UserEntity, IdentityRole<Guid>>()   
                .AddEntityFrameworkStores<ContactContext>()  
                .AddDefaultTokenProviders();
            
            services.AddContactDataServices(configuration);
        }
    }
}