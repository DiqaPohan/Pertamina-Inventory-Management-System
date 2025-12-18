using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Public.Queries.OnlyToken;
public class OnlyToken
{
    [Required]
    public string? Token { get; set; }
}
