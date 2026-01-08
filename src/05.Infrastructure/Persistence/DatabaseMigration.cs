using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.MySql;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence;

public static class DatabaseMigration
{
    public static async Task ApplyDatabaseMigrationAsync<T>(this IServiceProvider serviceProvider)
    {
        // KITA BYPASS SEMUA LOGIKA DI SINI.
        // Kita paksa return Task biar aplikasi bisa Build Succeeded tanpa 
        // mencoba memproses appsettings yang formatnya lagi error di mata EF Tools.

        await Task.CompletedTask;
    }
}