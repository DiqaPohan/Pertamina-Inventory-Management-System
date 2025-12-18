using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Domain.Entities;

namespace Pertamina.SolutionTemplate.Application.Services.Persistence;

public interface ISolutionTemplateDbContext
{
    #region Essential Entities
    DbSet<Audit> Audits { get; }
    #endregion Essential Entities

    #region Business Entities
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.Data> Data { get; }
    DbSet<RequestData> RequestData { get; }
    //DbSet<HistoricalAppPhase> HistoricalAppPhase { get; }
    //DbSet<DraftHistoricalAppPhase> DraftHistoricalAppPhase { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationArea> ApplicationArea { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel1> ApplicationCapabilityLevel1 { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCapabilityLevel2> ApplicationCapabilityLevel2 { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationCriticality> ApplicationCriticality { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationLicense> ApplicationLicense { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationPackage> ApplicationPackage { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationStatus> ApplicationStatus { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationType> ApplicationType { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationUserManagement> ApplicationUserManagement { get; }
    DbSet<Pertamina.SolutionTemplate.Domain.Entities.ApplicationUtilization> ApplicationUtilization { get; }
    #endregion Business Entities

    Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken = default) where THandler : notnull;
}
