using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Application.Services.CurrentUser;
using Pertamina.SolutionTemplate.Application.Services.DateAndTime;
using Pertamina.SolutionTemplate.Application.Services.DomainEvent;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.Common.Extensions;
using Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer.Configuration;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.SqlServer;

public class SqlServerSolutionTemplateDbContext : SolutionTemplateDbContext
{
    public SqlServerSolutionTemplateDbContext(
        DbContextOptions<SqlServerSolutionTemplateDbContext> options,
        ICurrentUserService currentUser,
        IDateAndTimeService dateTime,
        IDomainEventService domainEvent) : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
        _domainEvent = domainEvent;
    }

    public SqlServerSolutionTemplateDbContext(DbContextOptions<SqlServerSolutionTemplateDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromNameSpace(typeof(AuditConfiguration).Namespace!);

        base.OnModelCreating(builder);
    }
}
