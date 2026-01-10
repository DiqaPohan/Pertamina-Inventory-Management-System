using Application.Services.Persistence;
using Domain.Entities; // Namespace untuk Entity Rack dan Item lu
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    #region Business Entities - SINKRON DENGAN INVENTORY
    public DbSet<Item> Items => Set<Item>();
    
    // RACKSLOT DIHAPUS, DIGANTI JADI RACK
    public DbSet<Rack> Racks => Set<Rack>(); 
    
    public DbSet<LoanTransaction> LoanTransactions => Set<LoanTransaction>();
    #endregion Business Entities

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(Persistence)} {CommonDisplayTextFor.Service}");
    }

    // Perhatikan signature method ini harus sama persis dengan Interface
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        LogWarning();
        return Task.FromResult(0);
    }
}