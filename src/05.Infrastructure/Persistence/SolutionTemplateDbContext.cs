using Domain.Entities; // Pastikan namespace ini benar
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Domain.Entities;
using Shared.Common.Enums;

namespace Application.Services.Persistence;

public interface ISolutionTemplateDbContext
{
    // Tambahkan Entitas Inventory Baru
    DbSet<Item> Items { get; }
    DbSet<RackSlot> RackSlots { get; }
    DbSet<LoanTransaction> LoanTransactions { get; }

    // Audit tetap biarkan ada
    DbSet<Audit> Audits { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}