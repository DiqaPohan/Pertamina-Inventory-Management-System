namespace Domain.Entities;

using Pertamina.SolutionTemplate.Domain.Abstracts;
using Shared.Common.Enums;

public class RackSlot : AuditableEntity 
{
    public string RackCode { get; set; } = string.Empty; // Contoh: RAK-A01
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public RackStatus Status { get; set; }

    public Guid? ItemId { get; set; }
    public virtual Item? StoredItem { get; set; }
}