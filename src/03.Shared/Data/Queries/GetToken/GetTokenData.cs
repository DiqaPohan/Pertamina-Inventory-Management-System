
using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Data.Queries.GetToken;

public class GetTokenForm
{
    [Required]
    public string? ClientID { get; set; }
    [Required]
    public string? ClientSecret { get; set; }
    [Required]
    public string? ClientScope { get; set; }
    [Required]
    public string? GrantType { get; set; }
}
