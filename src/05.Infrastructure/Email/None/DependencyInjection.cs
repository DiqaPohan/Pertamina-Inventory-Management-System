using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Email;

namespace Pertamina.SolutionTemplate.Infrastructure.Email.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneEmailService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, NoneEmailService>();

        return services;
    }
}
