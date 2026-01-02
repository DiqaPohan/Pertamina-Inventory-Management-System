// Di dalam class SolutionTemplateDbContext
public DbSet<Item> Items => Set<Item>();
public DbSet<RackSlot> RackSlots => Set<RackSlot>();
public DbSet<LoanTransaction> LoanTransactions => Set<LoanTransaction>();

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Menerapkan konfigurasi dari assembly ini (Langkah selanjutnya)
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}