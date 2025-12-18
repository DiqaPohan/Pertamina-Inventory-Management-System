using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Persistence;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNonePersistenceService(this IServiceCollection services)
    {
        services.AddScoped<ISolutionTemplateDbContext, NoneSolutionTemplateDbContext>();

        return services;
    }
}
