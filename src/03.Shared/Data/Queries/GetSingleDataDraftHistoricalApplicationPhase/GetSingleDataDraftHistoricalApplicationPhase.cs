namespace Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataDraftHistoricalApplicationPhase;
public class GetSingleDataDraftHistoricalApplicationPhase
{
    public string? Code_Apps { get; set; }
    public string? Code_Apps_Update { get; set; }
    public string? Name_Apps_Update { get; set; }

    public DateTimeOffset? Date { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public string? Phase { get; set; }

    public string? Keterangan { get; set; }

    public string? Source { get; set; }
    public string? Reason { get; set; }

    public string? Approved_Status { get; set; }

    public string? IsApproved { get; set; }

    public DateTimeOffset? Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset? Modified { get; set; }

    public string? ModifiedBy { get; set; }

    public string? IsDeleted { get; set; }
}
