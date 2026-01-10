using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Domain.Entities;


namespace Application.Services.Persistence;

public interface ISolutionTemplateDbContext
{
    DbSet<Item> Items { get; }
    DbSet<Rack> Racks { get; }
    DbSet<LoanTransaction> LoanTransactions { get; }

    // Properti audit bawaan biarkan tetap ada
    DbSet<Audit> Audits { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}