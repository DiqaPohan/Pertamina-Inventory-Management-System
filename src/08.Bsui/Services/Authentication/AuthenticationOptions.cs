using Pertamina.SolutionTemplate.Shared.Services.Authentication.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Services.Authentication;

public class AuthenticationOptions
{
    public const string SectionKey = nameof(Authentication);

    public int RefreshRateInSeconds { get; set; }
    public string Provider { get; set; } = AuthenticationProvider.None;
}
