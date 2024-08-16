using DAL;
using DAL.Models;
using FCP_BACKEND.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Defness.Extensions
{
    public static class ServicesConfigurer
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
            return services;
        }

        public static void ConfigAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<User>(x =>
            {
                x.Password.RequiredLength = 8;
                x.Password.RequireUppercase = true;
                x.Password.RequireLowercase = true;
                x.Password.RequireNonAlphanumeric = true;
                x.Password.RequireDigit = true;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()

                .AddApiEndpoints();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.RequireHttpsMetadata = true;
                   options.SaveToken = true;
                   options.TokenValidationParameters = JwtBuilder.Parameters(configuration);
               });
            services.AddScoped<PasswordValidator<User>>();
            services.AddAuthorizationBuilder();
        }
    }
}
