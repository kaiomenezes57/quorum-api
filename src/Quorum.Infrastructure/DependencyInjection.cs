using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quorum.Application.Interfaces;
using Quorum.Domain.Repositories;
using Quorum.Infrastructure.Auth;
using Quorum.Infrastructure.Persistence;
using Quorum.Infrastructure.Persistence.Repositories;

namespace Quorum.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(IConfiguration configuration)
        { 
            services.AddDbContext(configuration);

            services.AddJwtAuthentication(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            
            services.AddScoped<IPollRepository, PollRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private void AddDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");
        
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(connectionString!);
            });
        }

        private void AddJwtAuthentication(IConfiguration configuration)
        {
            var jwtKey = 
                configuration["Jwt:Key"] ?? 
                throw new InvalidOperationException("JWT Key not configured.");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey)
                        ),

                        ValidateIssuer = false,
                        ValidateAudience = false,

                        ValidateLifetime = true
                    };
                });
        }
    }
}