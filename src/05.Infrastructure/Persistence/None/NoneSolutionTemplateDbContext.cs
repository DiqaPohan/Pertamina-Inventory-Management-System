using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Application.Services.Persistence;
using Pertamina.SolutionTemplate.Domain.Entities;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.None;

public class NoneSolutionTemplateDbContext : DbContext, ISolutionTemplateDbContext
{
    private readonly ILogger<NoneSolutionTemplateDbContext> _logger;

    public NoneSolutionTemplateDbContext(ILogger<NoneSolutionTemplateDbContext> logger)
    {
        _logger = logger;
    }

    #region Essential Entities
    public DbSet<Audit> Audits => Set<Audit>();
    #endregion Essential Entities

    #region Business Entities
    public DbSet<Data> Data => Set<Data>();
    public DbSet<RequestData> RequestData => Set<RequestData>();
    //public DbSet<HistoricalAppPhase> HistoricalAppPhase => Set<HistoricalAppPhase>();
    //public DbSet<DraftHistoricalAppPhase> DraftHistoricalAppPhase => Set<DraftHistoricalAppPhase>();
    public DbSet<ApplicationArea> ApplicationArea => Set<ApplicationArea>();
    public DbSet<ApplicationCapabilityLevel1> ApplicationCapabilityLevel1 => Set<ApplicationCapabilityLevel1>();
    public DbSet<ApplicationCapabilityLevel2> ApplicationCapabilityLevel2 => Set<ApplicationCapabilityLevel2>();
    public DbSet<ApplicationCriticality> ApplicationCriticality => Set<ApplicationCriticality>();
    public DbSet<ApplicationLicense> ApplicationLicense => Set<ApplicationLicense>();
    public DbSet<ApplicationPackage> ApplicationPackage => Set<ApplicationPackage>();
    public DbSet<ApplicationStatus> ApplicationStatus => Set<ApplicationStatus>();
    public DbSet<ApplicationType> ApplicationType => Set<ApplicationType>();
    public DbSet<ApplicationUserManagement> ApplicationUserManagement => Set<ApplicationUserManagement>();
    public DbSet<ApplicationUtilization> ApplicationUtilization => Set<ApplicationUtilization>();

    #endregion Business Entities

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(Persistence)} {CommonDisplayTextFor.Service}");
    }

    public Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken) where THandler : notnull
    {
        LogWarning();

        return Task.FromResult(0);
    }
}
