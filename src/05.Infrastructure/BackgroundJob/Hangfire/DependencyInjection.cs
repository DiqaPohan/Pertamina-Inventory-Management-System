using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Application.Services.BackgroundJob;
//using Pertamina.SolutionTemplate.Infrastructure.BackgroundJob.Hangfire.Storages.MySql;
using Pertamina.SolutionTemplate.Infrastructure.BackgroundJob.Hangfire.Storages.SqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultManagedSqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultSqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Vault;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Extensions;

namespace Pertamina.SolutionTemplate.Infrastructure.BackgroundJob.Hangfire;

public static class DependencyInjection
{
    public static IServiceCollection AddHangfireBackgroundJobService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var hangfireBackgroundJobOptions = configuration.GetSection(HangfireBackgroundJobOptions.SectionKey).Get<HangfireBackgroundJobOptions>();

        switch (hangfireBackgroundJobOptions.Storage.Provider)
        {
            case HangfireBackgroundJobStorageProvider.SqlServer:
                var sqlServerOptions = configuration.GetSection(SqlServerOptions.SectionKey).Get<SqlServerOptions>();
                services.AddHangfireUsingSqlServerDatabase(sqlServerOptions, healthChecksBuilder);
                break;
            case HangfireBackgroundJobStorageProvider.VaultSqlServer:
                var vaultOptions = configuration.GetSection(VaultSqlServerOptions.SectionKey).Get<VaultSqlServerOptions>();
                var vaultClient = ClientConnect.UseAppRoleAuth(vaultOptions.URL, vaultOptions.RoleID, vaultOptions.SecretID, vaultOptions.Namespace, "Hangfire Background Job");
                var vaultSqlSecret = StaticSecret.GetSecret(vaultClient, vaultOptions.SecretPath, vaultOptions.MountPoint);
                var vaultSqlOptions = new SqlServerOptions
                {
                    ConnectionString = $"Data Source={vaultSqlSecret.Data.Data["Server"]};" +
                                        $"Initial Catalog={vaultSqlSecret.Data.Data["Database"]};" +
                                        $"User ID={vaultSqlSecret.Data.Data["id"]};" +
                                        $"Password={vaultSqlSecret.Data.Data["pwd"]};" +
                                        $"MultipleActiveResultSets=true;Encrypt=False;"
                };
                services.AddHangfireUsingSqlServerDatabase(vaultSqlOptions, healthChecksBuilder);
                break;
            case HangfireBackgroundJobStorageProvider.VaultManagedSqlServer:
                var vaultManagedOptions = configuration.GetSection(VaultManagedSqlServerOptions.SectionKey).Get<VaultManagedSqlServerOptions>();
                var vaultManagedClient = ClientConnect.UseAppRoleAuth(vaultManagedOptions.URL, vaultManagedOptions.RoleID, vaultManagedOptions.SecretID, vaultManagedOptions.Namespace, "Hangfire Background Job");
                var vaultManagedSqlSecret = DynamicSecret.GenerateCredential(vaultManagedClient, vaultManagedOptions.DBRoleName, vaultManagedOptions.MountPoint);
                var vaultManagedSqlOptions = new SqlServerOptions
                {
                    ConnectionString = $"Server={vaultManagedOptions.DBServer};" +
                                       $"Database={vaultManagedOptions.DBName};" +
                                       $"user id={vaultManagedSqlSecret.Data.Username};" +
                                       $"pwd={vaultManagedSqlSecret.Data.Password};" +
                                       $"Trusted_Connection=False;MultipleActiveResultSets=true;"
                };
                services.AddHangfireUsingSqlServerDatabase(vaultManagedSqlOptions, healthChecksBuilder);
                break;
            //case HangfireBackgroundJobStorageProvider.MySql:
            //    var mySqlOptions = configuration.GetSection(MySqlOptions.SectionKey).Get<MySqlOptions>();
            //    services.AddHangfireUsingMySqlDatabase(mySqlOptions, healthChecksBuilder);
            //    break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Hangfire)} {nameof(BackgroundJob).SplitWords()} {nameof(HangfireBackgroundJobOptions.Storage)} {nameof(HangfireBackgroundJobOptions.Storage.Provider)}: {hangfireBackgroundJobOptions.Storage.Provider}");
        }

        services.AddTransient<IBackgroundJobService, HangfireBackgroundJobService>();
        services.AddHangfireServer(options => options.WorkerCount = hangfireBackgroundJobOptions.WorkerCount);

        healthChecksBuilder.AddHangfire(
            setup => setup.MinimumAvailableServers = 1,
            name: $"{nameof(BackgroundJob).SplitWords()} {CommonDisplayTextFor.Service} ({nameof(Hangfire)})");

        return services;
    }

    public static IApplicationBuilder UseHangfireBackgroundJobService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var hangfireBackgroundJobOptions = configuration.GetSection(HangfireBackgroundJobOptions.SectionKey).Get<HangfireBackgroundJobOptions>();

        app.UseHangfireDashboard(hangfireBackgroundJobOptions.Dashboard.Url, new DashboardOptions
        {
            Authorization = new[]
            {
                new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = false,
                    SslRedirect = false,
                    LoginCaseSensitive = true,
                    Users = new []
                    {
                        new BasicAuthAuthorizationUser
                        {
                            Login = hangfireBackgroundJobOptions.Dashboard.Username,
                            PasswordClear =  hangfireBackgroundJobOptions.Dashboard.Password
                        }
                    }
                })
            }
        });

        return app;
    }
}
