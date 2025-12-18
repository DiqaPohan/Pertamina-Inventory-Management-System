using Pertamina.SolutionTemplate.Domain.Abstracts;

namespace Pertamina.SolutionTemplate.Domain.Entities;

public class ApplicationCapabilityLevel2 : AuditableEntity
{
    public string? Id { get; set; }
    public string? Nama { get; set; }
    public string? Keterangan { get; set; }
    public string? Level_1 { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset Modified { get; set; }
    public string? ModifiedBy { get; set; }
    public string? IsDeleted { get; set; }
}
