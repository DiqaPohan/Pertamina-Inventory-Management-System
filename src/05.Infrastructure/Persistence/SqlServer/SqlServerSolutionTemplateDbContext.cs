using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

public class SqlServerSolutionTemplateDbContext : SolutionTemplateDbContext
{
    public SqlServerSolutionTemplateDbContext(DbContextOptions<SqlServerSolutionTemplateDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AuditConfiguration).Assembly);
        base.OnModelCreating(builder);
    }
}