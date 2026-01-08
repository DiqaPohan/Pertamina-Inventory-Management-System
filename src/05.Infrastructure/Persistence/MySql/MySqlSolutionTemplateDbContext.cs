using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Infrastructure.Persistence;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.MySql;

public class MySqlServerSolutionTemplateDbContext : SolutionTemplateDbContext
{
    public MySqlServerSolutionTemplateDbContext(DbContextOptions<MySqlServerSolutionTemplateDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}