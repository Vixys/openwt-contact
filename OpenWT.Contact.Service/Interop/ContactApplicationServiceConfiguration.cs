using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWT.Contact.Infrastructure.Interop;

namespace OpenWT.Contact.Service.Interop
{
    public static class ContactApplicationServiceConfiguration
    {
        public static void AddContactApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContactInfrastructureServices(configuration);
        }
    }
}