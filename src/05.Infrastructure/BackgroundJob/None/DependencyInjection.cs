using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.BackgroundJob;

namespace Pertamina.SolutionTemplate.Infrastructure.BackgroundJob.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneBackgroundJobService(this IServiceCollection services)
    {
        services.AddTransient<IBackgroundJobService, NoneBackgroundJobService>();

        return services;
    }
}
