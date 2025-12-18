using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Authorization;

namespace Pertamina.SolutionTemplate.Infrastructure.Authorization.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneAuthorizationService(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationService, NoneAuthorizationService>();

        return services;
    }
}
