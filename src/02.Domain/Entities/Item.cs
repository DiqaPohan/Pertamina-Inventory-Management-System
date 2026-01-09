namespace Domain.Entities;

using Pertamina.SolutionTemplate.Domain.Abstracts;
using Pertamina.SolutionTemplate.Shared.Common.Enums;


public class Item : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public ItemCategory Category { get; set; }
    public string? RackId { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.Pending;
    public int TotalStock { get; set; }
    public int AvailableStock { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImageUrl { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public virtual ICollection<RackSlot> RackSlots { get; set; } = new HashSet<RackSlot>();
}