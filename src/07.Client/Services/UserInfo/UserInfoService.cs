using Pertamina.SolutionTemplate.Base.ValueObjects;

namespace Pertamina.SolutionTemplate.Client.Services.UserInfo;

public class UserInfoService
{
    public string AccessToken { get; set; } = default!;
    public string? PositionId { get; set; }
    public string? IpAddress { get; set; }
    public Geolocation? Geolocation { get; set; }
}
