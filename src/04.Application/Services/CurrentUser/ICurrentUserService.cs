using Pertamina.SolutionTemplate.Base.ValueObjects;

namespace Pertamina.SolutionTemplate.Application.Services.CurrentUser;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string Username { get; }
    string ClientId { get; }
    string? PositionId { get; }
    string IpAddress { get; }
    Geolocation? Geolocation { get; }
}
