using Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

namespace Pertamina.SolutionTemplate.Application.Services.Authorization;

public class AuthorizationOptions
{
    public const string SectionKey = nameof(Authorization);

    public string Provider { get; set; } = AuthorizationProvider.None;
}
