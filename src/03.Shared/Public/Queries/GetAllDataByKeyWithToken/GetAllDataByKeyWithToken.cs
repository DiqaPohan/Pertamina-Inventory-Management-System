using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Public.Queries.GetAllDataByKeyWithToken;
public class GetAllDataByKeyWithToken
{
    [Required]
    public string? Token { get; set; }
    public string? Key { get; set; }
    public string? KeyValue { get; set; }
    public string? ApplicationStatus { get; set; }
}
