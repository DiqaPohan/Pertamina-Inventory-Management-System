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

    // Relasi ke slot rak (Satu barang bisa di banyak tempat)
    public virtual ICollection<RackSlot> RackSlots { get; set; } = new HashSet<RackSlot>();
}