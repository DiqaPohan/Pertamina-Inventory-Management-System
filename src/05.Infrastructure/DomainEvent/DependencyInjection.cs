using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.DomainEvent;

namespace Pertamina.SolutionTemplate.Infrastructure.DomainEvent;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainEventService(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();

        return services;
    }
}
