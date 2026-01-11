namespace Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Pertamina.SolutionTemplate.Domain.Abstracts;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

[Table("Items", Schema = "dbo")]
public class Item : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public ItemCategory Category { get; set; }

    // Properti Foreign Key
    public string? RackId { get; set; }

    // Navigasi ke Entity Rack
    [ForeignKey("RackId")]
    public virtual Rack? Rack { get; set; }

    public ItemStatus Status { get; set; } = ItemStatus.Pending;
    public int TotalStock { get; set; }
    public int AvailableStock { get; set; }
    public string Unit { get; set; } = "pcs";
    public string? ImageUrl { get; set; }
    public DateTime? ExpiryDate { get; set; }

}