namespace Pertamina.SolutionTemplate.Shared.Data.Queries.OutputGetDataByToken;
public class OutputGetDataByTokenData
{
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public List<GetSingleDataWithToken.GetSingleDataWithToken>? Items { get; set; }
    public DateTime? Tanggal { get; set; }
}
