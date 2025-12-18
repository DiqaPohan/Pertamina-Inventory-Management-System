using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;

namespace Pertamina.SolutionTemplate.Infrastructure.DateAndTime;

public static class DependencyInjection
{
    public static IServiceCollection AddDateAndTimeService(this IServiceCollection services)
    {
        services.AddTransient<IDateAndTimeService, DateAndTimeService>();

        return services;
    }
}
