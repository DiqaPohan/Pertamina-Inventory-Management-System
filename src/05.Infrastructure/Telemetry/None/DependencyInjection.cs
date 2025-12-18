using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Infrastructure.Logging;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Telemetry.None;

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
