namespace Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetApplicationTypeByTokenData;
public class OutputGetApplicationTypeByTokenData
{
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public List<GetSingleDataApplicationType.GetSingleDataApplicationType>? Items { get; set; }
    public DateTime? Tanggal { get; set; }
}
