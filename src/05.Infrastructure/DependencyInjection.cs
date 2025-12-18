using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Infrastructure.AppInfo;
using Pertamina.SolutionTemplate.Infrastructure.Authentication;
using Pertamina.SolutionTemplate.Infrastructure.Authorization;
using Pertamina.SolutionTemplate.Infrastructure.BackgroundJob;
using Pertamina.SolutionTemplate.Infrastructure.CurrentUser;
using Pertamina.SolutionTemplate.Infrastructure.DateAndTime;
using Pertamina.SolutionTemplate.Infrastructure.DomainEvent;
using Pertamina.SolutionTemplate.Infrastructure.Ecm;
using Pertamina.SolutionTemplate.Infrastructure.Email;
using Pertamina.SolutionTemplate.Infrastructure.HealthCheck;
using Pertamina.SolutionTemplate.Infrastructure.Otp;
using Pertamina.SolutionTemplate.Infrastructure.Persistence;
using Pertamina.SolutionTemplate.Infrastructure.Sms;
using Pertamina.SolutionTemplate.Infrastructure.Storage;
using Pertamina.SolutionTemplate.Infrastructure.Telemetry;
using Pertamina.SolutionTemplate.Infrastructure.UserProfile;

namespace Pertamina.SolutionTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Health Check
        var healthChecksBuilder = services.AddHealthCheckService(configuration);
        #endregion Health Check

        #region AppInfo
        services.AddAppInfoService(configuration);
        #endregion AppInfo

        #region Authentication
        services.AddAuthenticationService(configuration, healthChecksBuilder);
        #endregion Authentication

        #region Authorization
        services.AddAuthorizationService(configuration, healthChecksBuilder);
        #endregion Authorization

        #region Background Job
        services.AddBackgroundJobService(configuration, healthChecksBuilder);
        #endregion Background Job

        #region Current User
        services.AddCurrentUserService();
        #endregion Current User

        #region DateTime
        services.AddDateAndTimeService();
        #endregion DateTime

        #region Domain Event
        services.AddDomainEventService();
        #endregion Domain Event

        #region Enterprise Content Management
        services.AddEcmService(configuration, healthChecksBuilder);
        #endregion Enterprise Content Management

        #region Email
        services.AddEmailService(configuration, healthChecksBuilder);
        #endregion Email

        #region One Time Password
        services.AddOtpService();
        #endregion One Time Password

        #region Persistence
        services.AddPersistenceService(configuration, healthChecksBuilder);
        #endregion Persistence

        #region SMS
        services.AddSmsService(configuration, healthChecksBuilder);
        #endregion SMS

        #region Storage
        services.AddStorageService(configuration, healthChecksBuilder);
        #endregion Storage

        #region Telemetry
        services.AddTelemetryService(configuration);
        #endregion Telemetry

        #region User Profile
        services.AddUserProfileService(configuration, healthChecksBuilder);
        #endregion User Profile

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app, IConfiguration configuration)
    {
        #region Authentication
        app.UseAuthenticationService(configuration);
        #endregion Authentication

        #region Authorization
        app.UseAuthorizationService(configuration);
        #endregion Authorization

        #region Background Job
        app.UseBackgroundJobService(configuration);
        #endregion Background Job

        #region Health Check
        app.UseHealthCheckService(configuration);
        #endregion Health Check

        return app;
    }
}
