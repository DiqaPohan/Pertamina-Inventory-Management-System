using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Pertamina.SolutionTemplate.Infrastructure.Persistence.MySql;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.None;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultManagedSqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultSqlServer;
using Pertamina.SolutionTemplate.Infrastructure.Vault;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var persistenceOptions = configuration.GetSection(PersistenceOptions.SectionKey).Get<PersistenceOptions>();

        switch (persistenceOptions.Provider)
        {
            case PersistenceProvider.None:
                services.AddNonePersistenceService();
                break;
            case PersistenceProvider.SqlServer:
                var sqlServerOptions = configuration.GetSection(SqlServerOptions.SectionKey).Get<SqlServerOptions>();
                services.AddSqlServerPersistenceService(sqlServerOptions, healthChecksBuilder);
                break;
            case PersistenceProvider.VaultSqlServer:
                var vaultOptions = configuration.GetSection(VaultSqlServerOptions.SectionKey).Get<VaultSqlServerOptions>();
                var vaultClient = ClientConnect.UseAppRoleAuth(vaultOptions.URL, vaultOptions.RoleID, vaultOptions.SecretID, vaultOptions.Namespace, "Persistence");
                var vaultSqlSecret = StaticSecret.GetSecret(vaultClient, vaultOptions.SecretPath, vaultOptions.MountPoint);
                var vaultSqlOptions = new SqlServerOptions
                {
                    ConnectionString = $"Data Source={vaultSqlSecret.Data.Data["Server"]};" +
                                        $"Initial Catalog={vaultSqlSecret.Data.Data["Database"]};" +
                                        $"User ID={vaultSqlSecret.Data.Data["id"]};" +
                                        $"Password={vaultSqlSecret.Data.Data["pwd"]};" +
                                        $"MultipleActiveResultSets=true;Encrypt=False;"
                };
                services.AddSqlServerPersistenceService(vaultSqlOptions, healthChecksBuilder);
                break;
            case PersistenceProvider.VaultManagedSqlServer:
                var vaultManagedOptions = configuration.GetSection(VaultManagedSqlServerOptions.SectionKey).Get<VaultManagedSqlServerOptions>();
                var vaultManagedClient = ClientConnect.UseAppRoleAuth(vaultManagedOptions.URL, vaultManagedOptions.RoleID, vaultManagedOptions.SecretID, vaultManagedOptions.Namespace, "Persistence");
                var vaultManagedSqlSecret = DynamicSecret.GenerateCredential(vaultManagedClient, vaultManagedOptions.DBRoleName, vaultManagedOptions.MountPoint);
                var vaultManagedSqlOptions = new SqlServerOptions
                {
                    ConnectionString = $"Server={vaultManagedOptions.DBServer};" +
                                       $"Database={vaultManagedOptions.DBName};" +
                                       $"user id={vaultManagedSqlSecret.Data.Username};" +
                                       $"pwd={vaultManagedSqlSecret.Data.Password};" +
                                       $"Trusted_Connection=False;MultipleActiveResultSets=true;"
                };
                services.AddSqlServerPersistenceService(vaultManagedSqlOptions, healthChecksBuilder);
                break;
            //case PersistenceProvider.MySql:
            //    var mySqlOptions = configuration.GetSection(MySqlOptions.SectionKey).Get<MySqlOptions>();
            //    services.AddMySqlPersistenceService(mySqlOptions, healthChecksBuilder);
            //    break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Persistence)} {nameof(PersistenceOptions.Provider)}: {persistenceOptions.Provider}");
        }

        return services;
    }
}
