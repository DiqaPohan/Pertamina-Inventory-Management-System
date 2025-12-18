using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Infrastructure.Ecm.Idms;
using Pertamina.SolutionTemplate.Infrastructure.Ecm.None;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Ecm;

public static class DependencyInjection
{
    public static IServiceCollection AddEcmService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var ecmOptions = configuration.GetSection(EcmOptions.SectionKey).Get<EcmOptions>();

        switch (ecmOptions.Provider)
        {
            case EcmProvider.None:
                services.AddNoneEcmService();
                break;
            case EcmProvider.Idms:
                services.AddIdmsEcmService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Ecm).ToUpper()} {nameof(EcmOptions.Provider)}: {ecmOptions.Provider}");
        }

        return services;
    }
}
