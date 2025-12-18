using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Services.Authentication.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Authentication.IdAMan;

public static class DependencyInjection
{
    public static IServiceCollection AddIdAManAuthenticationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<IdAManAuthenticationOptions>(configuration.GetSection(IdAManAuthenticationOptions.SectionKey));

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var idAManAuthenticationOptions = configuration.GetSection(IdAManAuthenticationOptions.SectionKey).Get<IdAManAuthenticationOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = idAManAuthenticationOptions.AuthorityUrl;
                options.Audience = $"{PrefixFor.ApiScope}{idAManAuthenticationOptions.ObjectId}";
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        healthChecksBuilder.Add(new HealthCheckRegistration(
            name: $"{nameof(Authentication)} {CommonDisplayTextFor.Service} ({nameof(IdAMan)})",
            instance: new IdAManAuthenticationHealthCheck(idAManAuthenticationOptions.HealthCheckUrl),
            failureStatus: HealthStatus.Unhealthy,
            tags: default));

        return services;
    }
}
