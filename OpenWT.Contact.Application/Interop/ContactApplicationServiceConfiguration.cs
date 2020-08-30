using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Service;
using OpenWT.Contact.Infrastructure.Interop;

namespace OpenWT.Contact.Application.Interop
{
    public static class ContactApplicationServiceConfiguration
    {
        public static void AddContactApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISkillService, SkillService>();

            services.AddAutoMapper(typeof(ContactApplicationServiceConfiguration).Assembly);
            
            services.AddContactInfrastructureServices(configuration);
        }
    }
}