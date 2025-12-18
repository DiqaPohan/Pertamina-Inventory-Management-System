using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Shared.Audits.Options;

namespace Pertamina.SolutionTemplate.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        #region Essential Options
        services.AddAuditOptions(configuration);
        #endregion Essential Options

        #region Business Options
        #endregion Business Options

        return services;
    }
}
