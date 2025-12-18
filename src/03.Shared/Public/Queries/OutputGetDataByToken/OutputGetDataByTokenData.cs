
namespace Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetDataByToken;
public class OutputGetDataByTokenData
{
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public List<GetSingleData.GetSingleDataData>? Items { get; set; }
    public DateTime? Tanggal { get; set; }
}

