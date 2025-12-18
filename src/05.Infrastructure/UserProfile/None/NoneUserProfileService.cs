using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Application.Services.UserProfile;
using Pertamina.SolutionTemplate.Application.Services.UserProfile.Models.GetUserProfile;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Common.Extensions;

namespace Pertamina.SolutionTemplate.Infrastructure.UserProfile.None;

public class NoneUserProfileService : IUserProfileService
{
    private readonly ILogger<NoneUserProfileService> _logger;

    public NoneUserProfileService(ILogger<NoneUserProfileService> logger)
    {
        _logger = logger;
    }

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(UserProfile).SplitWords()} {CommonDisplayTextFor.Service}");
    }

    public Task<GetUserProfileResponse> GetUserProfileAsync(string username, CancellationToken cancellationToken)
    {
        LogWarning();

        return Task.FromResult(new GetUserProfileResponse());
    }
}
