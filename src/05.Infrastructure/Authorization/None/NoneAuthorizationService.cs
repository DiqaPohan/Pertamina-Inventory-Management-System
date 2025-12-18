using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Application.Services.Authorization;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.Shared.Services.Authorization.Models.GetAuthorizationInfo;

namespace Pertamina.SolutionTemplate.Infrastructure.Authorization.None;

public class NoneAuthorizationService : IAuthorizationService
{
    private readonly ILogger<NoneAuthorizationService> _logger;

    public NoneAuthorizationService(ILogger<NoneAuthorizationService> logger)
    {
        _logger = logger;
    }

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(Authorization)} {CommonDisplayTextFor.Service}");
    }

    public Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, CancellationToken cancellationToken)
    {
        LogWarning();

        return Task.FromResult(new GetAuthorizationInfoResponse());
    }
}
