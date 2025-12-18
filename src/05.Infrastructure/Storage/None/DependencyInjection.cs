using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Storage;

namespace Pertamina.SolutionTemplate.Infrastructure.Storage.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneStorageService(this IServiceCollection services)
    {
        services.AddSingleton<IStorageService, NoneStorageService>();

        return services;
    }
}
