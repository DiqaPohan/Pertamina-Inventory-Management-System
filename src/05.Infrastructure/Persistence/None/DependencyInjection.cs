using Microsoft.Extensions.DependencyInjection;
using Application.Services.Persistence;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNonePersistenceService(this IServiceCollection services)
    {
        services.AddScoped<ISolutionTemplateDbContext, NoneSolutionTemplateDbContext>();

        return services;
    }
}
