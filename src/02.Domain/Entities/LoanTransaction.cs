namespace Domain.Entities;

using Pertamina.SolutionTemplate.Domain.Abstracts;
using Shared.Common.Enums;

public class LoanTransaction : AuditableEntity
{
    public Guid ItemId { get; set; }
    public virtual Item Item { get; set; } = null!;

    public string BorrowerName { get; set; } = string.Empty;
    public string BorrowerPhone { get; set; } = string.Empty;

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; } // Terisi saat dikonfirmasi Admin

    public LoanStatus Status { get; set; }
    public string QrCodeToken { get; set; } = string.Empty; // Digunakan Staff untuk validasi scan
}