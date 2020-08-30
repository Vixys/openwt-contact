using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWT.Contact.Data.Context;

namespace OpenWT.Contact.Data.Interop
{
    public static class ContactDataServiceConfiguration
    {
        public static void AddContactDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectingString = configuration.GetValue<string>("DbSettings:ConnectionStrings");

            switch (configuration.GetValue<string>("DbSettings:SqlProvider")?.ToLower())
            {
                case "postgre":
                case "postgresql":
                    services.AddDbContext<ContactContext>(opt => opt.UseNpgsql(connectingString));
                    break;
                case "sqlite":
                    break;
                case "sqlserver":
                default:
                    break;
            }
        }
    }
}