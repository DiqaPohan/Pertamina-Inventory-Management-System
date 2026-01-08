using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Infrastructure.Persistence;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

public class SqlServerSolutionTemplateDbContext : SolutionTemplateDbContext
{
    public SqlServerSolutionTemplateDbContext(DbContextOptions<SqlServerSolutionTemplateDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // KITA PAKSA DI SINI UNTUK MENGGUNAKAN LOCALDB
        if (!optionsBuilder.IsConfigured)
        {
            // Gunakan tanda @ agar karakter backslash \ tidak dianggap error
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DB_Inventory_Project;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
        }
    }
}