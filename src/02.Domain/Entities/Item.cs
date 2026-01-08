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
    public string? ImageUrl { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public virtual ICollection<RackSlot> RackSlots { get; set; } = new HashSet<RackSlot>();
}