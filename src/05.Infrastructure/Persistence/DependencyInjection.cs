using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        // 1. Definisikan Connection String LocalDB
        // Gunakan tanda @ di depan kutip agar \MSSQLLocalDB dibaca benar oleh sistem
        var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=DB_Inventory_Project;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;";

        var sqlServerOptions = new SqlServerOptions
        {
            ConnectionString = connectionString
        };

        // 2. Langsung daftarkan service SQL Server
        services.AddSqlServerPersistenceService(sqlServerOptions, healthChecksBuilder);

        return services;
    }
}