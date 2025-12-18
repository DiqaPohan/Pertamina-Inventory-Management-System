using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Public.Queries.GetSingleDataByKeyAndToken;
public class GetSingleDataByKeyAndToken
{
    [Required]
    public string? Token { get; set; }
    [Required]
    public string? Key { get; set; }
    [Required]
    public string? KeyValue { get; set; }
    [Required]
    public string? ApplicationStatus { get; set; }
}
