using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Otp;

namespace Pertamina.SolutionTemplate.Infrastructure.Otp;

public static class DependencyInjection
{
    public static IServiceCollection AddOtpService(this IServiceCollection services)
    {
        services.AddTransient<IOtpService, OtpService>();

        return services;
    }
}
