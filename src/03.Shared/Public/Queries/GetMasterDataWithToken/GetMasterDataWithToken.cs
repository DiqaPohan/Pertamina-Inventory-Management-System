using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Public.Queries.GetMasterDataWithToken;
public class GetMasterDataWithToken
{
    [Required]
    public string? Token { get; set; }
    public string? TableName { get; set; }
}
