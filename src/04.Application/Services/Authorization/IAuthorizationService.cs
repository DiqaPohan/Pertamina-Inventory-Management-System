using Pertamina.SolutionTemplate.Shared.Services.Authorization.Models.GetAuthorizationInfo;

namespace Pertamina.SolutionTemplate.Application.Services.Authorization;

public interface IAuthorizationService
{
    Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, CancellationToken cancellationToken);
}
