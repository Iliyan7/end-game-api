using EndGame.Api.TokenProviders;
using EndGame.Api.TokenProviders.Contracts;
using EndGame.DataAccess;
using EndGame.Services;
using EndGame.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EndGame.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("EndGame");
            services.AddDbContext<EndGameContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });
            });
        }

        public static void AddJwtBearerAuth(this IServiceCollection services, IConfiguration config)
        {
            var issuer = config["TokenProvider:Issuer"];
            var audience = config["TokenProvider:Audience"];
            var issuerSigningKey = config["TokenProvider:IssuerSigningKey"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey))
                    };
                });
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IGamesService, GamesService>();

            services.AddSingleton<ITokenProvider, JwtTokenProvider>();
        }
    }
}
