using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Services.CurrentUser;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.DomainEvent;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.MySql.Configuration;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.MySql;

public class MySqlSolutionTemplateDbContext : SolutionTemplateDbContext
{
    public MySqlSolutionTemplateDbContext(
        DbContextOptions<MySqlSolutionTemplateDbContext> options,
        ICurrentUserService currentUser,
        IDateAndTimeService dateTime,
        IDomainEventService domainEvent) : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
        _domainEvent = domainEvent;
    }

    public MySqlSolutionTemplateDbContext(DbContextOptions<MySqlSolutionTemplateDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromNameSpace(typeof(AuditConfiguration).Namespace!);

        base.OnModelCreating(builder);
    }
}
