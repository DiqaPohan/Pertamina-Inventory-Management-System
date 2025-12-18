using Pertamina.SolutionTemplate.Bsui.Services.Logging;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Services.Authentication.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneAuthenticationService(this IServiceCollection services)
    {
        LoggingHelper
            .CreateLogger()
            .LogWarning("{ServiceName} is set to None.", $"{nameof(Authentication)} {CommonDisplayTextFor.Service}");

        return services;
    }
}
