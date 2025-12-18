using System.ComponentModel.DataAnnotations;
namespace Pertamina.SolutionTemplate.Shared.Data.Queries.GetInsertDataWithToken;
public class GetInsertDataWithToken
{
    [Required]
    public string? Token { get; set; }

    [Required]
    public string? AppName { get; set; }
    [Required]
    public string? AppArea { get; set; }

    [Required]
    public string? AppType { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public string? Level_1 { get; set; }
    [Required]
    public string? Level_2 { get; set; }
    [Required]
    public string? Utilization { get; set; }
    [Required]
    public string? Status { get; set; }
    [Required]
    public string? BusinessOwnerPIC { get; set; }
    [Required]
    public string? BusinessOwner { get; set; }
    [Required]
    public string? Developer { get; set; }
    [Required]
    public string? Link { get; set; }
    [Required]
    public string? Users { get; set; }
    [Required]
    public string? StartDevelopment { get; set; }
    [Required]
    public string? StartImplementation { get; set; }
    [Required]
    public string? Criticality { get; set; }
    [Required]
    public string? HoldingArea { get; set; }
}
