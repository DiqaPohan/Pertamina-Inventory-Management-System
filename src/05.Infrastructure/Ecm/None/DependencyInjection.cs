using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Ecm;

namespace Pertamina.SolutionTemplate.Infrastructure.Ecm.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneEcmService(this IServiceCollection services)
    {
        services.AddTransient<IEcmService, NoneEcmService>();

        return services;
    }
}
