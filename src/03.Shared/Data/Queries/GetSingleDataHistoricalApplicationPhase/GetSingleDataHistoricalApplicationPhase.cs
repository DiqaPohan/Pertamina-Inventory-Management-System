namespace Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleDataHistoricalApplicationPhase;
public class GetSingleDataHistoricalApplicationPhase
{
    public string? Code_Apps { get; set; }

    public DateTimeOffset? Date { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public string? Phase { get; set; }

    public string? Keterangan { get; set; }

    public DateTimeOffset? Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset? Modified { get; set; }

    public string? ModifiedBy { get; set; }

    public string? IsDeleted { get; set; }
}
