using Pertamina.SolutionTemplate.Domain.Abstracts;
using Shared.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Racks", Schema = "dbo")] // Memastikan masuk ke schema dbo
public class Rack : AuditableEntity // Pake AuditableEntity biar sama kayak Item
{
    [Key]
    public string RackId { get; set; } = string.Empty;
    public RackStatus Status { get; set; } = RackStatus.Available;

    // Navigasi: Satu Rak bisa berisi banyak Item
    public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
}