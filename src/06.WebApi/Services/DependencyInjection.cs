using Pertamina.SolutionTemplate.WebApi.Services.BackEnd;
using Pertamina.SolutionTemplate.WebApi.Services.Documentation;

namespace Pertamina.SolutionTemplate.WebApi.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBackEndService(configuration);
        services.AddDocumentationService(configuration);

        return services;
    }
}
