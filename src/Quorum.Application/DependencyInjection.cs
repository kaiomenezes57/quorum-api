using Microsoft.Extensions.DependencyInjection;
using Quorum.Application.Features.Polls.Services.Points;

namespace Quorum.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly);
        });
        
        services.AddScoped<IPointsService, PointsService>();
    }
}