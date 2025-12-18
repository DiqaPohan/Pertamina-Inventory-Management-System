
namespace Pertamina.SolutionTemplate.Shared.Public.Queries.OutputGetMasterDataByTokenData;
public class OutputGetMasterDataByTokenData
{
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public List<GetSingleMasterData.GetSingleMasterDataData>? Items { get; set; }
    public DateTime? Tanggal { get; set; }
}
