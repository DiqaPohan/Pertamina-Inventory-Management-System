using Pertamina.SolutionTemplate.Domain.Abstracts;

namespace Pertamina.SolutionTemplate.Domain.Entities;

public class ApplicationCriticality : AuditableEntity
{
    public string? Id { get; set; }
    public string? Nama { get; set; }
    public string? Keterangan { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset Modified { get; set; }
    public string? ModifiedBy { get; set; }
    public string? IsDeleted { get; set; }
}
