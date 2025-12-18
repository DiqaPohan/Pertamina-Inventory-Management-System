using Pertamina.SolutionTemplate.Bsui.Services.External.Location;

namespace Pertamina.SolutionTemplate.Bsui.Services.External;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocationExternalService(configuration);

        return services;
    }
}
