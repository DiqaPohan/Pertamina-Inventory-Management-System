using Pertamina.SolutionTemplate.Bsui.Services.AppInfo;
using Pertamina.SolutionTemplate.Bsui.Services.Authentication;
using Pertamina.SolutionTemplate.Bsui.Services.Authorization;
using Pertamina.SolutionTemplate.Bsui.Services.External;
using Pertamina.SolutionTemplate.Bsui.Services.FrontEnd;
using Pertamina.SolutionTemplate.Bsui.Services.Geolocation;
using Pertamina.SolutionTemplate.Bsui.Services.Security;
using Pertamina.SolutionTemplate.Bsui.Services.Telemetry;
using Pertamina.SolutionTemplate.Bsui.Services.UI;

namespace Pertamina.SolutionTemplate.Bsui.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddBsui(this IServiceCollection services, IConfiguration configuration)
    {
        #region AppInfo
        services.AddAppInfoService(configuration);
        #endregion AppInfo

        #region Authentication
        services.AddAuthenticationService(configuration);
        #endregion Authentication

        #region Authorization
        services.AddAuthorizationService(configuration);
        #endregion Authorization

        #region External
        services.AddExternalService(configuration);
        #endregion External

        #region Front End
        services.AddFrontEndService(configuration);
        #endregion Front End

        #region Geolocation
        services.AddGeolocationService(configuration);
        #endregion Geolocation

        #region Security
        services.AddSecurityService();
        #endregion Security

        #region Telemetry
        services.AddTelemetryService(configuration);
        #endregion Telemetry

        #region User Interface
        services.AddUIService();
        #endregion User Interface

        return services;
    }

    public static IApplicationBuilder UseBsui(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseFrontEndService(configuration);

        return app;
    }
}
