using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Infrastructure.AppInfo;
//using Pertamina.SolutionTemplate.Infrastructure.HealthCheck.Storages.MySql;
using Pertamina.SolutionTemplate.Infrastructure.HealthCheck.Storages.SqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Logging;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultManagedSqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultSqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Vault;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.HealthCheck;

public static class DependencyInjection
{
    public static IHealthChecksBuilder AddHealthCheckService(this IServiceCollection services, IConfiguration configuration)
    {
        var appInfoOptions = configuration.GetSection(AppInfoOptions.SectionKey).Get<AppInfoOptions>();
        var healthCheckOptions = configuration.GetSection(HealthCheckOptions.SectionKey).Get<HealthCheckOptions>();

        if (healthCheckOptions.UI.Enabled)
        {
            var healthChecksUIBuilder = services.AddHealthChecksUI(settings => settings.AddHealthCheckEndpoint($"{appInfoOptions.FullName} {nameof(Infrastructure)}", $"{healthCheckOptions.UI.AbsoluteUri}{healthCheckOptions.Endpoint}"));

            switch (healthCheckOptions.UI.Storage.Provider)
            {
                case HealthCheckStorageProvider.SqlServer:
                    var sqlServerOptions = configuration.GetSection(SqlServerOptions.SectionKey).Get<SqlServerOptions>();
                    healthChecksUIBuilder.AddSqlServerStorage(sqlServerOptions);
                    break;
                case HealthCheckStorageProvider.VaultSqlServer:
                    var vaultOptions = configuration.GetSection(VaultSqlServerOptions.SectionKey).Get<VaultSqlServerOptions>();
                    var vaultClient = ClientConnect.UseAppRoleAuth(vaultOptions.URL, vaultOptions.RoleID, vaultOptions.SecretID, vaultOptions.Namespace, "HealthCheck");
                    var vaultSqlSecret = StaticSecret.GetSecret(vaultClient, vaultOptions.SecretPath, vaultOptions.MountPoint);
                    var vaultSqlOptions = new SqlServerOptions
                    {
                        ConnectionString = $"Data Source={vaultSqlSecret.Data.Data["Server"]};" +
                                        $"Initial Catalog={vaultSqlSecret.Data.Data["Database"]};" +
                                        $"User ID={vaultSqlSecret.Data.Data["id"]};" +
                                        $"Password={vaultSqlSecret.Data.Data["pwd"]};" +
                                        $"MultipleActiveResultSets=true;Encrypt=False;"
                    };
                    healthChecksUIBuilder.AddSqlServerStorage(vaultSqlOptions);
                    break;
                case HealthCheckStorageProvider.VaultManagedSqlServer:
                    var vaultManagedOptions = configuration.GetSection(VaultManagedSqlServerOptions.SectionKey).Get<VaultManagedSqlServerOptions>();
                    var vaultManagedClient = ClientConnect.UseAppRoleAuth(vaultManagedOptions.URL, vaultManagedOptions.RoleID, vaultManagedOptions.SecretID, vaultManagedOptions.Namespace, "HealthCheck");
                    var vaultManagedSqlSecret = DynamicSecret.GenerateCredential(vaultManagedClient, vaultManagedOptions.DBRoleName, vaultManagedOptions.MountPoint);
                    var vaultManagedSqlOptions = new SqlServerOptions
                    {
                        ConnectionString = $"Server={vaultManagedOptions.DBServer};" +
                                           $"Database={vaultManagedOptions.DBName};" +
                                           $"user id={vaultManagedSqlSecret.Data.Username};" +
                                           $"pwd={vaultManagedSqlSecret.Data.Password};" +
                                          $"MultipleActiveResultSets=true;Encrypt=False;"
                    };
                    healthChecksUIBuilder.AddSqlServerStorage(vaultManagedSqlOptions);
                    break;
                default:
                    throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(HealthCheck)} {nameof(HealthCheckOptions.UI)} {nameof(HealthCheckOptions.UI.Storage)} {nameof(HealthCheckOptions.UI.Storage.Provider)}: {healthCheckOptions.UI.Storage.Provider}");
            }
        }
        else
        {
            LoggingHelper
                .CreateLogger()
                .LogWarning("{ServiceName} is not enabled.", $"{nameof(HealthCheck)} {nameof(HealthCheckOptions.UI)}");
        }

        return services.AddHealthChecks();
    }

    public static IApplicationBuilder UseHealthCheckService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var healthCheckOptions = configuration.GetSection(HealthCheckOptions.SectionKey).Get<HealthCheckOptions>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks(healthCheckOptions.Endpoint, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            if (healthCheckOptions.UI.Enabled)
            {
                endpoints.MapHealthChecksUI(options =>
                {
                    options.UIPath = healthCheckOptions.UI.Endpoints.UI;
                    options.ApiPath = healthCheckOptions.UI.Endpoints.Api;
                    options.AddCustomStylesheet(@"wwwroot\healthchecks\site.css");
                });
            }
        });

        return app;
    }
}
