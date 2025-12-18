using Pertamina.SolutionTemplate.Bsui.Services.Logging;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Services.Telemetry.None;

public static class DependencyInjection
{
    public static IServiceCollection AddNoneTelemetryService(this IServiceCollection services)
    {
        LoggingHelper
            .CreateLogger()
            .LogWarning("{ServiceName} is set to None.", $"{nameof(Telemetry)} {CommonDisplayTextFor.Service}");

        return services;
    }
}
