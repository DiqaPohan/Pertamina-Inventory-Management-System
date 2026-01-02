namespace Domain.Entities;

using Pertamina.SolutionTemplate.Domain.Abstracts;
using Shared.Common.Enums;


public class InventoryItem : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ItemCategory Category { get; set; }
    public int TotalStock { get; set; }
    public int AvailableStock { get; set; }
    public string Unit { get; set; } = "pcs";

    // Relasi opsional: barang bisa terletak di banyak slot rak
    public virtual ICollection<RackStatus> RackSlots { get; set; } = new HashSet<RackStatus>();
}