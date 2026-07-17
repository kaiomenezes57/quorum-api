using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quorum.Domain.Repositories;
using Quorum.Infrastructure.Persistence;
using Quorum.Infrastructure.Persistence.Repositories;

namespace Quorum.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Default");
            options.UseMySQL(connectionString);
        });
        
        services.AddScoped<IPollRepository, PollRepository>();

        return services;
    }
}