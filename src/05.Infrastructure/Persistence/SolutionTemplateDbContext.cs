using Application.Services.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pertamina.SolutionTemplate.Domain.Entities;
using System.Reflection;

namespace Infrastructure.Persistence;

public class SolutionTemplateDbContext : DbContext, ISolutionTemplateDbContext
{
    // Pake DbContextOptions tanpa generic biar gak error CS0029 di file Anak
    public SolutionTemplateDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();
    public DbSet<RackSlot> RackSlots => Set<RackSlot>();
    public DbSet<LoanTransaction> LoanTransactions => Set<LoanTransaction>();
    public DbSet<Audit> Audits => Set<Audit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}