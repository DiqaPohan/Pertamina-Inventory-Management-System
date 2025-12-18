namespace Pertamina.SolutionTemplate.Domain.Interfaces;

public interface IModifiable
{
    DateTimeOffset? Modified { get; set; }
    string? ModifiedBy { get; set; }
}
