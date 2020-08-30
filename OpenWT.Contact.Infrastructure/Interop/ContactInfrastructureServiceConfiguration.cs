using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWT.Contact.Data.Interop;
using OpenWT.Contact.Infrastructure.Contract;
using OpenWT.Contact.Infrastructure.Repository;

namespace OpenWT.Contact.Infrastructure.Interop
{
    public static class ContactInfrastructureServiceConfiguration
    {
        public static void AddContactInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ISkillRepository, ISkillRepository>();
            
            services.AddContactDataServices(configuration);
        }
    }
}