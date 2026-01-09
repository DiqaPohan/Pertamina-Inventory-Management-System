using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Infrastructure.Persistence;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

public class SqlServerSolutionTemplateDbContext : SolutionTemplateDbContext
{
    public SqlServerSolutionTemplateDbContext(DbContextOptions<SqlServerSolutionTemplateDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // JANGAN di-hardcode di sini bro, biarkan options dari Factory yang bekerja
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // PAKSA PAKAI SKEMA SolutionTemplate AGAR TIDAK JADI dbo
        modelBuilder.HasDefaultSchema("SolutionTemplate");
    }
}