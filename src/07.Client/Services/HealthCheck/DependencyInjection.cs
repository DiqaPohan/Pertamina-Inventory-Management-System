using Microsoft.Extensions.DependencyInjection;

namespace Pertamina.SolutionTemplate.Client.Services.HealthCheck;

public static class DependencyInjection
{
    public static IServiceCollection AddHealthCheckService(this IServiceCollection services)
    {
        services.AddSingleton<HealthCheckService>();

        return services;
    }
}
