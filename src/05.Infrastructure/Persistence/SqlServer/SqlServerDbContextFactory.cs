using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerSolutionTemplateDbContext>
{
    public SqlServerSolutionTemplateDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqlServerSolutionTemplateDbContext>();

        // Lu tulis manual kenceng-kenceng di sini biar dia gak baca JSON yang error itu
        var connectionString = "Server=localhost;Database=DB_Inventory_Project;Trusted_Connection=True;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString);

        return new SqlServerSolutionTemplateDbContext(optionsBuilder.Options);
    }
}