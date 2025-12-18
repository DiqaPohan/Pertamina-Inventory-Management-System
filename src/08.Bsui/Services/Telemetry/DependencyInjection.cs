using Pertamina.SolutionTemplate.Bsui.Services.Telemetry.ApplicationInsights;
using Pertamina.SolutionTemplate.Bsui.Services.Telemetry.None;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Services.Telemetry;

public static class DependencyInjection
{
    public static IServiceCollection AddTelemetryService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelemetryOptions>(configuration.GetSection(TelemetryOptions.SectionKey));

        var telemetryOptions = configuration.GetSection(TelemetryOptions.SectionKey).Get<TelemetryOptions>();

        switch (telemetryOptions.Provider)
        {
            case TelemetryProvider.None:
                services.AddNoneTelemetryService();
                break;
            case TelemetryProvider.ApplicationInsights:
                services.AddApplicationInsightsTelemetryService(configuration);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Telemetry)} {nameof(TelemetryOptions.Provider)}: {telemetryOptions.Provider}");
        }

        return services;
    }
}
