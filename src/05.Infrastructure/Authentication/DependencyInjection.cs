using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.Authentication;
using Pertamina.SolutionTemplate.Infrastructure.Authentication.IdAMan;
using Pertamina.SolutionTemplate.Infrastructure.Authentication.IS4IM;
using Pertamina.SolutionTemplate.Infrastructure.Authentication.None;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Services.Authentication.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.SectionKey));

        var authenticationOptions = configuration.GetSection(AuthenticationOptions.SectionKey).Get<AuthenticationOptions>();

        switch (authenticationOptions.Provider)
        {
            case AuthenticationProvider.None:
                services.AddNoneAuthenticationService();
                break;
            case AuthenticationProvider.IdAMan:
                services.AddIdAManAuthenticationService(configuration, healthChecksBuilder);
                break;
            case AuthenticationProvider.IS4IM:
                services.AddIS4IMAuthenticationService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authentication)} {nameof(AuthenticationOptions.Provider)}: {authenticationOptions.Provider}");
        }

        return services;
    }

    public static IApplicationBuilder UseAuthenticationService(this WebApplication app, IConfiguration configuration)
    {
        var authenticationOptions = configuration.GetSection(AuthenticationOptions.SectionKey).Get<AuthenticationOptions>();

        switch (authenticationOptions.Provider)
        {
            case AuthenticationProvider.None:
                break;
            case AuthenticationProvider.IdAMan:
                app.UseAuthentication();
                break;
            case AuthenticationProvider.IS4IM:
                app.UseAuthentication();
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authorization)} {nameof(AuthenticationOptions.Provider)}: {authenticationOptions.Provider}");
        }

        return app;
    }
}
