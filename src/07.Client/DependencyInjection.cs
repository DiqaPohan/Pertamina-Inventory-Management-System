using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Client.Services.BackEnd;
using Pertamina.SolutionTemplate.Client.Services.HealthCheck;
using Pertamina.SolutionTemplate.Client.Services.UserInfo;

namespace Pertamina.SolutionTemplate.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthCheckService();
        services.AddBackEndService(configuration);
        services.AddUserInfoService();

        return services;
    }
}
