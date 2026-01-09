using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerSolutionTemplateDbContext>
{
    public SqlServerSolutionTemplateDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlServerSolutionTemplateDbContext>();

        // Gunakan connection string kamu
        var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DB_Inventory_Project;Trusted_Connection=True;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Pertamina.SolutionTemplate.Infrastructure"));

        // Tambahkan ini untuk mematikan internal caching EF yang sering bikin error assembly saat design-time
        return new SqlServerSolutionTemplateDbContext(optionsBuilder.Options);
    }
}