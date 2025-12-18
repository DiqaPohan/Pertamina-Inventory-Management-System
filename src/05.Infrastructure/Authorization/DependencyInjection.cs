using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Authorization;
using Pertamina.SolutionTemplate.Infrastructure.Authorization.IdAMan;
using Pertamina.SolutionTemplate.Infrastructure.Authorization.IS4IM;
using Pertamina.SolutionTemplate.Infrastructure.Authorization.None;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Authorization;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthorizationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<AuthorizationOptions>(configuration.GetSection(AuthorizationOptions.SectionKey));

        var authorizationOptions = configuration.GetSection(AuthorizationOptions.SectionKey).Get<AuthorizationOptions>();

        switch (authorizationOptions.Provider)
        {
            case AuthorizationProvider.None:
                services.AddNoneAuthorizationService();
                break;
            case AuthorizationProvider.IdAMan:
                services.AddIdAManAuthorizationService(configuration, healthChecksBuilder);
                break;
            case AuthorizationProvider.IS4IM:
                services.AddIS4IMAuthorizationService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authorization)} {nameof(AuthorizationOptions.Provider)}: {authorizationOptions.Provider}");
        }

        return services;
    }

    public static IApplicationBuilder UseAuthorizationService(this WebApplication app, IConfiguration configuration)
    {
        var authorizationOptions = configuration.GetSection(AuthorizationOptions.SectionKey).Get<AuthorizationOptions>();

        switch (authorizationOptions.Provider)
        {
            case AuthorizationProvider.None:
                app.MapControllers();
                break;
            case AuthorizationProvider.IdAMan:
                app.UseAuthorization();
                app.MapControllers().RequireAuthorization();
                break;
            case AuthorizationProvider.IS4IM:
                app.UseAuthorization();
                app.MapControllers().RequireAuthorization();
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authorization)} {nameof(AuthorizationOptions.Provider)}: {authorizationOptions.Provider}");
        }

        return app;
    }
}
