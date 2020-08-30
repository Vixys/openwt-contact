using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWT.Contact.Data.Interop;

namespace OpenWT.Contact.Infrastructure.Interop
{
    public static class ContactInfrastructureServiceConfiguration
    {
        public static void AddContactInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContactDataServices(configuration);
        }
    }
}