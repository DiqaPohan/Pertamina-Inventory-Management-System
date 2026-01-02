namespace Domain.Entities;

using Pertamina.SolutionTemplate.Domain.Abstracts;
using Shared.Common.Enums;

public class Item : AuditableEntity
{
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public ItemCategory Category { get; set; }
	public int TotalStock { get; set; }
	public int AvailableStock { get; set; }
	public string Unit { get; set; } = "pcs";

	// Navigasi ke rak tempat barang ini disimpan
	public virtual ICollection<RackStatus> RackStatus { get; set; } = new HashSet<RackStatus>();

	// Histori peminjaman barang ini
	public virtual ICollection<LoanTransaction> LoanTransactions { get; set; } = new HashSet<LoanTransaction>();
}