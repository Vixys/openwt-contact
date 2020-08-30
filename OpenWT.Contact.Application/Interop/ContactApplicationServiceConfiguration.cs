using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenWT.Contact.Application.Contract;
using OpenWT.Contact.Application.Service;
using OpenWT.Contact.Infrastructure.Interop;

namespace OpenWT.Contact.Application.Interop
{
    public static class ContactApplicationServiceConfiguration
    {
        public static void AddContactApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactSkillService, ContactSkillService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(ContactApplicationServiceConfiguration).Assembly);

            services.AddContactInfrastructureServices(configuration);

            services.AddAuthentication(options =>  
                {  
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })  
                // Adding Jwt Bearer  
                .AddJwtBearer(options =>  
                {  
                    options.SaveToken = true;  
                    options.RequireHttpsMetadata = false;  
                    options.TokenValidationParameters = new TokenValidationParameters()  
                    {  
                        ValidateIssuer = true,  
                        ValidateAudience = true,  
                        ValidAudience = configuration.GetValue<string>("JWT:ValidAudience"),  
                        ValidIssuer = configuration.GetValue<string>("JWT:ValidIssuer"),  
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret")))  
                    };  
                });  
        }
    }
}