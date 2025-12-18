using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Sms;

namespace Pertamina.SolutionTemplate.Infrastructure.Sms.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneSmsService(this IServiceCollection services)
    {
        services.AddSingleton<ISmsService, NoneSmsService>();

        return services;
    }
}
