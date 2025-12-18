using Pertamina.SolutionTemplate.Shared.Services.Authentication.Constants;

namespace Pertamina.SolutionTemplate.Application.Services.Authentication;

public class AuthenticationOptions
{
    public const string SectionKey = nameof(Authentication);

    public string Provider { get; set; } = AuthenticationProvider.None;
}
