using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Data.Queries.GetListByToken;
public class GetAllDataByToken
{
    [Required]
    public string? Token { get; set; }

    [Required]
    public string? AppName { get; set; }
}
